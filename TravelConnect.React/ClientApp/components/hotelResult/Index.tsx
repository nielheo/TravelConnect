import * as React from 'react'
import Helmet from 'react-helmet'

import { Panel, Grid, Row, Col, Pagination, Button } from 'react-bootstrap'

import { RouteComponentProps } from 'react-router-dom';

import * as moment from 'moment'
import * as queryString from 'query-string'

import FormInput from '../commons/FormInput'
import FormTextbox from '../commons/FormTextbox'
import FormDropdown from '../commons/FormDropdown'
import SelectDate from '../commons/SelectDate'

import Camelize from '../commons/Camelize'

import HotelItem from './HotelItem'
import HotelFilter from './HotelFilter'
import Header from '../Header'

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
            checkIn: query.cin ? this._parseDate(query.cin) : moment().add(7, 'days'),
            checkOut: query.cout ? this._parseDate(query.cout) : moment().add(9, 'days'),
            rooms: query.rooms || '2',
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
        return fetch('/api/hotels' + request, {
            method: 'get',
            headers: {
                'Content-Type': 'application/json',
                'Accept-Encoding': 'gzip',
            }
        }).then(res => {
            console.log(res)
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

    componentDidMount() {
        this._sendRequest(this._constructRequest())
            .then(r => {
                console.log(r)
                this.setState({ result: r })

                if (r.cacheKey) {
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

        this.props.history.push(url)
    }

    _onCountryChange = (e: any) => {
        this.setState({ country: e.target.value })
    }

    _onCityChange = (e: any) => {
        this.setState({ city: e.target.value })
    }

    _onCheckInChange = (date: any) => {
        this.setState({
            checkIn: date
        })
    }

    _onCheckOutChange = (date: any) => {
        this.setState({
            checkOut: date
        })
    }

    _getHotelsInfo = (codes: string[]) => {
        console.log(codes.join(','))
        //console.log(`_getHotelInfo: ${city}.${id}`)
        fetch(`/api/hotels/${codes.join(',')}/info`, {
            method: 'get',
            headers: {
                'Content-Type': 'application/json',
                'Accept-Encoding': 'gzip',
            }
        }).then(res => {
            console.log(res)
            if (res) return res.json()
            }).then(res => {
                //let hotel = this.state.result.hotels.find((f: any) => f.id === id)
                //console.log(`_getHotelInfo - Result: ${city}.${id}`)
                console.log(res)
                if (res.length) {
                    let hotels = this.state.result.hotels

                    hotels.map((hotel: any) => {
                        console.log('---------------------')
                        console.log(hotel)
                        let r = res.find((rs: any) => rs.code === hotel.id)
                        if (r) {
                            console.log(r)
                            
                            hotel.address = r.address1
                            hotel.thumbnail = r.hotelImage.thumbnail
                        }
                    })

                    console.log('**************')
                    console.log(hotels)

                    this.setState({
                        result: {
                            ...this.state.result,
                            hotels: hotels
                        }
                    })
                }
            //    let hotel = this.state.hotel
                //hotel.address = res.address1 || '-'
                //hotel.thumbnail = res.hotelImage.thumbnail
                //hotel.latitude = res.latitude
                //hotel.longitude = res.longitude

                //let hotels: any[] = []

                //this.state.result.hotels.map((h: any) => {
                //    if (h.id !== id)
                //        hotels.push(h)
                //    else
                //        hotels.push(hotel)
                //})

                
                //console.log(hotels)

                ////this.state.result.hotels.filter((h: any) => h.id !== id).map((h: any) => {
                ////    hotels.push(h)
                ////})

                ////console.log(hotels)

                //this.setState({
                //    result: {
                //        ...this.state.result,
                //        hotels: hotels
                //    }
                    
                //})
            //    this.setState({ hotel: hotel })

            }).catch(err => { })
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

        const itemsPerPage = 20
        const { page } = this.state
        let _totalPages = hotels.length
            ? Math.ceil(hotels.length / itemsPerPage)
            : 0
        let _page = page > _totalPages ? _totalPages : page
        let _startIndex = (_page - 1) * itemsPerPage
        let _endIndex = _page * itemsPerPage

        let missingDetails: string[] = []
        hotels.slice(_startIndex, _endIndex).map((htl: any) => {
            if (!htl.address) {
                missingDetails.push(htl.id)
            }
        });

        if (missingDetails.length) {
            this._getHotelsInfo(missingDetails)
        }

        return <section>
            <Helmet>
                <title>{Camelize(this.state.city)} Hotels, {Camelize(this.state.city)}: Greate value, enjoy travel</title>
                <meta name='description' content={'Great value for hotels in ' + Camelize(this.state.city)} />
                <meta name='keywords' content={Camelize(this.state.city) + ' hotels, ' + Camelize(this.state.city)} />
                <link rel="canonical" href={'http://travelconn.azurewebsites.net/' + this.state.locale                    + '/hotels/' + this.state.country + '/' + this.state.city} />
            </Helmet>
            <Header />
            <Row className='bg-gray'>
                <Col md={1}>
                    <FormTextbox
                        onChange={this._onCountryChange}
                        label='Country'
                        error={this.state.searchClicked && !this.state.country ? '* required' : ''}
                        disabled={false}
                        value={this.state.country}
                    />
                </Col>
                <Col md={4}>
                    <FormTextbox
                        onChange={this._onCityChange}
                        label='City'
                        error={this.state.searchClicked && !this.state.city ? '* required' : ''}
                        disabled={false}
                        value={this.state.city}
                    />
                </Col>
                <Col md={2}>
                    <SelectDate
                        key="checkIn"
                        label="Check In Date"
                        onChange={this._onCheckInChange}
                        selected={this.state.checkIn}
                        error=""
                        disabled={false}
                    />
                </Col>
                <Col md={2}>
                    <SelectDate
                        key="checkIn"
                        label="Check Out Date"
                        onChange={this._onCheckOutChange}
                        selected={this.state.checkOut}
                        error=""
                        disabled={false}
                    />
                </Col>
                <Col md={2}>
                    <FormTextbox
                        onChange={this._onCityChange}
                        label='Rooms'
                        error={this.state.searchClicked && !this.state.city ? '* required' : ''}
                        disabled={true}
                        value={this.state.rooms.split('|').length + ' room' + (this.state.rooms.split('|').length > 1 ? 's' : '')}
                    />
                </Col>
                <Col md={1}>
                    <FormInput
                        label=' '
                        error=''
                        disabled={false}
                    >
                        <Button>Search</Button>
                    </FormInput>
                </Col>
            </Row>
            <Row>
                <hr />
                <Col md={3}>
                    {
                        //this.state.result && this.state.result.hotels &&
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

                                <Row><Col md={12}>
                                    <h3>{hotels.length.toLocaleString('en-US')} hotel{(hotels.length > 1) && 's'} found in {Camelize(this.state.city) + ', ' + this.state.country.toUpperCase()}</h3></Col></Row>
                                <Row className="text-right">
                                    <Col md={12}>
                                        <Pagination prev next first last ellipsis boundaryLinks
                                            items={_totalPages} maxButtons={5} activePage={_page}
                                            onSelect={this._onPageChange} />
                                    </Col>
                                </Row>

                                {
                                    hotels.slice(_startIndex, _endIndex).map((hotel: any) =>
                                        <HotelItem hotel={hotel}
                                            url={`/${this.state.locale}/hotels/${this.state.country}/`
                                                + `/${this.state.city}/${hotel.id}`
                                                + `?cin=${this.state.checkIn.format('YYYY-MM-DD')}`
                                                + `&cout=${this.state.checkOut.format('YYYY-MM-DD')}`
                                                + `&rooms=${this.state.rooms}&currency=${this.state.currency}`
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
                            : <Row><Col md={12}><h3>Searching hotels in {Camelize(this.state.city) + ', ' + this.state.country.toUpperCase()}. Please wait ...</h3></Col></Row>
                    }
                </Col>
            </Row>
        </section>
    }
}