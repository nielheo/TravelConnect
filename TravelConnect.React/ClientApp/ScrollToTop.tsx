import * as React from 'react'

import { RouteComponentProps, withRouter } from 'react-router-dom';

export default class ScrollToTop extends React.Component<any, any> {
  componentDidUpdate() {
      window.scrollTo(0, 0)
  }

  public render() {
    return <section>{this.props.children}</section>
  }
}
