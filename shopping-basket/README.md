# Shopping Basket API

shoppingbasket.azurewebsites.net

A micro-service that manages shopping baskets of customers. The service contains of a REST API which lets you do the following:

- Add products to the basket incl. the amount
- Remove products from the basket
- Retrieve the contents of the basket of a customer

Only products that are known in the system can be added to the basket. Each product has a unique identifier, a title and a price. A product also has stock, which is updated when a product is added or removed from the basket. Also, a product cannot be added to the basket when there is nothing in stock.

Customers are identified with a unique numeric identifier and the application must manage multiple shopping baskets: 1 basket of each customer.
