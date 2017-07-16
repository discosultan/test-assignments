// We are using common.js require syntax to tell WebPack of dependencies.
require('./component/weatherPage.js');

// Define root module and its dependencies. All the app's components
// reside in their own modules for now. For a larger application, where
// module boundaries are better defined, it might make sense to group
// multiple components under a single module.
angular.module('app', ['app.component.weatherPage']);

// Additional config would go here or in its dedicated config file.