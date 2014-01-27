//var chessSharpPlay = angular.module('chessSharpPlay', []);

//chessSharpPlay.factory('gameService', function () {
//    var shinyNewServiceInstance = {};
//    //factory function body that constructs shinyNewServiceInstance

//    shinyNewServiceInstance.getGame = function(gameId, callback) {

//        $.getJSON("/Home/GetGame/" + gameId,
//            function(data) {
//                callback(data);
//            }).
//            fail(function(data) {
//                if (console) {
//                    console.log("Get game failure.");
//                    console.log(data);
//                }
//                //failure(data);
//            });
//    };

//    return shinyNewServiceInstance;
//});

chessSharpPlay.factory('gameApi', ['$http', function ($http) {
    var mockBoard = [
    ];

    for (var col = 0; col < 8; col++) {
        var squares = [];

        for (var row = 0; row < 8; row++) {

            var piece = {
                pieceType: "generic",
                alive: true
            };

            if (row < 2 || row > 5)
                squares.push(piece);
            else
                squares.push({});
        }

        mockBoard.push(squares);
    }

    return {
        challengePlayer: function (playerId) {

        },
        getPlayer: function (playerId) {
            return {
                playerId: playerId,
                playerName: "Bob Dole",
                wins: 5,
                losses: 5
            };
        },
        getGame: function (gameId, onSuccess, onFailure) {
            $http({
                method: 'GET',
                url: '/ChessApi/GetGame/' + gameId
            })
            .success(function (data) {
                onSuccess(data);
            })
            .error(function (data) {
                onFailure(data);
            });
        },
        getActiveGames: function (onSuccess, onFailure) {
            $http({
                method: 'GET',
                url: '/ChessApi/GetActiveGames/'
            })
            .success(function (data) {
                onSuccess(data);
            })
            .error(function (data) {
                onFailure(data);
            });
        },
        makeMove: function (gameId, move, onSuccess, onFailure) {
            $http({
                method: 'POST',
                url: '/ChessApi/MakeMove',
                data: { id: gameId, move: move }
            })
            .success(function (data) {
                onSuccess(data);
            })
            .error(function (data, message) {
                onFailure(data);
            });;
        }
    };
}]);