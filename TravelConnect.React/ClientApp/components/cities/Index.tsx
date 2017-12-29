import * as React from 'react'

import { NavLink, Link } from 'react-router-dom'
import { RouteComponentProps } from 'react-router-dom'

import Header from '../Header'

export default class Cities extends React.Component<RouteComponentProps<{
  countryCode: string
}>, any> {
  constructor(props: any) {
    super(props);

    this.state = {
      countryCode: props.match.params.countryCode
    }
  }

  //console.log(Commons._GetAirport('BKK'))
  public render() {

    return <div>
      <Header />
      <h1>All Cities in {this.state.countryCode.toUpperCase()}</h1>
      <hr />
    </div>
  }
}
