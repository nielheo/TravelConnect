import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as Commons from './Commons'
import { NavLink, Link } from 'react-router-dom';

import { Panel, Grid, Row, Col, Tab, Tabs } from 'react-bootstrap'

import HotelSearch from './hotelSearch'
import Header from './Header'
import Helmet from 'react-helmet'

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
      <Helmet>
        <title>TravelConnect: Great value for hotel and flight booking</title>
        <meta name='description' content={'Great value for hotels aroung the globe, flight to all continents'} />
        <meta name='keywords' content='Hotel booking, Flight ticket, hotel, flight' />
      </Helmet>
      <Header />
      <Tabs defaultActiveKey={2} id="uncontrolled-tab-example">
        <Tab eventKey={1} title="Tab 1">
          <ul>
            <li>
              <NavLink exact to={'/flight/search'}>Flight Search</NavLink>, connect with Travelport's uAPI
            </li>
          </ul>
        </Tab>
        <Tab eventKey={2} title="Tab 2">
          <section>
            <Row><Col md={12}><br/>
                <ul>
                    <li>
                        Hotel Search is connected with EAN
              </li>
                </ul></Col>
            </Row>
            <Row>
                <HotelSearch history={this.props.history} />
            </Row>
          </section>
        </Tab>
      </Tabs>
      <hr/>
      <Row>
        <Col md={12}><h4>Popular Destinations</h4><hr /></Col>
      </Row>
      <Row>
        <Col md={3}>
          <ul>
            <li><NavLink to='hotels/id/bali'>Bali, Indonesia</NavLink></li>
            <li><Link to='hotels/cn/hong kong'>Hong Kong, China</Link></li>
            <li><Link to='hotels/th/bangkok'>Bangkok, Thailand</Link></li>
            <li><Link to='hotels/vn/hanoi'>Hanoi, Vietnam</Link></li>
            <li><Link to='hotels/ph/boracay'>Boracay, Philippine</Link></li>
          </ul>
        </Col>
        <Col md={3}></Col>
        <Col md={3}></Col>
        <Col md={3}></Col>

      </Row>

    </div>
  }
}