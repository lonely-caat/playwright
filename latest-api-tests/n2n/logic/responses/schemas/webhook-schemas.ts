import { z } from "zod"

export const webhookSchema = z.object({
    totalCount: z.number(),
    webhooks: z.array(
        z.object({
            id: z.string(),
            status: z.null(),
            createdAt: z.number(),
            queryStr: z.null(),
            updatedAt: z.number(),
            name: z.string(),
            gql: z.string(),
            url: z.string(),
            active: z.boolean()
        })
    )
})
