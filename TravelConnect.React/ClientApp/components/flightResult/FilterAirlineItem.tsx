import * as React from 'react'
import { Row, Col } from 'react-bootstrap'

import * as Commons from '../Commons'

export default class FlightDetails extends React.Component<{ airline: any }, any> {
  constructor(props: any) {
    super(props)
    this.state = {
      airlineFullname: ''
    }
  }

  componentDidMount() {
    const { code } = this.props.airline
    //console.log(this.props.airline.code)
    Commons._GetAirline(code).then((res: any) => { this.setState({ airlineFullname: res.name }) })
  }
  
  public render() {
    //console.log(this.state.airlineFullname)
    const { airline } = this.props
    return <Row>
      <Col md={12}><input type='checkbox' /> {this.state.airlineFullname ? this.state.airlineFullname + ' (' + airline.code + ')' : airline.code} - {airline.count} {airline.loaded ? '' : 'loading'}</Col>
    </Row>
  }
}