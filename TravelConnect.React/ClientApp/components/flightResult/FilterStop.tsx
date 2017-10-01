import * as React from 'react'

import { Row, Col } from 'react-bootstrap'

import FilterStopItem from './FilterStopItem'

import { FilterStopType } from '../Classes'

export default class FilterStop extends React.Component<{
  stops: FilterStopType[],
  onChangeFilter: Function
}, any> {
  constructor(props: any) {
    super(props)
  }

  _compareStop = (a: any, b: any) => {
    if (a.stop < b.stop)
      return -1
    if (a.stop > b.stop)
      return 1

    return 0
  }

  public render() {
    return <section>
      {
        this.props.stops &&
        this.props.stops.sort(this._compareStop).map((stop: any) =>
          <FilterStopItem stop={stop} key={'stopFilter_' + stop.stop} onChangeFilter={this.props.onChangeFilter} />
        )
      }
    </section>
  }
}