import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as Commons from './Commons'
import { NavLink, Link } from 'react-router-dom';

import { Panel, Grid, Row, Col, Tab, Tabs } from 'react-bootstrap'

import HotelSearch from './hotelSearch'
import Header from './Header'

export default class Home extends React.Component<RouteComponentProps<{}>, any> {
  constructor(props: any) {
    super(props);
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
      
  
    </div>;
  }
}