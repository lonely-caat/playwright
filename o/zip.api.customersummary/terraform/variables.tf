variable "app_name" {
  description = "The name of the application"
}

variable "env" {
  description = "The name of the environment that holds Docker images"
  default = "Common"
}

variable "environment" {
  description = "The name of the environment that holds terraform values"
}

variable "team" {
  description = "The name of the team deploying the application"
}

variable "region" {
  description = "Region of bucket and Docker Registry"
  default = "ap-southeast-2"
}

variable "oidc_id"{
  description = "OIDC id key for that particular enviornment"
}

variable "namespace"{
  description = "team namespace"
  default = "business-apps-and-support"
}