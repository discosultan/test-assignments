# Pronoodle.Products 📦

Products backoffice for PRONOODLE INC.

## Requirements

- .NET Core SDK 2.0
- Latest Chrome, Firefox or Edge (IE not supported!)

## Solution overview

- **src**
  - **Pronoodle.Products** - Core library including business logic around products.
  - **Pronoodle.Products.Web** - Web interface for importing and viewing products. Communication is happening over websockets. Each file upload is performed on a dedicated socket. Product overview is live and streamed over another dedicated socket. Reconnection is not implemented. Number of entries displayed on the UI is limited and there is no way to run out memory there through large CSV files. Server uses an in-memory product repository and therefore can run out of memory.
- **tests**
  - **Pronoodle.Products.Integration** - Integration tests test happy flow for each websocket endpoint.

## Getting started

1. Start **Pronoodle.Products.Web** server. Default port is 5000.

2. In browser, navigate to `http://localhost:5000/`.

3. Try import a products `.csv` file (sample found in repository root).

## Assignment description

We have received the attached csv file from a customer; a company in the fashion industry. The customer has an ERP system where he manages products for his store. The ERP system does not have a webshop feature and can only export all data in it to a csv file.

The customer asks us to build a webshop with the feature to import the csv file. This is the first time the customer sends us the csv file with product data. The customer tells us that he changed a few prices in the csv file because those prices changed just after he made an export.

The file the customer has sent only contains a subset of all products in the ERP system. The full list of products is over 100 MB in size and it should be possible to import this complete file.

Build an MVC web application with the following features:

- The customer has to be able to import the csv file using the web interface.

- Analyze the data in the csv file and create a logical data model based upon your analysis.

- Create an overview page with all the products from the imported csv(s) listed.
