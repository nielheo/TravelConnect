import * as React from 'react'
import { RouteComponentProps } from 'react-router-dom'
import { connect } from 'react-redux'
import { Panel, Grid, Row, Col, Pagination, PageHeader } from 'react-bootstrap'
import * as moment from 'moment'
import { Carousel } from 'react-responsive-carousel'
import * as queryString from 'query-string'

import { ApplicationState } from '../../store'
import * as HotelStore from '../../store/Hotel'
import Camelize from '../commons/Camelize'

import Helmet from 'react-helmet'

import Room from './Room'
import Header from '../Header'
type HotelDetailProps =
  HotelStore.HotelState
  & typeof HotelStore.actionCreators
  & RouteComponentProps<{
    country: string
    city: string
    hotelId: number }>

class HotelDetail_Index extends React.Component<HotelDetailProps, any> {
  constructor(props: any) {
    super(props);
    
    let query = queryString.parse(props.location.search)
    
    this.state = {
      country: props.match.params.country,
      city: props.match.params.city,
      checkIn: query.cin ? this._parseDate(query.cin) : moment().add(7, 'days'),
      checkOut: query.cout ? this._parseDate(query.cout) : moment().add(9, 'days'),
      hotelId: props.match.params.hotelId,
      rooms: query.rooms || '2',
      locale: props.match.params.locale || 'en_US',
      currency: query.currency || 'USD',
      result: null,
    };
  }

  _parseDate = (value: string) => {
    var parsed = moment(value, 'YYYY-MM-DD')
    if (parsed.isValid())
      return parsed
    else
      return null
  }
  
  _sendRequest = (request: any) => {
    //console.log(request)
    return fetch('http://localhost:6500/api/hotels' + request, {
      method: 'get',
      headers: {
        'Content-Type': 'application/json',
        'Accept-Encoding': 'gzip',
      }
    }).then(res => {
      if (res) return res.json()
    }).catch(err => { })
  }

  _constructRequest = () => {
    let req = '/' + this.state.country + '/' + this.state.city + '/' + this.state.hotelId + '/rooms'
      + '?checkin=' + moment(this.state.checkIn).format('YYYY-MM-DD')
      + '&checkout=' + moment(this.state.checkOut).format('YYYY-MM-DD')
      + '&rooms=' + this.state.rooms
      + '&locale=' + this.state.locale
      + '&currency=' + this.state.currency

    return req
  }
  
  componentDidMount() {
    this._sendRequest(this._constructRequest())
      .then(r => {
        this.setState({ result: r })
      })
  }

  _rawMarkup = (content: any) => {
    return { __html: content };
  }

  _selectRoom = (room: any) => {
    this.props.setSelectedRoom(room)
    this.props.setSelectedHotel({
      hotelId: this.state.hotelId,
      checkIn: this.state.checkIn,
      checkOut: this.state.checkOut,
      rooms: this.state.rooms,
      locale: this.state.locale,
      currency: this.state.currency,
      hotelDetail: this.state.result.hotelDetail
    })
    this.props.setRecheckedPrice(null)
    this.props.setRateUnchange()
    this.props.history.push('/bookhotel')
  }

  _image = (rating: number) => {
    let imageTooltip = rating.toString() + ' Star' + (rating > 1 ? 's' : '')
    const images = require.context('../commons/images', true);
    let sRating = '00' + (rating * 10).toString()
    return <img marginWidth={30} height={40} title={imageTooltip}
      src={images('./star-' + sRating.slice(sRating.length - 2) + '.png')} />
  }
  
  public render() {
    //console.log(this.state.result)
    let { result } = this.state
    let roomTypeIds: any[] = []
    if (result) {
      result.rooms.map((room: any) => {
        if (roomTypeIds.indexOf(room.roomTypeId) < 0)
          roomTypeIds.push(room.roomTypeId)
      })
    }
    
    return <section>
        <Header />
      {
        this.state.result 
          ? <section>
            <Helmet>
              <title>{Camelize(result.hotelDetail.name)}, {Camelize(this.state.city)}: Greate value, enjoy travel</title>
              <meta name='description' content={'Great value for ' + Camelize(result.hotelDetail.name) + ' in '
                + Camelize(this.state.city) + ',' } />
              <meta name='keywords' content={result.hotelDetail.name + ',' + Camelize(this.state.city) + ' hotels, ' + Camelize(this.state.city)} />
              <link rel="canonical" href={'http://travelconn.azurewebsites.net/' + this.state.locale                + '/hotels/' + this.state.country + '/' + this.state.city + '/' + this.state.hotelId } />
            </Helmet>
          <Row>
            <Col md={12}>
              <PageHeader>{result.hotelDetail.name}<br />
                <small>{result.hotelDetail.address}, {result.hotelDetail.city}, {result.hotelDetail.country}</small>
              </PageHeader>
            </Col>
          </Row>
          {
            <Row>
              <Col md={12}>
                  <Carousel>
                    {
                      result.hotelDetail.hotelImages.map((img: any) => 
                        <div>
                          <img src={img.highResUrl} />
                          <p className="legend">{img.caption}</p>
                        </div>
                        )
                    }
                </Carousel>
              </Col>
            </Row>
          }
          <Row>
              <Col md={12}> {roomTypeIds.map((roomTypeId: any) =>
                <Room
                  room={result.rooms.filter((room: any) => room.roomTypeId === roomTypeId)} key={roomTypeId}
                  onSelect={this._selectRoom}
                />)}
              </Col>
          </Row>
          {
            result.hotelDetail.checkInInstructions && <section>
            <Row>
              <Col md={3}><h4>Check In Instructions</h4></Col>
              <Col md={9}>
                <span dangerouslySetInnerHTML={this._rawMarkup(result.hotelDetail.checkInInstructions)} />
              </Col>
            </Row>
            <hr />
                </section>
          }
          {
            result.hotelDetail.specialCheckInInstructions && <section>
              <Row>
                <Col md={3}><h4>Special Check In Instructions</h4></Col>
                <Col md={9}>
                  <span dangerouslySetInnerHTML={this._rawMarkup(result.hotelDetail.specialCheckInInstructions)} />
                </Col>
              </Row>
              <hr />
            </section>
          }
          {
              result.hotelDetail.propertyAmenities.length && <section>
                <Row>
                  <Col md={3}><h4>Property Amenities</h4></Col>
                  <Col md={9}>
                    <ul>
                      {result.hotelDetail.propertyAmenities.map((am: any) => <li>{am.name}</li>)}
                    </ul>
                  </Col>
                </Row>
                <hr />
              </section>

          }
          {
              result.hotelDetail.propertyInformation && <section>
            <Row>
              <Col md={3}><h4>Property Information</h4></Col>
              <Col md={9}>
                    <span dangerouslySetInnerHTML={this._rawMarkup(result.hotelDetail.propertyInformation)} />
              </Col>
              </Row>
            <hr />
                </section>
          }
          {
            result.hotelDetail.areaInformation && <section>
              <Row>
                <Col md={3}><h4>Area Information</h4></Col>
                <Col md={9}>
                    <span dangerouslySetInnerHTML={this._rawMarkup(result.hotelDetail.areaInformation)} />
                </Col>
              </Row>
              <hr />
            </section>
          }
          
          {
            result.hotelDetail.hotelPolicy && <section>
              <Row>
                <Col md={3}><h4>Hotel Policy</h4></Col>
                <Col md={9}>
                    <span dangerouslySetInnerHTML={this._rawMarkup(result.hotelDetail.hotelPolicy)} />
                </Col>
              </Row>
              <hr />
            </section>
          }
          {
            result.hotelDetail.roomInformation && <section>
              <Row>
                <Col md={3}><h4>Room Information</h4></Col>
                <Col md={9}>
                    <span dangerouslySetInnerHTML={this._rawMarkup(result.hotelDetail.roomInformation)} />
                </Col>
              </Row>
              <hr />
            </section>
          }
            <Row><Col> - </Col></Row>
          </section>
          : <h3>Searching Rate and Available for your selected hotel ....</h3>
        
      }
    </section>
  }
}


// Wire up the React component to the Redux store
export default connect(
  (state: ApplicationState) => state.hotel, // Selects which state properties are merged into the component's props
  HotelStore.actionCreators                 // Selects which action creators are merged into the component's props
)(HotelDetail_Index) as typeof HotelDetail_Index
