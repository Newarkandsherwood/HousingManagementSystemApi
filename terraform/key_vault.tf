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
  value        = var.cosmos_database_id
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}
resource "azurerm_key_vault_secret" "cosmos-tenant-container-id" {
  name         = "cosmos-tenant-container-id"
  value        = var.cosmos_tenant_container_id
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}
resource "azurerm_key_vault_secret" "cosmos-endpoint-url" {
  name         = "cosmos-endpoint-url"
  value        = var.cosmos_endpoint_url
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}
resource "azurerm_key_vault_secret" "cosmos-authorization-key" {
  name         = "cosmos-authorization-key"
  value        = var.cosmos_authorization_key
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

resource "azurerm_key_vault_secret" "cosmos-communal-staging-container-id" {
  name         = "cosmos-communal-staging-container-id"
  value        = var.cosmos_communal_staging_container_id
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "cosmos-leasehold-staging-container-id" {
  name         = "cosmos-leasehold-staging-container-id"
  value        = var.cosmos_leasehold_staging_container_id
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "capita-url-staging" {
    name         = "capita-url-staging"
    value        = var.capita_url_staging
    key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "capita-username-staging" {
  name         = "capita-username-staging"
  value        = var.capita_username_staging
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "capita-password-staging" {
  name         = "capita-password-staging"
  value        = var.capita_password_staging
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "capita-stdjobcode-staging" {
  name         = "capita-stdjobcode-staging"
  value        = var.capita_stdjobcode_staging
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "capita-source-staging" {
  name         = "capita-source-staging"
  value        = var.capita_source_staging
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "capita-sublocation-staging" {
  name         = "capita-sublocation-staging"
  value        = var.capita_sublocation_staging
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

resource "azurerm_key_vault_secret" "cosmos-communal-production-container-id" {
  name         = "cosmos-communal-production-container-id"
  value        = var.cosmos_communal_production_container_id
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "cosmos-leasehold-production-container-id" {
  name         = "cosmos-leasehold-production-container-id"
  value        = var.cosmos_leasehold_production_container_id
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "capita-url-production" {
  name         = "capita-url-production"
  value        = var.capita_url_production
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "capita-username-production" {
  name         = "capita-username-production"
  value        = var.capita_username_production
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "capita-password-production" {
  name         = "capita-password-production"
  value        = var.capita_password_production
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "capita-stdjobcode-production" {
  name         = "capita-stdjobcode-production"
  value        = var.capita_stdjobcode_production
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "capita-source-production" {
  name         = "capita-source-production"
  value        = var.capita_source_production
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}

resource "azurerm_key_vault_secret" "capita-sublocation-production" {
  name         = "capita-sublocation-production"
  value        = var.capita_sublocation_production
  key_vault_id = azurerm_key_vault.hro-management-api-key-vault.id
}
