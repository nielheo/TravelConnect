import * as React from 'react'
import { Grid, Row, Col, Pagination, Panel } from 'react-bootstrap'

import { RouteComponentProps } from 'react-router-dom'
import { connect } from 'react-redux'
import { ApplicationState } from '../../store'
import * as FlightStore from '../../store/Flight'

import * as moment from 'moment'
import * as Commons from '../Commons'

import Segment from './Segment'

type FlightProps =
  FlightStore.FlightState
  & typeof FlightStore.actionCreators
  & RouteComponentProps<{ route: string }>

class FlightPax_Index extends React.Component<FlightProps, any> {
  //console.log(Commons._GetAirport('BKK'))
  
  public render() {
    const { searchResult, selectedDeparture, selectedReturn } = this.props
    let filteredDeparture = searchResult.filter(res => res.legs[0].routes === selectedDeparture.routes)
    let filteredReturn = filteredDeparture.filter(res => res.legs[1].routes === selectedReturn.routes)
    console.log(filteredReturn)
    return <Row>
      <Col md={3}>
        <Row>
          <Col md={12}>
            <h4>
            </h4>
          </Col>
        </Row>
        <Panel>
          <Row>
            <Col md={12}>
              <h4>Departure</h4>
            </Col>
          </Row>
          {
            filteredReturn[0].legs[0].segments.map((s: any) =>
              <Segment key={s.marketingFlight.airline + s.marketingFlight.number} segment={s} />
              )
          }
          <Row>
            <Col md={12}>
              <br/><h4>Return</h4>
            </Col>
          </Row>
          {
            filteredReturn[0].legs[1].segments.map((s: any) => 
              <Segment key={s.marketingFlight.airline + s.marketingFlight.number} segment={s} />
            )
          }
          <Row>
            <Col md={6}>
              <br /><h4>Total Fare:</h4>
            </Col>
            <Col md={6}>
              <br /><h4>{filteredReturn[0].totalFare.curr} {filteredReturn[0].totalFare.amount.toFixed(2)}</h4>
            </Col>
          </Row>
          <Row>
            <Col md={6}>
              Base Fare: 
            </Col>
            <Col md={6} className='text-right'>
              {filteredReturn[0].baseFare.curr} {Commons.FormatNum(filteredReturn[0].baseFare.amount.toFixed(2))}
            </Col>
          </Row>
          <Row>
            <Col md={6}>
              Total Taxes: 
            </Col>
            <Col md={6} className='text-right'>
              {filteredReturn[0].taxes.curr} {Commons.FormatNum(filteredReturn[0].taxes.amount.toFixed(2))}
            </Col>

          </Row>
        </Panel>
      </Col>
      <Col md={9}>Main</Col>
    </Row>
  }
}

// Wire up the React component to the Redux store
export default connect(
  (state: ApplicationState) => state.flight, // Selects which state properties are merged into the component's props
  FlightStore.actionCreators                 // Selects which action creators are merged into the component's props
)(FlightPax_Index) as typeof FlightPax_Index