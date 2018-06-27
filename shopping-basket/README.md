# Shopping Basket API

## The assignment

Build a micro-service that manages shopping baskets of customers. The service contains of a REST API which lets you do the following:

- Add products to the basket incl. the amount
- Remove products from the basket
- Retrieve the contents of the basket of a customer

Only products that are known in the system can be added to the basket. Each product has a unique identifier, a title and a price. A product also has stock, which is updated when a product is added or removed from the basket. Also, a product cannot be added to the basket when there is nothing in stock.

Customers are identified with a unique numeric identifier and the application must manage multiple shopping baskets: 1 basket of each customer.

The application only has a REST API, which can be accessed by using Postman (https://www.getpostman.com) or another (command-line) HTTP client tool. There is no need for a GUI!

Create some unit-tests per actor which will test the happy flow, but also an error situation if possible.

## Guidelines

It is mandatory to create the solution using .NET Core (preferred) or .NET Framework, using the C# programming language. Use ASP.NET to build a REST API and use Akka.NET (http://getakka.net) for most of the program logic (such as business rules and state). Usage of other frameworks next to the above is optional.

There is no need for a database for state persistence. All state will be in-memory and will be lost when the application has stopped.

## Other guidelines

- The content-type of the data that is send to and returned from the API is ‘application/json’.
- API endpoints must be setup following the RESTful guidelines including using the correct method verbs and return status codes.
- Identifying the customer can be done in the path or querystring.
- Create a BasketActor for the business logic of added, retrieving and deleting products from the shopping basket. This actor contains the in-memory state of the shopping baskets of all customers.
- Create a ProductActor for all business logic around managing products. This actor contains a list of the products and their stock-level. This list of products will be hard-coded and set as state at the start of the application.
- When adding/removing products to/from the shopping basket, the BasketActor must inform the ProductActor that the stock level must be updated. The ProductActor must decline the request when stock level is 0, when adding a product to the shopping basket.
- Use the Akka Testkit for unit-tests.
