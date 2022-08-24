data "azurerm_service_plan" "hro-app-service-plan" {
  name                = var.service_plan_name
  resource_group_name = var.resource_group_name
}

resource "azurerm_windows_web_app_slot" "hro-management-api-staging-slot" {
  name           = "staging"
  app_service_id = azurerm_windows_web_app.hro-management-api.id
  https_only     = true

  app_settings = {
    ANCM_ADDITIONAL_ERROR_PAGE_LINK = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.ancm-additional-error-page-link-staging.id})"
    ASPNETCORE_ENVIRONMENT          = "Staging"
    AUTHENTICATION_IDENTIFIER       = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.authentication-identifier-staging.id})"
    JWT_SECRET                      = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.jwt-secret-staging.id})"
    SENTRY_DSN                      = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.sentry-dsn.id})"
  }

  identity {
    type         = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.hro-management-api-vault-access-identity.id]
  }

  key_vault_reference_identity_id = azurerm_user_assigned_identity.hro-management-api-vault-access-identity.id

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

  identity {
    type         = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.hro-management-api-vault-access-identity.id]
  }

  key_vault_reference_identity_id = azurerm_user_assigned_identity.hro-management-api-vault-access-identity.id

  app_settings = {
    ASPNETCORE_ENVIRONMENT          = "Production"
    ANCM_ADDITIONAL_ERROR_PAGE_LINK = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.ancm-additional-error-page-link-production.id})"
    AUTHENTICATION_IDENTIFIER       = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.authentication-identifier-production.id})"
    JWT_SECRET                      = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.jwt-secret-production.id})"
    SENTRY_DSN                      = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.sentry-dsn.id})"
  }
}
