import Link from 'next/link'

import { getChatSessions } from '@/api/external/chat'
import { Separator } from '@/components/ui/separator'
import { Pencil2Icon } from '@radix-ui/react-icons'
import { SidebarItem } from './sidebar-item'

export default async function Sidebar() {
  const response = await getChatSessions({ userId: '1' })

  if (response.code === 'error') {
    return <div>Error</div> // TODO: handle error view
  }

  const chatSessions = response.data.chatSessions

  // TODO: Create a mobile version of the sidebar. Might use https://ui.shadcn.com/docs/components/sheet
  return (
    <aside className="hidden w-52 border-r border-r-border bg-secondary px-2 py-8 text-foreground md:block">
      <div className="w-full">
        <h4 className="text-h4 mb-8 px-2 text-primary">Chat Copilot</h4>
        <nav>
          <ul className="flex w-full flex-col items-start gap-1 space-x-0">
            <li className="mb-2 line-clamp-1 w-full rounded hover:bg-gray-500/20">
              <Link href="/" className="flex items-center justify-between rounded px-2 py-3">
                New chat
                <Pencil2Icon className="ml-2 inline-block h-4 w-4" />
              </Link>
            </li>
            <div className="px-2 text-xs text-muted">Lorem, ipsum.</div>
            <Separator orientation="horizontal" className="my-2 bg-border" />
            {chatSessions.map((chatSession) => (
              <SidebarItem key={chatSession.id} href={`/${chatSession.id}`}>
                {chatSession.id}
              </SidebarItem>
            ))}
          </ul>
        </nav>
      </div>
    </aside>
  )
}
