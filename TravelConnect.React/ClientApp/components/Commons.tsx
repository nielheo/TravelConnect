var Cacheman = require('cacheman') as any;
var Engine = require('cacheman-memory') as any;
var engine = new Engine();

var cache = new Cacheman('tc', { ttl: 60 * 60, engine: engine })

export function _GetAirport(code: string) {
  let key = 'airport_' + code

  return cache.get(key).then((airport:any) => {
    if (airport) {
      return airport
    } else {
      return fetch('/api/airports/' + code, {
        method: 'get',
        headers: {
          'Accept-Encoding': 'gzip',
        }
      }).then((airport: any) => airport.json())
        .then((airport: any) => {
          return cache.set(key, airport).then((airport:any) => {
            //console.log('2.1')
            //console.log(cities)
            return airport
          })
        })
    }
  })
}
