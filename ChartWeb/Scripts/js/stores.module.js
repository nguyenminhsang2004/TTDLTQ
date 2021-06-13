/// <reference path="../vendor/angular-1.5.min.js" />

app.controller('storesController', function ($scope, $http, $rootScope) {
    $scope.month = "None";

    $scope.viewChartRevenue = (label, model, title) => {
        $scope.myDataSourceRevenue = {
            chart: {
                caption: title,
                subcaption: "Revenue",
                xaxisname: "Năm",
                yaxisname: "Doanh thu (USD)",
                formatnumberscale: "1",
                plottooltext:
                    "$label: <b>$seriesName</b> đạt doanh thu <b>$dataValue</b>",
                theme: "fusion"
            },
            categories: [
                {
                    category: label
                }
            ],
            dataset: model
        };
    }

    $scope.modelLabel = [];
    $scope.modelRevenue = [];
    $http.get('/api/dates/year')
    .then(
        (res) => {
            angular.forEach(res.data, (item) => {
                $scope.modelLabel.push(item);
            });
        },
        (err) => { console.log(err); }
    );
    $http.get("/api/stores/revenue/?month=0")
        .then(
            (res) => {
                console.log(res.data);
                angular.forEach(res.data, (item) => {
                    $scope.modelRevenue.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $scope.viewChartRevenue($scope.modelLabel,$scope.modelRevenue, "Doanh thu của các cửa hàng 2016 - 2018 (USD).");

    $scope.viewChartSales = (label, model, title) => {
        $scope.myDataSourceSales = {
            chart: {
                caption: title,
                subcaption: "Sales",
                xaxisname: "Năm",
                yaxisname: "Doanh số (Chiếc)",
                formatnumberscale: "1",
                plottooltext:
                    "$label: <b>$seriesName</b> đạt doanh số <b>$dataValue</b>",
                theme: "fusion"
            },
            categories: [
                {
                    category: label
                }
            ],
            dataset: model
        };
    }

    $scope.modelSales = [];

    $http.get("/api/stores/sales/?month=0")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.modelSales.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $scope.viewChartSales($scope.modelLabel,$scope.modelSales, "Doanh số của từng cửa hàng 2016 - 2018.");
    /*end load chart default*/

    $scope.months = ["None", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];

    $scope.selectedMonth = undefined;
    $scope.getSelectedMonth = () => {
        if ($scope.selectedMonth !== undefined) {
            return "Tháng " + $scope.selectedMonth;
        }
        else {
            return "Tháng";
        }
    };

    $scope.changeMonth = (item) => {
        $scope.month = item;
        $scope.changeChartRevenue($scope.month);
        $scope.changeChartSales($scope.month);
    }

    //end months
    $scope.changeChartRevenue = (month) => {
        $scope.title = 'Doanh thu của từng cửa hàng tháng ' + month + " các năm";
        if (isNaN(parseInt(month))) {
            month = 0;
            $scope.title = 'Doanh thu của từng cửa hàng tháng 2016 - 2018';
        }

        var apiUrl = '/api/stores/revenue/?month=' + month;
        $scope.valueRevenue = [];
        $http.get(apiUrl)
            .then(
                (res) => {
                    angular.forEach(res.data, (item) => {
                        $scope.valueRevenue.push(item);
                    });
                    $scope.viewChartRevenue($scope.modelLabel,$scope.valueRevenue, $scope.title);
                },
                (err) => { console.log(err); }
            );

    }

    $scope.changeChartSales = (month) => {
        $scope.title = 'Doanh số của từng cửa hàng tháng ' + month + ' các năm ';
        if (isNaN(parseInt(month))) {
            month = 0;
            $scope.title = 'Doanh số của từng cửa hàng tháng 2016 - 2018';
        }

        var apiUrl = '/api/stores/sales/?month=' + month;
        $scope.valueSales = [];
        $http.get(apiUrl)
            .then(
                (res) => {
                    angular.forEach(res.data, (item) => {
                        $scope.valueSales.push(item);
                    });
                    $scope.viewChartSales($scope.modelLabel,$scope.valueSales, $scope.title);
                },
                (err) => { console.log(err); }
            );

    }
})