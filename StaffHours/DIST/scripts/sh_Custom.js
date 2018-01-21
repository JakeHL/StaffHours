const Endpoint = "/api/employeeoverview";



var Employees = {

    OverviewsRetreived: function (data) { },

    getOverviews: function() {
        var request = new XMLHttpRequest();
        request.open('GET', Endpoint, true);

        request.onload = function () {
            if (this.status >= 200 && this.status < 400) {
                // Success!
                var data = JSON.parse(this.response);
                Employees.OverviewsRetreived(data);
            } else {
                console.log("Error getting data");
            }
        }

        request.onerror = function () {
            console.log("Error connecting to endpoint");
        }

        request.send();
    }

}