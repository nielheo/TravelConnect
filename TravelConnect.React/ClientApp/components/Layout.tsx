import * as React from 'react';

import { Grid, Row, Col } from 'react-bootstrap'
import { NavMenu } from './NavMenu';

export class Layout extends React.Component<{}, {}> {
  public render() {
    return <Grid >
      <Row>
        <Col md={12}>
          {this.props.children}
        </Col>
      </Row>
    </Grid>;
  }
}