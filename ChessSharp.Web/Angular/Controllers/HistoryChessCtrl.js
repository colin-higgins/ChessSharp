
chessSharpPlay.controller('HistoryChessCtrl', ['$scope', 'gameApi', function ($scope, gameApi) {

    $scope.viewGame = function(id) {
        var onSuccess = function(game) {
            $scope.game = game;
            $scope.busy = false;
        };
        var onFail = function(data) {
            $scope.busy = false;
        };
        gameApi.getGame(id, onSuccess, onFail);
    };

    $scope.isDark = function (square) {
        var oddColumn = square.Column % 2 == 1;
        var oddRow = square.Row % 2 == 1;

        if (oddColumn && !oddRow)
            return false;
        if (!oddColumn && oddRow)
            return false;

        return true;
    };

}]);
