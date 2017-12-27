import * as React from 'react'

import { Panel, Grid, Row, Col } from 'react-bootstrap'
import * as moment from 'moment'

import FormInput from '../commons/FormInput'
import FormTextbox from '../commons/FormTextbox'
import FormDropdown from '../commons/FormDropdown'
import SelectDate from '../commons/SelectDate'

export default class Occupancy extends React.Component<{
  index: number,
  adult: number,
  child: number,
  childAges: any,
  onAdultChange: any,
  onChildChange: any,
  onChildAgeChange: any,
}, any> {
  public render() {
    return <section>
      <Col md={2}>
        <FormInput
          label=' '
          error='' >
          <p>{'Room ' + this.props.index}</p>
        </FormInput>
      </Col>
      <Col md={2}>
        <FormInput label='Adult' error=''>
          <select className="form-control" value={this.props.adult}
            onChange={(e) => this.props.onAdultChange(e, this.props.index)}>
            <option value='1'>1</option>
            <option value='2'>2</option>
            <option value='3'>3</option>
          </select>
        </FormInput>
      </Col>
      <Col md={2}>
        <FormInput label='Child' error=''>
          <select className="form-control" value={this.props.child}
            onChange={(e) => this.props.onChildChange(e, this.props.index)}>
            <option value='0'>0</option>
            <option value='1'>1</option>
            <option value='2'>2</option>
          </select>
        </FormInput>
        
      </Col>
      {
        (this.props.child >= 1) &&
        
          <Col md={1} mdPush={10}>
            <FormInput label='Age' error=''>
            <select className="form-control" value={this.props.childAges[0]}
              onChange={(e) => this.props.onChildAgeChange(e, this.props.index, 0)} >
                <option value='0'>0</option>
                <option value='1'>1</option>
                <option value='2'>2</option>
                <option value='3'>3</option>
                <option value='4'>4</option>
                <option value='5'>5</option>
                <option value='6'>6</option>
                <option value='7'>7</option>
                <option value='8'>8</option>
                <option value='9'>9</option>
                <option value='10'>10</option>
                <option value='11'>11</option>
                <option value='12'>12</option>
              </select>
            </FormInput>
          </Col>
        }
        {
        (this.props.child >= 2) &&
          <Col md={1} mdPush={10}>
            <FormInput label='Age' error=''>
            <select className="form-control" value={this.props.childAges[1]}
                onChange={(e) => this.props.onChildAgeChange(e, this.props.index, 1)}>
              <option value='0'>0</option>
              <option value='1'>1</option>
              <option value='2'>2</option>
              <option value='3'>3</option>
              <option value='4'>4</option>
              <option value='5'>5</option>
              <option value='6'>6</option>
              <option value='7'>7</option>
              <option value='8'>8</option>
              <option value='9'>9</option>
              <option value='10'>10</option>
              <option value='11'>11</option>
              <option value='12'>12</option>
              </select>
            </FormInput>
          </Col>
      }
    </section>
  }
}
