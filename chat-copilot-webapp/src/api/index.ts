const baseURL = 'http://localhost:5023' // TODO: Move to env

type FetchOptions = Partial<RequestInit>

interface ApiArgs<T> extends FetchOptions {
  url: string
  params?: T
}

export const api = <TParams extends Record<string, any>>({
  url,
  params,
  ...options
}: ApiArgs<TParams>) => {
  const urlWithParams = new URL(url, baseURL)
  if (params) {
    Object.keys(params).forEach((key: string) =>
      urlWithParams.searchParams.append(key, params[key])
    )
  }

  return fetch(urlWithParams, options)
}
