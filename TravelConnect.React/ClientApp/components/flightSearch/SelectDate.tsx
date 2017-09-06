import * as React from 'react'

import DatePicker from 'react-datepicker'
import * as moment from 'moment'

export default class SelectDate extends React.Component<{ onChange: any, selected: any, label: string, error: string, disabled: boolean }, any> {
    public render() {
        return <div className={'form-group ' + (this.props.error ? 'has-error' : '')}>
            <label className="control-label">{this.props.label}</label>
            <DatePicker
                className="form-control"
                onChange={this.props.onChange}
                selected={this.props.selected}
                disabled={this.props.disabled}
                dateFormat="D MMM YYYY"
            />
            {
                this.props.error &&
                <span className='control-label'>{this.props.error}</span>
            }
        </div>
    }
}