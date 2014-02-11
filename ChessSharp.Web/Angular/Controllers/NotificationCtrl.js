/// <reference path="NotificationCtrl.js" />

chessSharpPlay.controller('NotificationCtrl', ['$scope', '$timeout', 'gameApi', function ($scope, $timeout, gameApi) {

    $scope.showNotifications = false;
    $scope.showGames = false;
    $scope.showChallenges = false;
    $scope.challenges = [];
    $scope.games = [];
    var mouseOut = true;

    $scope.toggleNotifications = function () {
        $scope.showNotifications = !$scope.showNotifications;
    };
    $scope.handleMouseOut = function () {
        mouseOut = true;
        $timeout(function () {
            if (mouseOut)
                $scope.showNotifications = false;
        }, 2000);
    };
    $scope.cancelMouseOut = function () {
        $scope.showNotifications = true;
        mouseOut = false;
    };

    $scope.toggleChallenges = function () {
        $scope.showChallenges = !$scope.showChallenges;

        if ($scope.showChallenges)
            $scope.showGames = false;
    };

    $scope.toggleGames = function () {
        $scope.showGames = !$scope.showGames;

        if ($scope.showGames)
            $scope.showChallenges = false;
    };

    $scope.readyGameCount = function() {
        var count = 0;
        for (var i = 0; i < $scope.games.length; i++) {
            if ($scope.games[i].IsPlayersTurn)
                count++;
        }
        return count;
    };

    var getGames = function () {
        var onSuccess = function (data) {
            $scope.games = data;
        };
        var onFail = function (data) {
            $scope.busy = false;
        };
        gameApi.getActiveGames(onSuccess, onFail);
    };
    var getChallenges = function () {
        var onSuccess = function (data) {
            $scope.challenges = data;
        };
        var onFail = function (data) {
            $scope.busy = false;
        };
        gameApi.getChallenges(onSuccess, onFail);
    };

    getChallenges();
    getGames();

    var timedGameRetrieve = function () {
        $timeout(function () {
            getGames();
            timedGameRetrieve();
        }, 15000);
    };

    timedGameRetrieve();

    var timedChallengeRetrieve = function () {
        $timeout(function () {
            getChallenges();
            timedChallengeRetrieve();
        }, 30000);
    };

    timedChallengeRetrieve();

}]);
