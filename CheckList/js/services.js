var services = angular.module('DQCSApp.services', ['ngResource']);

services.factory('DQCS', function ($resource) {
    return $resource('api/checks/:id'
        , { id: '@id' }
        , {
            queryOfpSystem: { method: 'GET', params: { id: 'GetSystems' }, isArray: true },
            queryTypes: { method: 'GET', params: { id: 'GetTypes' }, isArray: true },
            update: { method: 'PUT' }
        }
    );
});