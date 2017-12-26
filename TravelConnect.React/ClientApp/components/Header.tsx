import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as Commons from './Commons'
import { NavLink, Link } from 'react-router-dom';

import { PageHeader } from 'react-bootstrap'

import HotelSearch from './hotelSearch'

export default class Home extends React.Component<any, any> {
    constructor() {
        super();
    
    }
    
    //console.log(Commons._GetAirport('BKK'))
    public render() {

        return <div>
            <PageHeader><NavLink to='/'>Travel Connect</NavLink><br />
            <small>Now everybody can go Online</small></PageHeader>
        </div>
    }
}