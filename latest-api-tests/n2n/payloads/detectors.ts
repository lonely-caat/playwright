type QueryType = 'REGEX' | 'MATCH';

export function detector(
  queryName: string,
  query: string,
  queryType: QueryType = 'REGEX',
  caseSensitive: boolean = true,
  enabled: boolean = true,
) {
  return {
    queryId: queryName,
    queryType: queryType,
    query: query,
    caseSensitive: caseSensitive,
    enabled: enabled,
  };
}


