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
  type = string
}

variable "authentication_identifier_staging" {
  type = string
}

variable "jwt_secret_production" {
  type = string
}

variable "jwt_secret_staging" {
  type = string
}

variable "sentry_dsn" {
  type = string
}

variable "ancm_additional_error_page_link_production" {
  type = string
}

variable "ancm_additional_error_page_link_staging" {
  type = string
}
