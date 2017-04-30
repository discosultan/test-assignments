. .\config.ps1

# Prints a connection string for the Storage Account.
# This should be set to app configuration.
az storage account show-connection-string -g $ResourceGroup -n $StorageAccount