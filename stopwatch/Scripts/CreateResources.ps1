. .\config.ps1

# Create an Azure Resource Group.
# All the following resources will be created within that group.
az group create -l $Location -n $ResourceGroup

# Create a Storage Account.
az storage account create -g $ResourceGroup -n $StorageAccount -l $Location --sku $StorageAccountType

# Create an App Service plan.
az appservice plan create --name $AppServicePlan --resource-group $ResourceGroup --sku $AppServicePlanType
# Create a web app using that plan.
az appservice web create --name $AppService --resource-group $ResourceGroup --plan $AppServicePlan