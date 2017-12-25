import * as React from 'react'
import { Route } from 'react-router-dom'
import { Layout } from './components/Layout'
import Home from './components/Home'
import FlightSearch from './components/flightSearch'
import FlightResult from './components/flightResult'
import FlightPax from './components/FlightPax'
import HotelResult from './components/hotelResult'
import HotelDetail from './components/hotelDetail'
import BookHotel from './components/bookHotel'

import ScrollToTop from './ScrollToTop'

export const routes = <Layout>
  <ScrollToTop>
    <Route exact path='/' component={Home} />
    <Route exact path='/:locale/hotels/:country/:city/:hotelId' component={HotelDetail} />
    <Route exact path='/hotels/:country/:city/:hotelId' component={HotelDetail} />
    <Route exact path='/:locale/hotels/:country/:city' component={HotelResult} />
    <Route exact path='/hotels/:country/:city' component={HotelResult} />
    <Route exact path='/bookHotel' component={BookHotel} />
  
    <Route path='/flight/search' component={FlightSearch} />
    <Route path='/flight/result/:route' component={FlightResult} />
    <Route path='/flight/pax/' component={FlightPax} />
  </ScrollToTop>
</Layout>