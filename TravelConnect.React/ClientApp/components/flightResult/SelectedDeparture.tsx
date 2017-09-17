import * as React from 'react'
import { Panel, Row, Col } from 'react-bootstrap'

import * as moment from 'moment'

export default class SelectedDeparture extends React.Component<{ departure: any, onReset: any}, any> {
  constructor(props: any) {
    super(props)
  }
  
  public render() {
    const leg = this.props.departure
    let i = 0
    return <Panel>
      <Row>
        <Col md={8}><b>Departure Flight</b></Col>
        <Col md={4} className='text-right'>
          <div onClick={this.props.onReset}>Change Departure</div>
        </Col>

      </Row>
      {leg.segments.map((s: any) => <section key={'selectedDeparture_' + i++}>
        
        <Row>
          <Col md={12}>{s.marketingFlight.airline} {s.marketingFlight.number} - Class: {s.brd}</Col>
        </Row>
        {
          s.operatingFlight.airline !== s.marketingFlight.airline &&
          <Row>
            <Col md={12}>operated by {s.operatingFlight.airline} {s.operatingFlight.number}</Col>

          </Row>
        }

        <Row>
          <Col md={6}><b>{s.origin}</b></Col>
          <Col md={6}><b>{s.destination}</b></Col>
        </Row>
        <Row>
          <Col md={6}>{moment(s.departure.time).format('ll')}</Col>
          <Col md={6}>{moment(s.arrival.time).format('ll')}</Col>
        </Row>
        <Row>
          <Col md={6}>{moment(s.departure.time).format('HH:MM')}
            <small> (gmt{s.departure.gmtOffset >= 0 ? '+' : ''}{s.departure.gmtOffset})</small></Col>
          <Col md={6}>{moment(s.arrival.time).format('HH:MM')}
            <small> (gmt{s.arrival.gmtOffset >= 0 ? '+' : ''}{s.arrival.gmtOffset})</small></Col>
        </Row>
        </section>
        )

      }
    </Panel>
  }
}
