import { requestHandler } from '@/lib/request-handler'
import { api } from '../index'

import {
  CreateChatSessionResponse,
  GetChatSessionsParams,
  GetChatSessionsResponse,
  GetMessagesParams,
  GetMessagesResponse,
  PostMessageParams,
  PostMessageResponse,
} from '@/types/api'

export const createChatSession = requestHandler<{}, CreateChatSessionResponse>((_, options) =>
  api({
    url: '/chat-sessions',
    ...options,
    method: 'POST',
    body: '{}',
  })
)

export const getChatSessions = requestHandler<GetChatSessionsParams, GetChatSessionsResponse>(
  (params, options) =>
    api({
      url: `/chat-sessions/${params?.userId}`,
      ...options,
    })
)

export const getMessages = requestHandler<GetMessagesParams, GetMessagesResponse>(
  (params, options) =>
    api({
      url: `/chat-sessions/${params?.chatSessionId}/messages`,
      ...options,
    })
)

export const sendMessage = requestHandler<PostMessageParams, PostMessageResponse>(
  (params, options) =>
    api({
      url: `/chat-sessions/${params?.chatSessionId}/messages`,
      ...options,
      method: 'POST',
      body: JSON.stringify({ input: params?.input }),
    })
)
