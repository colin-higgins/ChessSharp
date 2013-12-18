/// <reference path="../Scripts/angular.js" />

var chessSharp = angular.module('csChessApp', ['ngRoute']);

chessSharp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/Angular/Templates/PlayChess.html',
            controller: 'ChessController'
        })
        .otherwise({
            templateUrl: '/Angular/Templates/PlayChess.html',
            controller: 'ChessController'
        });
}]);