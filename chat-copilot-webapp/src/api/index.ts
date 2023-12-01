const baseURL = 'http://localhost:5023' // TODO: Move to env

export const api = <TParams extends Record<string, any>>(
  url: string,
  params?: TParams,
  options?: RequestInit
) => {
  const urlWithParams = new URL(url, baseURL)
  if (params) {
    Object.keys(params).forEach((key: string) =>
      urlWithParams.searchParams.append(key, params[key])
    )
  }

  return fetch(urlWithParams, options)
}
