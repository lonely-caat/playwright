#!/usr/bin/env bash

set -e

DIR_NAME=${1:-"/app"}

apt-get update
apt-get install -y openjdk-11-jre-headless
apt-get clean

dotnet tool install --global dotnet-sonarscanner --version 4.8.0
export PATH="$PATH:/root/.dotnet/tools"

dotnet sonarscanner begin /key:"${CI_PROJECT_NAME}" /d:sonar.host.url="https://sonar.internal.mgmt.au.edge.zip.co" /d:sonar.login="${SONARQUBE_USERNAME}" /d:sonar.password="${SONARQUBE_PASSWORD}" /d:sonar.cs.opencover.reportsPaths="${DIR_NAME}/coverage.opencover.xml"

sed -i -e 's/PACKAGESUSERNAME/'${ARTIFACTORY_USER}'/g' -e 's/PACKAGESPASSWORD/'${ARTIFACTORY_PASS}'/g' ./src/NuGet.config
dotnet restore ./src/Zip.Api.CustomerProfile.sln --configfile ./src/NuGet.config

dotnet test src/Zip.Api.CustomerProfile.sln --logger "junit;LogFilePath=${DIR_NAME}/results.xml" /p:CollectCoverage=true /p:Exclude=[xunit.*]* /p:CoverletOutputFormat=opencover /p:CoverletOutput="${DIR_NAME}/coverage.opencover.xml"
dotnet sonarscanner end /d:sonar.login="${SONARQUBE_USERNAME}" /d:sonar.password="${SONARQUBE_PASSWORD}"
