import * as React from 'react'
import * as moment from 'moment'

import { Row, Col } from 'react-bootstrap'

import FlightDetail from './FlightDetail'

export default class FlightDetails extends React.Component<{ segments: any }, any> {
  constructor(props: any) {
    super(props)
    this.state = {
      showDetails: false,
    }
  }

  _handleShow = (show: any) => {
    if (show !== this.state.showDetails) {
      this.setState({
        showDetails: show
      })
    }
  }

  /*
  _dateOnly = (dateTime: any) => {
    return moment({
      year: dateTime.year(),
      month: dateTime.month(),
      day: dateTime.date()
    });
  }

  _dateDiff = (dateStart: any, dateEnd: any) => {
    return this._dateOnly(dateEnd).diff(this._dateOnly(dateStart), 'days')
  }

  _minuteDiff = (dateStart: any, dateEnd: any) => {
    return dateEnd.diff(dateStart, 'minutes')
  }

  _durationFormat = (date: any) => {
    return Math.floor(date / 60) + 'h ' +
      ((date % 60) ? date % 60 + 'm' : '')
  }

  _getStopOvers = (leg: any) => {
    if (leg.segments.length === 1)
      return null

    let stopOvers: any[] = []
    for (var i: number = 0; i < leg.segments.length - 1; i++) {
      stopOvers.push({
        code: leg.segments[i].destination,
        stopTime: this._minuteDiff(moment(leg.segments[i].arrival.time),
          moment(leg.segments[i + 1].departure.time)),
        date1: leg.segments[i].arrival.time,
        date2: leg.segments[i + 1].departure.time
      })
    }

    return stopOvers
  }
  */

  public render() {
    /*const { depart } = this.props
    const firstLeg = depart.legs[0]
    const firstSegment = firstLeg.segments[0]
    const lastSegment = firstLeg.segments[firstLeg.segments.length - 1]
    const dateDiff = this._dateDiff(moment(firstSegment.departure.time),
      moment(lastSegment.arrival.time))
    const stopOvers: any = this._getStopOvers(firstLeg)*/
    return <section>
      {this.state.showDetails
        ?
        <section>
          <Row onClick={() => { this._handleShow(false) }}>
            <Col md={12}>Hide flight details</Col>
          </Row>
          <Row className='bg-faded'>
            <Col md={12}>{this.props.segments.map((s: any) => <FlightDetail segment={s} />)}</Col>
          </Row>

        </section>
        : <Row onClick={() => { this._handleShow(true) }}>
          <Col md={12}>Show flight details</Col>
        </Row>
      }

    </section>
  }
}