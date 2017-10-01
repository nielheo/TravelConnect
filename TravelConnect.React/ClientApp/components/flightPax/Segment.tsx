import * as React from 'react'
import { Row, Col } from 'react-bootstrap'

import * as Commons from '../Commons'

import * as moment from 'moment'

export default class Segment extends React.Component<{
  segment: any
}, any> {
  public render() {
    const s = this.props.segment
    return <section>
      <Row>
        <Col md={12}>
          {s.marketingFlight.airline} {s.marketingFlight.number} - Class: {s.brd}
        </Col>
      </Row>
      <Row>
        <Col md={6}>
          <b>{s.origin}</b>
        </Col>
        <Col md={6}>
          <b>{s.destination}</b>
        </Col>
      </Row>
      <Row>
        <Col md={6}>
          {moment(s.departure.time).format('ll')}
        </Col>
        <Col md={6}>
          {moment(s.arrival.time).format('ll')}
        </Col>
      </Row>
      <Row>
        <Col md={6}>
          {moment(s.departure.time).format('HH:MM')}
          <small> (gmt {s.departure.gmtOffset >= 0 ? '+' : ''}{s.departure.gmtOffset})</small>
        </Col>
        <Col md={6}>
          {moment(s.arrival.time).format('HH:MM')}
          <small> (gmt {s.arrival.gmtOffset >= 0 ? '+' : ''}{s.arrival.gmtOffset})</small>
        </Col>
      </Row>
    </section>
  }
}