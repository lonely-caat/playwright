#!/bin/sh

case $TEST_ENV in
  dev) 
    echo "Environment to run tests on > dev"
    cp .env.dev .env
    ;;
  staging)
    echo "Environment to run tests on > staging"
    cp .env.staging .env
    ;;
  sandbox)
    echo "Environment to run tests on > sandbox"
    cp .env.sandbox .env
    ;;  
  *)
    echo "Environment not defined, so Environment to run tests on is sandbox"
    cp .env.sandbox .env
   # cp .env.dev .env
    ;;
esac
