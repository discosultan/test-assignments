## About

CGI's assignment where the assignee is required to build a web API and an front end SPA for a movie portal. This repository only includes the front end part of the assignment implemented in Angular.

## Setup

1. Install latest [NodeJS](https://nodejs.org/en/)

2. From the project folder, execute the following command:
 ```
 npm install
 ```
 This will install all required dependencies, including a local version of Webpack that is going to build and bundle the app.

3. To run the app execute the following command:
 ```
 npm start
 ```
 This command starts the Webpack development server that serves the built bundles. You can now browse the app at http://localhost:8080. Changes in the code will automatically build and reload the app.

----

## Assignment

Code a movie database site

----

Technologies used: ASP.NET MVC, AngularJS, Foundation 6 CSS Framework

Make it an SPA (Single Page Application). Data is fetched from server API in JSON format and bound to Angular templates.
Views are styled using Foundation6 - responsive CSS framework.

Design the application incrementally. Start by creting read-only responsive views of movie list and details and
data API for read-only access.

### Iteration1

#### Views

----

1. List of movies:
  - Display title, genre, year, rating
  - Add search to search movies by title. Do simple search first and then see if this could be done as autocomplete
  - Add multi select control to filter movies by certain category. One should be able to filter movies by multiple categories at the same time.

2. Movie details
  - Display movie title, year, rating and description


#### Data and business logic

----

1. Create classes for Movie [id (int), title, description, rating (int), category id], Movies list and Category (id, name)
2. Create movies Repository class:
   - method to returned fixed (hard coded) list of movies
   - method to return movie by id (from the hard coded list)
3. Create movie service class:
   - list of movies (return list of movies from Repository class method)
   - details of selected movie (return from movie repository by id)
5. Create MVC Controller for returning list of movies and movie details (using movie service class) in json format:
   - API method to return list of movies
   - API method to return movie details

#### Tests

1. Create tests for movie service methods: list of movies and movie details


The reason for having repository and service classes is for easy replacement of repository implementation in further iterations
and making it possible to keep all business logic in service layer independent of data.

### API

- http://40.113.15.185:3000/movies
- http://40.113.15.185:3000/movies/[id]
- http://40.113.15.185:3000/categories
