import * as React from 'react'

import { Panel, Grid, Row, Col, Pagination, Button, Table } from 'react-bootstrap'

import { RouteComponentProps } from 'react-router-dom';


export default class GtaCountries_Country extends React.Component<
    {country:any}, any> {
    constructor(props: any) {
        super(props);

        this.state = {
            cities: null
        };
    }

    _sendRequest = (countryCode: string) => {
        return fetch(`/api/gtageo/countries/${countryCode}/cities`, {
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
        this._sendRequest(this.props.country.code)
            .then(r => {
                this.setState({ cities: r })
            })
    }

    public render() {
        let { country } = this.props
        return <tr>
            <td>{country.code}</td>
            <td>{country.name}</td>
            <td>
                {
                    this.state.cities
                        ? <label>{`${this.state.cities.length} cities`}</label>
                        : <span>-----</span>
                }

            </td>
        </tr>

    }
}