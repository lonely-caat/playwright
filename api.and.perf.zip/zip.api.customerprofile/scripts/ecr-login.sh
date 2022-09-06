#!/usr/bin/env sh

aws configure set credential_source Ec2InstanceMetadata
aws configure set role_arn arn:aws:iam::${AWS_ACCOUNT_ID}:role/${AWS_ROLE}
aws ecr get-login --no-include-email \
		--region ${AWS_DEFAULT_REGION} --registry-ids ${AWS_ACCOUNT_ID} > ecrLoginCommand
chmod 755 ecrLoginCommand