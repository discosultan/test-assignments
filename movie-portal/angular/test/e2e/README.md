Use npm to install Protractor globally with:

```npm install -g protractor```

This will install two command line tools, protractor and webdriver-manager. Try running protractor --version to make sure it's working.

The webdriver-manager is a helper tool to easily get an instance of a Selenium Server running. Use it to download the necessary binaries with:

```webdriver-manager update```

Now start up a server with:

```webdriver-manager start```

This will start up a Selenium Server and will output a bunch of info logs. Your Protractor test will send requests to this server to control a local browser.

-----

Make sure webpack dev server is running:

```npm start```

Execute e2e tests with:

```npm run e2e```