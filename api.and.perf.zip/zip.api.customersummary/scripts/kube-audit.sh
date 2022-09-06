#!/usr/bin/env sh

set -e

kubeaudit all -f ${K8S_MANIFEST_DIR}/deployment.yaml 2> output

if grep -q error output; then
    echo "ERROR"
    cat output
    rm -rf output
    exit 1
fi

if grep -q warning output; then
    echo "WARNING"
    cat output
    rm -rf output
    exit 0
fi