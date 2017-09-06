import * as React from 'react'
import { RouteComponentProps } from 'react-router-dom'

const CryptoJS = require('crypto-js') as any;

export default class FlightSearch extends React.Component<RouteComponentProps<{ route: string }>, any> {
    constructor(props: any) {
        super(props);

        let route = props.match.params.route
        
        var bytes = CryptoJS.AES.decrypt(decodeURIComponent(route), 'DheoTech');
        var data = JSON.parse(bytes.toString(CryptoJS.enc.Utf8));
        
        this.state = {
            origin: data.origin,
            destination: data.destination,
            depart: data.depart,
            return: data.return,
        };
    } 
    
    public render() {
        //console.log(this.state)
        return <div className="col-md-12">
            
            <div className="row">
                <div className="col-md-12">
                    <h1>Select your flight</h1>
                </div>
            </div>
            
        </div>
    }
}
