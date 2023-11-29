import { cn } from '@/lib/classnames'

interface ContainerProps extends React.ComponentPropsWithoutRef<'div'> {
  children: React.ReactNode
}

export default function Container({ children, className, ...props }: ContainerProps) {
  return (
    <div className={cn('container max-w-4xl px-2', className)} {...props}>
      {children}
    </div>
  )
}
