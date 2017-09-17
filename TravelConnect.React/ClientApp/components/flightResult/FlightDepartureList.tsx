import * as React from 'react'
import * as moment from 'moment'

import FlightDeparture from './FlightDeparture'

import { Panel, Grid, Row, Col, Pagination, ButtonToolbar, ToggleButtonGroup, ToggleButton } from 'react-bootstrap'



import * as Commons from '../Commons'

export default class FlightDepartureList extends React.Component<{
  departures: any[], onSelectDeparture: any
}, any> {
  constructor(props: any) {
    super(props)
    this.state = {
      page: 1,
      sortType: 1
    }
  }

  _onPageChange = (a: any) => {
    if (a !== this.state.page) {
      this.setState({
        page: a
      })
    }
  }

  _onSortChange = (a: any) => {
    if (a !== this.state.sortType) {
      this.setState({ sortType: a })
    }
  }

  _compareLeg = (a: any, b: any, leg: number) => {
    if (this.state.sortType === 1) {
      if (a.totalFare.amount < b.totalFare.amount)
        return -1;
      if (a.totalFare.amount > b.totalFare.amount)
        return 1;
    }

    let aMoment = moment(a.legs[leg].segments[0].departure.time)
    let bMoment = moment(b.legs[leg].segments[0].departure.time)

    if (aMoment.isBefore(bMoment))
      return -1

    if (aMoment.isAfter(bMoment))
      return 1

    let aArrival = moment(a.legs[leg].segments[a.legs[leg].segments.length - 1].arrival.time)
    let bArrival = moment(b.legs[leg].segments[b.legs[leg].segments.length - 1].arrival.time)

    if (aArrival.isBefore(bArrival))
      return -1

    if (aArrival.isAfter(bArrival))
      return 1

    if (this.state.sortType === 2) {
      if (a.totalFare.amount < b.totalFare.amount)
        return -1;
      if (a.totalFare.amount > b.totalFare.amount)
        return 1;
    }

    return 0;
  }

  _compareDepart = (a: any, b: any) => {
    return this._compareLeg(a, b, 0)
  }

  public render() {
    const { departures, onSelectDeparture } = this.props
    const itemsPerPage = 20
    const { page } = this.state
    let _totalPages = departures.length ? Math.ceil(departures.length / itemsPerPage) : 0
    let _page = page > _totalPages ? _totalPages : page
    let _startIndex = (_page - 1) * itemsPerPage
    let _endIndex = _page * itemsPerPage
    return <section>
      <h2>Select your flight</h2>
      <h4>Select from {Commons.FormatNum(departures.length)} Departure{departures.length ? 's' : ''} Flight</h4>
      <Row className="text-right">
        <Col md={12}>
          <ButtonToolbar>
            <ToggleButtonGroup type="radio" name='sortType' defaultValue={1}>
              <ToggleButton value={1} onClick={() => this._onSortChange(1)}>Price</ToggleButton>
              <ToggleButton value={2} onClick={() => this._onSortChange(2)}>Departure</ToggleButton>

            </ToggleButtonGroup>
            <Pagination prev next first last ellipsis boundaryLinks
              items={_totalPages} maxButtons={5} activePage={_page}
              onSelect={this._onPageChange} />
          </ButtonToolbar>
          
        </Col>
      </Row>
      {
        departures.sort(this._compareDepart).slice(_startIndex, _endIndex).map((r: any) =>
          <FlightDeparture itin={r} leg={r.legs[0]} key={r.routes} onSelectDepart={onSelectDeparture} />)
      }
      <Row className="text-right">
        <Col md={12}>
          <Pagination prev next first last ellipsis boundaryLinks
            items={_totalPages} maxButtons={5} activePage={_page}
            onSelect={this._onPageChange} />
        </Col>
      </Row>
    </section>
  }
}