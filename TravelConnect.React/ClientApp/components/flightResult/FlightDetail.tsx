import * as React from 'react'
import * as moment from 'moment'
import { Row, Col } from 'react-bootstrap'

import * as Commons from '../Commons'

export default class FlightDetails extends React.Component<{ segment: any, leg: any }, any> {
  constructor(props: any) {
    super(props)
    this.state = {
      showDetails: false,
      originAirport: null,
      destinationAirport: null,
      marketingAirlineName: null,
      operatingAirlineName: null,
    }
  }

  _durationFormat = (date: any) => {
    return Math.floor(date / 60) + 'h ' +
      ((date % 60) ? date % 60 + 'm' : '')
  }

  _handleShow = (show: any) => {
    if (show !== this.state.showDetails) {
      this.setState({
        showDetails: show
      })
    }
  }

  componentDidMount() {
    const { segment } = this.props
    Commons._GetAirport(segment.origin).then((res: any) => { this.setState({ originAirport: res }) })
    Commons._GetAirport(segment.destination).then((res: any) => { this.setState({ destinationAirport: res }) })
    Commons._GetAirline(segment.marketingFlight.airline).then((res: any) => { this.setState({ marketingAirlineName: res.name }) })
    Commons._GetAirline(segment.operatingFlight.airline).then((res: any) => { this.setState({ operatingAirlineName: res.name }) })
  }

  public render() {
    const { segment } = this.props
    const { originAirport, destinationAirport } = this.state
    //let lastTicketDate = moment(this.props.leg.itin.lastTicketDate).isValid()
    //  ? moment(this.props.leg.itin.lastTicketDate)
    //  : moment('20010101')
    return <section>
      <Row>
        <Col md={3}>
          <big> {moment(segment.departure.time).format('HH:mm')}</big><br/>
          <small>{moment(segment.departure.time).format('LL')}</small>
        </Col>
        <Col md={3}>
          <big> {moment(segment.arrival.time).format('HH:mm')}</big><br/>
          <small>{moment(segment.arrival.time).format('LL')}</small>
        </Col>
        <Col md={3}>
          <h4> {this._durationFormat(segment.elapsed)}</h4>
        </Col>
      </Row>
      {
        <section>
          <Row>
            <Col md={9} mdOffset={3}>
              {originAirport ? originAirport.cityName : segment.origin}
              <i> to </i>
              {destinationAirport ? destinationAirport.cityName : segment.destination}
            </Col>
          </Row>
          <Row>
            <Col md={9} mdOffset={3}>
              {originAirport ? originAirport.name + ' (' + originAirport.code + ')' : segment.origin}
              <i> to </i>
              {destinationAirport ? destinationAirport.name + ' (' + destinationAirport.code + ')' : segment.destination}
            </Col>
          </Row>
          <Row>
            <Col md={9} mdOffset={3}>
              {(this.state.marketingAirlineName
                ? this.state.marketingAirlineName + ' (' + segment.marketingFlight.airline + ')'
                : segment.marketingFlight.airline) + ' ' + segment.marketingFlight.number}

              {segment.marketingFlight.airline !== segment.operatingFlight.airline
                ? ' flight by ' + segment.operatingFlight.airline + ' ' + segment.operatingFlight.number
                : ''}
            </Col>
          </Row>
          <Row>
            <Col md={9} mdOffset={3}>
              Booking Class: {segment.brd}
            </Col>
          </Row>
        </section>
      }
      
    </section>
  }
}