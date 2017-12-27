import * as React from 'react'
import { NavLink, Link } from 'react-router-dom';

import { Panel, Grid, Row, Col, Pagination } from 'react-bootstrap'

export default class HotelItem extends React.Component<{ hotel: any, url: any }, any> {
  constructor(props: any) {
    super(props)
  }

  _rawMarkup = (content: any) => {
    return { __html: content };
  }

  public render() {
    let { hotel } = this.props

    return <Panel>
      <Row>
        <Col md={12}><h4>{hotel.name}</h4></Col>
      </Row>
      <Row>
        <Col md={12}><span dangerouslySetInnerHTML={this._rawMarkup(hotel.shortDesc)} /></Col>
      </Row>
      <Row>
        <Col md={12}>{hotel.currCode} {hotel.rateFrom} - {hotel.currCode} {hotel.rateTo}</Col>
      </Row>
      <Row>
            <Col md={12}>
                <a href={this.props.url} target='_blank' >
          <input
            className='btn'
            type="button" value="Select"
            />
          </a>
        </Col>
      </Row>
    </Panel>
  }

}