(function () {
    var myModule = angular.module("testAngularApp");

    myModule.service("testAngularService", function ($http) {
        return ({
            getValues: getValues,
            getValue: getValue,
            postValue: postValue,
            deleteValue: deleteValue
        });

        function getValues() {
            var request = $http({
                headers: { 'Content-Type': "application/json;charset=utf-8" },
                method: "get",
                url: "/api/AngularValues"
            });
            return request;
        };

        function getValue(id) {
            var request = $http({
                headers: { 'Content-Type': "application/json;charset=utf-8" },
                method: "get",
                url: "/api/AngularValues/" + id
            });
            return request;
        };

        function postValue(value) {
            var request = $http({
                headers: { 'Content-Type': "application/json;charset=utf-8" },
                method: "post",
                url: "/api/AngularValues",
                data: JSON.stringify(value)
            });
            return request;
        };

        function deleteValue(id) {
            var request = $http({
                headers: { 'Content-Type': "application/json;charset=utf-8" },
                method: "delete",
                url: "/api/AngularValues/" + id
            });
            return request;
        };


    });
}());