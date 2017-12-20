import * as React from 'react'
import { Route } from 'react-router-dom'
import { Layout } from './components/Layout'
import Home from './components/Home'
import FetchData from './components/FetchData'
import Counter from './components/Counter'
import FlightSearch from './components/flightSearch'
import FlightResult from './components/flightResult'
import FlightPax from './components/FlightPax'
import HotelResult from './components/hotelResult'

export const routes = <Layout>
  <Route exact path='/' component={Home} />
  <Route path='/counter' component={Counter} />
  <Route path='/flight/search' component={FlightSearch} />
  <Route path='/flight/result/:route' component={FlightResult} />
  <Route path='/flight/pax/' component={FlightPax} />
  <Route path='/hotels/:country/:city' component={HotelResult} />

  <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
</Layout>;