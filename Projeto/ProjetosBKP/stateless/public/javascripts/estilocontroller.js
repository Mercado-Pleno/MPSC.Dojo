angular.module('exemplo',[])
  .controller('estilocontroller', ['$scope','$http',
    function($scope, $http) {
      var sessionToken = sessionStorage.token;
      console.log("Session Token: " + sessionToken);
      if (sessionToken) {
        $http.defaults.headers.common.Authorization = 'BASIC ' + sessionToken;
      }
      console.log("Header: " + $http.defaults.headers.common.Authorization);
      debugger;
      $http.get('/estilos').success(function(lista) {
        debugger;
        $scope.estilos = lista;
      })
      .error(function(data, status, headers, config) {
        debugger;
        window.location.href = "/login";
      });
      ;
    }
]);
