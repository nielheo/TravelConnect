import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as Commons from './Commons'
import FlightSearch from './flightSearch'
import { NavLink, Link } from 'react-router-dom';

export default class Home extends React.Component<RouteComponentProps<{}>, any> {
  //console.log(Commons._GetAirport('BKK'))
  public render() {
    
    return <div>

      <h1>Travel Connect</h1>
      <p>Welcome to Travel Connect, bring your Travel Shop Online</p>
      <ul>
        <li>
          <NavLink exact to={'/flight/search'}>Flight Search</NavLink>, connect with Travelport's uAPI
        </li>
        <li>
          <NavLink exact to={'/hotel/search'}>Hotel Search</NavLink>, connect with EAN
        </li>
      </ul>
  
    </div>;
  }
}