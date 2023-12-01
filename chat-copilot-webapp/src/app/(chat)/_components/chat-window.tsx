'use client'

import { useState } from 'react'

import { nextRoute } from '@/api/next-route'
import { Message } from '@/types/chat'
import { CreateChatSessionResponse } from '@/types/api'
import Container from './container'
import ChatMessage from './chat-message'
import ChatInput, { FormSchema } from './chat-input'

interface ChatWindowProps {
  chat?: {
    messages?: Message[]
    sessionId?: string
  }
}

export default function ChatWindow({ chat }: ChatWindowProps) {
  const [messages, setMessages] = useState(chat?.messages ?? [])

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

  const onMessageEntered = async (values: FormSchema) => {
    // TODO: send the message to the server
    createMessage('User', values.prompt)

    if (!chat?.sessionId) {
      const createChatSessionRsp = await nextRoute<CreateChatSessionResponse>({
        url: '/api/chat/session',
        method: 'POST',
      })

      if (createChatSessionRsp.code === 'error') {
        // TODO: handle error by alerting user with toast. Use https://ui.shadcn.com/docs/components/toast
        console.error('Something went wrong')
        return
      }

      createMessage('Bot', createChatSessionRsp.data.initialMessage)
      replaceUrlWithSessionId(createChatSessionRsp.data.id)
    }
  }

  return (
    <div className="flex h-full flex-col py-16">
      <div className="flex flex-1 flex-col overflow-y-scroll">
        <Container className="flex flex-col items-start justify-items-start">
          {messages.map((message) => (
            <ChatMessage key={message.createdOn} message={message} />
          ))}
        </Container>
      </div>
      <div className="h-10">
        <Container>
          <ChatInput onMessageEntered={onMessageEntered} />
        </Container>
      </div>
    </div>
  )
}
