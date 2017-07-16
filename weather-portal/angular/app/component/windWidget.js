angular
    .module('app.component.windWidget', [])
    .component('windWidget', windWidget());

function windWidget() {
    return {
        template: require('./windWidget.html'),

        bindings: {
            value: '<'
        }
    };
}