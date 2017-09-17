import * as React from 'react'
import * as moment from 'moment'

import FlightDeparture from './FlightDeparture'

import { Panel, Grid, Row, Col, Pagination } from 'react-bootstrap'

import * as Commons from '../Commons'

export default class FlightReturnList extends React.Component<{
  itins: any[], onSelect: any
}, any> {
  constructor(props: any) {
    super(props)
    this.state = {
      page: 1
    }
  }

  _onPageChange = (a: any) => {
    if (a !== this.state.page) {
      this.setState({
        page: a
      })
    }
  }


  public render() {
    const { itins, onSelect } = this.props
    const itemsPerPage = 20
    const { page } = this.state
    let _totalPages = itins.length ? Math.ceil(itins.length / itemsPerPage) : 0
    let _page = page > _totalPages ? _totalPages : page
    let _startIndex = (_page - 1) * itemsPerPage
    let _endIndex = _page * itemsPerPage

    console.log(itins)

    return <section>
      <h2>Select your return flight</h2>
      <h4>Select from {Commons.FormatNum(itins.length)} Return{itins.length ? 's' : ''} Flight</h4>
      <Row className="text-right">
        <Col md={12}>
          <Pagination prev next first last ellipsis boundaryLinks
            items={_totalPages} maxButtons={5} activePage={_page}
            onSelect={this._onPageChange} />
        </Col>
      </Row>
      {
        itins.slice(_startIndex, _endIndex).map((r: any) =>
          <FlightDeparture itin={r} leg={r.legs[1]} key={r.legs[1].routes} onSelectDepart={onSelect} />)
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