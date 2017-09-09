import * as React from 'react'

export default class SelectPax extends React.Component<{ onChange: any, selected: string, label: string, error: string, disabled: boolean }, any> {
  _generateOptions = () => {
    var options = []
    for (var i: number = 0; i <= 9; i++) {
      options.push(<option value={i} key={i}>{i}</option>)
    }
    return options
  }

  public render() {
    return <div className={'form-group ' + (this.props.error ? 'has-error' : '')}>
      <label className="control-label">{this.props.label}</label>
      <select
        className="form-control"
        onChange={this.props.onChange}
        disabled={this.props.disabled}
        defaultValue={this.props.selected}
      >
        {
          this._generateOptions().map(o => { return o })
        }
      </select>
      {
        this.props.error &&
        <span className='control-label'>{this.props.error}</span>
      }
    </div>
  }
}