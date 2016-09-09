//jshint strict: false
exports.config = {

  allScriptsTimeout: 11000,

  specs: ['scenarios.js'],

  capabilities: {
    'browserName': 'chrome'
  },

  baseUrl: 'http://localhost:8082',

  framework: 'jasmine',

  jasmineNodeOpts: {
    defaultTimeoutInterval: 30000
  }

};