'use client'

import Link from 'next/link'
import { usePathname } from 'next/navigation'

import { cn } from '@/lib/classnames'

export const SidebarItem = ({ href, children }: { href: string; children: React.ReactNode }) => {
  const pathname = usePathname()
  const isActive = pathname === href

  return (
    <li>
      <Link
        href={href}
        className={cn(
          '!text-small mb-1 line-clamp-1 block rounded px-2 py-3',
          isActive ? 'bg-primary text-primary-foreground' : 'hover:bg-gray-500/20'
        )}
      >
        <span className="line-clamp-1">{children}</span>
      </Link>
    </li>
  )
}
