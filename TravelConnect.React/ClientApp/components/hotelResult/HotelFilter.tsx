import * as React from 'react'

import { Panel, Grid, Row, Col, Pagination } from 'react-bootstrap'

export default class HotelFilter extends React.Component<{
  hotels: any,
  filteredHotelName: string,
  onFilterHotelNameChange: any
}, any> {
  constructor(props: any) {
    super(props)
  }

  public render() {
    return <section>
      <Row><Col md={12}><h1></h1></Col></Row>
      <Panel>
      
        <Row>
          <Col md={12}>
            <div className='form-group'>
              <label className='control-label'>Filter by Hotel Name</label>
              <input type='textbox'
                className="form-control"
                value={this.props.filteredHotelName}
                onChange={this.props.onFilterHotelNameChange}
              />
            </div>
          </Col>
        </Row>
        <Row>
          <Col md={12}>
            <div className='form-group'>
              <label className='control-label'>Filter by Star Rating</label>
              <input type='textbox'
                className="form-control"
                value=''
              />
            </div>
          </Col>
        </Row>
      </Panel>
    </section>
  }
}