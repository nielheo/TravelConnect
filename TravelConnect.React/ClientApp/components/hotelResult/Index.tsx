import * as React from 'react'

import { Panel, Grid, Row, Col, Pagination } from 'react-bootstrap'

import { RouteComponentProps } from 'react-router-dom';

import * as moment from 'moment'
import * as queryString from 'query-string'

import FormInput from '../commons/FormInput'
import FormTextbox from '../commons/FormTextbox'
import FormDropdown from '../commons/FormDropdown'
import SelectDate from '../commons/SelectDate'

import HotelItem from './HotelItem'
import HotelFilter from './HotelFilter'

export default class HotelResult_Index extends React.Component<
  RouteComponentProps<{
    country: string
    city: string
  }>, any> {
  constructor(props: any) {
    super(props);
    //let now = moment()
    //let today = moment({ year: now.year(), month: now.month(), day: now.day() })

    let query = queryString.parse(props.location.search)
    //let rooms = this._parseRoom(query)

    this.state = {
      country: props.match.params.country,
      city: props.match.params.city,
      checkIn: this._parseDate(query.cin),
      checkOut: this._parseDate(query.cout),
      rooms: query.rooms,
      //occupancies: query.rooms,
      locale: props.match.params.locale || 'en_US',
      currency: query.currency || 'USD',
      page: 1,
      result: null,

      filteredHotelName: '',
      filteredStarRating: [],
    };
  }

  _parseDate = (value: string) => {
    var parsed = moment(value, 'YYYY-MM-DD')
    if (parsed.isValid())
      return parsed
    else
      return null
  }

  _parseOccupancy = (room: string) => {
    let oc = room.split(',')
    let cAges = []
    for (var i = 1; i < oc.length; i++) {
      cAges.push(parseInt(oc[i]))
    }

    let occupancy = { adult: parseInt(oc[0]), childAges: cAges }

    return occupancy
  }

  _parseRoom = (value: any) => {
    let rooms = []
    if (value.room1) rooms.push(this._parseOccupancy(value.room1))
    if (value.room2) rooms.push(this._parseOccupancy(value.room2))
    if (value.room3) rooms.push(this._parseOccupancy(value.room3))
    if (value.room4) rooms.push(this._parseOccupancy(value.room4))
    return rooms
  }

  _sendRequest = (request: any) => {
    //console.log(request)
    return fetch('/api/hotels' + request, {
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
    let req = '/' + this.state.country + '/' + this.state.city
      + '?checkin=' + moment(this.state.checkIn).format('YYYY-MM-DD')
      + '&checkout=' + moment(this.state.checkOut).format('YYYY-MM-DD')
      + '&rooms=' + this.state.rooms
      + '&locale=' + this.state.locale
      + '&currency=' + this.state.currency

    return req
  }

  _constructMoreRequest = () => {
    let req = '/more'
      + '?locale=' + this.state.locale
      + '&currency=' + this.state.currency
      + '&cacheKey=' + this.state.result.cacheKey
      + '&cacheLocation=' + this.state.result.cacheLocation
      + '&requestKey=' + this.state.result.requestKey
    console.log(req)
    return req
  }

  _getMore = () => {
    this._sendRequest(this._constructMoreRequest()).then(rs => {
      var result = this.state.result

      rs.hotels.map((htl: any) => {
        result.hotels.push(htl)
      })

      result.cacheKey = rs.cacheKey
      result.cacheLocation = rs.cacheLocation
      result.requestKey = rs.requestKey

      this.setState({ result: result })

      if (this.state.result.cacheKey) this._getMore()
    })
  }

  componentWillMount() {
    console.log(this.state)
  }

  componentDidMount() {
    this._sendRequest(this._constructRequest())
      .then(r => {

        this.setState({ result: r })

        if (this.state.result.cacheKey) {
          console.log('get more')
          this._getMore()
        }
      })
  }

  _onPageChange = (a: any) => {
    if (a !== this.state.page) {
      this.setState({
        page: a
      })
    }
  }

  _onFilterHotelNameChange = (e: any) => {
    this.setState({ filteredHotelName: e.target.value })
  }

  _onFilterStarRatingChange = (e: any, starRating: number) => {
    if (this.state.filteredStarRating.indexOf(starRating) < 0 && e.target.checked) {
      var filtered = this.state.filteredStarRating
      filtered.push(starRating)

      this.setState({
        filteredStarRating: filtered
      })
    }

    if (this.state.filteredStarRating.indexOf(starRating) >= 0 && !e.target.checked) {
      var filtered = this.state.filteredStarRating.filter((s: any) => s !== starRating)
      //filtered.splice(this.state.filteredStarRating.indexOf(starRating), 1)

      console.log(filtered)

      this.setState({
        filteredStarRating: filtered
      })
    }
  }

  _onHotelSelect = (id: number) => {
    var url = '/' + this.state.locale + '/hotels/' + this.state.country + '/' + this.state.city + '/' + id
    url += '?cin=' + this.state.checkIn.format('YYYY-MM-DD')
    url += '&cout=' + this.state.checkOut.format('YYYY-MM-DD')
    url += '&rooms=' + this.state.rooms + '&currency=' + this.state.currency

    console.log(this.props)
    this.props.history.push(url)
    console.log(id)
  }

  public render() {
    let hotels = (this.state.result && this.state.result.hotels) || []
    if (this.state.filteredHotelName)
      hotels = hotels.filter((h: any) =>
        h.name.toLowerCase().indexOf(this.state.filteredHotelName.toLowerCase()) >= 0)

    let hotelsByName = hotels

    if (this.state.filteredStarRating.length)
      hotels = hotels.filter((h: any) =>
        this.state.filteredStarRating.indexOf(h.starRating) >= 0)

    console.log(hotels.length)

    const itemsPerPage = 20
    const { page } = this.state
    let _totalPages = hotels.length
      ? Math.ceil(hotels.length / itemsPerPage)
      : 0
    let _page = page > _totalPages ? _totalPages : page
    let _startIndex = (_page - 1) * itemsPerPage
    let _endIndex = _page * itemsPerPage

    return <Row>
      <Col md={3}>
        {
          this.state.result && this.state.result.hotels &&
          <HotelFilter
            hotels={hotelsByName}
            filteredHotels={hotels}
            filteredHotelName={this.state.filteredHotelName}
            onFilterHotelNameChange={this._onFilterHotelNameChange}
            filteredStarRating={this.state.filteredStarRating}
            onFilterStarRatingChange={this._onFilterStarRatingChange}
          />
        }
      </Col>
      <Col md={9}>
        {
          this.state.result
            ? <section>
              <Row><Col md={12}><h3>Select from {hotels.length} hotels</h3></Col></Row>
              <Row className="text-right">
                <Col md={12}>
                  <Pagination prev next first last ellipsis boundaryLinks
                    items={_totalPages} maxButtons={5} activePage={_page}
                    onSelect={this._onPageChange} />
                </Col>
              </Row>

              {
                hotels.slice(_startIndex, _endIndex).map((hotel: any) =>
                  <HotelItem hotel={hotel} url={
                    '/' + this.state.locale + '/hotels/' + this.state.country + '/' + this.state.city + '/' + hotel.id
                          + '?cin=' + this.state.checkIn.format('YYYY-MM-DD')
                          + '&cout=' + this.state.checkOut.format('YYYY-MM-DD')
                          + '&rooms=' + this.state.rooms + '&currency=' + this.state.currency
                  } />
                )
              }
              <Row className="text-right">
                <Col md={12}>
                  <Pagination prev next first last ellipsis boundaryLinks
                    items={_totalPages} maxButtons={5} activePage={_page}
                    onSelect={this._onPageChange} />
                </Col>
              </Row>
            </section>
            : <Row><Col md={12}>Search your hotels. Please wait....</Col></Row>
        }
      </Col>
    </Row>
  }
}
