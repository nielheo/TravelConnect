import * as React from 'react'
import { RouteComponentProps } from 'react-router-dom'
import DatePicker from 'react-datepicker'
import * as moment from 'moment'

import AirportAutocomplete from './AirportAutocomplete'


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
        console.log(this.state)
        console.log(this.state.clicked && !this.state.origin)
        return <div>
            
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
                    <div className="form-group">
                        <label className="control-label">Departure Date</label>
                        <DatePicker
                            className="form-control"
                            onChange={this._handleDepartureChange}
                            selected={this.state.departure}
                        />
                    </div>
                </div>
                <div className="col-md-6">
                    <div className="form-group">
                        <label className="control-label">Return Date</label>
                        <DatePicker
                            className="form-control"
                            onChange={this._handleReturnChange}
                            selected={this.state.return}
                            disabled={!this.state.isReturn}
                        />
                    </div>
                </div>
            </div>
            <table className="form-horizontal">
                <tbody>
                    
                    <tr>
                        <td cellPadding='5'>
                            
                        </td>
                        <td cellPadding='5'>
                            

                        </td>
                    </tr>
                    <tr>
                        <td cellPadding={5}>
                            <button onClick={this._handleSearchClick}>Search</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    }
}
