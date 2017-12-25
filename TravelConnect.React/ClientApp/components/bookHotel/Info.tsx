import * as React from 'react'

export default class BookHotel_Info extends React.Component<
  { title: string, info: string }, any> {
  constructor(props: any) {
    super(props);
  }

  _rawMarkup = (content: any) => {
    return { __html: content };
  }

  public render() {
    return <section>
      <h4>{this.props.title}</h4>
      <p>
        <span dangerouslySetInnerHTML={this._rawMarkup(this.props.info)} />
      </p>
      <hr />
    </section>
  }
}