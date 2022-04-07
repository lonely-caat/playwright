#!/bin/bash

set -e

src_dir="./src"
charts_dir="$src_dir/charts/api-customer-summary"
manifests_dir="$charts_dir/manifests"

mkdir -p $manifests_dir

function the_end() {
    skaffold delete -f $src_dir/skaffold-dev.yaml
}
trap the_end SIGINT SIGTERM

rm -rf $manifests_dir/*
helm template --values $charts_dir/values-local.yaml --output-dir $manifests_dir $charts_dir/

skaffold dev --tail -v info -f $src_dir/skaffold-dev.yaml
