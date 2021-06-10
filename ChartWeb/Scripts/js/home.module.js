/// <reference path="../vendor/angular-1.5.min.js" />

app.controller('homeController', function ($scope, $http) {

    

    $scope.chartByYear = (model) => {
        $scope.dataSourceByYear = {
            "chart": {
                "caption": "Doanh thu của cửa hàng từ năm 2016 - 2018",
                "plottooltext": "Năm <b>$label</b> doanh thu chiếm <b>$percentValue</b> tổng doanh thu",
                "showLegend": "0",
                "enableMultiSlicing": "0",
                "showPercentValues": "1",
                "legendPosition": "bottom",
                "useDataPlotColorForLabels": "1",
                "theme": "fusion",
            },
            "data": model
        };
    }
    $scope.chartBySales = (model) => {
        $scope.dataSourceBySales = {
            chart: {
                caption: "Doanh số theo từng loại sản phẩm từ năm 2016 - 2018",
                yaxisname: "Doanh số",
                showvalues: "1",
                numberprefix: "",
                theme: "fusion"
            },
            data: model
        };


    }

    $scope.chartByTop = (model) => {
        $scope.dataSourceByTop = {
            chart: {
                caption: "Top 10 sản phẩm bán chạy từ năm 2016 - 2018",
                subcaption: "BikeStores",
                xaxisname: "Tên sản phẩm",
                yaxisname: "Doanh thu (USD)",
                numbersuffix: "",
                theme: "fusion"
            },
            data: model
        };
    }

    $scope.chartByInventory = (model) => {
        $scope.dataSourceByInventory = {
            chart: {
                theme: "fusion",
                caption: "Lượng sản phẩm tồn kho",
                subcaption: "2016 - 2018",
                showvalues: "1",
                numbersuffix: "chiếc",
                numberprefix: "",
                plottooltext:
                    "<b>$label</b> có <b>$dataValue</b> tồn kho",
                is2d:"0"
            },
            data: model
        };
    }



    $scope.modelByYear = [];
    $http.get("/api/home/revenuebyyear")
    .then((res) => {
        angular.forEach(res.data, (item) => {
            $scope.modelByYear.push(item);
        });
    }, (err) => { console.log(err.data); });

    $scope.chartByYear($scope.modelByYear);

    $scope.modelSales = [];
    $http.get("/api/home/salescategories")
        .then((res) => {
            angular.forEach(res.data, (item) => {
                $scope.modelSales.push(item);
            });
        }, (err) => { console.log(err.data); });

    $scope.chartBySales($scope.modelSales);

    $scope.modelTop = [];
    $http.get("/api/home/revenuetop")
        .then((res) => {
            angular.forEach(res.data, (item) => {
                $scope.modelTop.push(item);
            });
        }, (err) => { console.log(err.data); });

    $scope.chartByTop($scope.modelTop);

    $scope.modelInventory = [];
    $http.get("/api/home/inventory")
        .then((res) => {
            angular.forEach(res.data, (item) => {
                $scope.modelInventory.push(item);
            });
        }, (err) => { console.log(err.data); });

    $scope.chartByInventory($scope.modelInventory);
})