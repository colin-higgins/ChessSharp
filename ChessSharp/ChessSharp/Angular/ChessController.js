﻿/// <reference path="../Scripts/angular.js" />

chessSharp.controller('ChessController', ['$scope', 'gameApi', function ($scope, gameApi) {

    var getGame = function (gameId) {
        var callback = function (game) {
            $scope.game = game;
        };

        gameApi.getGame(gameId, callback);
    };

    getGame();

    var tryMakeMove = function (move) {
        var callback = function (squares) {
            $scope.game.Board.Squares = squares;
            $scope.readyToMove = null;
            $scope.destination = null;
        };
        gameApi.makeMove($scope.game.GameId, move, callback);
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
        if (!$scope.readyToMove) {
            $scope.readyToMove = square;
            return;
        }
        else if ($scope.readyToMove === square) {
            $scope.readyToMove = null;
            $scope.destination = null;
            return;
        }
        else if ($scope.destination === square) {
            $scope.destination = null;
            return;
        }
        
        $scope.destination = square;
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
