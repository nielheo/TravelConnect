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
  }

  _onContinueClick = () => {
    this.props.history.push('/thankyou')
  }
  
  public render() {
    let idx=0
    return <section>
      <h4>Enter Your Details</h4>
      <Row>
        <Col md={1}>
          <FormDropdown
            label='Title'
            value=''
            disabled={false}
            error=''
            onChange={null}
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
            value=''
            disabled={false}
            error=''
            onChange={null}
            
          />
        </Col>
        <Col md={3}>
          <FormTextbox
            label='Last Name'
            value=''
            disabled={false}
            error=''
            onChange={null}
          />
        </Col>
      </Row>
      <Row>
        <Col md={5}>
          <FormTextbox
            label='Email Address'
            value=''
            disabled={false}
            error=''
            onChange={null}

          />
        </Col>
      </Row>
      <Row>
        <Col md={5}>
          <FormTextbox
            label='Confirm Email Address'
            value=''
            disabled={false}
            error=''
            onChange={null}
          />
        </Col>
      </Row>
      <hr/>
      {
        
        this.props.recheckedRoomPrice.rooms[0].roomGroups.map((r: any) =>
          <RoomGuest recheckedRoomPrice={this.props.recheckedRoomPrice} index={idx++} />)
      }
      <Row>
        <Col md={12}><Button bsStyle="primary" onClick={this._onContinueClick}>Continue</Button></Col>
      </Row>
    </section>
  }
}