
var gameApiInstance = function($http) {
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
        getChallenges: function (onSuccess, onFailure) {
            $http({
                method: 'GET',
                url: '/ChessApi/GetIncomingChallenges/'
            })
                .success(function (data) {
                    onSuccess(data);
                })
                .error(function (data) {
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