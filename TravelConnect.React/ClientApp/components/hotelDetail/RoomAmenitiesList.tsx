import * as React from 'react'

import { Panel, Grid, Row, Col, Button } from 'react-bootstrap'

export default class HotelResult_Index extends React.Component<{ amenities: any, rateCode: string }, any> {
  constructor(props: any) {
    super(props)

    this.state = {
      viewMore: false
    }
  }

  _viewMoreClick = () => {
    this.setState({ viewMore: true })
  }

  _viewLessClick = () => {
    this.setState({ viewMore: false })
  }

  public render() {
    let { amenities } = this.props
    let displayAmenities = this.state.viewMore ? amenities : amenities.slice(0, 5)
    let id = 0
    return <section>
      {amenities &&
        <section><br />
        Room Amenities: <br />
        <ul>
          {
            displayAmenities.map((ra: any) => {
              id++
              return <li key={'ra_' + this.props.rateCode + '_' + id}>{ra.name}</li>
              
            })
          }
          {
            amenities.length > 5 && !this.state.viewMore && <li>... <Button bsStyle="link" onClick={this._viewMoreClick}>view more</Button></li>
          }
          {
            amenities.length > 5 && this.state.viewMore && <li>... <Button bsStyle="link" onClick={this._viewLessClick}>view less</Button></li>
          }
        </ul>
      </section>


      }

    </section>
  }
}