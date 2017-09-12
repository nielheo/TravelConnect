import * as React from 'react'

import { Row, Col } from 'react-bootstrap'

import FilterAirlineItem from './FilterAirlineItem'


export default class FilterAirline extends React.Component<{ airlines: any }, any> {
  public render() {
    //console.log(this.props.airlines)
    return <section>
      <h4>Filter by Airlnes</h4>
      {
        this.props.airlines &&
        this.props.airlines.filter((airline: any) => airline.count).map((airline: any) => 
          <FilterAirlineItem airline={airline} key={'airlineFilter_' + airline.code} />

        )
      }

    </section>
  }
}