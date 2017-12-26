import * as React from 'react'
import { Row, Col } from 'react-bootstrap'

import FormInput from '../commons/FormInput'
import FormTextbox from '../commons/FormTextbox'
import FormDropdown from '../commons/FormDropdown'


export default class BookHotel_Info extends React.Component<
  { recheckedRoomPrice: any, index: number }, any> {
  constructor(props: any) {
    super(props);
  }

  _occupancylabel = (occupancy: any) => {
    let label = ''
    if (occupancy.adultCount) 
      label += occupancy.adultCount + ' adult' + (occupancy.adultCount > 1 && 's')

    if (occupancy.childAges && occupancy.childAges.length)
      label += (label && ' and ') + occupancy.childAges.length + ' child' + ((occupancy.childAges.length > 1) ? 'ren' : '')

    return label
  }

  _smokingOptions = (pref: string[]) => {
    let options = []
    if (pref.indexOf('E') >= 0)
      options.push(<option value='E'>- No Preference -</option>)
    if (pref.indexOf('NS') >= 0)
      options.push(<option value='NS'>Non-smoking Room</option>)
    if (pref.indexOf('S') >= 0)
      options.push(<option value='S'>Smoking Room</option>)

    return options
  }
  
  public render() {
    let roomPrice = this.props.recheckedRoomPrice.rooms[0]
    let room = roomPrice.roomGroups[this.props.index]
    return <section>
      <h4>{roomPrice.rateDesc}</h4>
      <h5>{this._occupancylabel(this.props.recheckedRoomPrice.occupancies[this.props.index])}</h5>

      <Row>
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
        <Col md={3}>
          <FormDropdown
            label='Bed Type'
            value=''
            disabled={false}
            error=''
            onChange={null}
          >
            {roomPrice.bedTypes.map((bed: any) => <option value={bed.id}>{bed.name}</option>)}

          </FormDropdown>
        </Col>
        <Col md={3}>
          <FormDropdown
            label='Smoking Preference'
            value=''
            disabled={false}
            error=''
            onChange={null}
          >
            {
              this._smokingOptions(roomPrice.smokingPreferences).map((option: any) => option)
            }
          </FormDropdown>
        </Col>
      </Row>
      <hr/>
    </section>
  }
}