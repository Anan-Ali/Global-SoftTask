var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {
    $scope.GetAll = function () {
        $http({
            method: "get",
            url: "http://localhost:61972/Employee/GetAll"
        }).then(function (response) {
            $scope.Employees = response.data;
        }, function () {
            alert("Error Occur");
        })
    };

    $scope.Employee = {
        Id: '',
        Name: '',
        Salary: ''
    };

    //$scope.Create = function (data) {
    //    $http({
    //        method: "post",
    //        url: "http://localhost:49827/Product/Create"
    //    }).then(function (response) {
    //        $scope.Products = response.data;
    //    }, function () {
    //        alert("Error Occur");
    //    })
    //}
})