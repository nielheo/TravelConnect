import * as React from 'react'

import { Panel, Row, Col, Checkbox } from 'react-bootstrap'

export default class HotelFilter extends React.Component<{
  hotels: any,
  filteredHotels: any,
  filteredHotelName: string,
  onFilterHotelNameChange: any,
  filteredStarRating: any,
  onFilterStarRatingChange: any
}, any> {
  constructor(props: any) {
    super(props)
  }

  _compareRating = (a: any, b: any) => {
    if (a.starRating < b.starRating)
      return 1

    if (a.starRating > b.starRating)
      return -1

    return 0
  }

  public render() {
    let ratings:any = []

    if (this.props.hotels)
      this.props.hotels.map((htl: any) => {
        let rat = ratings.filter((rat: any) => rat.starRating === htl.starRating)
        if (!rat.length)
          ratings.push({ starRating: htl.starRating, count: 1 })
        else 
          rat[0].count++
      })

    ratings.sort(this._compareRating)
    console.log(this.props.filteredStarRating)
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

              { ratings && ratings.map((rat: any) =>
                <Row>
                  <Col md={12}>
                    <Checkbox inline
                      onChange={(e) => this.props.onFilterStarRatingChange(e, rat.starRating)}
                      checked={this.props.filteredStarRating.indexOf(rat.starRating) >= 0}
                    >
                      {rat.starRating + ' stars (' + rat.count + ' hotels)'}
                    </Checkbox>
                  </Col>
                </Row>
                )
              }
            </div>
          </Col>
        </Row>
      </Panel>
    </section>
  }
}