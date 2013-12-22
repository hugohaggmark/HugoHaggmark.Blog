'use strict';

var taskManagerApp = angular.module('taskManagerApp', ["angles"])
    .service('signalRSvc', function ($rootScope) {
        var initialize = function () {
            var cpuHub = $.connection.cpuHub;

            cpuHub.client.cpuInfo = function (machineName, cpu) {
                $rootScope.$emit("cpuInfo", machineName, cpu);
            }

            $.connection.hub.start().done(function () {
                cpuHub.server.initRootUri();
            });
        };

        return {
            initialize: initialize
        };
    })
    .controller('ChartController', function ($scope, signalRSvc, $rootScope) {
        $scope.machineName = "localhost";
        $scope.cpuChartLabel = "Total % Processor Time";
        $scope.lineChartData = {
            labels: [""],
            datasets: [
                {
                    fillColor: "rgba(241,246,250,0.5)",
                    strokeColor: "rgba(17,125,187,1)",
                    pointColor: "rgba(17,125,187,1)",
                    pointStrokeColor: "#fff",
                    data: [0]
                }
            ]
        };

        $scope.options = {

            //Boolean - If we show the scale above the chart data			
            scaleOverlay: false,

            //Boolean - If we want to override with a hard coded scale
            scaleOverride: true,

            //** Required if scaleOverride is true **
            //Number - The number of steps in a hard coded scale
            scaleSteps: 10,
            //Number - The value jump in the hard coded scale
            scaleStepWidth: 10,
            //Number - The scale starting value
            scaleStartValue: 0,

            //String - Colour of the scale line	
            scaleLineColor: "rgba(0,0,0,.1)",

            //Number - Pixel width of the scale line	
            scaleLineWidth: 1,

            //Boolean - Whether to show labels on the scale	
            scaleShowLabels: true,

            //Interpolated JS string - can access value
            scaleLabel: "<%=value%>",

            //String - Scale label font declaration for the scale label
            scaleFontFamily: "'Arial'",

            //Number - Scale label font size in pixels	
            scaleFontSize: 12,

            //String - Scale label font weight style	
            scaleFontStyle: "normal",

            //String - Scale label font colour	
            scaleFontColor: "#666",

            ///Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,

            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",

            //Boolean - Whether the line is curved between points
            bezierCurve: false,

            //Boolean - Whether to show a dot for each point
            pointDot: false,

            //Boolean - Whether to animate the chart
            animation: false,
        };

        signalRSvc.initialize();

        var updateChartData = function (machineName, cpu) {
            if ($scope.lineChartData.labels.length > 20) {
                $scope.lineChartData.labels.shift();
            }

            $scope.lineChartData.labels.push("");

            if ($scope.lineChartData.datasets[0].data.length > 20) {
                $scope.lineChartData.datasets[0].data.shift();
            }

            $scope.lineChartData.datasets[0].data.push(cpu);
        }

        $scope.$parent.$on("cpuInfo", function (e, machineName, cpu) {
            $scope.$apply(function () {                
                $scope.machineName = machineName;
                updateChartData(machineName, cpu)
            });
        });
    });