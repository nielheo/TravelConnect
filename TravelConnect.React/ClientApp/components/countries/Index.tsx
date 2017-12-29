import * as React from 'react'

import { NavLink, Link } from 'react-router-dom'


import Header from '../Header'

export default class Home extends React.Component<any, any> {
  constructor() {
    super();

  }

  //console.log(Commons._GetAirport('BKK'))
  public render() {

    return <div>
      <Header />
      <h1>All Countries</h1>
      <hr />
    </div>
  }
}
