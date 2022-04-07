include env
include rds_creds.mk
-include aws_creds.mk

build:
	docker build \
		--cache-from $(ARTIFACTORY_DOCKER_REPOSITORY)/$(APP_NAME):latest \
		--build-arg ARTIFACTORY_USER=$(ARTIFACTORY_USER) --build-arg ARTIFACTORY_PASS=$(ARTIFACTORY_PASS) -t $(DOCKER_IMAGE) .
	
test:
	chmod +x scripts/ci-test.sh
	docker run -i \
	--env ARTIFACTORY_PASS=$(ARTIFACTORY_PASS) \
	--env ARTIFACTORY_USER=$(ARTIFACTORY_USER) \
	--env CI_PROJECT_NAME="$(CI_PROJECT_NAME)" \
	--env SONARQUBE_USERNAME="$(SONARQUBE_USERNAME)" \
	--env SONARQUBE_PASSWORD="$(SONARQUBE_PASSWORD)" \
	-v $(PWD):/app -w /app mcr.microsoft.com/dotnet/core/sdk:3.1 scripts/ci-test.sh
.PHONY: test

artifactDir:
	mkdir -p $(ARTIFACT_DIR)
	
outputDir:
	mkdir -p $(HELM_OUTPUT_DIR)

image: build artifactDir
	docker save -o $(TARBALL) $(DOCKER_IMAGE)

aws_creds.mk: aws_credentials
	sed 's/"//g ; s/=/:=/' < $< > $@

assumeRoleInAwsCli:
	chmod +x ./scripts/assume-role.sh
	docker run -i --env-file env --env AWS_ACCOUNT_ID=$(MANAGEMENT_AWS_ACCOUNT_ID) \
	--entrypoint="" zipau-docker.jfrog.io/cmdlabs/aws-utils:1.0.2 /opt/scripts/assume-role.sh > aws_credentials

deployinatorPull: artifactoryDockerLogin
	docker pull $(DEPLOYINATOR_ARTIFACTORY_IMAGE)
	mkdir -p $(ARTIFACT_DIR)
	docker save -o $(DEPLOYINATOR_TARBALL) $(DEPLOYINATOR_ARTIFACTORY_IMAGE)
	
deployinatorImage: deployinatorPull
	docker load -i $(DEPLOYINATOR_TARBALL)

assumeRole:
	chmod +x ./scripts/assume-role.sh
	./scripts/assume-role.sh $(AWS_ACCOUNT_ID) $(AWS_ROLE)

assumeRoleInDeployinator: deployinatorImage
	docker run --env-file env -v $(PWD):/app $(DEPLOYINATOR_ARTIFACTORY_IMAGE) assumeRole AWS_ACCOUNT_ID=$(MANAGEMENT_AWS_ACCOUNT_ID)

assumeRoleInDeployinatorForDeployment: deployinatorImage
	docker run --env-file env --env-file env-$(ENVIRONMENT) -v $(PWD):/app $(DEPLOYINATOR_ARTIFACTORY_IMAGE) assumeRole

artifactoryDockerLogin:
	jfrog rt config --url=$(ARTIFACTORY_URL) --user=$(ARTIFACTORY_USER) --password=$(ARTIFACTORY_PASS)
	docker login -u $(ARTIFACTORY_USER) -p $(ARTIFACTORY_PASS) $(ARTIFACTORY_DOCKER_REPOSITORY)
.PHONY: artifactoryDockerLogin

publish: image artifactoryDockerLogin
	jfrog rt config --url=$(ARTIFACTORY_URL) --user=$(ARTIFACTORY_USER) --password=$(ARTIFACTORY_PASS)
	jfrog rt docker-push $(DOCKER_IMAGE) $(DOCKER_REPOSITORY_KEY) --build-name=$(APP_NAME) --build-number=$(CI_COMMIT_SHORT_SHA)
	jfrog rt build-collect-env $(APP_NAME) $(CI_COMMIT_SHORT_SHA)
	jfrog rt build-publish $(APP_NAME) $(CI_COMMIT_SHORT_SHA)
	
helmInit: outputDir
	helm init --client-only --stable-repo-url https://charts.helm.sh/stable/
	
helmTemplate: helmInit
	helm template -f $(HELM_DIR)/$(HELM_TEMPLATE_VALUES) \
	--set-string image.repository=$(IMAGE_NAME),image.tag=$(IMG_TAG_COMMIT_SHORT_SHA),environment=$(ENVIRONMENT),\
	appVersion=$(IMG_TAG_COMMIT_SHORT_SHA)-$(PATCH_NUMBER) \
	--output-dir $(HELM_OUTPUT_DIR) $(HELM_DIR)
	
kubeconfig: helmTemplate
	aws eks update-kubeconfig --name $(EKS_CLUSTER_NAME)
	
namespace: kubeconfig
	kubectl config set-context --current --namespace=$(NAMESPACE)
	kubectl delete secret zip-api-newrelic-license-key || true
	kubectl create secret generic zip-api-newrelic-license-key --from-literal=newRelicLicenseKey=$(NEW_RELIC_LICENSE_KEY) || true

kubectlApply: namespace
	kubectl apply -f $(K8S_MANIFEST_DIR)/service.yaml \
	-f $(K8S_MANIFEST_DIR)/deployment.yaml \
	-f $(K8S_MANIFEST_DIR)/ingress.yaml \
	-f $(K8S_MANIFEST_DIR)/auto-scaling.yaml

deploymentCheck: kubectlApply
	kubectl -n $(NAMESPACE) rollout status deployment/release-name-$(APP_NAME) -v4 || \
		(kubectl -n $(NAMESPACE) rollout undo deployment/release-name-$(APP_NAME) && \
		 echo 'Failed to deploy latest version, hence rolling back to previous version'; exit 1)

deploy: deployinatorImage
	docker run --env-file env \
	--env-file env-$(ENVIRONMENT) \
	--env NAMESPACE=$(K8S_NAMESPACE) \
	--env IMG_TAG_COMMIT_SHORT_SHA=$(CI_COMMIT_SHORT_SHA) \
	--env ENVIRONMENT=$(ENVIRONMENT) \
	--env NEW_RELIC_LICENSE_KEY=$($(CI_NEW_RELIC_LICENSE_KEY_TYPE)) \
	--env PATCH_NUMBER=$(PATCH_NUMBER) \
	-v $(PWD):/app -w /app $(DEPLOYINATOR_ARTIFACTORY_IMAGE) deploymentCheck

smokeTest:		
	docker run --env TEST_URL=$(TEST_URL) \
	-v $(PWD):/work:Z -w /work/test \
	$(ARTIFACTORY_DOCKER_REPOSITORY)/python:3.6-alpine sh -c 'pip -q install tox && tox'

kubectlDelete: namespace
	kubectl delete -f $(K8S_MANIFEST_DIR)

undeploy: deployinatorImage
	docker run --env-file env \
	--env-file env-$(ENVIRONMENT) \
	--env NAMESPACE=$(K8S_NAMESPACE) \
	-v $(PWD):/app -w /app $(DEPLOYINATOR_ARTIFACTORY_IMAGE) kubectlDelete

perfTest:
	docker run --rm -v $(PWD)/tests/perf:/bzt-configs blazemeter/taurus taurus.yml