
chessSharpPlay.controller('PlayChessCtrl', ['$scope', 'gameApi', function ($scope, gameApi) {

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

    var tryMakeMove = function (move) {
        var onSuccess = function (game) {
            $scope.game = game;
            $scope.readyToMove = null;
            $scope.destination = null;
            $scope.busy = false;
        };
        var onFailure = function(error) {
            $scope.Failure = error;
            $scope.readyToMove = null;
            $scope.destination = null;
            $scope.busy = false;
        };
        $scope.busy = true;
        gameApi.makeMove($scope.game.GameId, move, onSuccess, onFailure);
    };

    $scope.isReady = function(square) {
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

    var pieceEnum = {
        Empty: 0,
        Pawn: 1,
        Knight: 2,
        Bishop: 3,
        Rook: 4,
        Queen: 5,
        King: 6,
    };

    var teamEnum = {
        Light: 0,
        Dark: 1,
    };

    var lightThenDark = function (lightPath, darkPath, team) {
        if (team == teamEnum.Light)
            return lightPath;
        if (team == teamEnum.Dark)
            return darkPath;

        return '../Images/placeholder.png';
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
    $scope.isOccupied = function (square) {
        if (!square.ChessPiece || square.ChessPiece.PieceType == pieceEnum.Empty)
            return false;
        return true;
    };
    $scope.pieceImage = function (piece) {

        var defaultImage = '../Images/placeholder.png';
        
        if (!piece)
            return defaultImage;

        var pieceType = piece.PieceType;
        var team = piece.Team;

        switch (pieceType) {
            case pieceEnum.Empty:
                return defaultImage;
            case pieceEnum.Pawn:
                return lightThenDark('../Images/lightPawn.png', '../Images/darkPawn.png', team);
            case pieceEnum.Knight:
                return lightThenDark('../Images/lightKnight.png', '../Images/darkKnight.png', team);
            case pieceEnum.Bishop:
                return lightThenDark('../Images/lightBishop.png', '../Images/darkBishop.png', team);
            case pieceEnum.Rook:
                return lightThenDark('../Images/lightRook.png', '../Images/darkRook.png', team);
            case pieceEnum.Queen:
                return lightThenDark('../Images/lightQueen.png', '../Images/darkQueen.png', team);
            case pieceEnum.King:
                return lightThenDark('../Images/lightKing.png', '../Images/darkKing.png', team);
            default:
                return defaultImage;
        }
    };

}]);
