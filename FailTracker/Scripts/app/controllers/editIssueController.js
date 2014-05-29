(function () {
    'use strinct';

    var controllerId = 'editIssueController';

    angular.module('failtrackerApp').controller(controllerId,
        ['$scope', '$http', editIssueController]);

    function editIssueController($scope, $http) {
        $scope.editing = false;
        $scope.init = init;
        $scope.save = save;
        $scope.cancel = cancel;
        $scope.edit = edit;

        $scope.init(issue){
            $scope.originalIssue = angular.extend({}, issue);
            $scope.issue = issue;
        }

        $scope.edit(){
            $scope.editing = true;
        }

        $scope.save(){
            $http.post("/Issue/Edit", $scope.issue)
                .success(function(data){ 
                    if(data.success !== true){
                        alert("There was a problem saving to the server: " + data.errorMessage);
                        return;
                    }

                    $scope.originalIssue = angular.extend({}, $scope.issue);

                    $scope.editing = false;
                })
                .error(function(){ 
                    alert("There was a problem saving the issue. Please try again.");
                });
        }

        $scope.cancel() { }
    }
});