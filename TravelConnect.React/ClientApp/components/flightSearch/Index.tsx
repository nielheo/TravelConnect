import * as React from 'react'
import { RouteComponentProps } from 'react-router-dom'
import * as moment from 'moment'

import AirportAutocomplete from './AirportAutocomplete'
import SelectDate from './SelectDate'

export default class FlightSearch extends React.Component<RouteComponentProps<{}>, any> {
    constructor() {
        super();
        this.state = {
            isReturn: true,
            origin: null,
            destination: null,
            departure: moment().add(2, 'days'),
            return: moment().add(5, 'days'),
            clicked: false,
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

    _handleSearchClick = () => {
        this.setState({
            clicked: true,
        })
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
                        error=""
                        disabled={!this.state.isReturn}
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
