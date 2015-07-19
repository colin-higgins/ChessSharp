
chessSharpPlay.controller('TestChessCtrl', ['$scope', '$rootScope',
    function ($scope, $rootScope) {

        $scope.getGame = function () {
            $scope.game = $rootScope.gameState;

            if (!$scope.game) {
                return;
            }

            $scope.game.Id = 99999;
        };

        var retrieveSelectedGame = function () {
            $scope.getGame();
        };

        retrieveSelectedGame();

        $scope.canMakeMove = function () {
            if (!$scope.readyToMove)
                return false;
            if (!$scope.destination)
                return false;
            if ($scope.busy)
                return false;
            return !hasMadeMove;
        };

        var hasMadeMove = false;

        var tryMakeMove = function (move) {

            if (hasMadeMove)
                return;

            $scope.moveToTest = JSON.stringify(move);

            hasMadeMove = true;
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

        $rootScope.setGameForTestView = function (gameState, move) {

            $rootScope.gameState = gameState;
            $scope.getGame();

            if (!gameState || !move) {
                return;
            }

            $scope.setFocus($scope.game.Board.Squares[move.StartRow][move.StartColumn]);
            $scope.setFocus($scope.game.Board.Squares[move.EndRow][move.EndColumn]);
        };

    }]);
