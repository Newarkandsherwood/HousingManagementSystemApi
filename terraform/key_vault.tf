resource "azurerm_key_vault" "hro-management-api-key-vault" {
  name                       = "hro-mgmt-api-key-vault"
  location                   = var.resource_group_location
  resource_group_name        = var.resource_group_name
  tenant_id                  = var.azure_ad_tenant_id
  soft_delete_retention_days = 7
  purge_protection_enabled   = false
  sku_name                   = "standard"
}

resource "azurerm_key_vault_access_policy" "hro-management-api-key-vault-access-policy" {
  key_vault_id = sensitive(azurerm_key_vault.hro-management-api-key-vault.id)
  tenant_id    = var.azure_ad_tenant_id
  object_id    = sensitive(azurerm_user_assigned_identity.hro-management-api-vault-access-identity.principal_id)

  secret_permissions = [
    "Get",
  ]
}

resource "azurerm_key_vault_secret" "sentry-dsn" {
  name         = "sentry-dsn"
  value        = var.sentry_dsn
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}
resource "azurerm_key_vault_secret" "cosmos-database-id" {
  name         = "cosmos-database-id"
  value        = var.sentry_dsn
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}
resource "azurerm_key_vault_secret" "cosmos-tenant-container-id" {
  name         = "cosmos-tenant-container-id"
  value        = var.sentry_dsn
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}
resource "azurerm_key_vault_secret" "cosmos-endpoint-url" {
  name         = "cosmos-endpoint-url"
  value        = var.sentry_dsn
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}
resource "azurerm_key_vault_secret" "cosmos-authorization-key" {
  name         = "cosmos-authorization-key"
  value        = var.sentry_dsn
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}


#---- Staging secrets
resource "azurerm_key_vault_secret" "ancm-additional-error-page-link-staging" {
  name         = "ancm-additional-error-page-link-staging"
  value        = var.ancm_additional_error_page_link_staging
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "authentication-identifier-staging" {
  name         = "authentication-identifier-staging"
  value        = var.authentication_identifier_staging
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "jwt-secret-staging" {
  name         = "jwt-secret-staging"
  value        = var.jwt_secret_staging
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

# #---- Production secrets
resource "azurerm_key_vault_secret" "ancm-additional-error-page-link-production" {
  name         = "ancm-additional-error-page-link-production"
  value        = var.ancm_additional_error_page_link_production
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "authentication-identifier-production" {
  name         = "authentication-identifier-production"
  value        = var.authentication_identifier_production
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "jwt-secret-production" {
  name         = "jwt-secret-production"
  value        = var.jwt_secret_production
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}
