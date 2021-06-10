/// <reference path="../vendor/angular-1.5.min.js" />

app.controller('categoriesController', function ($scope, $http, $rootScope) {
    $scope.hienthi = true;
    $scope.doiHienThi = () => {
        $scope.hienthi = !$scope.hienthi;
    }
    $scope.year = "None";
    $scope.month = "None";

    $scope.viewChart = (dataSource, title) => {
        $scope.myDataSource = {
            "chart": {
                "caption": title,
                "subCaption": "BikeStores",
                "xAxisName": "Loại sản phẩm (Categories)",
                "yAxisName": "Doanh thu (USD)",
                "numberSuffix": "",
                "theme": "fusion",
            },
            "data": dataSource
        };
    }

    $scope.model = [];
    $http.get('/api/categories/revenue/?year=0&month=0')
    .then(
        (res) => {
            angular.forEach(res.data, (item) => {
                $scope.model.push(item);
            })
        },
        (err) => { console.log(err); }
    );
    $scope.viewChart($scope.model, "Doanh thu theo từng loại sản phẩm 2016 - 2018");

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
        $scope.changeChart($scope.year, $scope.month);
    }

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
        $scope.changeChart($scope.year, $scope.month);
    }

    $scope.changeChart = (year, month) => {
        $scope.title = 'Doanh thu của từng loại sản phẩm cửa hàng tháng ' + month + '/ ' + year;
        if (isNaN(parseInt(month))) {
            month = 0;
            if (parseInt(year) !== 0)
                $scope.title = 'Doanh thu của từng loại sản phẩm cửa hàng năm ' + year;
            else
                $scope.title = 'Doanh thu của từng loại sản phẩm cửa hàng năm 2016 - 2018';
        }
        if (isNaN(parseInt(year))) {
            year = 0;
            if (parseInt(month) !== 0)
                $scope.title = 'Doanh thu của từng loại sản phẩm cửa hàng tháng ' + month + " của các năm";
            else
                $scope.title = 'Doanh thu của từng loại sản phẩm cửa hàng năm 2016 - 2018';
        }
        var apiUrl = '/api/categories/revenue/?year=' + year + '&month=' + month;

        $http.get(apiUrl)
        .then(
            (res) => {
                $scope.model = [];
                angular.forEach(res.data, (item) => {
                    $scope.model.push(item);
                });
                $scope.viewChart($scope.model, $scope.title);
            },
            (err) => { console.log(err); }
        );
    }
})