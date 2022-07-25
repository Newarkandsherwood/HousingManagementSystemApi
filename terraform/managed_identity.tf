resource "azurerm_user_assigned_identity" "example" {
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location

  name = "search-api"
}


resource "azurerm_role_assignment" "example" {
  name               = var.app_service_name
  scope              = var.service_principal_id
  delegated_managed_identity_resource_id = "id"
  principal_id       = var.service_principal_id
}
