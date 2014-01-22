/// <reference path="Templates/Piece.html" />
/// <reference path="Templates/Piece.html" />
/// <reference path="Templates/Piece.html" />
/// <reference path="../Scripts/angular.js" />

chessSharpPlay.directive('csPiece', function () {

    var pieceEnum = function () {
        this.Empty = 0;
        this.Pawn = 1;
        this.Knight = 2;
        this.Bishop = 3;
        this.Rook = 4;
        this.Queen = 5;
        this.King = 6;
    };

    var teamEnum = function () {
        this.Light = 0;
        this.Dark = 1;
    };
    
    function link(scope) {
        scope.pieceImage = function(pieceType, team) {

            var lightThenDark = function(lightPath, darkPath, team) {
                if (team == teamEnum.Light)
                    return lightPath;
                if (team == teamEnum.Dark)
                    return darkPath;
            };

            switch (pieceType) {
                case pieceEnum.Empty:
                    return '';
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
                    return lightThenDark('../Images/lightTrex.png', '../Images/darkTrex.png', team);      
            }
        };
    }

    return {
        templateUrl: '../Angular/Directives/Templates/Piece.html',
        scope: {
            pieceType: '=piece',
            team: '='
        }
    };

});
