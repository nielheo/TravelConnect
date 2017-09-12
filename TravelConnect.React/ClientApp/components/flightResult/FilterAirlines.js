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

  public render() {
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