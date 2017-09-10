var Cacheman = require('cacheman') as any;
var Engine = require('cacheman-memory') as any;
var engine = new Engine();

var cache = new Cacheman('tc', { ttl: 60 * 60, engine: engine })

export function _GetAirport(code: string) {
  let key = 'airport_' + code

  return cache.get(key).then((airport: any) => {
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
          return cache.set(key, airport).then((airport: any) => {
            return airport
          })
        })
    }
  })
}

export function _GetAirline(code: string) {
  let key = 'airline_' + code

  return cache.get(key).then((airline: any) => {
    if (airline) {
      return airline
    } else {
      return fetch('/api/airlines/' + code, {
        method: 'get',
        headers: {
          'Accept-Encoding': 'gzip',
        }
      }).then((airline: any) => airline.json())
        .then((airline: any) => {
          return cache.set(key, airline).then((airline: any) => {
            return airline
          })
        })
    }
  })
}