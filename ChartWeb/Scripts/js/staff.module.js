/// <reference path="../vendor/angular-1.5.min.js" />

app.controller('staffController', function ($scope, $http, $rootScope) {
    $scope.year = "None";
    $scope.yearTop = "None";
    $scope.month = "None";
    $scope.quarter = "None";
    $scope.topStaff = "None";
    $scope.hienthi = true;
    $scope.doiHienThi = () => {
        $scope.hienthi = !$scope.hienthi;
    }

    $scope.viewChartRevenue = (dataSource, title) => {
        $scope.dataSourceRevenue = {
            chart: {
                caption: title,
                yaxisname: "Doanh thu (USD)",
                anchorradius: "5",
                plottooltext: "Nhân viên $label bán được <b>$dataValue</b>",
                showhovereffect: "1",
                showvalues: "0",
                numbersuffix: "$",
                theme: "fusion",
                anchorbgcolor: "#72D7B2",
                palettecolors: "#72D7B2"
            },
            data: dataSource
        };
    }

    $scope.modelRevenue = [];
    $http.get('/api/staffs/revenue/?year=0&month=0')
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.modelRevenue.push(item);
                })
            },
            (err) => { console.log(err); }
        );
    $scope.viewChartRevenue($scope.modelRevenue, "Doanh thu của từng nhân viên 2016 - 2018");

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

    $scope.changeChartRevenue = (year, month) => {
        $scope.title = 'Doanh thu của từng nhân viên cửa hàng tháng ' + month + '/ ' + year;
        if (isNaN(parseInt(month))) {
            month = 0;
            if (parseInt(year) !== 0)
                $scope.title = 'Doanh thu của từng nhân viên cửa hàng năm ' + year;
            else
                $scope.title = 'Doanh thu của từng nhân viên cửa hàng năm 2016 - 2018';
        }
        if (isNaN(parseInt(year))) {
            year = 0;
            if (parseInt(month) !== 0)
                $scope.title = 'Doanh thu của từng nhân viên cửa hàng tháng ' + month + " của các năm";
            else
                $scope.title = 'Doanh thu của từng nhân viên cửa hàng năm 2016 - 2018';
        }
        var apiUrl = '/api/staffs/revenue/?year=' + year + '&month=' + month;

        $http.get(apiUrl)
            .then(
                (res) => {
                    $scope.model = [];
                    angular.forEach(res.data, (item) => {
                        $scope.model.push(item);
                    });
                    $scope.viewChartRevenue($scope.model, $scope.title);
                },
                (err) => { console.log(err); }
            );
    }

    $scope.viewChartSales = (dataSource, title) => {
        $scope.dataSourceSales = {
            chart: {
                caption: title,
                yaxisname: "Doanh số",
                aligncaptionwithcanvas: "0",
                plottooltext: "<b>$dataValue</b> chiếc",
                theme: "fusion"
            },
            data: dataSource
        };
    }

    $scope.modelSales = [];
    $http.get('/api/staffs/sales/?year=0&month=0')
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.modelSales.push(item);
                })
            },
            (err) => { console.log(err); }
        );
    $scope.viewChartSales($scope.modelSales, "Doanh số của từng nhân viên 2016 - 2018");

    $scope.changeChartSales = (year, month) => {
        $scope.title = 'Doanh số của từng nhân viên cửa hàng tháng ' + month + '/ ' + year;
        if (isNaN(parseInt(month))) {
            month = 0;
            $scope.title = 'Doanh số của từng nhân viên cửa hàng năm ' + year;
        }
        if (isNaN(parseInt(year))) {
            year = 0;
            $scope.title = 'Doanh số của từng nhân viên cửa hàng tháng ' + month + " của các năm";
        }
        var apiUrl = '/api/staffs/sales/?year=' + year + '&month=' + month;

        $http.get(apiUrl)
            .then(
                (res) => {
                    $scope.model = [];
                    angular.forEach(res.data, (item) => {
                        $scope.model.push(item);
                    });
                    $scope.viewChartSales($scope.model, $scope.title);
                },
                (err) => { console.log(err); }
            );
    }

    $scope.quarters = ["None", 1, 2, 3, 4];

    $scope.selectedQuarter = undefined;
    $scope.getSelectedQuarter = () => {
        if ($scope.selectedQuarter !== undefined) {
            return "Quý " + $scope.selectedQuarter;
        }
        else {
            return "Quý";
        }
    };
    $scope.changeQuarter = (item) => {
        $scope.quarter = item;
        $scope.changeChartStaffTop($scope.topStaff, $scope.quarter, $scope.yearTop);
    }

    $scope.selectedYearTop = undefined;
    $scope.getSelectedYearTop = () => {
        if ($scope.selectedYearTop !== undefined) {
            return $scope.selectedYearTop;
        }
        else {
            return "Năm";
        }
    };

    $scope.changeYearTop = (item) => {
        $scope.yearTop = item;
        $scope.changeChartStaffTop($scope.topStaff, $scope.quarter, $scope.yearTop);
    }

    $scope.top = ["None", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

    $scope.selectedSelectedStaffTop = undefined;
    $scope.getSelectedStaffTop = () => {
        if ($scope.selectedSelectedStaffTop !== undefined) {
            return "Top " + $scope.selectedSelectedStaffTop;
        }
        else {
            return "Top nhân viên";
        }
    };

    $scope.changeStaffTop = (item) => {
        $scope.topStaff = item;
        $scope.changeChartStaffTop($scope.topStaff, $scope.quarter, $scope.yearTop);
    }

    $scope.viewChartStaffTop = (dataSource, title) => {
        $scope.dataSourceStaffTop = {
            chart: {
                theme: "fusion",
                caption: title,
                subcaption: "Doanh thu (USD)",
                showvalues: "1",
                numbersuffix: "",
                numberprefix: "$",
                plottooltext:
                    "<b>$label</b> đạt doanh thu <b>$dataValue</b>",
                is2d: "0"
            },
            data: dataSource
        };
    }

    $scope.modelStaffTop = [];
    $http.get('/api/staffs/topstaff/?top=0&quarter=0&year=0')
        .then(
            (res) => {
                angular.forEach(res.data, (item) => {
                    $scope.modelStaffTop.push(item);
                })
            },
            (err) => { console.log(err); }
        );
    $scope.viewChartStaffTop($scope.modelStaffTop, "Doanh thu của top 10 nhân viên năm 2016 - 2018");

    $scope.changeChartStaffTop = (top, quarter, year) => {
        $scope.title = 'Doanh thu của nhân viên cửa hàng theo top, quý và năm';
        if (isNaN(parseInt(quarter))) {
            quarter = 0;
        }
        if (isNaN(parseInt(year))) {
            year = 0;
        }
        if (isNaN(parseInt(top))) {
            top = 0;
        }
        var apiUrl = '/api/staffs/topstaff/?top=' + top + '&quarter=' + quarter + '&year=' + year;

        $http.get(apiUrl)
            .then(
                (res) => {
                    $scope.model = [];
                    angular.forEach(res.data, (item) => {
                        $scope.model.push(item);
                    });
                    $scope.viewChartStaffTop($scope.model, $scope.title);
                },
                (err) => { console.log(err); }
            );
    }
})