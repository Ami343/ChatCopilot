import * as z from 'zod'

const schema = z.object({
  chatSessionId: z.string(),
  input: z.string(),
})

export async function POST(request: Request) {
  /**
   * TODO: Implement streaming the chat response: https://nextjs.org/docs/app/building-your-application/routing/route-handlers#streaming
   */
}
