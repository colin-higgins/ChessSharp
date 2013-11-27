var chessSharp = angular.module('chessSharp', []);

chessSharp.factory('gameService', function () {
    var shinyNewServiceInstance = {};
    //factory function body that constructs shinyNewServiceInstance

    shinyNewServiceInstance.getGame = function(gameId, callback) {

        $.getJSON("/Home/GetGame/" + gameId,
            function(data) {
                callback(data);
            }).
            fail(function(data) {
                if (console) {
                    console.log("Get game failure.");
                    console.log(data);
                }
                //failure(data);
            });
    };

    return shinyNewServiceInstance;
});