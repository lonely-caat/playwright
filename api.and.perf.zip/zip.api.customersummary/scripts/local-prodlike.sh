#!/bin/bash

set -e

src_dir="./src"
charts_dir="$src_dir/charts/api-customer-summary"
manifests_dir="$charts_dir/manifests"

function the_end() {
    skaffold delete -f $src_dir/skaffold-run.yaml
}
trap the_end SIGINT SIGTERM

rm -rf $manifests_dir/*
helm template --values $charts_dir/values-prodlike.yaml --output-dir $manifests_dir $charts_dir/

skaffold run --tail -f $src_dir/skaffold-run.yaml
