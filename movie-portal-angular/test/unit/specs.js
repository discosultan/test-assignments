import 'angular';
import 'angular-mocks/angular-mocks';

let testsContext = require.context('.', true, /\.spec$/);
testsContext.keys().forEach(testsContext);