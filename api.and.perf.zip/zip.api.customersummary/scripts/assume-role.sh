#!/usr/bin/env bash

set -eo pipefail

AWS_ACCOUNT_ID=${1? "ERROR: AWS_ACCOUNT_ID is missing"}
AWS_ROLE=${2? "ERROR: AWS_ROLE is missing"}

if [ -z $3 ]; then
  PREFIX=''
else
  PREFIX="${3}_"
fi

set -u

DURATION="${AWS_ROLE_DURATION:-3600}"
SESSION_NAME="${AWS_ROLE_SESSION_NAME:-`date +%s`}"

OUTPUT=`aws sts assume-role --role-arn "arn:aws:iam::$AWS_ACCOUNT_ID:role/$AWS_ROLE" \
                          --role-session-name "$SESSION_NAME" \
                          --duration-seconds $DURATION \
                          --output json`

CREDS=`echo $OUTPUT | jq .Credentials`

echo "export ${PREFIX}AWS_ACCESS_KEY_ID=`echo $CREDS | jq -r .AccessKeyId`
export ${PREFIX}AWS_SECRET_ACCESS_KEY=`echo $CREDS | jq -r .SecretAccessKey`
export ${PREFIX}AWS_SESSION_TOKEN=`echo $CREDS | jq -r .SessionToken`"