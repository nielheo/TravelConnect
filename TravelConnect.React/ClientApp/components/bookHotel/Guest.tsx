import * as React from 'react'
import { Row, Col, Button } from 'react-bootstrap'

import FormInput from '../commons/FormInput'
import FormTextbox from '../commons/FormTextbox'
import FormDropdown from '../commons/FormDropdown'
import RoomGuest from './RoomGuest'


export default class BookHotel_Info extends React.Component<
  { recheckedRoomPrice: any, history: any }, any> {
  constructor(props: any) {
    super(props);

    this.state = {
      clicked: false,
      title: '',
      firstName: '',
      lastName: '',
      email: '',
      confirmEmail: '',
      errors: []
    }
  }

  _onContinueClick = () => {
    let errors = []
    if (!this.state.title) errors.push('Title is required')
    if (!this.state.firstName) errors.push('First Name is required')
    if (!this.state.lastName) errors.push('Last Name is required')
    if (!this.state.email) errors.push('Email is required')
    if (!this.state.confirmEmail) errors.push('Confirm Email is required')
    if (this.state.confirmEmail !== this.state.email) errors.push('Confirm Email is not matched')

    if (errors.length === 0)
      this.props.history.push('/thankyou')

    this.setState({ errors: errors })
    this.setState({ clicked: true })
  }

  _onTitleChange = (e: any) => {
    this.setState({ title: e.target.value })
  }

  _onFirstNameChange = (e: any) => {
      this.setState({ firstName: e.target.value })
  }

  _onLastNameChange = (e: any) => {
      this.setState({ lastName: e.target.value })
  }

  _onEmailChange = (e: any) => {
      this.setState({ email: e.target.value })
  }

  _onConfirmEmailChange = (e: any) => {
      this.setState({ confirmEmail: e.target.value })
  }
  
  public render() {
    let idx=0
    return <section>
      <h4>Enter Your Details</h4>
      <Row>
        <Col md={1}>
          <FormDropdown
            label='Title'
            value={this.state.title}
            disabled={false}
            error={ (this.state.clicked && !this.state.title) ? '* Required' : ''}
            onChange={this._onTitleChange}
          >
            <option value=''></option>
            <option value='Mr.'>Mr.</option>
            <option value='Ms.'>Ms.</option>
            <option value='Mrs.'>Mrs.</option>
          </FormDropdown>
        </Col>
        <Col md={3}>
          <FormTextbox
            label='First Name'
            value={this.state.firstName}
            disabled={false}
            error={(this.state.clicked && !this.state.firstName) ? '* Required' : ''}
            onChange={this._onFirstNameChange}
            
          />
        </Col>
        <Col md={3}>
          <FormTextbox
            label='Last Name'
                    value={this.state.lastName}
            disabled={false}
            error={(this.state.clicked && !this.state.lastName) ? '* Required' : ''}
            onChange={this._onLastNameChange}
          />
        </Col>
      </Row>
      <Row>
        <Col md={5}>
          <FormTextbox
            label='Email Address'
                    value={this.state.email}
            disabled={false}
            error={(this.state.clicked && !this.state.email) ? '* Required' : ''}
            onChange={this._onEmailChange}

          />
        </Col>
      </Row>
      <Row>
        <Col md={5}>
          <FormTextbox
            label='Confirm Email Address'
                    value={this.state.confirmEmail}
            disabled={false}
            error={(this.state.clicked && !this.state.confirmEmail) ? '* Required'
                : (this.state.confirmEmail !== this.state.email ? '* not matched' : '')}
            onChange={this._onConfirmEmailChange}
          />
        </Col>
      </Row>
      <hr/>
      {
        
        this.props.recheckedRoomPrice.rooms[0].roomGroups.map((r: any) =>
          <RoomGuest recheckedRoomPrice={this.props.recheckedRoomPrice} index={idx++} />)
      }
      <Row>
            <Col md={12}>
                {
                    this.state.errors.length ? <ul className='danger'>
                        {this.state.errors.map((err: any) => <li>{err}</li>)}
                    </ul> : null
                }
                <Button bsStyle="primary" onClick={this._onContinueClick}>Continue</Button>
            </Col>
      </Row>
    </section>
  }
}