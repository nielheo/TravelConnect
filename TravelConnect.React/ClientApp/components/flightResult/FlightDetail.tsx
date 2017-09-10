import * as React from 'react'
import * as moment from 'moment'
import { Row, Col } from 'react-bootstrap'

import * as Commons from '../Commons'

export default class FlightDetails extends React.Component<{ segment: any }, any> {
  constructor(props: any) {
    super(props)
    this.state = {
      showDetails: false,
      originAirport: null,
      destinationAirport: null,
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
    console.log('FlightDetail-DidMount')
    const { segment } = this.props
    Commons._GetAirport(segment.origin).then(res => { this.setState({ originAirport: res }) })
    Commons._GetAirport(segment.destination).then(res => { this.setState({ destinationAirport: res }) })
  }


  public render() {
    const { segment } = this.props
    const { originAirport, destinationAirport } = this.state
    return <section>
    <div className='row col-md-12'>
      <div className='col-md-3'>
        <h4> {moment(segment.departure.time).format('HH:mm')}</h4>
      </div>
      <div className='col-md-3'>
        <h4> {moment(segment.arrival.time).format('HH:mm')}</h4>
      </div>
      <div className='col-md-3'>
        <h4> {this._durationFormat(segment.elapsed)}</h4>
      </div>
    </div>
    {
      <section>
        <Row>
          <Col md={9} mdOffset={3}>
              {originAirport ? originAirport.cityName : segment.origin}
              <i> to </i>
              {destinationAirport ? destinationAirport.cityName : segment.destination }
          </Col>
        </Row>
        <Row>
          <Col md={9} mdOffset={3}>
              {originAirport ? originAirport.name + ' (' + originAirport.code + ')' : segment.origin}
              <i> to </i>
              {destinationAirport ? destinationAirport.name + ' (' + destinationAirport.code + ')' : segment.destination }
          </Col>
        </Row>
        <Row>
          <Col md={9} mdOffset={3}>
            {segment.marketingFlight.airline + ' ' + segment.marketingFlight.number}

            {segment.marketingFlight.airline !== segment.operatingFlight.airline
              ? ' flight by ' + segment.operatingFlight.airline + ' ' + segment.operatingFlight.number
              : ''}
          </Col>
        </Row>
      </section>
    }
    </section>
  }
}