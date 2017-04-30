# Shared variables among all the scripts.

$Location = "westeurope"

$ResourceGroup = "stopwatch"

$StorageAccount = "stopwatch"
$StorageAccountType = "Standard_LRS"

$AppServicePlan = "stopwatch"
$AppServicePlanType = "FREE"
$AppService = "stopwatch$(Get-Random)" # Make sure this is unique!