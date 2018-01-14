import * as React from 'react'

import { Panel, Grid, Row, Col } from 'react-bootstrap'
import * as moment from 'moment'
import { AsyncTypeahead, Typeahead } from 'react-bootstrap-typeahead'

import { debounce } from 'lodash'

import FormInput from '../commons/FormInput'
import FormTextbox from '../commons/FormTextbox'
import FormDropdown from '../commons/FormDropdown'
import SelectDate from '../commons/SelectDate'

import Occupancy from './Occupancy'

export default class HotelSearch_Index extends React.Component<{ history: any }, any> {
  constructor() {
    super();
    //let now = moment()
    //let today = moment({ year: now.year(), month: now.month(), day: now.day() })

    this.state = {
      country: '',
      city: '',
      checkIn: moment().add(7, 'days'),
      checkOut: moment().add(9, 'days'),
      rooms: 1,
      occupancies: [{ adult: 2, child: 0, childAges: [0, 0] },
          { adult: 2, child: 0, childAges: [0, 0] },
          { adult: 2, child: 0, childAges: [0, 0] },
          { adult: 2, child: 0, childAges: [0, 0] }],
      searchClicked: false,
      cityOptions: []
    };
    //this._CityAutocomplete = debounce(this._CityAutocomplete, 300)
    }

  _CityAutocomplete = (query: string) => {
    return fetch(`/api/gtageo/searchcities?cityname=${query}`, {
        method: 'get',
        headers: {
            'Content-Type': 'application/json',
            'Accept-Encoding': 'gzip',
        }
    }).then(res => {
        if (res) {
            return res.json()
        }
    }).catch(err => { })
  }

  _onCountryChange = (e: any) => {
    this.setState({country: e.target.value})
  }

  _onCityChange = (e: any) => {
      this.setState({ city: e.target.value })
      //this._CityAutocomplete()
  }

  _onCheckInChange = (date: any) => {
    this.setState({
      checkIn: date
    })
  }

  _onCheckOutChange = (date: any) => {
    this.setState({
      checkOut: date
    })
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

  _createResultUrl = () => {
    var url = '/hotels/' + this.state.country + '/' + this.state.city
    url += '?cin=' + this.state.checkIn.format('YYYY-MM-DD')
    url += '&cout=' + this.state.checkOut.format('YYYY-MM-DD')

    let aRoom = []
    for (var x = 0; x < this.state.rooms; x++) {
        let room = this.state.occupancies[x].adult
        if (this.state.occupancies[x].child > 0) {
            room += ',' + this.state.occupancies[x].childAges.slice(0, this.state.occupancies[x].child).join(',')
        }
        //console.log(this.state.occupancies[x])
        aRoom.push(room)
    }

    url += '&rooms=' + aRoom.join('|')
    
    return url
  }

  _onSearchClick = () => {
    this.setState({ searchClicked: true })
    //console.log(this._createResultUrl())
    this.props.history.push(this._createResultUrl())
  }

  _selectCity = (option: any) => {
      
      this.setState({
          city: option[0].code,
          country: option[0].countryCode
      })
  }

  _handleCitySearch = (query: any) => {
      this.setState({ isLoading: true })

      this._CityAutocomplete(query).then((res: any) => {
          
          this.setState({
              isLoading: false,
              cityOptions: res
          })
      })
  }

  public render() {
      var idx = 2
      console.log(this.state)
    return <Col md={12}>

      <Row>
        <Col md={12}>
          <h3>Search your accomodation</h3>
        </Col>
      </Row>
      
      <Row>
        <Col md={6}>
            <FormInput
                label="Destination"
                disabled={false}
                error=""
            >

                 <AsyncTypeahead
                        {...this.state}
                        options={this.state.cityOptions}
                        labelKey="name"
                        minLength={2}
                        onSearch={this._handleCitySearch}
                        placeholder="Search for Destination"
                        onChange={this._selectCity}
                   
                />
            </FormInput>
        </Col>
        <Col md={3}>
          <SelectDate
            key="checkIn"
                    label="Check In Date"
                    onChange={this._onCheckInChange}
            selected={this.state.checkIn}
            error=""
            disabled={false}
          />
        </Col>
        <Col md={3}>
          <SelectDate
            key="checkIn"
                    label="Check Out Date"
                    onChange={this._onCheckOutChange}
            selected={this.state.checkOut}
            error=""
            disabled={false}
          />
        </Col>
      </Row>
      <Row>
        <Col md={3}>
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
            <Col md={3}></Col>
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
