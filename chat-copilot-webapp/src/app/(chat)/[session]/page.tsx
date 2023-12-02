import { getMessages } from '@/api/external/chat'
import ChatWindow from '~chat/_components/chat-window'

export default async function SessionChatWindow({ params }: { params: { session: string } }) {
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
