import { ChatSession, Message } from '@/types/chat'

// External API

export interface CreateChatSessionResponse {
  id: string
  initialMessage: string
}

export interface GetChatSessionsParams {
  userId: string
}

export interface GetChatSessionsResponse {
  chatSessions: ChatSession[]
}

export interface GetMessagesParams {
  chatSessionId: string
}

export interface GetMessagesResponse {
  messages: Message[]
}

export interface PostMessageParams {
  chatSessionId: string
  input: string
}

export interface PostMessageResponse {
  messages: Message[]
}
