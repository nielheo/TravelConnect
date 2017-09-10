import * as React from 'react'

import * as moment from 'moment'

import FlightDetails from './FlightDetails'

export default class FlightDeparture extends React.Component<{ depart: any }, any> {
  _dateOnly = (dateTime: any) => {
    return moment({
      year: dateTime.year(),
      month: dateTime.month(),
      day: dateTime.date()
    });
  }

  _dateDiff = (dateStart: any, dateEnd: any) => {
    return this._dateOnly(dateEnd).diff(this._dateOnly(dateStart), 'days')
  }

  _minuteDiff = (dateStart: any, dateEnd: any) => {
    return dateEnd.diff(dateStart, 'minutes')
  }

  _durationFormat = (date: any) => {
    return Math.floor(date / 60) + 'h ' +
      ((date % 60) ? date % 60 + 'm' : '')
  }

  _getStopOvers = (leg: any) => {
    if (leg.segments.length === 1)
      return null

    let stopOvers: any[] = []
    for (var i: number = 0; i < leg.segments.length - 1; i++) {
      stopOvers.push({
        code: leg.segments[i].destination,
        stopTime: this._minuteDiff(moment(leg.segments[i].arrival.time),
          moment(leg.segments[i + 1].departure.time)),
        date1: leg.segments[i].arrival.time,
        date2: leg.segments[i + 1].departure.time
      })
    }

    return stopOvers
  }


  public render() {
    const { depart } = this.props
    const firstLeg = depart.legs[0]
    const firstSegment = firstLeg.segments[0]
    const lastSegment = firstLeg.segments[firstLeg.segments.length - 1]
    const dateDiff = this._dateDiff(moment(firstSegment.departure.time),
      moment(lastSegment.arrival.time))
    const stopOvers: any = this._getStopOvers(firstLeg)
    return <div className='row col-md-12 alert alert-primary card'>
      <div className='col-md-9'>
        <div className='col-md-12 clearfix'>
          <div className='col-md-4'>
            <h4>
              <b>
              {
                moment(firstSegment.departure.time).format('HH:mm')
                + '-' + moment(lastSegment.arrival.time).format('HH:mm')
              }
              </b>
              {
                (dateDiff ? ' (+' + dateDiff + ')' : '')
              }
            </h4>
          </div>
          <div className='col-md-4'>
            <h4>
              <b>
                {
                  this._durationFormat(firstLeg.elapsed)
                }
              </b>
            </h4>
              {firstSegment.origin + '-' +
                firstLeg.segments.map((s:any) => s.destination).join('-')
              }
            
          </div>
          <div className='col-md-4'>
            <h4>
              {
                firstLeg.segments.length === 1 ? 'Direct '
                  : (firstLeg.segments.length - 1) + ' Stop'
                  + (firstLeg.segments.length > 2 ? 's' : '')
              }
            </h4>
            {
              stopOvers && stopOvers.map((s: any) => <p>{s.code + ' ' + this._durationFormat(s.stopTime) }</p>)
            }
          </div>
        </div>
        <div className='col-md-12 clearfix'>
          <FlightDetails segments={firstLeg.segments} />
        </div>
      </div>
      <div className='col-md-3'>
        <h3>{depart.curr} {depart.totalPrice.toFixed(2)}</h3>
        <button className='form-control'>Select</button>
      </div>

    </div>
  }
}