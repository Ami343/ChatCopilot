import { getMessages } from '@/api/chat'
import ChatWindow from '~chat/_components/chat-window'

export default async function Session({ params }: { params: { session: string } }) {
  const response = await getMessages({ chatSessionId: params.session })

  if (response.code === 'error') {
    return <div>Error</div> // TODO: handle error view
  }

  const chat = {
    messages: response.data.messages,
    sessionId: params.session,
  }

  return <ChatWindow chat={chat} />
}