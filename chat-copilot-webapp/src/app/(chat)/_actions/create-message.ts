'use server'

import { sendMessage } from '@/api/chat'

export async function sendMessageAction({
  chatSessionId,
  input,
}: {
  chatSessionId: string
  input: string
}) {
  const response = await sendMessage({ chatSessionId, input })

  return response
}
