/// <reference path="../vendor/angular-1.5.min.js" />

var app = angular.module('myApp', ["ngMaterial", "ngRoute", "ng-fusioncharts"]);
app.controller('MyController', function ($rootScope, $http, $rootScope) {
   
    $http.get("/api/home/revenue")
        .then((res) => {
            $rootScope.tongdoanhthu = res.data;
        },
            (err) => { console.log(err.data); }
    );

    $http.get("/api/home/sales")
        .then((res) => {
            $rootScope.tongdoanhso = res.data;
        }, (err) => { console.log(err.data); }
    );

    $http.get("/api/home/brands")
        .then((res) => {
            $rootScope.tongsothuonghieu = res.data;
        }, (err) => { console.log(err.data); }
    );

    $http.get("/api/home/categories")
        .then((res) => {
            $rootScope.tongsoloaisanpham = res.data;
        }, (err) => { console.log(err.data); }
    );

    $http.get("/api/home/products")
        .then((res) => {
            $rootScope.tongsosanpham = res.data;
        }, (err) => { console.log(err.data); }
    );
});