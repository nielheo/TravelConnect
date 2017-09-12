import * as React from 'react'
import { RouteComponentProps } from 'react-router-dom'
import * as queryString from 'query-string'
import { Grid, Row, Col, Pagination } from 'react-bootstrap'
import FlightDeparture from './FlightDeparture'
import FilterAirline from './FilterAirline'

import * as Commons from '../Commons'

import * as moment from 'moment'

const CryptoJS = require('crypto-js') as any;

export default class FlightSearch extends React.Component<RouteComponentProps<{ route: string }>, any> {
  constructor(props: any) {
    super(props);

    let route = props.match.params.route

    let data = queryString.parse(route)
    
    this.state = {
      ...data,
      result: null,
      page: 1,
      itemsPerPage: 20,
      requestId: '',
      airlines: []
    }
  }

  _compareDepart = (a: any, b: any) => {
    if (a.totalPrice < b.totalPrice)
      return -1;
    if (a.totalPrice > b.totalPrice)
      return 1;

    if (a.legs[0].segments[0].departure < b.legs[0].segments[0].departure)
      return -1

    if (a.legs[0].segments[0].departure > b.legs[0].segments[0].departure)
      return 1

    return 0;
  }

  _GenerateRoute = (result: any) => {
    let x: number = 0
    if (result) {
      result.pricedItins.map((i: any) => {
        i.itinNo = x++
        i.legs.map((l: any) => {
          l.routes = l.segments.map((s: any) => {
            return s.marketingFlight.airline + s.marketingFlight.number
          }).join('-') + ':' + i.curr + i.totalPrice.toFixed(2)
          l.airlines = l.segments.map((s: any) => {
            return s.marketingFlight.airline
          })
        })
      })

      result.pricedItins.map((i: any) => {
        i.routes = i.legs.map((l: any) => l.routes).join('|')
        i.airlines = [] 
        i.legs.map((l: any) => l.airlines.map((l2: any) => i.airlines.push(l2)))
        i.uniqueAirline = Array.from(new Set(i.airlines)).join(',')
        if (i.uniqueAirline.indexOf(',') > -1)
          i.uniqueAirline = 'Multi'
      })
    }
    return result
  }

  
  _GetDepartures = () => {
    if (this.state.result) {

      let departs: any[] = []
      this.state.result.pricedItins.map((r: any) => {
        if (!departs.filter(d => d.legs[0].routes === r.legs[0].routes).length)
          departs.push(r)
      })
      departs = departs.sort(this._compareDepart)
      return departs
    } else {
      return null
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
        let currResult = this.state.result
        result.pricedItins.map((itin: any) => {
          currResult.pricedItins.push(itin)
        })
        //console.log(currResult)
        this.setState({
          result: currResult,
        })
      })
      .then(() => this.setState({ departures: this._GetDepartures() }))
      .then(res => {
        this.setState({ result: this._GenerateRoute(res) })
      })
  //    .then(() => this.setState({ departures: this._GetDepartures() }))
  }

  _generateRequest = (airline: any) => {
    let request: any = {
      availableFlightsOnly: true,
      directFlightsOnly: false,
      segments: [{
        departure: this.state.depart,
        origin: this.state.origin,
        destination: this.state.destination
      }],
      ptcs: [],
    }

    if (this.state.return)
      request.segments.push({
        departure: this.state.return,
        origin: this.state.destination,
        destination: this.state.origin
      })

    if (parseInt(this.state.pax.split('-')[0]))
      request.ptcs.push({ code: 'ADT', quantity: parseInt(this.state.pax.split('-')[0]) })
    if (parseInt(this.state.pax.split('-')[1]))
      request.ptcs.push({ code: 'CNN', quantity: parseInt(this.state.pax.split('-')[1]) })
    if (parseInt(this.state.pax.split('-')[2]))
      request.ptcs.push({ code: 'INF', quantity: parseInt(this.state.pax.split('-')[2]) })

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
    //.then(res => console.log(res))
  }
  
  componentDidMount() {
    let request = this._generateRequest(null)
    return this._sendRequest(request)
      .then(res => {
        this.setState({ result: this._GenerateRoute(res), requestId: res.requestId })
      })
      .then(() => this.setState({ departures: this._GetDepartures() }))
      .then(() => this.setState({ airlines: this._setAirlineList(this.state.departures) }))
      .then(() => this.state.result.airlines.map((airline: any) => {
        let req = this._generateRequest(airline)
        this._sendRequest(req).then(res => this._GenerateRoute(res))
          .then(res => {
            if (airline === 'PR')
              console.log(res)
            if (res) {
              let result = this.state.result
              res.pricedItins.map((i: any) => {
                if (!result.pricedItins.filter((r: any) => r.routes === i.routes).length)
                  result.pricedItins.push(i)
              })
              //result.pricedItins.sort(this._compare)
              this.setState({
                result: result
              })
            }
            //console.log(result)
          }).then(() => this.setState({ departures: this._GetDepartures() }))
          .then(() => {
            let airlines1 = this.state.airlines.filter((d: any) => d.code !== airline)
            //console.log(airline)
            //console.log(airlines1)
            airlines1.push({
              code: airline,
              loaded: true,
              count: this.state.departures.filter((d: any) => d.uniqueAirline === airline).length
            })

            this.setState({ airlines: airlines1.sort(this._compareAirlineList)})
          })
      }))
  }

  _handlePageChange = (a: any) => {
    if (a !== this.state.page) {
      this.setState({
        page: a
      })
    }
  }

  _compareAirlineList = (a: any, b: any) => {
    if (a.code < b.code)
      return -1
    if (a.code > b.code)
      return 1

    return 0
  }

  _setAirlineList = (departures: any) => {
    if (!departures)
      return {}

    let airlines = Array.from(new Set(departures.map((d: any) => d.uniqueAirline)))

    let a = airlines.map((airline: any) => {
      return {
        code: airline,
        count: departures.filter((d: any) => d.uniqueAirline === airline).length,
        loaded: airline === 'Multi' ? true : false,
      }
    })

    return a.sort(this._compareAirlineList)
  }

  public render() {
    
    let _totalItems = this.state.departures ? this.state.departures.length : 0
    let _totalPages = _totalItems ? Math.ceil(_totalItems / this.state.itemsPerPage) : 0
    let _page = this.state.page > _totalPages ? _totalPages : this.state.page
    let _startIndex = (_page - 1) * this.state.itemsPerPage
    let _endIndex = _page * this.state.itemsPerPage
    //console.log(this.state.airlines)
    //this._setUniqueAirline(this.state.departures)
    return <Row>
      <Col md={3}>
        <h4>Filter Results:</h4>
        <FilterAirline airlines={this.state.airlines} />
      </Col>
      <Col md={9}>
        <h1>Select your flight</h1>
        
        {
          this.state.departures
            ? <section>
              <h4>Select from {Commons.FormatNum(this.state.departures.length)} Departures Flight</h4>
              <h4>{this.state.result ? Commons.FormatNum(this.state.result.pricedItins.length) : 0} Routes available</h4>
              <Row className="text-right">
                <Col md={12}>
                  <Pagination
                    prev
                    next
                    first
                    last
                    ellipsis
                    boundaryLinks
                    items={_totalPages}
                    maxButtons={5}
                    activePage={_page}
                    onSelect={this._handlePageChange} />
                </Col>
              </Row>
              {
                this.state.departures.slice(_startIndex, _endIndex).map((r: any) =>
                  <FlightDeparture depart={r} />)}
              <Row className="text-right">
                <Col md={12}>
                  <Pagination
                    prev
                    next
                    first
                    last
                    ellipsis
                    boundaryLinks
                    items={_totalPages}
                    maxButtons={5}
                    activePage={_page}
                    onSelect={this._handlePageChange} />
                </Col>
              </Row>

            </section>
            : <h4>Loading......</h4>
        }
      </Col>
    </Row>
  }
}