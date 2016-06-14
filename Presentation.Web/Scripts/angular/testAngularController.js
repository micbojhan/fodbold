(function () {
    "use strict";
    var myModule = angular.module("testAngularApp");

    myModule.controller("testAngularController", function (testAngularService) {
        var testCtr = this;
        testCtr.values = [{ Id: 0, Name: "Number 0" }];
        
        testCtr.getValues = function () {
            console.log("get: nul");
            testAngularService.getValues().success(function(result) {
                testCtr.values = result;
                console.log("got: " +result);
            });
        };

        testCtr.addValue = function (value) {
            console.log("add: " + value);
            testAngularService.postValue(value).success(function (result) {
                console.log("added: " + result);
                testCtr.values.push(result);
            });
        };

        testCtr.removeValue = function (id) {
            console.log("remove: " + id);
            testAngularService.deleteValue(id).success(function (result) {
                remove(testCtr.values, result);
                console.log("removed: " + result, result.Id);
            });
        };


        function remove(arr, item) {
            for (var i = arr.length; i--;) {
                if (arr[i].Id === item.Id) {
                    arr.splice(i, 1);
                }
            }
        }


        //testCtr.getValues();
    });

}());

