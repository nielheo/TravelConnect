import * as React from 'react'
import { RouteComponentProps } from 'react-router-dom'
import * as moment from 'moment'
//import CryptoJS from 'crypto-js'
const CryptoJS = require('crypto-js') as any;

import AirportAutocomplete from './AirportAutocomplete'
import SelectDate from './SelectDate'
import SelectPax from './SelectPax'

export default class FlightSearch extends React.Component<RouteComponentProps<{}>, any> {
    constructor() {
        super();
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
        console.log(selectedItem)
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
        
        if (this._validateSearch()) {
            let request:any = {
                origin: this.state.origin.id,
                destination: this.state.destination.id,
                depart: this.state.departure.format('L'),
                return: this.state.isReturn ? this.state.return.format('L') : null,
                paxs: []
            }
            if (this.state.totalAdt) request.paxs.push({ code: 'ADT', qty: this.state.totalAdt })
            if (this.state.totalCnn) request.paxs.push({ code: 'CNN', qty: this.state.totalCnn })
            if (this.state.totalInf) request.paxs.push({ code: 'INF', qty: this.state.totalInf })
            console.log(request)
            let enc = CryptoJS.AES.encrypt(JSON.stringify(request), 'DheoTech').toString()
            /*
            console.log(enc)


            var bytes = CryptoJS.AES.decrypt(enc, 'DheoTech');
            var plaintext = bytes.toString(CryptoJS.enc.Utf8);
            console.log(plaintext)
            */
            this.props.history.push('/flight/result/' + encodeURIComponent(enc))
        }
    }

    _handleDepartureChange = (date: any) => {
        this.setState({
            departure:date
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
        console.log(e.target.value)
    }

    _handleInfantChange = (e: any) => {
        this.setState({
            totalInf: e.target.value
        })
        console.log(e.target.value)
    }

    public render() {
        //console.log(this.state)
        //console.log(this.state.clicked && !this.state.origin)
        
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
                        label="Departure Date"
                        onChange={this._handleDepartureChange}
                        selected={this.state.departure}
                        error=""
                        disabled={false}
                    />
                </div>
                <div className="col-md-6">
                    <SelectDate
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
                        onChange={this._handleAdultChange}
                        selected={this.state.totalAdt}
                        label='Adult'
                        disabled={false}
                        error=''
                    />
                </div>
                <div className="col-md-3">
                    <SelectPax
                        onChange={this._handleChildChange}
                        selected={this.state.totalCnn}
                        label='Child'
                        disabled={false}
                        error=''
                    />
                </div>
                <div className="col-md-3">
                    <SelectPax
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
