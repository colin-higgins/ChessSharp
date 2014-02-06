
chessSharpPlay.controller('PlayChessCtrl', ['$scope', '$timeout', '$routeParams', 'gameApi',
    function ($scope, $timeout, $routeParams, gameApi) {

        var currentGameId = $routeParams.gameId;

        var playerCanMove = function () {
            var canMove = $scope.game && $scope.game.IsCurrentPlayersMove;
            return canMove;
        };

        $scope.getGame = function () {
            var onSuccess = function (game) {
                $scope.game = game;
                $scope.busy = false;
            };
            var onFail = function (data) {
                $scope.busy = false;
            };
            $scope.busy = true;
            gameApi.getGame(currentGameId, onSuccess, onFail);
        };

        var retrieveSelectedGame = function () {
            if (currentGameId && currentGameId > 0)
                $scope.getGame();
        };

        retrieveSelectedGame();

        var timedGameRetrieve = function () {
            $timeout(function () {
                //Only update the game if it's not the player's turn
                if (!$scope.game.IsCurrentPlayersMove) {
                    retrieveSelectedGame();
                }
                timedGameRetrieve();
            }, 8000);
        };

        timedGameRetrieve();

        $scope.canMakeMove = function () {
            if (!$scope.readyToMove)
                return false;
            if (!$scope.destination)
                return false;
            if ($scope.busy)
                return false;
            return true;
        };

        var tryMakeMove = function (move) {
            var onSuccess = function (game) {
                $scope.readyToMove = null;
                $scope.destination = null;
                $scope.busy = false;
                retrieveSelectedGame();
                getCurrentGameList();
            };
            var onFailure = function (error) {
                $scope.Failure = error.message;
                $scope.readyToMove = null;
                $scope.destination = null;
                $scope.busy = false;
            };
            $scope.busy = true;
            gameApi.makeMove($scope.game.Id, move, onSuccess, onFailure);
        };

        $scope.isReady = function (square) {
            return square === $scope.readyToMove;
        };

        $scope.isDestination = function (square) {
            return square === $scope.destination;
        };

        $scope.MakeMove = function () {
            var start = $scope.readyToMove;
            var end = $scope.destination;

            if (!start || !end)
                return;

            var move = {
                StartColumn: start.Column,
                StartRow: start.Row,
                EndColumn: end.Column,
                EndRow: end.Row
            };

            tryMakeMove(move);
        };

        var teamEnum = {
            Light: 0,
            Dark: 1,
        };

        $scope.setFocus = function (square) {
            var moveCount = $scope.game.MoveCount;
            var team = teamEnum.Light;
            if (moveCount % 2 == 1)
                team = teamEnum.Dark;

            if (!$scope.readyToMove) {
                if (team != square.ChessPiece.Team)
                    return;
                $scope.readyToMove = square;
                $scope.destination = null;
                return;
            }
            if ($scope.readyToMove === square) {
                $scope.readyToMove = null;
                $scope.destination = null;
                return;
            }
            if ($scope.destination === square) {
                $scope.destination = null;
                return;
            }
            if (square.ChessPiece && $scope.readyToMove.ChessPiece.Team === square.ChessPiece.Team) {
                $scope.readyToMove = square;
                $scope.destination = null;
                return;
            }

            $scope.destination = square;
        };

        $scope.isDark = function (square) {
            var oddColumn = square.Column % 2 == 1;
            var oddRow = square.Row % 2 == 1;

            if ($scope.isReady(square) || $scope.isDestination(square))
                return false;
            if (oddColumn && !oddRow)
                return false;
            if (!oddColumn && oddRow)
                return false;

            return true;
        };

    }]);
