import * as React from 'react';

const typeahead = require('react-bootstrap-typeahead') as any;
const AsyncTypeahead = typeahead.asyncContainer(typeahead.Typeahead) as any;

export default class AirportAutocomplete extends React.Component<{ onChange: any, label: string, error: string }, any> {
  constructor(props: any) {
    super(props);
    this.state = {
    };
  }

  public render() {
    return <div className={'form-group ' + (this.props.error ? 'has-error' : '')}>
      <label className="control-label">{this.props.label}</label>
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
      {
        this.props.error &&
        <span className='control-label'>{this.props.error}</span>
      }
    </div>
  }
}