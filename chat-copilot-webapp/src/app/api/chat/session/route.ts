import { createChatSession } from '@/api/chat'

export async function POST() {
  const response = await createChatSession()

  if (response.code === 'error') {
    return Response.json({ success: false, error: response.error }, { status: 500 })
  }

  return Response.json(response.data)
}
