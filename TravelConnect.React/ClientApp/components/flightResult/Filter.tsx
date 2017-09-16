import * as React from 'react'
import { Row, Col } from 'react-bootstrap'

import FilterStop from './FilterStop'
import FilterAirline from './FilterAirline'

import * as Commons from '../Commons'

import { FilterStopType, FilterAirlineType } from '../Classes'


interface FlightFilterState {
  stops: FilterStopType[],
  airlines: FilterAirlineType[],
}

export default class Filter extends React.Component<{
  filteredAirlines: string[],
  filteredStops: number[],
  loadedAirlines: string[],
  onChangeFilterAirline: any,
  onChangeFilterShop: any,
  itins: any[]
}, FlightFilterState> {
  constructor(props: any) {
    super(props)
    this.state = {
      stops: [],
      airlines: [],
    }
  }

  _compareFilterSop = (a: FilterStopType, b: FilterStopType) => {
    if (a.stop < b.stop)
      return -1
    if (a.stop > b.stop)
      return 1

    return 0
  }
    
  public render() {
    let stops = Array.from(new Set(this.props.itins.map(i => i.departStop)))
    let filterStops = stops.map((s: number) => {
      return  {
        stop: s,
        count: this.props.itins.filter(i => i.departStop === s).length,
        selected: this.props.filteredStops.indexOf(s) > -1,
      } as FilterStopType
    }).sort(this._compareFilterSop)
    
    return <Row>
      <h4>Filter Results:</h4>
      <FilterStop stops={filterStops} onChangeFilter={this.props.onChangeFilterShop} />
      <FilterAirline airlines={this.state.airlines} onChangeFilter={this.props.onChangeFilterAirline} />
    </Row>
  }
}