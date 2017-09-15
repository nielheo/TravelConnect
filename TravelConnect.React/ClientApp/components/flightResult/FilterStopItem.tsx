import * as React from 'react'
import { Row, Col } from 'react-bootstrap'

import * as Commons from '../Commons'

export default class FilterStopItem extends React.Component<{ stop: any, onSetFilter: any }, any> {
  constructor(props: any) {
    super(props)
  }

  //componentDidMount() {
  //  const { code } = this.props.airline
  //  //console.log(this.props.airline.code)
  //  Commons._GetAirline(code).then((res: any) => { this.setState({ airlineFullname: res.name }) })
  //}

  _submitChange = (event: any, value: any) => {
    this.props.onSetFilter(event, value) 
  }

  public render() {
    //console.log(this.state.airlineFullname)
    const { stop } = this.props
    const that = this
    return <Row>
      <Col md={12}><input type='checkbox' checked={stop.selected}
        onChange={(e) => this.props.onSetFilter(e.target.checked, stop.stop)} />
        {stop.stop ? stop.stop + ' Stop' + (stop.stop > 1 ? 's' : '') : 'Direct'} - {stop.count}
      </Col>
    </Row>
  }
}