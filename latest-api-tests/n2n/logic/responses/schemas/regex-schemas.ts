import { z } from 'zod';

export function regexSchema() {
  return z.object({
    id: z.string(),
    regex: z.string(),
    name: z.string(),
    status: z.string(),
    deleted: z.boolean(),
    classification: z.string(),
    compliance: z.string(),
    distribution: z.string(),
    category: z.string(),
    subcategory: z.string(),
    createdAt: z.number(),
    tags: z.array(z.unknown()),
    enabled: z.boolean(),
    hidden: z.boolean(),
  });
}
