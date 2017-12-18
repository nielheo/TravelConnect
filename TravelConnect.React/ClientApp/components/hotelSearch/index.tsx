import * as React from 'react'

import { Panel, Grid, Row, Col } from 'react-bootstrap'
import * as moment from 'moment'

import FormInput from '../commons/FormInput'
import FormTextbox from '../commons/FormTextbox'
import FormDropdown from '../commons/FormDropdown'
import SelectDate from '../commons/SelectDate'

export default class HotelSearch_Index extends React.Component<{}, any> {
  constructor() {
    super();
    //let now = moment()
    //let today = moment({ year: now.year(), month: now.month(), day: now.day() })

    this.state = {
      checkIn: moment().add(90, 'days'),
      checkOut: moment().add(92, 'days'),
    };
  }

  _onCountryChange = (code: string) => {

  }

  public render() {
    return <Col md={12}>

      <Row>
        <Col md={12}>
          <h3>Search your accomodation</h3>
        </Col>
      </Row>
      
      <Row>
        <Col md={6}>
          <FormTextbox
            onChange={() => this._onCountryChange('ID')}
            label='Country'
            error=''
            disabled={false}
            value=''
          />
        </Col>
        <Col md={6}>
          <FormTextbox
            onChange={() => this._onCountryChange('ID')}
            label='City'
            error=''
            disabled={false}
            value=''
          />
        </Col>
      </Row>
      
      <Row>
        <Col md={6}>
          <SelectDate
            key="checkIn"
            label="Check In Date"
            onChange={this._onCountryChange}
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
            onChange={this._onCountryChange}
            //selected={this.state.checkOut}
            error=""
            value=''
            disabled={false}
          >
            <option value='1'>1 room</option>
            <option value='2'>2 rooms</option>
            <option value='3'>3 rooms</option>
            <option value='4'>4 rooms</option>
          </FormDropdown>
        </Col>
        <Col md={2}>
          <FormInput
            label=' '
            error=''
            disabled={false} >
            <p className='middle'>Room 1</p>
          </FormInput>
        </Col>
        <Col md={2}>
          <FormDropdown
            key="Adult"
            label="Adult"
            onChange={this._onCountryChange}
            //selected={this.state.checkOut}
            error=""
            value=''
            disabled={false}
          >
            <option value='1'>1</option>
          </FormDropdown>
        </Col>
        <Col md={2}>
          <FormDropdown
            key="Child"
            label="Child"
            onChange={this._onCountryChange}
            //selected={this.state.checkOut}
            error=""
            value=''
            disabled={false}
          >
            <option value='1'>1</option>
          </FormDropdown>
        </Col>
      </Row>
      <Row>
        <Col md={12}>
          <input
            className='btn'
            type="button" value="Search"
          />
        </Col>
      </Row>
    </Col>
  }
}
