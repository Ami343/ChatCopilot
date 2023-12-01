import { revalidateTag } from 'next/cache'

import { createChatSession } from '@/api/external/chat'
import { CHAT_SESSIONS } from '@/constants/tags'

export async function POST() {
  const response = await createChatSession()

  if (response.code === 'error') {
    return Response.json({ success: false, error: response.error }, { status: 500 })
  }

  revalidateTag(CHAT_SESSIONS)

  return Response.json(response.data)
}
