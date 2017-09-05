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
            origin: {},
            destination: {},
            departure: moment().add(2, 'days'),
            return: moment().add(5, 'days'),
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

    _handleOnClick = () => {
        console.log("----")
        console.log(this.state)
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
        return <div>
            <h1>Search your flight</h1>
            <table className="form-horizontal">
                <tbody>
                    <tr>
                        <td cellPadding="5">
                            <input type="radio" onChange={this.toggleIsReturn} checked={!this.state.isReturn} /> One Way&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="radio" onChange={this.toggleIsReturn} checked={this.state.isReturn} /> Round Trip
                            { this.state.isReturn }
                        </td>
                    </tr>
                    <tr>
                        <td cellPadding='5'>
                            <AirportAutocomplete
                                onChange={this._handleOriginChanged}
                                label="Origin Airport"
                                
                            />
                        </td>
                        <td cellPadding='5'>
                            <AirportAutocomplete
                                onChange={this._handleDestinationChanged}
                                label="Destination Airport"
                            />
                        </td>
                    </tr>
                    <tr>
                        <td cellPadding='5'>
                            <label>Departure Date</label>
                            <DatePicker
                                className="form-control"
                                onChange={this._handleDepartureChange}
                                selected={this.state.departure}
                            />
                        </td>
                        <td cellPadding='5'>
                            <form>
                            <div className="control-group warning">
                                <label>Return Date</label>
                                <DatePicker
                                    className="form-control"
                                    onChange={this._handleReturnChange}
                                    selected={this.state.return}
                                />
                            </div>
                            <div className="control-group warning">
                                <label className="control-label" htmlFor="inputWarning">Input with warning</label>
                                    <div className="controls">
                                    <input type="text" id="inputWarning" />
                                            <span className="help-inline">Something may have gone wrong</span>
                                </div>
                                </div>
                                </form>
                        </td>
                    </tr>
                    <tr>
                        <td cellPadding={5}>
                            <button onClick={this._handleOnClick}>Search</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    }
}
