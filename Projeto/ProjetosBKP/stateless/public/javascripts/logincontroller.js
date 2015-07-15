angular.module('loginmodule',[])
  .controller('logincontroller', ['$scope','$http',
    function($scope, $http) {
      $scope.loginUsuario = function() {
        var objeto = {username: $scope.username,
           senha: $scope.senha};
        console.log("Enviando: " + JSON.stringify(objeto));           
        $http.post('/login',objeto)
           .success(function(data,status) {
             var token = btoa("{\"username\" : \"" 
                        + $scope.username 
                        + "\", \"senha\" : \"" 
                        + $scope.senha + "\"}");
             $http.defaults.headers.common.Authorization = 'BASIC ' + token;
             sessionStorage.token = token;
             console.log("Login OK");
             window.location.href = "/";
           })
           .error(function(data, status) {
           });
      };
    }
]);

