import ChatLayout from '@/components/layouts/chat'

export default function ChatRootLayout({ children }: { children: React.ReactNode }) {
  return <ChatLayout>{children}</ChatLayout>
}
