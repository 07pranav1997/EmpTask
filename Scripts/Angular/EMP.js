/// <reference path="../angular.min.js" />



var app = angular.module("empAPP", []);

app.controller("empController", function ($scope, $http, $log) {
    $scope.btntext1 = "Save";
    $scope.btntext2 = "Clear";

    $scope.newEmployee = null;

    $scope.resetForm = function () {
        $scope.newEmployee = null;
    }

    $scope.showEmp = function () {

        $http.get("/Home/GetData").then(function (d) {
            $scope.emp = d.data;
        }, function () {
            alert('Failed');
        });
    }   

    $scope.addEmployee = function () {
        $http({
            method: "POST",
            url: "/Home/AddRecord",
            data: $scope.newEmployee
        }).then(function (response) {
            $scope.getEmployees();
            $log.info;
        }).then(function (response) {
            $scope.error = response.data;
            $log.info;
        })

    };

    $scope.deleteEmployee = function () {
        $http({
            method: "DELETE",
            url: "/Home/DeleteRecord?id=" + id,

        })
    }

    $scope.showEmp();

})