## Assignment

### Things we look for in this test

We expect the candidate to show us her/his skills in Design/Architecture, ability to build testable and
maintainable software using industry best practices.

### Description

Create a .NET REST API serving the domain of Customers and Orders.
The code should have reasonable Unit-tests coverage.

### Domain

Customer has Name and Email.
Customer can have multiple Orders.
Order can belong to only one Customer.
Order has at least two fields: Price and CreatedDate.

### API

The application should at least expose these endpoints which do the following:
- Return a list of customers, without orders;
- Return a customer and his orders;
- Add a new Order for an existing Customer;
- Add a new Customer.

### Persistence

You can use the database of your choice, like mssql, postgres, sqlite, mongodb etc.

### Source Code

You should create a private Git repository on https://bitbucket.org/ and give the user
https://bitbucket.org/albumprinter read access to the repository.

We prefer to see that the candidate can use source control systems, so please make sure you’re not
pushing everything in a single commit, but having small atomic steps.

### Tools and libraries

You are free to use any additional third-party libraries and frameworks.

### Additional information

The authentication/authorization of the API is outside of scope.
Feel free to improve the application as you see fit.
If you have any questions please reach out Vasili.

### Final Notes

Simple is better than complex - complex is better than complicated. Also, readability counts.
If the implementation is hard to explain, it's a bad idea.
If the implementation is easy to explain, it may be a good idea.
Finally, please keep in mind that this assignment is a starting point of discussion, not a destination and
please ensure that the assignment works and compiles before sending it for a review.
