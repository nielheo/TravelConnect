import * as React from 'react'

import { Row, Col } from 'react-bootstrap'

import FilterAirlineItem from './FilterAirlineItem'

import { FilterAirlineType } from '../Classes'

export default class FilterAirline extends React.Component<{
  airlines: FilterAirlineType[],
  onChangeFilter: any
}, any> {
  _compareAirlineList = (a: any, b: any) => {
    if (a.code < b.code)
      return -1
    if (a.code > b.code)
      return 1

    return 0
  }
  public render() {
    //console.log(this.props.airlines)
    return <section>
      <h4>Filter by Airlnes</h4>
      {
        this.props.airlines &&
        this.props.airlines.sort(this._compareAirlineList).filter((airline: any) => airline.count).map((airline: any) =>
          <FilterAirlineItem airline={airline} key={'airlineFilter_' + airline.code} onSetFilter={this.props.onChangeFilter} />

        )
      }

    </section>
  }
}