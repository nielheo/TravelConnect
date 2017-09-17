import * as React from 'react'
import { RouteComponentProps } from 'react-router-dom'
import { connect } from 'react-redux'
import { ApplicationState } from '../../store'
import * as FlightStore from '../../store/Flight'

import * as queryString from 'query-string'
import { Grid, Row, Col, Pagination } from 'react-bootstrap'

import FlightDepartureList from './FlightDepartureList'
import FlightReturnList from './FlightReturnList'

import Filter from './Filter'
import SelectedDeparture from './SelectedDeparture'

import * as Commons from '../Commons'

import * as moment from 'moment'

const CryptoJS = require('crypto-js') as any;

type FlightProps =
  FlightStore.FlightState
  & typeof FlightStore.actionCreators
  & RouteComponentProps<{ route: string }>

interface FlightResultState {
  request: any
  departures: any[]
  page: number
  itemsPerPage: number
  requestId: string 
  airlines: string[]
  filteredAirlines: string[]
  filteredStops: number[]
  loadedAirlines: string[]
}

class FlightResult_Index extends React.Component<FlightProps, FlightResultState> {
  constructor(props: any) {
    super(props);

    let route = props.match.params.route

    let data = queryString.parse(route)
    
    this.state = {
      request: data,
      departures: [],
      page: 1,
      itemsPerPage: 20,
      requestId: '',
      airlines: [],
      filteredAirlines: [],
      filteredStops: [],
      loadedAirlines: [],
    }
  }

  //Compares
  _compareLeg = (a: any, b: any, leg:number) => {
    if (a.totalFare.amount < b.totalFare.amount)
      return -1;
    if (a.totalFare.amount > b.totalFare.amount)
      return 1;
    
    let aMoment = moment(a.legs[leg].segments[0].departure.time)
    let bMoment = moment(b.legs[leg].segments[0].departure.time)
    
    if (aMoment.isBefore(bMoment))
      return -1

    if (aMoment.isAfter(bMoment))
      return 1

    let aArrival = moment(a.legs[leg].segments[a.legs[leg].segments.length - 1].arrival.time)
    let bArrival = moment(b.legs[leg].segments[b.legs[leg].segments.length - 1].arrival.time)

    if (aArrival.isBefore(bArrival))
      return -1

    if (aArrival.isAfter(bArrival))
      return 1

    return 0;
  }

  _compareDepart = (a: any, b: any) => {
    return this._compareLeg(a, b, 0)
  }

  _compareReturn = (a: any, b: any) => {
    return this._compareLeg(a, b, 1)
  }

  _compareAirlineList = (a: any, b: any) => {
    if (a.code < b.code)
      return -1
    if (a.code > b.code)
      return 1

    return 0
  }

  _GenerateRoute = (result: any) => {
    let x: number = 0
    if (result) {
      result.pricedItins.map((i: any) => {
        i.itinNo = x++
        i.totalFare
        i.legs.map((l: any) => {
          l.itin = i,
          l.brds = l.segments.map((s: any) => s.brd).join(',')
          l.routes = l.segments.map((s: any) => {
            return s.marketingFlight.airline + s.marketingFlight.number + s.brd
          }).join('-') + ':' + i.totalFare.curr + i.totalFare.amount
          l.airlines = l.segments.map((s: any) => {
            return s.marketingFlight.airline
          })
        })
      })

      result.pricedItins.map((i: any) => {
        i.departStop = i.legs[0].segments.length - 1
        i.routes = i.legs.map((l: any) => l.routes)

        //i.routes = i.legs.map((l: any) => l.routes).join('|')
        i.airlines = [] 
        i.legs.map((l: any) => l.airlines.map((l2: any) => i.airlines.push(l2)))
        i.uniqueAirline = Array.from(new Set(i.airlines)).join(',')
        if (i.uniqueAirline.indexOf(',') > -1)
          i.uniqueAirline = 'Multi'

        i.departUniqueAirline = Array.from(new Set(i.legs[0].airlines)).join(',')
        if (i.departUniqueAirline.indexOf(',') > -1)
          i.departUniqueAirline = 'Multi'
      })
    }
    return result.pricedItins
  }

  _GetDepartures = () : any[] => {
    if (this.props.searchResult) {
      let departs: any[] = []
      this.props.searchResult.map((r: any) => {
        if (!departs.filter(d => d.legs[0].routes === r.legs[0].routes
            && d.totalFare.amount === r.totalFare.amount
        ).length)
          departs.push(r)
      })
      departs = departs.sort(this._compareDepart)
      return departs
    } else {
      return []
    }
  }

  _loadNext = (requestId: string, page: number) => {
    console.log(requestId)
    fetch('/api/flights/' + requestId + '?page=' + page, {
      method: 'get',
      headers: {
        'Content-Type': 'application/json',
        'Accept-Encoding': 'gzip',
      },
    }).then(res => res.json())
      .then(res => {
        //console.log(res)
        let result = this._GenerateRoute(res)
        //console.log(result)
        let currResult = this.props.searchResult

        result.pricedItins.map((itin: any) => {
          if (!currResult.filter(r => r.routes.join('|') === itin.routes.join('|')
            && r.totalFare.amount === itin.totalFare.amount
          ).length)
            currResult.push(itin)
        //  currResult.push(itin)
        })
        //console.log(currResult)
        this.props.setResult(currResult)
      })
      .then(() => this.setState({ departures: this._GetDepartures() }))
      //.then(res => {

        //this.setState({ result: this._GenerateRoute(res) })
      //})
  //    .then(() => this.setState({ departures: this._GetDepartures() }))
  }

  _generateRequest = (airline: any) => {
    let request: any = {
      availableFlightsOnly: true,
      directFlightsOnly: false,
      segments: [{
        departure: this.state.request.depart,
        origin: this.state.request.origin,
        destination: this.state.request.destination
      }],
      ptcs: [],
    }

    if (this.state.request.return)
      request.segments.push({
        departure: this.state.request.return,
        origin: this.state.request.destination,
        destination: this.state.request.origin
      })

    if (parseInt(this.state.request.pax.split('-')[0]))
      request.ptcs.push({ code: 'ADT', quantity: parseInt(this.state.request.pax.split('-')[0]) })
    if (parseInt(this.state.request.pax.split('-')[1]))
      request.ptcs.push({ code: 'CNN', quantity: parseInt(this.state.request.pax.split('-')[1]) })
    if (parseInt(this.state.request.pax.split('-')[2]))
      request.ptcs.push({ code: 'INF', quantity: parseInt(this.state.request.pax.split('-')[2]) })

    if (airline)
      request.airlines = [airline]
    
    return request
  }

  _sendRequest = (request: any) => {
    //console.log(request)
    return fetch('/api/flights', {
      method: 'post',
      headers: {
        'Content-Type': 'application/json',
        'Accept-Encoding': 'gzip',
      },
      body: JSON.stringify(request)
    }).then(res => {
      if(res) return res.json()
      }).catch(err => { })
  }
  
  _appendResult = (newResults: any[]) => {
    if (newResults) {
      let result = this.props.searchResult
      newResults.map((i: any) => {
        if (!result.filter((r: any) => r.routes.join('|') === i.routes.join('|')).length)
          result.push(i)
      })
      //result.pricedItins.sort(this._compare)
      this.props.setResult(result)
    }
  }

  _resetDeparture = () => {
    this.props.setSelectedDeparture(null)
  }
  
  componentDidMount() {
    let request = this._generateRequest(null)
    return this._sendRequest(request)
      .then(res => {
        this.setState({
          requestId: res.requestId,
          airlines: res.airlines
        })
        let resWithRoutes = this._GenerateRoute(res)
        //console.log(resWithRoutes)
        this.props.setResult(resWithRoutes)
      })
      .then(() => this.setState({ departures: this._GetDepartures() }))
      .then(() => this.state.airlines.map((airline: any) => {
        //Fetch data per airline
        let req = this._generateRequest(airline)
        this._sendRequest(req).then(res => this._GenerateRoute(res))
          .then(res => {
            if (res) { this._appendResult(res) }
          }).then(() => this.setState({ departures: this._GetDepartures() }))
          .then(() => {
            let loaded = this.state.loadedAirlines
            loaded.push(airline)
            this.setState({ loadedAirlines: loaded })
          })
      }))
  }
  
  _setFilteredAirline = (code: string, selected: boolean) => {
    if (selected) {
      if (!this.state.filteredAirlines.filter(a => a === code).length) {
        let airlines = this.state.filteredAirlines
        airlines.push(code)
        this.setState({ filteredAirlines: airlines})
      }
    } else {
      let airlines = this.state.filteredAirlines.filter(a => a !== code)
      this.setState({ filteredAirlines: airlines })
    }
  } 

  _setFilteredStop = (stop: number, selected: boolean) => {
    if (selected) {
      if (!this.state.filteredStops.filter(a => a === stop).length) {
        let stops = this.state.filteredStops
        stops.push(stop)
        this.setState({ filteredStops: stops })
      }
    } else {
      let stops = this.state.filteredStops.filter(a => a !== stop)
      this.setState({ filteredStops: stops })
    }
  } 

  _selectDeparture = (departure: any) => {
    this.props.setSelectedDeparture(departure)
  //  this.props.history.push('/flight/return')
  }

  _selectReturn = (returnLeg: any) => {
    this.props.setSelectedReturn(returnLeg)
    this.props.history.push('/flight/pax')
  }

  
  public render() {
    let filteredByStops =
      this.state.filteredStops.length > 0
        ? this.state.departures.filter(d => this.state.filteredStops.indexOf(d.departStop) > -1)
        : this.state.departures

    let filteredAirlines = Array.from(new Set(filteredByStops.map(fs => fs.departUniqueAirline)))
    let airlines = this.state.filteredAirlines.filter(fa => filteredAirlines.indexOf(fa) > -1)
    let filtered =
      airlines.length > 0
        ? filteredByStops.filter(f => airlines.indexOf(f.departUniqueAirline) > -1)
        : filteredByStops

    let _totalItems = filtered ? filtered.length : 0
    let _page = this.state.page

    let _returnItins: any[] = (this.props.selectedDeparture && this.state.departures)
      ? this.props.searchResult.filter(r =>
          r.legs[0].routes === this.props.selectedDeparture.routes)
        .sort(this._compareReturn)
      : []

    //console.log(this.props.searchResult) 

    //console.log(this.props.selectedDeparture.legs[0])

    return <Row>
      <Col md={3}>
        {this.props.selectedDeparture
          ? <SelectedDeparture departure={this.props.selectedDeparture} onReset={this._resetDeparture} />
          : <Filter
            filteredAirlines={this.state.filteredAirlines}
            filteredStops={this.state.filteredStops}
            loadedAirlines={this.state.loadedAirlines}
            onChangeFilterAirline={this._setFilteredAirline}
            onChangeFilterShop={this._setFilteredStop}
            itins={this.state.departures}
          />
        }
      </Col>
      <Col md={9}>
        {
          this.state.departures.length
            ? this.props.selectedDeparture
              ? <FlightReturnList
                  itins={_returnItins}
                  onSelect={this._selectReturn }
                />
              : <FlightDepartureList 
                departures={filtered}
                onSelectDeparture={this._selectDeparture}
              />
            : <h4>Loading......</h4>
        }
      </Col>
    </Row>
  }
}

// Wire up the React component to the Redux store
export default connect(
  (state: ApplicationState) => state.flight, // Selects which state properties are merged into the component's props
  FlightStore.actionCreators                 // Selects which action creators are merged into the component's props
)(FlightResult_Index) as typeof FlightResult_Index