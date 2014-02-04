
var gameApiInstance = function($http) {
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
        challengePlayer: function(playerId) {

        },
        getPlayer: function(playerId) {
            return {
                playerId: playerId,
                playerName: "Bob Dole",
                wins: 5,
                losses: 5
            };
        },
        getGame: function(gameId, onSuccess, onFailure) {
            $http({
                method: 'GET',
                url: '/ChessApi/GetGame/' + gameId
            })
                .success(function(data) {
                    onSuccess(data);
                })
                .error(function(data) {
                    onFailure(data);
                });
        },
        getActiveGames: function(onSuccess, onFailure) {
            $http({
                method: 'GET',
                url: '/ChessApi/GetActiveGames/'
            })
                .success(function(data) {
                    onSuccess(data);
                })
                .error(function(data) {
                    onFailure(data);
                });
        },
        makeMove: function(gameId, move, onSuccess, onFailure) {
            $http({
                method: 'POST',
                url: '/ChessApi/MakeMove',
                data: { id: gameId, move: move }
            })
                .success(function(data) {
                    onSuccess(data);
                })
                .error(function(data, message) {
                    onFailure(data);
                });
            ;
        }
    };
};

chessSharpPlay.factory('gameApi', ['$http', gameApiInstance]);
chessSharpHistory.factory('gameApi', ['$http', gameApiInstance]);