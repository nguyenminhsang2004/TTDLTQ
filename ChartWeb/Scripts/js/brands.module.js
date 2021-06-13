/// <reference path="../vendor/angular-1.5.min.js" />

app.controller('brandsController', function ($scope, $http, $rootScope) {
    $scope.year = "None";
    $scope.month = "None";

    $scope.viewChartRevenue = (model, title) => {
        $scope.myDataSourceRevenue = {
            chart: {
                caption: title,
                subcaption: "2016 - 2018",
                yaxisname: "Doanh thu (USD)",
                decimals: "1",
                theme: "fusion"
            },
            data: model
        };
    }

    $scope.modelRevenue = [];

    $http.get("/api/brands/revenue/?year=0&month=0")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.modelRevenue.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $scope.viewChartRevenue($scope.modelRevenue, "Doanh thu của từng thương hiệu (USD).");

    $scope.viewChartSales = (model, title) => {
        $scope.myDataSourceSales = {
            chart: {
                caption: title,
                yaxisname: "Doanh số (chiếc)",
                subcaption: "[2016 - 2018]",
                numbersuffix: "",
                rotatelabels: "1",
                setadaptiveymin: "1",
                theme: "fusion"
            },
            data: model
        };
    }

    $scope.modelSales = [];

    $http.get("/api/brands/sales/?year=0&month=0")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.modelSales.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );

    $scope.viewChartSales($scope.modelSales, "Doanh số của từng thương hiệu (chiếc).");
    /*end load chart default*/
    $http.get('/api/dates/getall')
        .then(
            (res) => {
                $scope.years = res.data;
            },
            (err) => { console.log(err); }
        );

    $scope.selectedYear = undefined;
    $scope.getSelectedYear = () => {
        if ($scope.selectedYear !== undefined) {
            return $scope.selectedYear;
        }
        else {
            return "Năm";
        }
    };

    $scope.changeYear = (item) => {
        $scope.year = item;
        $scope.changeChartRevenue($scope.year, $scope.month);
        $scope.changeChartSales($scope.year, $scope.month);
    }
    //end years
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
        $scope.changeChartRevenue($scope.year, $scope.month);
        $scope.changeChartSales($scope.year, $scope.month);
    }

    //end months
    $scope.changeChartRevenue = (year, month) => {
        $scope.title = 'Doanh thu của từng thương hiệu cửa hàng tháng ' + month + '/ ' + year;
        if (isNaN(parseInt(year))) {
            year = 0;
            if (parseInt(month) !== 0)
                $scope.title = 'Doanh thu của từng thương hiệu cửa hàng tháng ' + month + " của các năm";
            else
                $scope.title = 'Doanh thu của từng thương hiệu cửa hàng năm 2016 - 2018';
        }
        if (isNaN(parseInt(month))) {
            month = 0;
            if (parseInt(year) !== 0)
                $scope.title = 'Doanh thu của từng thương hiệu cửa hàng năm ' + year;
            else
                $scope.title = 'Doanh thu của từng thương hiệu cửa hàng năm 2016 - 2018';
        }

        var apiUrl = '/api/brands/revenue/?year=' + year + '&month=' + month;
        $scope.valueRevenue = [];
        $http.get(apiUrl)
            .then(
                (res) => {
                    angular.forEach(res.data, (item) => {
                        $scope.valueRevenue.push(item);
                    });
                    $scope.viewChartRevenue($scope.valueRevenue, $scope.title);
                },
                (err) => { console.log(err); }
            );
    }

    $scope.changeChartSales = (year, month) => {
        $scope.title = 'Doanh số của từng thương hiệu cửa hàng tháng ' + month + '/ ' + year;
        if (isNaN(parseInt(year))) {
            year = 0;
            if (parseInt(month) !== 0)
                $scope.title = 'Doanh số của từng thương hiệu cửa hàng tháng ' + month + " của các năm";
            else
                $scope.title = 'Doanh số của từng thương hiệu cửa hàng năm 2016 - 2018';
        }
        if (isNaN(parseInt(month))) {
            month = 0;
            if (parseInt(year) !== 0)
                $scope.title = 'Doanh số của từng thương hiệu cửa hàng năm ' + year;
            else
                $scope.title = 'Doanh số của từng thương hiệu cửa hàng năm 2016 - 2018';
        }

        var apiUrl = '/api/brands/sales/?year=' + year + '&month=' + month;
        $scope.valueSales = [];
        $http.get(apiUrl)
            .then(
                (res) => {
                    angular.forEach(res.data, (item) => {
                        $scope.valueSales.push(item);
                    });
                    $scope.viewChartSales($scope.valueSales, $scope.title);
                },
                (err) => { console.log(err); }
            );
    }
})