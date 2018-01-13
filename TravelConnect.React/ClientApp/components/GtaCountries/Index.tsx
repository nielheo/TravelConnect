import * as React from 'react'

import { Panel, Grid, Row, Col, Pagination, Button, Table } from 'react-bootstrap'

import { RouteComponentProps } from 'react-router-dom';

import Header from '../Header'
import Country from './Country'

export default class GtaCountries_Index extends React.Component<
    RouteComponentProps<{}>, any> {
    constructor(props: any) {
        super(props);
        
        this.state = {
            countries: null
        };
    }
    
    _sendRequest = () => {
        return fetch('/api/gtageo/countries', {
            method: 'get',
            headers: {
                'Content-Type': 'application/json',
                'Accept-Encoding': 'gzip',
            }
        }).then(res => {
            if (res) return res.json()
        }).catch(err => { })
    }
    
    componentDidMount() {
        this._sendRequest()
            .then(r => {
                this.setState({ countries: r })
            })
    }

    _compareCountry = (a: any, b: any) => {
        if (a.name < b.name)
            return -1
        if (a.name > b.name)
            return 1

        if (a.code < b.code)
            return -1
        if (a.code > b.code)
            return 1
        
        return 0
    }
    
    public render() {
        return <section>
            <Header />
            <h1>Countries</h1>
            {
                this.state.countries
                    ? <Table>
                        <thead>
                            <tr>
                                <th>Code</th>
                                <th>Country Name</th>
                                <th>Total City</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                this.state.countries.sort(this._compareCountry)
                                    .map((c: any) => <Country country={c} />)
                            }
                        </tbody>
                    </Table>
                    : <label>Loading....</label>
            }
            </section>
        
    }
}