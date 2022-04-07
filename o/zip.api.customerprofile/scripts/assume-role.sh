#!/usr/bin/env sh

set -euo pipefail

AWS_ACCOUNT_ID=$1
AWS_ROLE=$2

if [ -z $AWS_ACCOUNT_ID ]; then
  echo "ERROR: AWS_ACCOUNT_ID is missing"
  exit 1
fi

if [ -z $AWS_ROLE ]; then
  echo "ERROR: AWS_ROLE is missing"
  exit 1
fi

unset AWS_SECRET_ACCESS_KEY
unset AWS_SECRET_KEY
unset AWS_SESSION_TOKEN

DURATION="${AWS_ROLE_DURATION:-3600}"
SESSION_NAME="${AWS_ROLE_SESSION_NAME:-`date +%s`}"

OUTPUT=`aws sts assume-role --role-arn "arn:aws:iam::$AWS_ACCOUNT_ID:role/$AWS_ROLE" \
                          --role-session-name "$SESSION_NAME" \
                          --duration-seconds $DURATION \
                          --output json`

CREDS=`echo $OUTPUT | jq .Credentials`

echo "export AWS_ACCESS_KEY_ID=`echo $CREDS | jq -r .AccessKeyId`
export AWS_SECRET_ACCESS_KEY=`echo $CREDS | jq -r .SecretAccessKey`
export AWS_SESSION_TOKEN=`echo $CREDS | jq -r .SessionToken`" > ./aws_credentials