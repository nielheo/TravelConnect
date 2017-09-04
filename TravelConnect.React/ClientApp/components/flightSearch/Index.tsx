import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';

import AirportAutocomplete from './AirportAutocomplete'

export default class FlightSearch extends React.Component<RouteComponentProps<{}>, any> {
    constructor() {
        super();
        this.state = {
            isReturn: true,
            origin: {},
            destination: {}
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
                            <input type="text" className='form-control' />
                        </td>
                        <td cellPadding='5'>
                            <label>Return Date</label>
                            <input type="text" className='form-control' disabled={!this.state.isReturn} />
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
