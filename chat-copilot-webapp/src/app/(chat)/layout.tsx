import ChatLayout from '~chat/_components/chat-layout'

export default function ChatRootLayout({ children }: { children: React.ReactNode }) {
  return <ChatLayout>{children}</ChatLayout>
}
