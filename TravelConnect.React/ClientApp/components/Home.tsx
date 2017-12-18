import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as Commons from './Commons'
import { NavLink, Link } from 'react-router-dom';

import { Panel, Grid, Row, Col } from 'react-bootstrap'

import HotelSearch from './hotelSearch'

export default class Home extends React.Component<RouteComponentProps<{}>, any> {
  constructor() {
    super();
    //let now = moment()
    //let today = moment({ year: now.year(), month: now.month(), day: now.day() })

    this.state = {
      activeTab: 'hotel',
    };
  }

  _onTabClick = (a: string) => {
    this.setState({ activeTab: a})
  }

  //console.log(Commons._GetAirport('BKK'))
  public render() {
    
    return <div>

      <h1>Travel Connect</h1>
      <p>Welcome to Travel Connect, bring your Travel Shop Online</p>
      <input
        className={this.state.activeTab === 'flight' ? 'btn btn-primary' : 'btn'}
        type="button" value="Flight"
        onClick={() => this._onTabClick('flight')}
      />
      <input
        className={this.state.activeTab === 'hotel' ? 'btn btn-primary' : 'btn'}
        type="button" value="Hotel"
        onClick={() => this._onTabClick('hotel')}
      />
      <Panel>
        {this.state.activeTab === 'hotel' &&
          <section>
            <Row>
              <ul>
                <li>
                  Hotel Search is connected with EAN
                </li>
              </ul>
            </Row>
            <Row>
              <HotelSearch />
            </Row>
          </section>
        }
        { this.state.activeTab === 'flight' &&
          <li>
            <NavLink exact to={'/flight/search'}>Flight Search</NavLink>, connect with Travelport's uAPI
          </li> }
      </Panel>
      <ul>
        
        
      </ul>
  
    </div>;
  }
}