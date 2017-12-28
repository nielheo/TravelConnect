import * as React from 'react'

import { Panel, Grid, Row, Col, Pagination, PageHeader, ButtonToolbar, Button } from 'react-bootstrap'

import RoomAmenitiesList from './RoomAmenitiesList'

export default class HotelResult_Index extends React.Component<{ room: any, onSelect: any }, any> {
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
                    
                    {room.bedTypes &&
                      <ButtonToolbar>
                      {room.bedTypes.map((bt: any) => <Button bsStyle='default' >{bt.name}</Button>)}
                      </ButtonToolbar>
                    }
                  </Col>
                  <Col md={3}><h4>{room.chargeableRate.currency} {room.chargeableRate.total.toLocaleString('en-US')}
                    <br/><small>All inclusive</small>
                  </h4>
                    
                    {
                      room.isNonRefundable
                        ? <h5>
                          {room.promoDesc && <section>
                            <div className='label label-danger label-outlined'>{room.promoDesc}</div><br />
                            </section>
                          }
                          <div className='label label-warning label-outlined'>Non Refundable</div>
                          </h5>
                        : <h5>
                          {room.promoDesc && <section>
                            <div className='label label-danger label-outlined'>{room.promoDesc}</div><br />
                          </section>
                          }
                          <div className='label label-success label-outlined'>Refundable</div>
                          </h5>
                    }
                    <Row>
                      <Col md={12}><br />
                        <Button
                          className='btn btn-primary'
                          onClick={() => this.props.onSelect(room)}
                        >Book Now</Button>
                        <br /><span>{room.isPrepaid ? '' : 'Pay at Hotel'}</span>
                      </Col>
                    </Row>
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