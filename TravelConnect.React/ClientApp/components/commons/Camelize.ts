const camelize = (str: string) => {
  return str.replace(/\b[a-z]/g, function (f) { return f.toUpperCase(); })
}

export default camelize