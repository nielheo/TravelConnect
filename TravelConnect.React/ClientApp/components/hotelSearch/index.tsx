import * as React from 'react'

import { Panel, Grid, Row, Col } from 'react-bootstrap'
import * as moment from 'moment'

import FormInput from '../commons/FormInput'
import FormTextbox from '../commons/FormTextbox'
import FormDropdown from '../commons/FormDropdown'
import SelectDate from '../commons/SelectDate'

import Occupancy from './Occupancy'

export default class HotelSearch_Index extends React.Component<{}, any> {
  constructor() {
    super();
    //let now = moment()
    //let today = moment({ year: now.year(), month: now.month(), day: now.day() })

    this.state = {
      country: '',
      city: '',
      checkIn: moment().add(90, 'days'),
      checkOut: moment().add(92, 'days'),
      rooms: 1,
      occupancies: [{ adult: 2, child: 0, childAges: [0, 0] },
          { adult: 2, child: 0, childAges: [0, 0] },
          { adult: 2, child: 0, childAges: [0, 0] },
          { adult: 2, child: 0, childAges: [0, 0] }],
      searchClicked: false,
    };
  }

  _onCountryChange = (e: any) => {
    this.setState({country: e.target.value})
  }

  _onCityChange = (e: any) => {
    this.setState({ city: e.target.value })
  }

  _onRoomsChange = (e: any) => {
    this.setState({ rooms: e.target.value})
  }

  _onAdultChange = (e: any, index: number) => {
    var occu = this.state.occupancies
    occu[index - 1].adult = parseInt(e.target.value)
    this.setState({ occupancies: occu })
  }

  _onChildChange = (e: any, index: number) => {
    var occu = this.state.occupancies
    occu[index - 1].child = parseInt(e.target.value)
    this.setState({ occupancies: occu })
  }

  _onChildAgeChange = (e: any, index: number, ageIndex: number) => {
    var occu = this.state.occupancies
    occu[index - 1].childAges[ageIndex] = parseInt(e.target.value)
    this.setState({ occupancies: occu })
  }

  _onSearchClick = () => {
    this.setState({ searchClicked: true })
  }

  public render() {
    var idx = 2
    console.log(this.state.occupancies)
    return <Col md={12}>

      <Row>
        <Col md={12}>
          <h3>Search your accomodation</h3>
        </Col>
      </Row>
      
      <Row>
        <Col md={6}>
          <FormTextbox
            onChange={this._onCountryChange}
            label='Country'
            error={this.state.searchClicked && !this.state.country ? '* required' : ''}
            disabled={false}
            value={this.state.country}
          />
        </Col>
        <Col md={6}>
          <FormTextbox
            onChange={this._onCityChange}
            label='City'
            error={this.state.searchClicked && !this.state.city ? '* required' : ''}
            disabled={false}
            value={this.state.city}
          />
        </Col>
      </Row>
      
      <Row>
        <Col md={6}>
          <SelectDate
            key="checkIn"
            label="Check In Date"
            onChange={this._onRoomsChange}
            selected={this.state.checkIn}
            error=""
            disabled={false}
          />
        </Col>
        <Col md={6}>
          <SelectDate
            key="checkIn"
            label="Check Out Date"
            onChange={this._onCountryChange}
            selected={this.state.checkOut}
            error=""
            disabled={false}
          />
        </Col>
      </Row>
      <Row>
        <Col md={6}>
          <FormDropdown
            key="Room"
            label="Room"
            onChange={this._onRoomsChange}
            //selected={this.state.checkOut}
            error=""
            value={this.state.rooms}
            disabled={false}
          >
            <option value='1'>1 room</option>
            <option value='2'>2 rooms</option>
            <option value='3'>3 rooms</option>
            <option value='4'>4 rooms</option>
          </FormDropdown>
        </Col>
        <Occupancy
          index={1}
          onAdultChange={this._onAdultChange}
          onChildChange={this._onChildChange}
          onChildAgeChange={this._onChildAgeChange}
          adult={this.state.occupancies[0].adult}
          child={this.state.occupancies[0].child}
          childAges={this.state.occupancies[0].childAges}
        />
        
      </Row>
      { 
        this.state.occupancies.slice(1, this.state.rooms).map((o: any) => 
        {
          return <Row>
            <Col md={6}></Col>
            <Occupancy
              index={idx++}
              onAdultChange={this._onAdultChange}
              onChildChange={this._onChildChange}
              onChildAgeChange={this._onChildAgeChange}
              adult={o.adult}
              child={o.child}
              childAges={o.childAges}
            />
          </Row>
        })
      }
      <Row>
        <Col md={12}>
          <input
            className='btn'
            type="button" value="Search"
            onClick={this._onSearchClick}
          />
        </Col>
      </Row>
    </Col>
  }
}
