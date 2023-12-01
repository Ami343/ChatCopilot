type BaseRequest<T> = (params?: T, options?: RequestInit) => Promise<Response>

type SuccessResponse<V> = {
  code: 'success'
  data: V
}

type ErrorResponse<E> = {
  code: 'error'
  error: E
}

type BaseResponse<V, E> = SuccessResponse<V> | ErrorResponse<E>

export const requestHandler =
  <T, V, E = unknown>(request: BaseRequest<T>) =>
  async <TResponse = void>(
    params?: T,
    options?: RequestInit
  ): Promise<BaseResponse<TResponse extends void ? V : TResponse, E>> => {
    try {
      const response = await request(params, {
        ...options,
        headers: {
          ...(options?.headers ?? {}),
          'Content-Type': 'application/json',
        },
      })

      const data = (await response.json()) as TResponse extends void ? V : TResponse

      return { code: 'success', data }
    } catch (e) {
      return { code: 'error', error: e as E }
    }
  }
