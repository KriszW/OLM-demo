window.SetFocusToElement = (element) => {
    element.focus();
};

window.drawGauge = (data) => {

    TESTER = document.getElementById(data.divID);

    var maxtco = data.maxTCO;

    var gaugeData = [
        {
            type: "indicator",
            mode: "gauge+number+delta",
            value: data.tco,
            title: { text: "TCO érték", font: { size: 24 } },
            delta: { reference: data.expectedTCO, decreasing: { color: "green" }, increasing: { color: "red" } },
            gauge: {
                axis: { range: [0, maxtco], tickwidth: 1, tickcolor: "white" },
                //axis: { tickvals: [data.expectedTCO, data.tco], ticktext: ["Elvárt",""], ticklen: 25, ticks: "outside", tickfont: { size:25 }, tickcolor: "white" },
                bar: { color: "black" },
                bgcolor: "lightgray",
                borderwidth: 2,

                steps: [
                    { range: [0, data.expectedTCO], color: "green" },
                    { range: [data.expectedTCO, data.maxAcceptableTCO], color: "orange" },
                    { range: [data.maxAcceptableTCO, maxtco], color: "darkred" }
                ],
                threshold: {
                    line: { color: "red", width: 4 },
                    thickness: 0.75,
                    value: data.expectedTCO
                }
            },
            showlegend: false
        }
    ];

    var layout = {
        width: document.height * .1,
        height: document.width,
        margin: { t: 25, r: 25, l: 25, b: 25 },
        paper_bgcolor: "#f5f5f5",
        font: { color: "black", family: "Arial" }
    };

    var config = { responsive: true, displaylogo: false };

    Plotly.newPlot(TESTER, gaugeData, layout, config);

};

window.drawWeeklyDayFollowUpWaste = (data) => {

    var div = document.getElementById(data.divID);

    var data = aggregateWeeklyDayDataIntoPlotlyData(data);

    var layout = {
        padding: 0,
        margin: 0,
        yaxis: {
            tickformat: ',.2%'
        },
        barmode: 'stack'
    };

    var config = { responsive: true, displaylogo: false };

    Plotly.newPlot(div, data, layout, config);
};

function aggregateWeeklyDayDataIntoPlotlyData(data) {
    var targetLine = { x: data.xAxisData, y: data.targetYAxisData, type: 'scatter', name: "Target szabászat selejt %" };

    var totalWasteLine = { x: data.xAxisData, y: data.totalWasteYAxisData, type: 'scatter', name: "total selejt" };

    var sawPercBar = { x: data.xAxisData, y: data.sawYAxisData, type: 'bar', name: "Fűrész hulladék %" };

    var FSPercBar = { x: data.xAxisData, y: data.fsYAxisData, type: 'bar', name: "Hossztoldott selejt %" };

    var tramPercBar = { x: data.xAxisData, y: data.tramYAxisData, type: 'bar', name: "Csille %" };

    var lamPercBar = { x: data.xAxisData, y: data.lamYAxisData, type: 'bar', name: "Kitett lamella selejt %" };

    return [targetLine, totalWasteLine, sawPercBar, FSPercBar, tramPercBar, lamPercBar];
}

window.drawDimensionDayFollowUpWaste = (data) => {

    var div = document.getElementById(data.divID);

    var data = aggregateDimensionDayDataIntoPlotlyData(data);

    var layout = {
        padding: 0,
        margin: 0,
        yaxis: {
            tickformat: ',.2%'
        },
        barmode: 'stack'
    };

    var config = { responsive: true, displaylogo: false };

    Plotly.newPlot(div, data, layout, config);
};

function aggregateDimensionDayDataIntoPlotlyData(data) {
    var targetLine = { x: data.xAxisData, y: data.targetYAxisData, type: 'scatter', name: "Target szabászat selejt %" };

    var sawPercBar = { x: data.xAxisData, y: data.sawYAxisData, type: 'bar', name: "Fűrész hulladék %" };

    var FSPercBar = { x: data.xAxisData, y: data.fsYAxisData, type: 'bar', name: "Hossztoldott selejt %" };

    var tramPercBar = { x: data.xAxisData, y: data.tramYAxisData, type: 'bar', name: "Csille %" };

    var lamPercBar = { x: data.xAxisData, y: data.lamYAxisData, type: 'bar', name: "Kitett lamella selejt %" };

    return [targetLine, sawPercBar, FSPercBar, tramPercBar, lamPercBar];
}

window.drawYearlyWeeksWasteFollowUp = (data) => {

    var div = document.getElementById(data.divID);

    var data = aggregateYearlyWeeksDataIntoPlotlyData(data);

    var layout = {
        padding: 0,
        margin: 0,
        yaxis: {
            tickformat: ',.2%'
        },
        barmode: 'stack'
    };

    var config = { responsive: true, displaylogo: false };

    Plotly.newPlot(div, data, layout, config);
};

function aggregateYearlyWeeksDataIntoPlotlyData(data) {
    var targetLine = { x: data.xAxisData, y: data.targetYAxisData, type: 'scatter', name: "Target szabászat selejt %" };

    var sawPercBar = { x: data.xAxisData, y: data.sawYAxisData, type: 'bar', name: "Fűrész hulladék %" };

    var FSPercBar = { x: data.xAxisData, y: data.fsYAxisData, type: 'bar', name: "Hossztoldott selejt %" };

    var tramPercBar = { x: data.xAxisData, y: data.tramYAxisData, type: 'bar', name: "Csille %" };

    var lamPercBar = { x: data.xAxisData, y: data.lamYAxisData, type: 'bar', name: "Kitett lamella selejt %" };

    return [targetLine, sawPercBar, FSPercBar, tramPercBar, lamPercBar];
}

window.drawRoutingChart = (data) => {

    var div = document.getElementById(data.divID);

    var max = data.max;

    var barColor = "red";

    if (data.actual >= data.expected) {
        barColor = "green";
    }

    var chartData = [
        {
            type: "indicator",
            mode: "number+gauge",
            value: data.actual,
            domain: { x: [0, 1], y: [0, 1] },
            title: {},
            gauge: {
                shape: "bullet",
                axis: { range: [0, max] },
                threshold: {
                    line: { color: "red", width: 2, gradient: { yanchor: "vertical" } },
                    thickness: 0.75,
                    value: data.expected
                },
                bgcolor: "lightgray",
                bar: { color: barColor }
            }
        }
    ];

    var h = 0;

    if (data.height != null) {
        h = data.height;
    }
    else {
        h = (window.getDocumentHeight() / data.horizontalCount) - data.heightDiff;
    }

    var layout = { height: h };
    var config = { responsive: true, displaylogo: false };

    Plotly.newPlot(div, chartData, layout, config);
};

window.getDocumentHeight = () => {
    var body = document.body,
        html = document.documentElement;

    var height = Math.max(body.scrollHeight, body.offsetHeight,
        html.clientHeight, html.scrollHeight, html.offsetHeight);

    return height;
};

window.getDocumentWidth = () => {
    var body = document.body,
        html = document.documentElement;

    var width = Math.max(body.scrollWidth, body.offsetWidth,
        html.clientWidth, html.scrollWidth, html.offsetWidth);

    return width;
};