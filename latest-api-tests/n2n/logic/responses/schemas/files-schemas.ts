import {z} from "zod";

export function filesSchema(){
  return z.object({id: z.string(),
    source: z.string(),
    path: z.string(),
    fileType: z.string(),
    regexHits: z.array(z.any()),
    createdAt: z.string(),
    lastModifiedAt: z.string(),
    contentLength: z.number(),
    formattedContentLength: z.string(),
    configurationId: z.string(),
    detectorHits: z.array(z.any()),
    permissions: z.array(z.any()),
    md5: z.string(),
    flow: z.string(),
    manual: z.boolean(),
    distributionTags: z.array(z.string()),
    complianceTags: z.array(z.string()),
    uIngestedTime: z.string()})
}

export function filesClassificationSchema(){
  return z.object({
    id: z.string(),
    source: z.string(),
    path: z.string(),
    category: z.string(),
    sensitive: z.boolean(),
    critical: z.boolean(),
    fileType: z.string(),
    pii: z.boolean(),
    subCategory: z.string(),
    regexHits: z.array(
        z.union([
          z.object({ "any string": z.number() }),
          z.object({ "any number": z.number() })
        ])
    ),
    createdAt: z.string(),
    lastModifiedAt: z.string(),
    classifierResult: z.number(),
    contentLength: z.number(),
    formattedContentLength: z.string(),
    configurationId: z.string(),
    subCategoryConfidence: z.number(),
    aip: z.string(),
    aipConfidence: z.number(),
    categoryConfidence: z.number(),
    signatures: z.string(),
    publicDataAttributes: z.array(z.unknown()),
    detectorHits: z.array(z.object({ queryId: z.string(), score: z.number() })),
    fileId: z.string(),
    permissions: z.array(
        z.object({
          trusteeId: z.string(),
          type: z.string(),
          loginName: z.string(),
          displayName: z.string(),
          roles: z.array(z.string())
        })
    ),
    keywords: z.object({}),
    md5: z.string(),
    flow: z.string(),
    manual: z.boolean(),
    distributionTags: z.array(z.unknown()),
    complianceTags: z.array(
        z.object({ name: z.string(), confidence: z.number() })
    ),
    uIngestedTime: z.string(),
    sourceSpecificLabels: z.array(z.unknown()),
    trustee_ids: z.array(z.unknown())
  })
}
