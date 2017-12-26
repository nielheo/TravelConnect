import * as React from 'react'
import { RouteComponentProps } from 'react-router-dom'
import { connect } from 'react-redux'
import { Panel, Grid, Row, Col, Pagination, PageHeader, Button, ButtonGroup, ButtonToolbar } from 'react-bootstrap'
import * as moment from 'moment'

import { ApplicationState } from '../../store'
import * as HotelStore from '../../store/Hotel'

import Info from './Info'
import Guest from './Guest'
import Header from '../Header'

type HotelDetailProps =
  HotelStore.HotelState
  & typeof HotelStore.actionCreators
  & RouteComponentProps<{
    country: string
    city: string
    hotelId: number
  }>

class BookHotel_Index extends React.Component<HotelDetailProps, any> {
  constructor(props: any) {
    super(props);
    
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

  _sendRequest = (request: any) => {
    //console.log(request)
    return fetch('/api/hotels/' + this.props.selectedHotel.hotelId + '/recheck' + request, {
      method: 'get',
      headers: {
        'Content-Type': 'application/json',
        'Accept-Encoding': 'gzip',
      }
    }).then(res => {
      if (res) return res.json()
    }).catch(err => { })
  }
  //public async Task<HotelRoomRS> RecheckPrice(int id, DateTime checkIn, DateTime checkOut, 
//            string locale, string currency, string rooms, string rateCode, string roomTypeCode)

  _constructRequest = () => {
    let req = '?checkin=' + moment(this.props.selectedHotel.checkIn).format('YYYY-MM-DD')
      + '&checkout=' + moment(this.props.selectedHotel.checkOut).format('YYYY-MM-DD')
      + '&rooms=' + this.props.selectedHotel.rooms
      + '&locale=' + this.props.selectedHotel.locale
      + '&currency=' + this.props.selectedHotel.currency
      + '&rateCode=' + this.props.selectedRoom.rateCode
      + '&roomTypeCode=' + this.props.selectedRoom.roomCode
    return req
  }

  componentDidMount() {
    this._sendRequest(this._constructRequest()) 
      .then(r => {
        this.props.setRecheckedPrice(r)
      //  this.props.setSelectedRoom(r.rooms && r.rooms.length && r.rooms[0])
      })
  }
  
  
  public render() {
    let { selectedHotel, selectedRoom, recheckedPrice } = this.props

    //console.log(selectedHotel)
    //console.log(selectedRoom)
    //console.log(recheckedPrice)


    return <section>
        <Header />
      <Row>
        <Col md={12}>
          <PageHeader>Book Hotel: {selectedHotel.hotelDetail.name} <br />
            <small>{selectedHotel.hotelDetail.address}, {selectedHotel.hotelDetail.city}, {selectedHotel.hotelDetail.country}</small>
          </PageHeader>
        </Col>
      </Row>
      <Row>
        <Col md={3}><img src={this._GetFirstImageUrl(selectedRoom.roomImages)} /></Col>
        <Col md={6}>
          <h4>{selectedRoom.rateDesc}</h4>
          {selectedRoom.valueAdds &&
            <ul>
            {selectedRoom.valueAdds.map((va: any) => <li key={'va_' + selectedRoom.rateCode + '_' + va.id}>{va.description}</li>)}
            </ul>
          }

          {selectedRoom.bedTypes &&
            <ButtonToolbar>
            {selectedRoom.bedTypes.map((bt: any) => <Button bsStyle='default' >{bt.name}</Button>)}
            </ButtonToolbar>
          }
        </Col>
        <Col md={3}>
          {
            recheckedPrice
              ? <section><h3>
                {recheckedPrice.rooms[0].chargeableRate.currency} {recheckedPrice.rooms[0].chargeableRate.total.toLocaleString('en-US')}
              </h3>
                Enter your detail below to secure your room now!
                </section>
              : <span>Checking latest price and availability... </span>
          }
          </Col>
      </Row>
      <hr/>
      <Row>
        <Col md={12}>
          {
            selectedHotel.hotelDetail.checkInInstructions &&
              <Info title='Check In Instructions' info={selectedHotel.hotelDetail.checkInInstructions} />
          }
          {
            selectedHotel.hotelDetail.specialCheckInInstructions &&
            <Info title='Special Check In Instructions' info={selectedHotel.hotelDetail.specialCheckInInstructions} />
          }
          {
            selectedHotel.hotelDetail.propertyInformation &&
            <Info title='Property Information' info={selectedHotel.hotelDetail.propertyInformation} />
          }
          {
            selectedHotel.hotelDetail.hotelPolicy &&
            <Info title='Hotel Policy' info={selectedHotel.hotelDetail.hotelPolicy} />
          }
          {
            selectedHotel.hotelDetail.roomInformation &&
            <Info title='Room Information' info={selectedHotel.hotelDetail.roomInformation} />
          }
          
        </Col>
      </Row>
      {
        recheckedPrice &&
        <Guest recheckedRoomPrice={this.props.recheckedPrice} history={this.props.history} />
      }
      <hr/>
    </section>
  }
}



// Wire up the React component to the Redux store
export default connect(
  (state: ApplicationState) => state.hotel, // Selects which state properties are merged into the component's props
  HotelStore.actionCreators                 // Selects which action creators are merged into the component's props
)(BookHotel_Index) as typeof BookHotel_Index
