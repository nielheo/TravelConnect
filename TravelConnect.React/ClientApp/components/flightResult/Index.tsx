import * as React from 'react'
import { RouteComponentProps } from 'react-router-dom'
import * as queryString from 'query-string'
import { Grid, Row, Col } from 'react-bootstrap'
import FlightDeparture from './FlightDeparture'

const CryptoJS = require('crypto-js') as any;

export default class FlightSearch extends React.Component<RouteComponentProps<{ route: string }>, any> {
  constructor(props: any) {
    super(props);

    let route = props.match.params.route

    let data = queryString.parse(route)

    //var bytes = CryptoJS.AES.decrypt(decodeURIComponent(route), 'DheoTech');
    //var data = JSON.parse(bytes.toString(CryptoJS.enc.Utf8));

    this.state = {
      ...data,
      result: null
    }
  }

  _GenerateRoute = (result: any) => {
    let x: number = 0
    result.pricedItins.map((i: any) => {
      i.itinNo = x++
      i.legs.map((l: any) => {
        l.routes = l.segments.map((s: any) => {
          return s.marketingFlight.airline + s.marketingFlight.number
        }).join('-') + ':' + i.curr + i.totalPrice.toFixed(2)
      })
    })
    return result
  }

  _GetDepartures = () => {
    let departs: any[] = []
    this.state.result.pricedItins.map((r: any) => {
      if (!departs.filter(d => d.legs[0].routes === r.legs[0].routes).length)
        departs.push(r)
    })

    return departs
  }

  componentDidMount() {
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

    fetch('/api/flights', {
      method: 'post',
      headers: {
        'Content-Type': 'application/json',
        'Accept-Encoding': 'gzip',
      },
      body: JSON.stringify(request)
    }).then(res => res.json())
      .then(res => this.setState({ result: this._GenerateRoute(res) }))
      .then(() => this.setState({ departures: this._GetDepartures() }))
  }

  public render() {
    return <Row>
      <Col md={12}>
        <h1>Select your flight</h1>
        {
          this.state.departures
            ? this.state.departures.map((r: any) =>
              <FlightDeparture depart={r} key={r.itinNo} />
            )
            : <h4>Loading......</h4>
        }
      </Col>
    </Row>
  }
}