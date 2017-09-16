import * as React from 'react'
import * as moment from 'moment'

import { Row, Col } from 'react-bootstrap'

import FlightDetail from './FlightDetail'

export default class FlightDetails extends React.Component<{ segments: any, leg: any }, any> {
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
  
  public render() {
    let lastTicketDate = moment(this.props.leg.itin.lastTicketDate).isValid()
      ? moment(this.props.leg.itin.lastTicketDate)
      : moment('20010101')
    return <section>
      {this.state.showDetails
        ?
        <section>
          <Row onClick={() => { this._handleShow(false) }}>
            <Col md={12}>Hide flight details</Col>
          </Row>
          <Row className='bg-faded'>
            <Col md={12}>{this.props.segments.map((s: any) => <FlightDetail segment={s} leg={this.props.leg} key={s.routes} />)}</Col>
          </Row>
          <Row>
            <Col md={12}>
              {'Last Ticket Date: ' + (moment('01-01-01').isSameOrAfter(lastTicketDate) ? '-' : lastTicketDate.format('LL'))}
            </Col>
          </Row>
        </section>
        : <Row onClick={() => { this._handleShow(true) }}>
          <Col md={12}>Show flight details</Col>
        </Row>
      }

    </section>
  }
}