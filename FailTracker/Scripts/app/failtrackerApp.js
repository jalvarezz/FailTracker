﻿(function () { 
    'use strict';

    window.onerror = function (msg) {
        if (window.alerts) {
            window.alerts.error("There was a problem with your last action. Please reload this page.");
        } else {
            alert("Something serious went wrong. Please close out of Fail Tracker, then try entering again.");
        }
    };

    var id = 'failtrackerApp';

    var failtrackerApp = angular.module(id, []);

    failtrackerApp.run([
            function () {
                //Startup code goes here!
            }
    ]);

})();