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

variable "cosmos_database_id" {
  type      = string
  sensitive = true
}

variable "cosmos_tenant_container_id" {
  type      = string
  sensitive = true
}

variable "cosmos_communal_staging_container_id" {
  type      = string
  sensitive = true
}

variable "cosmos_communal_production_container_id" {
  type      = string
  sensitive = true
}

variable "cosmos_leasehold_staging_container_id" {
  type      = string
  sensitive = true
}

variable "cosmos_leasehold_production_container_id" {
  type      = string
  sensitive = true
}

variable "cosmos_endpoint_url" {
  type      = string
  sensitive = true
}
variable "cosmos_authorization_key" {
  type      = string
  sensitive = true
}

variable "capita_url_staging" {
  type      = string
  sensitive = true
}

variable "capita_url_production" {
  type      = string
  sensitive = true
}

variable "capita_username_staging" {
  type      = string
  sensitive = true
}

variable "capita_username_production" {
  type      = string
  sensitive = true
}

variable "capita_password_staging" {
  type      = string
  sensitive = true
}

variable "capita_password_production" {
  type      = string
  sensitive = true
}

variable "capita_stdjobcode_staging" {
  type      = string
  sensitive = true
}

variable "capita_stdjobcode_production" {
  type      = string
  sensitive = true
}

variable "capita_source_staging" {
  type      = string
  sensitive = true
}

variable "capita_source_production" {
  type      = string
  sensitive = true
}

variable "capita_sublocation_staging" {
  type      = string
  sensitive = true
}

variable "capita_sublocation_production" {
  type      = string
  sensitive = true
}
