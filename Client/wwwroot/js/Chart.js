$.ajax({
    url: "https://localhost:44351/API/Employees/Register/",
    type: "GET"
}).done((result) => {

    console.log("gender:");
    console.log(result);

    var male = result.result.filter((g) => {
        return g.gender === 0;
    });
    var female = result.result.filter((g) => {
        return g.gender === 1;
    });

    GenderPieChart(male.length, female.length);

}).fail((error) => {

    console.log(error);

});

$.ajax({
    url: "https://localhost:44351/API/Universities/Count",
    type: "GET"
}).done((result) => {

    console.log("univ:");
    console.log(result);

    var univName = [];
    var totalUniv = [];

    $.each(result.result, function (key, val) {
        univName.push(val.universityName);
        totalUniv.push(val.univCount);
    });

    console.log(univName);
    console.log(totalUniv);

    UnivBarChart(univName, totalUniv);

}).fail((error) => {

    console.log(error);

});

function GenderPieChart(male, female) {
    let options = {
        series: [male, female],
        labels: ["Male", "Female"],
        chart: {
            type: 'donut',
            toolbar: {
                show: true,
                offsetX: 0,
                offsetY: 0,
                tools: {
                    download: true,
                    selection: true,
                    zoom: true,
                    zoomin: true,
                    zoomout: true,
                    pan: true,
                    reset: true | '<img src="/static/icons/reset.png" width="20">',
                    customIcons: []
                },
                export: {
                    csv: {
                        filename: undefined,
                        columnDelimiter: ',',
                        headerCategory: 'category',
                        headerValue: 'value',
                        dateFormatter(timestamp) {
                            return new Date(timestamp).toDateString()
                        }
                    },
                    svg: {
                        filename: undefined,
                    },
                    png: {
                        filename: undefined,
                    }
                },
                autoSelected: 'zoom'
            }
        },
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    width: 200
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    };

    var chart = new ApexCharts(document.querySelector("#pieChartGender"), options);
    chart.render();
}

function UnivBarChart(univName, totalUniv) {

    var options = {
        series: [{
            data: totalUniv
        }],
        chart: {
            type: 'bar',
            height: 350,
            toolbar: {
                show: true,
                offsetX: 0,
                offsetY: 0,
                tools: {
                    download: true,
                    selection: true,
                    zoom: true,
                    zoomin: true,
                    zoomout: true,
                    pan: true,
                    reset: true | '<img src="/static/icons/reset.png" width="20">',
                    customIcons: []
                },
                export: {
                    csv: {
                        filename: undefined,
                        columnDelimiter: ',',
                        headerCategory: 'category',
                        headerValue: 'value',
                        dateFormatter(timestamp) {
                            return new Date(timestamp).toDateString()
                        }
                    },
                    svg: {
                        filename: undefined,
                    },
                    png: {
                        filename: undefined,
                    }
                },
                autoSelected: 'zoom'
            }
        },
        plotOptions: {
            bar: {
                borderRadius: 4,
                horizontal: true,
            }
        },
        dataLabels: {
            enabled: false
        },
        xaxis: {
            categories: univName
        }
    };

    var chart = new ApexCharts(document.querySelector("#barChartUniv"), options);
    chart.render();
}