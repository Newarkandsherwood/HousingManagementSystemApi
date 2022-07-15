data "azurerm_service_plan" "hro-app-service-plan" {
  name                = var.service_plan_name
  resource_group_name = var.resource_group_name
}

resource "azurerm_windows_web_app_slot" "hro-management-api-staging-slot" {
  name           = "staging"
  app_service_id = azurerm_windows_web_app.hro-management-api.id
  https_only     = true

  app_settings = {
    ANCM_ADDITIONAL_ERROR_PAGE_LINK = var.ancm_additional_error_page_link_staging
    ASPNETCORE_ENVIRONMENT          = "Staging"
    AUTHENTICATION_IDENTIFIER       = var.authentication_identifier_staging
    JWT_SECRET                      = var.jwt_secret_staging
    SENTRY_DSN                      = var.sentry_dsn
  }
  auth_settings {
    enabled = false
  }

  site_config {
    health_check_path = "/health"
  }
}

resource "azurerm_windows_web_app" "hro-management-api" {
  name                = var.app_service_name
  location            = var.resource_group_location
  resource_group_name = var.resource_group_name
  service_plan_id     = data.azurerm_service_plan.hro-app-service-plan.id
  https_only          = true

  auth_settings {
    enabled = false
  }

  site_config {
    health_check_path = "/health"
  }

  app_settings = {
    ASPNETCORE_ENVIRONMENT          = "Production"
    ANCM_ADDITIONAL_ERROR_PAGE_LINK = var.ancm_additional_error_page_link_production
    AUTHENTICATION_IDENTIFIER       = var.authentication_identifier_production
    JWT_SECRET                      = var.jwt_secret_production
    SENTRY_DSN                      = var.sentry_dsn
  }
}
