import * as React from 'react'
import { RouteComponentProps } from 'react-router-dom'
import { connect } from 'react-redux'
import { ApplicationState } from '../../store'
import * as FlightStore from '../../store/Flight'

import * as moment from 'moment'
import * as queryString from 'query-string'
//import CryptoJS from 'crypto-js'
const CryptoJS = require('crypto-js') as any;

import AirportAutocomplete from './AirportAutocomplete'
import SelectDate from '../commons/SelectDate'
import SelectPax from './SelectPax'

type FlightProps =
  FlightStore.FlightState
  & typeof FlightStore.actionCreators
  & RouteComponentProps<{}>;

class FlightSearch_Index extends React.Component<FlightProps, any> {
  constructor(props: any) {
      super(props)
    //let now = moment()
    //let today = moment({ year: now.year(), month: now.month(), day: now.day() })

    this.state = {
      isReturn: true,
      origin: null,
      destination: null,
      departure: moment().add(2, 'days'),
      return: moment().add(5, 'days'),
      totalAdt: 1,
      totalCnn: 0,
      totalInf: 0,
      clicked: false,
      isDataValid: false,
    };
  }

  toggleIsReturn = () => {
    this.setState({
      isReturn: !this.state.isReturn
    })
  }

  _handleOriginChanged = (selectedItem: any) => {
    this.setState({
      origin: selectedItem[0]
    })
  }

  _handleDestinationChanged = (selectedItem: any) => {
    this.setState({
      destination: selectedItem[0]
    })
  }

  _validateSearch = () => {
    let valid: boolean = true
    if (!this.state.origin) valid = false
    if (!this.state.destination) valid = false
    if (this.state.isReturn && (this.state.return < this.state.departure)) valid = false

    if (this.state.isDataValid !== valid)
      this.setState({
        isDataValid: valid
      })
    return valid
  }

  _handleSearchClick = () => {
    this.setState({
      clicked: true,
    })
    console.log(this.state.departure.format('L'))
    if (this._validateSearch()) {
      let request: any = {
        origin: this.state.origin.id,
        destination: this.state.destination.id,
        depart: this.state.departure.format('YYYY-MM-DD'),
        pax: this.state.totalAdt + '-' + this.state.totalCnn + '-' + this.state.totalInf
      }
      if (this.state.isReturn)
        request.return = this.state.return.format('YYYY-MM-DD')
      //console.log(request)
      //let enc = CryptoJS.AES.encrypt(JSON.stringify(request), 'DheoTech').toString()
      /*
      console.log(enc)

      var bytes = CryptoJS.AES.decrypt(enc, 'DheoTech');
      var plaintext = bytes.toString(CryptoJS.enc.Utf8);
      console.log(plaintext)
      */
      let qs = queryString.stringify(request)
      this.props.setSearch(request)
      //console.log(qs)
      this.props.history.push('/flight/result/' + qs)//encodeURIComponent(enc))
    }
  }

  _handleDepartureChange = (date: any) => {
    this.setState({
      departure: date
    })
  }

  _handleReturnChange = (date: any) => {
    this.setState({
      return: date
    })
  }

  _handleAdultChange = (e: any) => {
    this.setState({
      totalAdt: e.target.value
    })
  }

  _handleChildChange = (e: any) => {
    this.setState({
      totalCnn: e.target.value
    })
  }

  _handleInfantChange = (e: any) => {
    this.setState({
      totalInf: e.target.value
    })
  }

  public render() {
    return <div className="col-md-12">

      <div className="row">
        <div className="col-md-12">
          <h1>Search your flight</h1>
        </div>
      </div>
      <div className="row">
        <div className="col-md-12">
          <input type="radio" onChange={this.toggleIsReturn} checked={!this.state.isReturn} /> One Way&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="radio" onChange={this.toggleIsReturn} checked={this.state.isReturn} /> Round Trip
                </div>
      </div>
      <div className="row">
        <div className="col-md-6">
          <AirportAutocomplete
            onChange={this._handleOriginChanged}
            label="Origin Airport"
            error={this.state.clicked && !this.state.origin ? 'Origin Airport is required' : ''}
          />
        </div>
        <div className="col-md-6">
          <AirportAutocomplete
            onChange={this._handleDestinationChanged}
            label="Destination Airport"
            error={this.state.clicked && !this.state.destination ? 'Destination Airport is required' : ''}
          />
        </div>
      </div>
      <div className="row">
        <div className="col-md-6">
          <SelectDate
            key="return"
            label="Departure Date"
            onChange={this._handleDepartureChange}
            selected={this.state.departure}
            error=""
            disabled={false}
          />
        </div>
        <div className="col-md-6">
          <SelectDate
            key="depart"
            label="Return Date"
            onChange={this._handleReturnChange}
            selected={this.state.isReturn ? this.state.return : null}
            error={this.state.isReturn && (this.state.return < this.state.departure) ? 'Invalid date' : ''}
            disabled={!this.state.isReturn}
          />
        </div>
      </div>
      <div className="row">
        <div className="col-md-3">
          <SelectPax
            key='adt'
            onChange={this._handleAdultChange}
            selected={this.state.totalAdt}
            label='Adult'
            disabled={false}
            error=''
          />
        </div>
        <div className="col-md-3">
          <SelectPax
            key='cnn'
            onChange={this._handleChildChange}
            selected={this.state.totalCnn}
            label='Child'
            disabled={false}
            error=''
          />
        </div>
        <div className="col-md-3">
          <SelectPax
            key='inf'
            onChange={this._handleInfantChange}
            selected={this.state.totalInf}
            label='Infant'
            disabled={false}
            error=''
          />
        </div>
      </div>
      <div className="row">
        <div className="col-md-12">
          <button onClick={this._handleSearchClick}>Search</button>
        </div>
      </div>
    </div>
  }
}

// Wire up the React component to the Redux store
export default connect(
  (state: ApplicationState) => state.flight, // Selects which state properties are merged into the component's props
  FlightStore.actionCreators                 // Selects which action creators are merged into the component's props
)(FlightSearch_Index) as typeof FlightSearch_Index