/// <reference path="Templates/Piece.html" />
/// <reference path="../Scripts/angular.js" />

var csPieceDirective = function() {

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

    var link = function(scope) {

        var imageRoot = '../../Images/';
        var defaultImage = imageRoot + 'placeholder.png';
        scope.imagePath = defaultImage;

        var lightThenDark = function(lightImageName, darkImageName, team) {
            if (team == teamEnum.Light)
                return imageRoot + lightImageName;
            if (team == teamEnum.Dark)
                return imageRoot + darkImageName;

            return defaultImage;
        };

        scope.isOccupied = function() {
            if (!scope.piece || scope.piece.PieceType == pieceEnum.Empty)
                return false;
            return true;
        };

        var pieceImage = function(piece) {

            if (!piece)
                return defaultImage;

            var pieceType = piece.PieceType;
            var team = piece.Team;

            switch (pieceType) {
            case pieceEnum.Empty:
                return defaultImage;
            case pieceEnum.Pawn:
                return lightThenDark('lightPawn.png', 'darkPawn.png', team);
            case pieceEnum.Knight:
                return lightThenDark('lightKnight.png', 'darkKnight.png', team);
            case pieceEnum.Bishop:
                return lightThenDark('lightBishop.png', 'darkBishop.png', team);
            case pieceEnum.Rook:
                return lightThenDark('lightRook.png', 'darkRook.png', team);
            case pieceEnum.Queen:
                return lightThenDark('lightQueen.png', 'darkQueen.png', team);
            case pieceEnum.King:
                return lightThenDark('lightKing.png', 'darkKing.png', team);
            default:
                return defaultImage;
            }
        };

        scope.imagePath = pieceImage(scope.piece);
    };

    return {
        restrict: 'E',
        templateUrl: '../Angular/Directives/Templates/Piece.html',
        scope: {
            piece: '=piece'
        },
        controller: ['$scope', function($scope) {
        }],
        link: link
    };

};

chessSharpPlay.directive('csPiece', csPieceDirective);
