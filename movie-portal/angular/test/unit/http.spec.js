import httpModule from '../../src/app/service/http';

describe('http service', () => {
    let http;
    let $httpBackend;

    beforeEach(() => {
        const mocksModule = 'mocks';
        angular.module(mocksModule, []).value('apiHost', '');
        angular.mock.module(mocksModule);
        angular.mock.module(httpModule);
    });

    beforeEach(angular.mock.inject((_$httpBackend_, _http_) => {
        $httpBackend = _$httpBackend_;
        $httpBackend.expectGET('any').respond({});

         http = _http_;
    }));

    it('should increment loading counter on request', () => {
        expect(http.getNumLoading()).toEqual(0);

        http.get('any');
        expect(http.getNumLoading()).toEqual(1);

        $httpBackend.flush();
        expect(http.getNumLoading()).toEqual(0);
    });

});