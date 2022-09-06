#!/usr/bin/env bash

set -e

DIR_NAME=${1:-"/app"}

apt-get update
apt-get install -y openjdk-11-jre-headless
apt-get clean

dotnet tool install --global dotnet-sonarscanner --version 4.8.0
dotnet tool install --global dotnet-reportgenerator-globaltool
export PATH="$PATH:/root/.dotnet/tools"

mkdir -p ${DIR_NAME}/TestResults || true
echo "{}" > ${DIR_NAME}/TestResults/coverage.json
export CoverletOutput=${DIR_NAME}/TestResults/converage.json
export TestResultFolder=${DIR_NAME}/TestResults
dotnet sonarscanner begin /k:"zip.api.customersummary" /n:"Zip.Api.CustomerSummary" /v:"1.0.0" \
 /d:sonar.host.url="https://sonar.internal.mgmt.au.edge.zip.co" \
 /d:sonar.login="e8de299be9446eef09e7d4dd79ab81ecc4bd6a0d" \
 /d:sonar.language="cs" \
 /d:sonar.exclusions="test/**/*" \
 /d:sonar.coverage.exclusions="test/**/*" \
 /d:sonar.cs.opencover.reportsPaths=$CoverletOutput \
 /d:sonar.verbose=false
dotnet restore
dotnet build
dotnet test Zip.Api.CustomerSummary.sln -l=trx -r $TestResultFolder \
 /p:CollectCoverage=true \
 /p:Exclude=[xunit.*]* \
 /p:CoverletOutputFormat=json \
 /p:MergeWith=$CoverletOutput \
 /p:CoverletOutput=$CoverletOutput || true
dotnet sonarscanner end /d:sonar.login="e8de299be9446eef09e7d4dd79ab81ecc4bd6a0d"
