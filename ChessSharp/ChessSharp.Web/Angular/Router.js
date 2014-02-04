/// <reference path="../Scripts/angular.js" />

var chessSharpPlay = angular.module('csPlayChessApp', ['ngRoute']);

chessSharpPlay.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/Angular/Templates/PlayChess.html',
            controller: 'PlayChessCtrl'
        })
        .otherwise({
            templateUrl: '/Angular/Templates/PlayChess.html',
            controller: 'PlayChessCtrl'
        });
}]);

var chessSharpChallenge = angular.module('csChallengeApp', ['ngRoute']);

var chessSharpHistory = angular.module('csHistoryChessApp', ['ngRoute']);