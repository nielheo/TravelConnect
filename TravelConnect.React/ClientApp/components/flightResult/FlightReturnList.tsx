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

    let returnItins: any[] = []

    itins.map((i: any) => {
      if (!returnItins.filter((r: any) => r.legs[1].routes === i.legs[1].routes
        && i.totalFare === r.totalFare).length)
        returnItins.push(i)
    })

    const itemsPerPage = 20
    const { page } = this.state
    let _totalPages = returnItins.length ? Math.ceil(returnItins.length / itemsPerPage) : 0
    let _page = page > _totalPages ? _totalPages : page
    let _startIndex = (_page - 1) * itemsPerPage
    let _endIndex = _page * itemsPerPage
    
    return <section>
      <h2>Select your return flight</h2>
      <h4>Select from {Commons.FormatNum(returnItins.length)} Return{returnItins.length ? 's' : ''} Flight</h4>
      <Row className="text-right">
        <Col md={12}>
          <Pagination prev next first last ellipsis boundaryLinks
            items={_totalPages} maxButtons={5} activePage={_page}
            onSelect={this._onPageChange} />
        </Col>
      </Row>
      {
        returnItins.slice(_startIndex, _endIndex).map((r: any) => {
          return <FlightDeparture itin={r} leg={r.legs[1]} key={r.legs[1].routes} onSelectDepart={onSelect} />
        })
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