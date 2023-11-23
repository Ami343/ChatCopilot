'use client'

import * as z from 'zod'
import { zodResolver } from '@hookform/resolvers/zod'
import { useForm } from 'react-hook-form'
import { PaperPlaneIcon } from '@radix-ui/react-icons'

import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormMessage,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'

const formSchema = z.object({
  prompt: z.string(),
})

export type FormSchema = z.infer<typeof formSchema>

interface ChatInputProps {
  onEnter?: (values: FormSchema) => void
}

export default function ChatInput({ onEnter }: ChatInputProps) {
  const form = useForm<FormSchema>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      prompt: '',
    },
  })

  function onSubmit(values: FormSchema) {
    if (values.prompt === '') return

    onEnter?.(values)
    form.reset()
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        <FormField
          control={form.control}
          name="prompt"
          render={({ field }) => (
            <FormItem className="x">
              <FormControl>
                <div className="relative h-11">
                  <Input className="h-11" placeholder="Type in your prompt..." {...field} />
                  <Button
                    type="submit"
                    variant="secondary"
                    size="icon"
                    className="absolute right-2 top-1/2 aspect-square h-8 w-8 -translate-y-1/2 transform"
                  >
                    <PaperPlaneIcon />
                  </Button>
                </div>
              </FormControl>
              <FormDescription>
                Lorem ipsum dolor sit amet consectetur adipisicing elit. Ut, deserunt?
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
      </form>
    </Form>
  )
}
