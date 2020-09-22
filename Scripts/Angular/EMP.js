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

    $scope.showQual = function () {
        $http.get("/Home/GetQualification").then(function (d) {
            $scope.qual = d.data;
        }, function () {
                alert('Failed');
        });
    }

    $scope.showEmpByID = function (id) {

        $http.get("/Home/GetDataById?id=" + id).then(function (d) {
            $scope.emp = d.data[0];
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
            $scope.showEmp();
            $log.info;
        }).then(function (response) {
            $scope.error = response.data;
            $log.info;
        })

    };

    $scope.updateEmployee = function () {
        $http({
            method: "PUT",
            url: "/Home/UpdateRecord",
            data: $scope.newEmployee
        }).then(function (response) {
            $scope.showEmp();
            $log.info;
        }).then(function (response) {
            $scope.error = response.data;
            $log.info;
        })
    };

    $scope.deleteEmployee = function (id) {
        $http.delete("/Home/DeleteRecord?id=" + id)
            .then(function (response) {
                $scope.showEmp();
                $log.info;
            }).then(function (response) {
                $log.info;
            })
    };

    $scope.showEmp();
    $scope.showQual();

})