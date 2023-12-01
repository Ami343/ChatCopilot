import { Message } from '@/types/chat'
import { useState } from 'react'

export default function useChatWindow(initialMessages: Message[] = []) {
  const [messages, setMessages] = useState(initialMessages)

  const createMessage = (actor: Message['actor'], content: Message['content']) => {
    setMessages((messages) => [
      ...messages,
      {
        actor,
        content,
        createdOn: new Date().toISOString(),
      },
    ])
  }

  const replaceUrlWithSessionId = (chatSessionId: string) => {
    const newUrl = `/${chatSessionId}`
    window.history.replaceState({ ...window.history.state, as: newUrl, url: newUrl }, '', newUrl)
  }

  return {
    messages,
    createMessage,
    replaceUrlWithSessionId,
  }
}
