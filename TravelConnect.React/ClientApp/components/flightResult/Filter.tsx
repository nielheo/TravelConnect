import * as React from 'react'
import { Row, Col } from 'react-bootstrap'

import FilterStop from './FilterStop'
import FilterAirline from './FilterAirline'

import * as Commons from '../Commons'

export default class Filter extends React.Component<{
  airlines: any, stops: any,
  onSetStopFilter: any, onSetAirlineFilter: any
}, any> {
  constructor(props: any) {
    super(props)
  }
  
  public render() {
    //console.log(this.state.airlineFullname)
    return <Row>
      <h4>Filter Results:</h4>
      <FilterStop stops={this.props.stops} onSetFilter={this.props.onSetStopFilter} />
      <FilterAirline airlines={this.props.airlines} onSetFilter={this.props.onSetAirlineFilter} />
    </Row>
  }
}