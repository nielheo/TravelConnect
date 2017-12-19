import * as React from 'react'

import { Panel, Grid, Row, Col } from 'react-bootstrap'

import { RouteComponentProps } from 'react-router-dom';

import * as moment from 'moment'

import FormInput from '../commons/FormInput'
import FormTextbox from '../commons/FormTextbox'
import FormDropdown from '../commons/FormDropdown'
import SelectDate from '../commons/SelectDate'

export default class HotelResult_Index extends React.Component<RouteComponentProps<{}>, any> {
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
  
  public render() {
    var idx = 2
    console.log(this.state.occupancies)
    return <div>----</div>
  }
}
