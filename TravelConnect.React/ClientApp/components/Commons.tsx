export function _GetAirport(code:string) {
  return fetch('/api/airports/' + code , {
    method: 'get',
    headers: {
      'Accept-Encoding': 'gzip',
    }
  }).then(res => res.json())
}
