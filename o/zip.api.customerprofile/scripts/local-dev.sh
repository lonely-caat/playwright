#!/bin/bash

set -eu

src_dir="./"
charts_dir="$src_dir/charts/zip-api-customerprofile"
manifests_dir="$charts_dir/manifests"

mkdir -p $manifests_dir

function the_end() {
    skaffold delete -f $src_dir/skaffolds/skaffold-dev.yaml
}
trap the_end SIGINT SIGTERM

rm -rf $manifests_dir/*
helm template --values $charts_dir/values-local.yaml \
    --name-template=release-name \
    --set customerProfileDatabase.password=$CUSTOMERPROFILEDATABASE__PASSWORD \
    --output-dir $manifests_dir $charts_dir/

skaffold dev --tail -f $src_dir/skaffolds/skaffold-dev.yaml
