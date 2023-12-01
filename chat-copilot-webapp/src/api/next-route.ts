import { requestHandler } from './request-handler'

type FetchOptions = Partial<RequestInit>

interface NextRouteParams extends FetchOptions {
  url: string
}

export const nextRoute = requestHandler<NextRouteParams, {}>((params, _) => {
  if (!params?.url) {
    throw new Error('Missing url')
  }

  if (!params.url.startsWith('/api')) {
    throw new Error('Invalid url')
  }

  const { url, ...options } = params

  return fetch(url, options)
})
