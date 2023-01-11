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
    COSMOS_DATABASE_ID              = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cosmos-database-id.id})"
    COSMOS_TENANT_CONTAINER_ID      = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cosmos-tenant-container-id.id})"
    COSMOS_COMMUNAL_CONTAINER_ID    = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cosmos-communal-staging-container-id.id})"
    COSMOS_LEASEHOLD_CONTAINER_ID   = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cosmos-leasehold-staging-container-id.id})"
    COSMOS_ENDPOINT_URL             = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cosmos-endpoint-url.id})"
    COSMOS_AUTHORIZATION_KEY        = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cosmos-authorization-key.id})"
    CAPITA_URL                      = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.capita-url-staging.id})"
    CAPITA_USERNAME                 = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.capita-username-staging.id})"
    CAPITA_PASSWORD                 = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.capita-password-staging.id})"
    CAPITA_STDJOBCODE               = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.capita-stdjobcode-staging.id})"
    CAPITA_SOURCE                   = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.capita-source-staging.id})"
    CAPITA_SUBLOCATION              = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.capita-sublocation-staging.id})"
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
    COSMOS_DATABASE_ID              = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cosmos-database-id.id})"
    COSMOS_TENANT_CONTAINER_ID      = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cosmos-tenant-container-id.id})"
    COSMOS_COMMUNAL_CONTAINER_ID    = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cosmos-communal-production-container-id.id})"
    COSMOS_LEASEHOLD_CONTAINER_ID   = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cosmos-leasehold-production-container-id.id})"
    COSMOS_ENDPOINT_URL             = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cosmos-endpoint-url.id})"
    COSMOS_AUTHORIZATION_KEY        = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cosmos-authorization-key.id})"
    CAPITA_URL                      = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.capita-url-production.id})"
    CAPITA_USERNAME                 = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.capita-username-production.id})"
    CAPITA_PASSWORD                 = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.capita-password-production.id})"
    CAPITA_STDJOBCODE               = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.capita-stdjobcode-production.id})"
    CAPITA_SOURCE                   = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.capita-source-production.id})"
    CAPITA_SUBLOCATION              = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.capita-sublocation-production.id})"
  }
}
