var app = angular.module("modul", ["ngRoute"]);

app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "DerslerinListesi.html",
            controller: "DerslerinListesiCtrl"
        })
        .when("/derslerinlistesi", {
            templateUrl: "DerslerinListesi.html",
            controller: "DerslerinListesiCtrl"
        })
        .when("/dersialanogrenciler", {
            templateUrl: "DersiAlanOgrenciler.html",
            controller: "DersiAlanOgrencilerCtrl"
        })
        .when("/not", {
            templateUrl: "Not.html",
            controller: "NotCtrl"
        });
});


app.controller("DerslerinListesiCtrl", function ($scope, $http, $rootScope, $location) {
    $rootScope.ogrencisorgulanacakdersid = null;
    $http.get("https://localhost:44357/api/Ders/TumDersleriGetir").then(function (response) {
        $scope.dersler = response.data;
    });
    $scope.derssil = function (dersid) {
        $http.get("https://localhost:44357/api/Ders/DersSil?DersID=" + dersid).then(function (response) {
            $scope.dersler = response.data;
        });

        $scope.dersialanogrencilerigetir = function (dersid, dersadi) {
            $rootScope.ogrencisorgulanacakdersid = dersid;
            $rootScope.secilendersadi = dersadi;
            $location.path('/dersialanogrenciler');
        }

    }


});
app.controller("DersiAlanOgrencilerCtrl", function ($scope, $rootScope, $location, $http) {
    $rootScope.listelenecekogrencidersid = null;
    if ($rootScope.ogrencisorgulanacakdersid == undefined || $rootScope.ogrencisorgulanacakdersid == null) {
        $location.path('/derslerinlistesi');
    }
    $http.get("https://localhost:44357/api/OgrenciDers/DersiAlanOgrencileriGetir?DersID=" + $rootScope.ogrencisorgulanacakdersid).then(function (response) {
        $scope.ogrenciler = response.data;
    });

    $scope.ogrencininderstenaldiginotlarigor = function (OgrenciDersID, ogrenciad) {
        $rootScope.listelenecekogrencidersid = OgrenciDersID;
        $rootScope.secilenogrenciadi = ogrenciad;
        $location.path('/not');
    }

});
app.controller("NotCtrl", function ($scope, $rootScope, $location, $http) {
    if ($rootScope.ogrencisorgulanacakdersid == undefined || $rootScope.ogrencisorgulanacakdersid == null) {
        $location.path('/derslerinlistesi');
    }
    else if ($rootScope.listelenecekogrencidersid == undefined || $rootScope.listelenecekogrencidersid == null)
        $location.path('/dersialanogrenciler');
    $http.get("https://localhost:44357/api/Not/OgrenciDersIDyeGoreNotlariGetir?OgrenciDersID=" + $rootScope.listelenecekogrencidersid).then(function (response) {
        $scope.notlar = response.data;
    });
});
