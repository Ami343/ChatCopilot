'use client'

import { useState } from 'react'

import Container from './container'
import ChatInput, { FormSchema } from './input'
import { set } from 'zod'

export default function ChatWindow() {
  const [messages, setMessages] = useState<string[]>([])

  const onEnter = (values: FormSchema) => {
    setMessages((messages) => [...messages, values.prompt])
  }

  return (
    <div className="flex h-full flex-col py-16">
      <div className="flex flex-1 flex-col overflow-y-scroll">
        <Container className="flex flex-col items-start">
          {messages.map((message, index) => (
            <div key={index} className="mb-4 rounded-xl bg-gray-700 px-4 py-2">
              {message}
            </div>
          ))}
        </Container>
      </div>
      <div className="h-10">
        <Container>
          <ChatInput onEnter={onEnter} />
        </Container>
      </div>
    </div>
  )
}
