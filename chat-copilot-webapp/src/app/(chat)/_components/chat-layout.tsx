import Sidebar from './sidebar'

export default function ChatLayout({ children }: { children: React.ReactNode }) {
  return (
    <div className="flex h-full">
      <Sidebar />
      <main className="h-screen flex-1 bg-card px-2 lg:px-8">{children}</main>
    </div>
  )
}
