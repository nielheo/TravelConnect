import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';

export default class FlightSearch extends React.Component<RouteComponentProps<{}>, any> {
    constructor() {
        super();
        this.state = { isReturn: true };
    } 

    toggleIsReturn = () => {
        this.setState({
            isReturn: !this.state.isReturn
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
                            <input type="text" className='form-control' />
                        </td>
                        <td cellPadding='5'>
                            <label>Destination Airport</label>
                            <input type="text" className='form-control' />
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
