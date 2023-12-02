export interface ChatSession {
  id: string
  createdOn: string
}

export interface Message {
  content: string
  actor: 'User' | 'Bot'
  createdOn: string
}
