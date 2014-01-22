/// <reference path="GameSelectionCtrl.js" />

chessSharpPlay.controller('GameSelectionCtrl', ['$scope', 'gameApi', function ($scope, gameApi) {

    var getGame = function (gameId) {
        var onSuccess = function (game) {
            $scope.game = game;
            $scope.busy = false;
        };
        var onFail = function (data) {
            $scope.busy = false;
        };
        $scope.busy = true;
        gameApi.getGame(gameId, onSuccess, onFail);
    };

    getGame();

}]);
