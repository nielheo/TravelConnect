import * as React from 'react'

import { Panel, Grid, Row, Col, Pagination, PageHeader } from 'react-bootstrap'

import { RouteComponentProps } from 'react-router-dom';

import * as moment from 'moment'
import * as queryString from 'query-string'

import Room from './Room'

export default class HotelResult_Index extends React.Component<
  RouteComponentProps<{
    country: string
    city: string
    hotelId: number
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
      hotelId: props.match.params.hotelId,
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
    let req = '/' + this.state.country + '/' + this.state.city + '/' + this.state.hotelId + '/rooms'
      + '?checkin=' + moment(this.state.checkIn).format('YYYY-MM-DD')
      + '&checkout=' + moment(this.state.checkOut).format('YYYY-MM-DD')
      + '&rooms=' + this.state.rooms
      + '&locale=' + this.state.locale
      + '&currency=' + this.state.currency

    return req
  }
  
  componentDidMount() {
    this._sendRequest(this._constructRequest())
      .then(r => {
        this.setState({ result: r })
      })
  }
  

  public render() {
    console.log(this.state.result)
    let { result } = this.state
    let roomTypeIds: any[] = []
    if (result)
      result.rooms.map((room: any) => {
        if (roomTypeIds.indexOf(room.roomTypeId) < 0)
          roomTypeIds.push(room.roomTypeId)
      })
    console.log(roomTypeIds)
    return <section>
      {
        this.state.result &&
        <section>
          <Row>
            <Col md={12}>
              <PageHeader>{result.hotelDetail.name} <br />
                <small>{result.hotelDetail.address}, {result.hotelDetail.city}, {result.hotelDetail.country}</small>

              </PageHeader>
            </Col>
          </Row>
          <Row>
            <Col md={12}> {roomTypeIds.map((roomTypeId: any) => <Room room={result.rooms.filter((room: any) => room.roomTypeId === roomTypeId)} key={roomTypeId} />)} </Col>
          </Row>
        </section>
        
      }
    </section>
  }
}
