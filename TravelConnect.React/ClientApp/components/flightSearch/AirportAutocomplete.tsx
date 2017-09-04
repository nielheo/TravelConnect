import * as React from 'react';

const typeahead = require('react-bootstrap-typeahead') as any;
const AsyncTypeahead = typeahead.asyncContainer(typeahead.Typeahead) as any;

export default class AirportAutocomplete extends React.Component<{onChange: any, label: any}, any> {
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


    public render() {
        console.log(this.state)
        return <div>
            <label>{this.props.label}</label>
            <AsyncTypeahead
                onChange={this.props.onChange}
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
        </div>
    }
}