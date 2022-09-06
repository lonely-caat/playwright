FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-env
WORKDIR /app

ARG ARTIFACTORY_USER
ARG ARTIFACTORY_PASS

# Copy csproj and restore as distinct layers
COPY ./test ./test
COPY ./src ./src
# Create a temp nuget.config with credentials injected
RUN sed -r "s/PACKAGESUSERNAME/$ARTIFACTORY_USER/" ./src/NuGet.config \
	| sed -r "s/PACKAGESPASSWORD/$ARTIFACTORY_PASS/" > ./src/nuget.ci.build.config

RUN dotnet restore --configfile ./src/nuget.ci.build.config ./src/Zip.Api.CustomerProfile.sln
RUN rm -rf ./src/nuget.ci.build.config

# Copy everything else and build
COPY . ./
RUN cd src/Zip.Api.CustomerProfile && dotnet publish -c Release -api.and.perf.zip out --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
RUN addgroup dotnet-users
RUN adduser -S -u 1000 -G dotnet-users dotnet
RUN chown :dotnet-users /usr/bin/dotnet

#newrelic related settings
ENV CORECLR_ENABLE_PROFILING=1
ENV CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A}
ENV CORECLR_PROFILER_PATH=/app/src/newrelic/libNewRelicProfiler.so
ENV CORECLR_NEWRELIC_HOME=/app/src/newrelic
ENV NEW_RELIC_DISTRIBUTED_TRACING_ENABLED=true

WORKDIR /app
COPY --from=build-env /app .
COPY --from=build-env ./app/src/Zip.Api.CustomerProfile/out ./src
RUN chown -R dotnet /app

USER 1000
ENV COMPlus_EnableDiagnostics 0

WORKDIR /app/src

ENV ASPNETCORE_URLS=http://*:5000
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 5000
ENTRYPOINT ["dotnet", "Zip.Api.CustomerProfile.dll"]
