import Sidebar from './sidebar'

export default function ChatLayout({ children }: { children: React.ReactNode }) {
  return (
    <div className="flex h-full">
      <Sidebar />
      <main className="h-screen flex-1 bg-primary px-2 text-primary-foreground lg:px-8">
        {children}
      </main>
    </div>
  )
}
