app.config(function ($routeProvider, $locationProvider) {
    $locationProvider.html5Mode(true);

    $routeProvider
        .when('/home', {
            templateUrl: '../../pages/home.html',
            controller: 'homeController'
        })
        .when('/stores', {
            templateUrl: '../../pages/stores.html',
            controller: 'storesController'
        })
        .when('/brands', {
            templateUrl: '../../pages/brands.html',
            controller: 'brandsController'
        })
        .when('/categories', {
            templateUrl: '../../pages/categories.html',
            controller: 'categoriesController'
        })
        .when('/products', {
            templateUrl: '../../pages/product.html',
            controller: 'productController'
        })
        .when('/inventory', {
            templateUrl: '../../pages/inventory.html',
            controller: 'inventoryController'
        })
        .when('/staff', {
            templateUrl: '../../pages/staff.html',
            controller: 'staffController'
        })
        .otherwise({ redirectTo: '/home' })
})