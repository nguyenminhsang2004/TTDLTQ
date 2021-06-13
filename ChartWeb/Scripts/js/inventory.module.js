/// <reference path="../vendor/angular-1.5.min.js" />

app.controller('inventoryController', function ($scope, $http, $rootScope) {
    $scope.display = true;
    $scope.changeDisplay = () => {
        $scope.display = !$scope.display;
    }
    $scope.viewChartByCategories = (label, model1, model2) => {
        $scope.dataSourceCategories = {
            chart: {
                caption: "Thống kê số liệu bán ra và tồn kho theo loại sản phẩm từ năm 2016 - 2018",
                subcaption: "Sales & Inventory",
                plottooltext: "$label: <b>$dataValue</b> sản phẩm $seriesName",
                theme: "fusion"
            },
            categories: [
                {
                    category: label
                }
            ],
            dataset: [
                {
                    seriesname: "bán ra",
                    data: model1
                },
                {
                    seriesname: "tồn kho",
                    data: model2
                }
            ]
        };
    }

    $scope.viewChartByProducts = (label, model1, model2) => {
        $scope.dataSourceProduct = {
            chart: {
                caption: "Thống kê số liệu bán ra và tồn kho theo sản phẩm từ năm 2016 - 2018",
                subcaption: "Sales & Inventory",
                yaxisname: "Số lượng (chiếc)",
                numvisibleplot: "8",
                labeldisplay: "auto",
                theme: "fusion"
            },
            categories: [
                {
                    category: label
                }
            ],
            dataset: [
                {
                    seriesname: "bán ra",
                    data: model1
                },
                {
                    seriesname: "tồn kho",
                    data: model2
                }
            ]
        };
    }

    $scope.viewChartByBrand = (label, model1, model2) => {
        $scope.dataSourceBrand = {
            chart: {
                caption: "Thống kê số liệu bán ra và tồn kho theo thương hiệu từ năm 2016 - 2018",
                yaxisname: "Số lượng (chiếc)",
                subcaption: "Sales & Inventory",
                numdivlines: "3",
                showvalues: "0",
                legenditemfontsize: "15",
                legenditemfontbold: "1",
                plottooltext: "$label: <b>$dataValue</b> sản phẩm $seriesName",
                theme: "fusion"
            },
            categories: [
                {
                    category: label
                }
            ],
            dataset: [
                {
                    seriesname: "bán ra",
                    data: model1
                },
                {
                    seriesname: "tồn kho",
                    data: model2
                }
            ]
        };
    }

    $scope.viewChartByStore = (label, model1, model2) => {
        $scope.dataSourceStore = {
            chart: {
                caption: "Thống kê số liệu bán ra và tồn kho theo cửa hàng từ năm 2016 - 2018",
                subcaption: "Sales & Inventory",
                numberprefix: "",
                numbersuffix: "",
                scrollheight: "10",
                numvisibleplot: "7",
                showanchors: "0",
                theme: "fusion"
            },
            categories: [
                {
                    category: label
                }
            ],
            dataset: [
                {
                    seriesname: "bán ra",
                    data: model1
                },
                {
                    seriesname: "tồn kho",
                    renderas: "area",
                    data: model2
                }
            ]
        };
    }

    $scope.labelcategory = [];
    $scope.salescategory = [];
    $scope.inventorycategory = [];
    $http.get("/api/inventory/categories")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.labelcategory.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $http.get("/api/inventory/salesbycategories")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.salescategory.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $http.get("/api/inventory/inventorybycategories")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.inventorycategory.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $scope.viewChartByCategories($scope.labelcategory, $scope.salescategory, $scope.inventorycategory);

    $scope.labelproduct = [];
    $scope.salesproduct = [];
    $scope.inventoryproduct = [];
    $http.get("/api/inventory/products")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.labelproduct.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $http.get("/api/inventory/salesbyproducts")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.salesproduct.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $http.get("/api/inventory/inventorybyproducts")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.inventoryproduct.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $scope.viewChartByProducts($scope.labelproduct, $scope.salesproduct, $scope.inventoryproduct);

    $scope.labelbrand = [];
    $scope.salesbrand = [];
    $scope.inventorybrand = [];
    $http.get("/api/inventory/brands")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.labelbrand.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $http.get("/api/inventory/salesbybrands")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.salesbrand.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $http.get("/api/inventory/inventorybybrands")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.inventorybrand.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $scope.viewChartByBrand($scope.labelbrand, $scope.salesbrand, $scope.inventorybrand);

    $scope.labelstore = [];
    $scope.salesstore = [];
    $scope.inventorystore = [];
    $http.get("/api/inventory/stores")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.labelstore.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $http.get("/api/inventory/salesbystores")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.salesstore.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $http.get("/api/inventory/inventorybystores")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.inventorystore.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $scope.viewChartByStore($scope.labelstore, $scope.salesstore, $scope.inventorystore);
})