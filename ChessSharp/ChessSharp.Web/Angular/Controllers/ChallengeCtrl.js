/// <reference path="GameSelectionCtrl.js" />

chessSharpChallenge.controller('ChallengeCtrl', ['$scope', function ($scope) {

    $scope.accept = function () {
        $scope.challengeAccepted = true;
    };

    $scope.decline = function () {
        $scope.challengeAccepted = false;
    };

}]);
