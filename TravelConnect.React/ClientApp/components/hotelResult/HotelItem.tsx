﻿import * as React from 'react'
import { NavLink, Link } from 'react-router-dom';

import { Panel, Grid, Row, Col, Pagination } from 'react-bootstrap'

export default class HotelItem extends React.Component<{ hotel: any, url: any }, any> {
    constructor(props: any) {
        super(props)

        this.state = {
            highlighted: false,
        //    hotel: this.props.hotel
        }
    }

    _rawMarkup = (content: any) => {
        return { __html: content };
    }

    _onMouseOver = () => {
        this.setState({ highlighted: true })
    }

    _onMouseLeave = () => {
        this.setState({ highlighted: false })
    }

    _compareHotelRoomMaxNightlyRate = (a: any, b: any) => {
        if (a.chargeableRate.maxNightlyRate < b.chargeableRate.maxNightlyRate)
            return -1
        if (a.chargeableRate.maxNightlyRate > b.chargeableRate.maxNightlyRate)
            return 1

        return 0
    }

    _image = (rating: number) => {
        let imageTooltip = rating.toString() + ' Star' + (rating > 1 ? 's' : '')
        const images = require.context('../commons/images', true);
        let sRating = '00' + (rating * 10).toString()
        return <img marginWidth={30} height={16} title={imageTooltip}
            src={images('./star-' + sRating.slice(sRating.length - 2) + '.png')} />
    }

    //componentWillMount() {
    //    console.log('componentWillMount')
    //    this.setState({
    //        hotel: this.props.hotel
    //    })
    //}

    //componentDidMount() {
    //    console.log('componentDidMount')
    //    let { hotel } = this.state
    //    if (!hotel.address) {
    //        fetch(`/api/hotels/country/${hotel.cityCode}/${hotel.id}/info`, {
    //            method: 'get',
    //            headers: {
    //                'Content-Type': 'application/json',
    //                'Accept-Encoding': 'gzip',
    //            }
    //        }).then(res => {
    //            console.log(res)
    //            if (res) return res.json()
    //            }).then(res => {
    //            //    let hotel = this.state.hotel
    //                hotel.address = res.address
    //                hotel.thumbnail = res.hotelImage.thumbnail
    //            //    this.setState({ hotel: hotel })

    //            }).catch(err => { })
    //    }
    //}

    _content = (hotel: any) => {
        let cheapestRoom = hotel.hotelRooms.sort(this._compareHotelRoomMaxNightlyRate)[0]

        return <Panel bsStyle={this.state.highlighted ? 'primary' : 'default'} onMouseOver={this._onMouseOver} onMouseLeave={this._onMouseLeave}>
            <Row>
                <Col md={3}>
                    {hotel.thumbnail &&
                        <img src={ //'https://i.travelapi.com' + hotel.thumbnail.replace('t.jpg', 's.jpg')
                        hotel.thumbnail
                            } />
                    }
                </Col>
                <Col md={9}>
                    <Row>
                        <Col md={9}><h4>{hotel.name} {this._image(hotel.starRating)}<br /><small>{hotel.address}</small></h4>
                        </Col>
                        <Col md={3}><h4><b>{cheapestRoom.chargeableRate.currency} {cheapestRoom.chargeableRate.maxNightlyRate.toLocaleString('en-US')}</b>
                            <br /><small>Nightly Rate</small>
                        </h4>
                        </Col>
                    </Row>
                    <Row>
                        <Col md={12}><span dangerouslySetInnerHTML={this._rawMarkup(hotel.shortDesc)} /></Col>
                    </Row>

                </Col>
            </Row>
        </Panel>
    }

    public render() {
        let { hotel } = this.props
        
        return <section>
            {this.state.highlighted
                ?
                <NavLink exact to={this.props.url} target='_blank'>
                    {this._content(hotel)}
                </NavLink>
                : this._content(hotel)
            }

        </section>
    }
}