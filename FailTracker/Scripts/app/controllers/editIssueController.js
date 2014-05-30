(function () {
    'use strinct';

    var controllerId = 'editIssueController';

    angular.module('failtrackerApp').controller(controllerId,
        ['$scope', '$http', 'alerts', editIssueController]);

    function editIssueController($scope, $http, alerts) {
        $scope.editing = false;
        $scope.init = init;
        $scope.index = index;
        $scope.save = save;
        $scope.cancel = cancel;
        $scope.edit = edit;
        $scope.deleteitem = deleteitem;

        function init(issue){
            $scope.originalIssue = angular.extend({}, issue);
            $scope.issue = issue;
        }

        function index(url) {
            window.location = url;
        }

        function edit(){
            $scope.editing = true;
        }

        function save(){
            $http({ method: 'POST', 
                    url: '/Issue/Edit/', 
                    data: $scope.issue, 
                    headers: { '__RequestVerificationToken':  $scope.antiForgeryToken }})
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

        function deleteitem(returnUrl) {
            if (confirm("Are you sure that you want to edit the issue '" + $scope.issue.subject + "'?")) {
                $http({
                    method: 'POST',
                    url: '/Issue/Delete/',
                    data: JSON.stringify({ id: $scope.issue.issueID }, null),
                    headers: { '__RequestVerificationToken': $scope.antiForgeryToken }
                })
                    .success(function (data) {
                        window.location = '/Issue';
                    })
                    .error(function (data) {
                        if (data.errorMessage) {
                            alerts.error("There was a problem deleting the issue: \n" + data.errorMessage);
                        } else {
                            alerts.error("There was a problem deleting the issue. Please try again.");
                        }
                    });
            }
        }

        function cancel() {
            $scope.issue = $scope.originalIssue;
            $scope.editing = false;
        }
    }
})();