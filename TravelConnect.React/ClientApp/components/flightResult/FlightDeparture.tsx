import * as React from 'react'
import * as moment from 'moment'

import { Panel, Grid, Row, Col } from 'react-bootstrap'

import * as Commons from '../Commons'

import FlightDetails from './FlightDetails'

export default class FlightDeparture extends React.Component<{
  itin: any,
  leg: any,
  onSelectDepart: any
}, any> {
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
    const { leg, itin } = this.props
    const firstSegment = leg.segments[0]
    const lastSegment = leg.segments[leg.segments.length - 1]
    const dateDiff = this._dateDiff(moment(firstSegment.departure.time),
      moment(lastSegment.arrival.time))
    const stopOvers: any = this._getStopOvers(leg)

    //console.log(itin)
    const airlines = Array.from(new Set(leg.segments.map((s: any) => s.marketingFlight.airline))).join(',')
    return <Panel>
      <Row>
        <Col md={1}>
          <img width={50} src={'https://images.trvl-media.com/media/content/expus/graphics/static_content/fusion/v0.1b/images/airlines/vector/s/' + airlines + '_sq.svg'} />
        </Col>
        <Col md={8}>
          <Row>
            <Col md={4}>
              <h4><b>{moment(firstSegment.departure.time).format('HH:mm')
                + '-' + moment(lastSegment.arrival.time).format('HH:mm')} </b>
                {(dateDiff ? <small>{' (+' + dateDiff + ')'}</small> : '')}
              </h4>
              {
                //Array.from(new Set(firstLeg.segments.map((s: any) => s.marketingFlight.airline))).join(',')
              }
            </Col>
            <Col md={4}>
              <h4><b>{this._durationFormat(leg.elapsed)}</b></h4>

              {firstSegment.origin + '-' +
                leg.segments.map((s: any) => s.destination).join('-')
              }
            </Col>
            <Col md={4}>
              <h4>
                {
                  leg.segments.length === 1 ? 'Direct '
                    : (leg.segments.length - 1) + ' Stop'
                    + (leg.segments.length > 2 ? 's' : '')
                }
              </h4>
              {
                stopOvers && stopOvers.map((s: any) => <p key={s.code}>{s.code + ' ' + this._durationFormat(s.stopTime)}</p>)
              }
            </Col>
          </Row>
          <FlightDetails segments={leg.segments} leg={leg} key={leg.routes} />
        </Col>
        <Col md={3}>
          <h3 className='text-center'>{itin.totalFare.curr} {Commons.FormatNum(itin.totalFare.amount.toFixed(2))}</h3>
          <div className='text-center'>Class: {itin.legs && leg.brds}</div>
          <button className='form-control' onClick={() => this.props.onSelectDepart(leg)}>Select</button>
        </Col>
      </Row>
    </Panel>
  }
}