FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-env
WORKDIR /app

ARG ARTIFACTORY_USER
ARG ARTIFACTORY_PASS

# Copy csproj and restore as distinct layers
COPY . .
COPY nuget.config ./Zip.Api.CustomerSummary.Api/nuget.config

WORKDIR /app/Zip.Api.CustomerSummary.Api
RUN dotnet add package NewRelic.Agent
RUN dotnet restore

# Copy everything else and build
RUN dotnet publish -c Release -api.and.perf.zip out

WORKDIR /app
# Build runtime image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN addgroup -g 1000 dotnet-users
RUN adduser --system -u 1000 -G dotnet-users dotnet
RUN chown :dotnet-users /usr/bin/dotnet

WORKDIR /app
COPY --from=build-env /app/Zip.Api.CustomerSummary.Api/out .
RUN chown -R dotnet /app

USER 1000
ENV COMPlus_EnableDiagnostics 0
ENV ASPNETCORE_URLS=http://*:5000

## NEW RELIC
ENV CORECLR_ENABLE_PROFILING=1
ENV CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A}
ENV CORECLR_PROFILER_PATH=/app/newrelic/libNewRelicProfiler.so
ENV CORECLR_NEWRELIC_HOME=/app/newrelic
ENV NEW_RELIC_DISTRIBUTED_TRACING_ENABLED=true
ENV TZ=Australia/Sydney
## NEW RELIC

EXPOSE 5000
ENTRYPOINT ["dotnet", "Zip.Api.CustomerSummary.Api.dll"]