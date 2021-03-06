(function(){

    var app = angular.module("Billing");

    var CustomersCtrl = function($scope, $http) {

        $scope.showCustomer = false;
        ListCustomers();

        $scope.getCustomer = function(currentCustomer){
            $scope.customer = currentCustomer;
            $scope.showCustomer = true;
        };

        $scope.save = function(){
            console.log($scope.customer);
            var promise = $http({
                method: "put",
                url: "http://localhost:9000/api/customers/" + $scope.customer.id,
                data: $scope.customer
            });

            $scope.message = "Please wait...";
            promise.then(function(response){
                $scope.customer = response.data;
                $scope.message = " ";
                ListCustomers();
            }, function(reason){
                $scope.message = "No data for that request";
            });
        };

        $scope.new = function(){
            $scope.customer.id = 0;
            var promise = $http({
                method: "post",
                url: "http://localhost:9000/api/customers",
                data: $scope.agent
            });
            $scope.message = "Please wait...";
            promise.then(function(response){
                $scope.customer = response.data;
                $scope.message = " ";
                ListAgents();
            }, function(reason){
                $scope.message = "No data for that request";
            });
        };

        function ListCustomers(){
            var promise = $http.get("http://localhost:9000/api/customers");
            $scope.message = "Please wait...";
            promise.then(function(response){
                $scope.customers = response.data;
                $scope.message = " ";
            }, function(reason){
                $scope.message = "No data for that request";
            });
        }
    };

    app.controller("CustomersCtrl", CustomersCtrl);

}());
