/// <reference path="GameSelectionCtrl.js" />

chessSharpPlay.controller('GameSelectionCtrl', ['$scope', 'gameApi', function ($scope, gameApi) {

    $scope.gameId = 0;

    $scope.getGame = function () {
        var onSuccess = function (game) {
            $scope.game = game;
            $scope.busy = false;
        };
        var onFail = function (data) {
            $scope.busy = false;
        };
        $scope.busy = true;
        gameApi.getGame($scope.gameId, onSuccess, onFail);
    };

}]);
