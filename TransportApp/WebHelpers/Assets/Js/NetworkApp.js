
var app = angular.module('app', ['ngFileUpload']);

app.controller('NetworkCtrl', ['$scope', '$http', 'Upload', function($scope, $http, Upload) {


    $.extend($scope, window.controllerData.NetworkModel);


    $scope.AddNetworks = function(networks) {
        for (var i = 0; i < networks.length; i++) {
            $scope.PossibleNetworks.push(networks[i]);

        }
       // $scope.$digest();

        
    };

    $scope.RemoveNetwork = function (networks) {
        for (var i = 0; i < networks.length; i++) {
            //$scope.PossibleNetworks.remove(networks[i]);
            $scope.PossibleNetworks.splice($scope.PossibleNetworks.indexOf(networks[i]), 1);
        }

       // $scope.$digest();
    };
}]);