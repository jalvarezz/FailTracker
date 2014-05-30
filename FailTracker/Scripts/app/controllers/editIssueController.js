﻿(function () {
    'use strinct';

    var controllerId = 'editIssueController';

    angular.module('failtrackerApp').controller(controllerId,
        ['$scope', '$http', 'alerts', editIssueController]);

    function editIssueController($scope, $http, alerts) {
        $scope.editing = false;
        $scope.init = init;
        $scope.save = save;
        $scope.cancel = cancel;
        $scope.edit = edit;

        function init(issue){
            $scope.originalIssue = angular.extend({}, issue);
            $scope.issue = issue;
        }

        function edit(){
            $scope.editing = true;
        }

        function save(){
            $http.post("/Issue/Edit", $scope.issue)
                .success(function(data){ 
                    $scope.originalIssue = angular.extend({}, $scope.issue);

                    $scope.editing = false;

                    alerts.success("Your changes have been saved!");
                })
                .error(function (data) {
                    if (data.errorMessage) {
                        alerts.error("There was a problem saving the issue: \n" + data.errorMessage);
                    } else {
                        alerts.error("There was a problem saving the issue. Please try again.");
                    }
                });
        }

        function cancel() {
            $scope.issue = null;
            $scope.editing = false;
        }
    }
})();