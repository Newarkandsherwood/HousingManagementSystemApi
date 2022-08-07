variable "service_plan_name" {
  type = string
}

variable "resource_group_name" {
  type = string
}

variable "resource_group_location" {
  type = string
}

variable "app_service_name" {
  type = string
}

variable "authentication_identifier_production" {
  type      = string
  sensitive = true
}

variable "authentication_identifier_staging" {
  type      = string
  sensitive = true
}

variable "jwt_secret_production" {
  type      = string
  sensitive = true
}

variable "jwt_secret_staging" {
  type      = string
  sensitive = true
}

variable "sentry_dsn" {
  type      = string
  sensitive = true
}

variable "ancm_additional_error_page_link_production" {
  type = string
}

variable "ancm_additional_error_page_link_staging" {
  type = string
}

variable "azure_ad_tenant_id" {
  type      = string
  sensitive = true
}

variable "service_principal_id" {
  type      = string
  sensitive = true
}
