import * as React from 'react'

import { Panel, Grid, Row, Col, Pagination, PageHeader } from 'react-bootstrap'

import RoomAmenitiesList from './RoomAmenitiesList'

export default class HotelResult_Index extends React.Component<{ room: any }, any> {
  constructor(props: any) {
    super(props)
  }

  _GetFirstImageUrl = (imgs: any) => {
    if (imgs == null)
      return ''
    if (imgs.length === 0)
      return ''

    if (imgs.filter((img: any) => img.isHeroImage).length)
      return imgs.filter((img: any) => img.isHeroImage)[0].url

    return imgs[0].url
  }

  public render() {
    let firstRoom = this.props.room[0]
    console.log(this.props.room)
    //let rate = room.chargeableRate
    
    return <Panel>
      <Row>
        <Col md={3}>
          <img src={this._GetFirstImageUrl(firstRoom.roomImages)} />
          <RoomAmenitiesList amenities={firstRoom.roomAmenities} rateCode={firstRoom.rateCode} />
        </Col>
        <Col md={9}>
          {
            this.props.room.map((room: any) => 
              <Panel>
                <Row>
                  <Col md={9}>
                    <h4>{room.rateDesc}</h4>
                    {room.valueAdds &&
                      <ul>
                        {room.valueAdds.map((va: any) => <li key={'va_' + room.rateCode + '_' + va.id}>{va.description}</li>)}
                      </ul>
                    }
                  </Col>
                  <Col md={3}><h4>{room.chargeableRate.currency} {room.chargeableRate.total}</h4>
                    {room.promoDesc} 
                  </Col>
                </Row>
              </Panel>
              )
          }
          
        </Col>
        
      </Row>
    </Panel>
  }
}