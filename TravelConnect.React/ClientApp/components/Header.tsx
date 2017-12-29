import * as React from 'react';

import { NavLink, Link } from 'react-router-dom';

export default class Home extends React.Component<any, any> {
    constructor() {
        super();
    
    }
    
    //console.log(Commons._GetAirport('BKK'))
    public render() {

        return <div>
            <h3><NavLink to='/'>Travel Connect</NavLink></h3>
            <hr/>
        </div>
    }
}