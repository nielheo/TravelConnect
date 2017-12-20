import * as React from 'react'

import { Panel, Grid, Row, Col } from 'react-bootstrap'

import { RouteComponentProps } from 'react-router-dom';

import * as moment from 'moment'
import * as queryString from 'query-string'

import FormInput from '../commons/FormInput'
import FormTextbox from '../commons/FormTextbox'
import FormDropdown from '../commons/FormDropdown'
import SelectDate from '../commons/SelectDate'

export default class HotelResult_Index extends React.Component<
  RouteComponentProps<{
    country: string
    city: string
    cin: string
    cout: string
    room1: string
    room2: string
    room3: string
    room4: string
}>, any> {
  constructor(props: any) {
    super(props);
    //let now = moment()
    //let today = moment({ year: now.year(), month: now.month(), day: now.day() })
    
    let query = queryString.parse(props.location.search)
    //let rooms = this._parseRoom(query)

    this.state = {
      country: props.match.params.country,
      city: props.match.params.city,
      checkIn: this._parseDate(query.cin),
      checkOut: this._parseDate(query.cout),
      rooms: query.rooms,
      //occupancies: query.rooms,
      locale: props.match.params.locale || 'en_US',
      currency: query.currency || 'USD',
      result: null,
    };
  }

  _parseDate = (value: string) => {
      var parsed = moment(value, 'YYYY-MM-DD')
      if (parsed.isValid())
          return parsed
      else 
          return null
  }

  _parseOccupancy = (room: string) => {
    let oc = room.split(',')
    let cAges = []
    for (var i = 1; i < oc.length; i++) {
        cAges.push(parseInt(oc[i]))
    }

    let occupancy = { adult: parseInt(oc[0]), childAges: cAges }

    return occupancy
  }

  _parseRoom = (value: any) => {
      let rooms = []
      if (value.room1) rooms.push(this._parseOccupancy(value.room1))
      if (value.room2) rooms.push(this._parseOccupancy(value.room2))
      if (value.room3) rooms.push(this._parseOccupancy(value.room3))
      if (value.room4) rooms.push(this._parseOccupancy(value.room4))
      return rooms
  }

  _sendRequest = (request: any) => {
    //console.log(request)
    return fetch('/api/hotels' + request, {
      method: 'get',
      headers: {
          'Content-Type': 'application/json',
          'Accept-Encoding': 'gzip',
      }
    }).then(res => {
        if (res) return res.json()
    }).catch(err => { })
  }

  _constructRequest = () => {
      let req = '/' + this.state.country + '/' + this.state.city
          + '?checkin=' + moment(this.state.checkIn).format('YYYY-MM-DD')
          + '&checkout=' + moment(this.state.checkOut).format('YYYY-MM-DD')
          + '&rooms=' + this.state.rooms
          + '&locale=' + this.state.locale
          + '&currency=' + this.state.currency

      return req
  }

  _constructMoreRequest = () => {
      let req = '/more'
          + '?locale=' + this.state.locale
          + '&currency=' + this.state.currency
          + '&cacheKey=' + this.state.result.cacheKey
          + '&cacheLocation=' + this.state.result.cacheLocation
          + '&requestKey=' + this.state.result.requestKey
      console.log(req)
      return req
  }

  _getMore = () => {
      this._sendRequest(this._constructMoreRequest()).then(rs => {
          var result = this.state.result

          rs.hotels.map((htl: any) => {
              result.hotels.push(htl)
          })
          
          result.cacheKey = rs.cacheKey
          result.cacheLocation = rs.cacheLocation
          result.requestKey = rs.requestKey

          this.setState({ result: result })

          if (this.state.result.cacheKey) this._getMore()
      })
  }

  componentWillMount() {
    console.log(this.state)
  }

  componentDidMount() {
      this._sendRequest(this._constructRequest())
          .then(r => {
              
              this.setState({ result: r })

              if (this.state.result.cacheKey) {
                  console.log('get more')
                  this._getMore()
              }
          })
  }
  
  public render() {
    return <div>
        {
            this.state.result
                ? <section>
                    <Row><Col md={12}><h3>{this.state.result.hotels.length} hotels found</h3></Col></Row>
                    {
                        this.state.result.hotels.map((hotel:any) => 
                            <Panel>
                                <Row>
                                    <Col md={12}>{hotel.name}</Col>
                                </Row>
                                <Row>
                                    <Col md={12}>{hotel.shortDesc}</Col>
                                </Row>
                                <Row>
                                    <Col md={12}>{hotel.currCode} {hotel.rateFrom} - {hotel.currCode} {hotel.rateTo}</Col>
                                </Row>
                            </Panel>
                        )
                    }
                </section>
                : <Row><Col md={12}>Search your hotels. Please wait....</Col></Row>
        }
      </div>
  }
}
