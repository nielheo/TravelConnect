import * as React from 'react'

import { Row, Col } from 'react-bootstrap'

import FlightAirlineItem from './FilterAirlineItem'


export default class FlightDetails extends React.Component<{ airlines: any }, any> {
  public render() {
    //console.log(this.props.airlines)
    return <section>
      {
        this.props.airlines &&
        this.props.airlines.filter((airline: any) => airline.count).map((airline: any) => 
          <FlightAirlineItem airline={airline} key={'airlineFilter_' + airline.code} />

        )
      }

    </section>
  }
}