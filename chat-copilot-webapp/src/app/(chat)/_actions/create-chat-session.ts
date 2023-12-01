'use server'

import { createChatSession } from '@/api/chat'

export async function createChatSessionAction() {
  const response = await createChatSession()

  return response
}
