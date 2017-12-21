﻿import * as React from 'react'

import { Panel, Grid, Row, Col, Pagination } from 'react-bootstrap'

export default class HotelItem extends React.Component<{ hotel: any }, any> {
    constructor(props: any) {
        super(props)
    }

    public render() {
        let { hotel } = this.props

        return <Panel>
            <Row>
                <Col md={12}><h4>{hotel.name}</h4></Col>
            </Row>
            <Row>
                <Col md={12}>{hotel.shortDesc}</Col>
            </Row>
            <Row>
                <Col md={12}>{hotel.currCode} {hotel.rateFrom} - {hotel.currCode} {hotel.rateTo}</Col>
            </Row>
        </Panel>
    }

}