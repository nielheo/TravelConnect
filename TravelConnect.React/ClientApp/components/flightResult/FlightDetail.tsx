import * as React from 'react'

import * as moment from 'moment'

export default class FlightDetails extends React.Component<{ segment: any }, any> {
  constructor(props: any) {
    super(props)
    this.state = {
      showDetails: false,
    }
  }

  _durationFormat = (date: any) => {
    return Math.floor(date / 60) + 'h ' +
      ((date % 60) ? date % 60 + 'm' : '')
  }

  _handleShow = (show: any) => {
    if (show !== this.state.showDetails) {
      this.setState({
        showDetails: show
      })
    }
  }


  public render() {
    const { segment } = this.props
    return <div className='row col-md-12'>
      <div className='col-md-3'>
        <h4> {moment(segment.departure.time).format('HH:mm')}</h4>
      </div>
      <div className='col-md-3'>
        <h4> {moment(segment.arrival.time).format('HH:mm')}</h4>
      </div>
      <div className='col-md-3'>
        <h4> {this._durationFormat(segment.elapsed)}</h4>
      </div>
    </div>
  }
}