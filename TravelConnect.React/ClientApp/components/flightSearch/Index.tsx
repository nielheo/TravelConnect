import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';

const typeahead = require('react-bootstrap-typeahead') as any;

//const Autocomplete = require("react-autocomplete") as any;
const AsyncTypeahead = typeahead.asyncContainer(typeahead.Typeahead) as any;

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
        this.setState({
            origin: selectedItem
        })
    }

    _handleDestinationChanged = (selectedItem: any) => {
        this.setState({
            destination: selectedItem
        })
    }

    public render() {
        console.log(this.state.isReturn)
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
                            <label>Origin Airport</label>
                            <AsyncTypeahead
                                onChange={this._handleOriginChanged}
                                onSearch={(query: any) => (
                                    fetch(`/api/airportautocomplete?query=${query}`)
                                        .then(resp => resp.json())
                                        .then(json => {
                                            this.setState({
                                                options: json.airportsRS.map((item: any) => {
                                                    return {
                                                        id: item.code,
                                                        label: item.fullName
                                                    }
                                                })
                                            })
                                        })
                                )}
                                delay={500}
                                options={this.state.options}
                            />
                        </td>
                        <td cellPadding='5'>
                            <label>Destination Airport</label>
                            <AsyncTypeahead
                                onChange={this._handleDestinationChanged}
                                onSearch={(query: any) => (
                                    fetch(`/api/airportautocomplete?query=${query}`)
                                        .then(resp => resp.json())
                                        .then(json => {
                                            this.setState({
                                                options: json.airportsRS.map((item: any) => {
                                                    return {
                                                        id: item.code,
                                                        label: item.fullName
                                                    }
                                                })
                                            })
                                        })
                                )}
                                delay={500}
                                options={this.state.options}
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
                </tbody>
            </table>
        </div>
    }
}
