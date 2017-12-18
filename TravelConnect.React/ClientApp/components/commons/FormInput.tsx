import * as React from 'react'

export default class FormInput extends React.Component<{ label: string, error: string, disabled: boolean }, any> {
  public render() {
    //console.log(this.props.airlines)
    return <div className={'form-group ' + (this.props.error ? 'has-error' : '')}>
      <label className="control-label" disabled={this.props.disabled}>{this.props.label}</label>
      {this.props.children}
      {
        this.props.error &&
        <span className='control-label'>{this.props.error}</span>
      }
    </div>
  }
}