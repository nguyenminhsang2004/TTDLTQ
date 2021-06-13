/// <reference path="../vendor/angular-1.5.min.js" />

app.controller('productController', function ($scope, $http, $rootScope) {
    $scope.year = "None";
    $scope.month = "None";
    $scope.id = "None";
    $scope.brand = "None";
    $scope.sosanpham = $rootScope.tongsosanpham;

    $scope.viewChart = (label, value, title, products) => {
        $scope.myDataSource = {
            chart: {
                caption: title,
                subcaption: "(" + products + " sản phẩm)",
                showvalues: "0",
                numvisibleplot: "12",
                plottooltext:
                    "<b>$label</b> có tổng doanh thu từ năm 2016 - 2018 là <b> $dataValue</b>",
                theme: "fusion"
            },
            categories: [
                {
                    category: label
                }
            ],
            dataset: [
                {
                    data: value
                }
            ]
        };
    }

    $scope.modellabel = [];
    $scope.modelvalue = [];
    $http.get("/api/products/label/?brand=0")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.modellabel.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );
    $http.get("/api/products/revenue/?brand=0&year=0&month=0")
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.modelvalue.push(item);
                });
            },
            (err) => { console.log(err.data); }
        );
    $scope.viewChart($scope.modellabel, $scope.modelvalue, "Doanh thu của từng sản phẩm năm 2016 - 2018 (USD).", $scope.sosanpham);
    /*end load chart default*/
    $http.get('/api/brands/label')
        .then(
            (res) => {
                $scope.brands = res.data;
            },
            (err) => { console.log(err); }
        );

    $scope.selectedBrand = undefined;
    $scope.getSelectedBrand = () => {
        if ($scope.selectedBrand !== undefined) {
            return $scope.selectedBrand;
        }
        else {
            return "Thương hiệu";
        }
    };
    $scope.changeBrand = (item) => {
        $scope.brand = item;
        $scope.id = item.id;
        $scope.changeChart($scope.id, $scope.year, $scope.month);
    }
    //end brands
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
        $scope.changeChart($scope.id, $scope.year, $scope.month);
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
        $scope.changeChart($scope.id, $scope.year, $scope.month);
    }

    //end months
    $scope.changeChart = (brand, year, month) => {
        if (isNaN(parseInt(brand))) {
            brand = 0;
        }
        if (isNaN(parseInt(year))) {
            year = 0;
        }
        if (isNaN(parseInt(month))) {
            month = 0;
        }

        var apiUrl = '/api/products/revenue/?brand=' + brand + '&year=' + year + '&month=' + month;
        $scope.label = [];
        $scope.value = [];
        $http.get("/api/products/label/?brand=" + brand)
            .then(
                (res) => {
                    angular.forEach(res.data, (item) => {
                        $scope.label.push(item);
                    });
                },
                (err) => { console.log(err.data); }
            );
        $http.get(apiUrl)
            .then(
                (res) => {
                    $scope.sosanpham = res.data.length;
                    angular.forEach(res.data, (item) => {
                        $scope.value.push(item);
                    });
                    $scope.viewChart($scope.label, $scope.value, "Doanh thu của từng sản phẩm (USD).", $scope.sosanpham);
                },
                (err) => { console.log(err); }
            );
    }
})