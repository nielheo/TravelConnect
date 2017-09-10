import * as React from 'react';

import { Grid, Row, Col } from 'react-bootstrap'
import { NavMenu } from './NavMenu';

export class Layout extends React.Component<{}, {}> {
  public render() {
    return <Grid >
      <Row>
        <Col sm={3}>
          <NavMenu />
        </Col>
        <Col sm={9}>
          {this.props.children}
        </Col>
      </Row>
    </Grid>;
  }
}