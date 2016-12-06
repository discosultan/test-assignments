// Angular E2E Testing Guide:
// https://docs.angularjs.org/guide/e2e-testing

describe('Movie Portal Application', () => {

  it('should redirect `/` to `/movies', () => {
    browser.get('/');
    expect(browser.getLocationAbsUrl()).toBe('/movies');
  });
});