import React from 'react'

import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'

import { cn } from '@/lib/classnames'
import { Message } from '@/types/chat'

export default function ChatMessage({ message }: { message: Message }) {
  return (
    <div
      className={cn(
        'text-tile-100 relative mb-4 max-w-md break-words  rounded-lg px-6 py-4 text-sm',
        message.actor === 'User'
          ? 'self-end bg-secondary'
          : 'self-start bg-primary text-primary-foreground'
      )}
    >
      <Avatar
        className={cn(
          'absolute  top-0  mt-1',
          message.actor === 'User' ? 'left-full ml-2' : 'right-full mr-2'
        )}
      >
        <AvatarImage src="x" alt="@shadcn" />
        <AvatarFallback>HB</AvatarFallback>
      </Avatar>
      {message.content}
    </div>
  )
}
