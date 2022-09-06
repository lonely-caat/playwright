#Helm Options
RELEASE_NAME=vault-db-pass
CHART_NAME=./rds_helm
CHART_VERSION=0.1.0
HELM_TILLER_SILENT=true #don't log password in helm console output

#Rds creds vars
AWS_ROLE=zip-role-ci-apps
AWS_DEFAULT_REGION= ap-southeast-2
VAULT_ADDRESS=https://vault.internal.mgmt.au.edge.zip.co
TF_VAR_NAMESPACE=customer-acquisition
TF_VAR_DATABASE_USERNAME=postgres
TF_VAR_DATABASE_NAME = "zip-services-customerprofile-$(ENVIRONMENT)"

ifdef CI
	ASSUME_REQUIRED?=assumeRoleRds
endif

setPassword: .env kubeConfig
	docker-compose run --rm envvars ensure --tags aws,dbpassword
	# Wait on database availability
	docker-compose run --rm aws sh -c 'aws rds wait db-instance-available --db-instance-identifier $(TF_VAR_DATABASE_NAME)'
	# Generate random password
	docker-compose run --rm aws sh -c 'sed -i "s/DATABASE_PASSWORD/DATABASE_PASSWORD=$$(cat /dev/urandom | tr -dc _A-Z-a-z-0-9 | head -c40)/g" .env'
	# Set password in AWS
	docker-compose run --rm kubernetes sh -c 'aws rds modify-db-instance --db-instance-identifier $(TF_VAR_DATABASE_NAME) --master-user-password $$DATABASE_PASSWORD --apply-immediately'
	# Set password in Vault
	docker-compose run --rm kubernetes sh -c 'helm tiller run $(TF_VAR_NAMESPACE) -- helm upgrade --atomic --install $(RELEASE_NAME) --namespace=$(TF_VAR_NAMESPACE) --version=$(CHART_VERSION) $(CHART_NAME) -f rds_vault_values.yaml --set vault.address=$$VAULT_ADDRESS --set vault.secret.password=$$DATABASE_PASSWORD --set vault.secret.username=$$TF_VAR_DATABASE_USERNAME --set vault.secret.location=secret/$$EKS_CLUSTER_NAME/$(TF_VAR_NAMESPACE)/rds/$$TF_VAR_DATABASE_NAME'
	docker-compose run --rm kubernetes kubectl -n $(TF_VAR_NAMESPACE) wait --for=condition=complete job/$(RELEASE_NAME)-dbpass
	docker-compose run --rm kubernetes sh -c 'helm tiller run $(TF_VAR_NAMESPACE) -- helm delete $(RELEASE_NAME) --purge'
.PHONY: setPassword

deleteFailedVaultChart: .env kubeConfig
	docker-compose run --rm kubernetes sh -c 'helm tiller run $(TF_VAR_NAMESPACE) -- helm delete $(RELEASE_NAME) --purge'
.PHONY: deleteFailedVaultChart

kubeConfig: .env $(ASSUME_REQUIRED)
	docker-compose run --rm envvars ensure --tags aws
	docker-compose run --rm kubernetes sh -c 'aws eks update-kubeconfig --name $${EKS_CLUSTER_NAME}'
.PHONY: kubeConfig

.env: envvars.yml
	#.env needs to exist for docker-compose to run anything
	touch .env
	docker-compose run --rm envvars validate
	docker-compose run --rm envvars envfile --overwrite
	docker-compose run --rm aws sh -c 'cat env-$(ENVIRONMENT) >> .env \
		&& printf "\nAWS_ROLE=$(AWS_ROLE)" >> .env \
		&& printf "\nAWS_DEFAULT_REGION=$(AWS_DEFAULT_REGION)" >> .env \
		&& printf "\nVAULT_ADDRESS=$(VAULT_ADDRESS)" >> .env \
		&& printf "\nHELM_TILLER_SILENT=$(HELM_TILLER_SILENT)" >> .env \
		&& printf "\nTF_VAR_NAMESPACE=$(TF_VAR_NAMESPACE)" >> .env \
		&& printf "\nTF_VAR_DATABASE_USERNAME=$(TF_VAR_DATABASE_USERNAME)" >> .env \
		&& printf "\nTF_VAR_DATABASE_NAME=$(TF_VAR_DATABASE_NAME)" >> .env'

assumeRoleRds: .env
	docker-compose run --rm envvars ensure --tags assume-role
	docker-compose run --rm aws sh -c '/opt/scripts/assume-role.sh >> .env'
