import { z } from "zod"

export const classificationSchema = z.object({
    id: z.number(),
    name: z.string(),
    tags: z.array(
        z.object({
            id: z.number(),
            name: z.string(),
            sensitivity: z.string(),
            aipTag: z.array(z.string())
        })
    ),
    sensitivity: z.string()
})
