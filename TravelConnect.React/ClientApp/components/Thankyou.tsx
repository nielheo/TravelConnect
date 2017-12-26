import * as React from 'react'
import { Row, Col } from 'react-bootstrap'

export default class Thankyou extends React.Component<
  any, any> {
  constructor(props: any) {
    super(props);
  }
  
  public render() {
    return <section>
      <Row><Col md={12}>
        <h1>Congratulations!</h1>
        <h3>You have successfully created a booking</h3>
        <hr />
        <p>
          There is no real booking created to your selected hotel.
        </p>
        <p>
          This site is a demo site to show you that having a Online Travel Agent is easy. Every body can have it.
        </p>
        <p>
          For more information please contact us at <a href='mailto:niel.heo@gmail.com'>niel.heo@gmail.com</a>
        </p>
      </Col></Row>
    </section>
  }
}