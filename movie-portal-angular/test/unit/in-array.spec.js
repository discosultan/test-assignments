import inArrayModule from '../../src/app/filter/in-array';

describe('in-array filter', () => {
    let inArrayFilter;

    beforeEach(() => {
        jasmine.addCustomEqualityTester(angular.equals);
        angular.mock.module(inArrayModule)
    });

    beforeEach(angular.mock.inject(_inArrayFilter_ => {
        inArrayFilter = _inArrayFilter_;        
    }));

    it('should filter based on elements in array', () => {
        const src = [
            { id: 1, category: 'A' },
            { id: 2, category: 'B' },
            { id: 3, category: 'B' }
        ];
        const filter = ['B'];
        const expected = [
            { id: 2, category: 'B' },
            { id: 3, category: 'B' }
        ];

        expect(inArrayFilter(src, filter, 'category')).toEqual(expected);
    });
});