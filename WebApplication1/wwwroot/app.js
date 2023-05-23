

async function Work() {
    let lineChart = BuildLine("Temp")
    let circleChart = BuildCircle("Temp")
    let flag = true
    while (1) {
        const response = await fetch("/api/data", {
            method: "GET",
            headers: { "Accept": "application/json" }
        });
        if (response.ok === true) {
            const data = await response.json();
            console.dir(data)
            if (flag) {
                console.dir(data.slice(-1)[0].tempD)
                let cold = 0, normal = 0, warm = 0
                data.forEach(el => {
                    if (el.tempD < 20)
                        cold++
                    else if (el.tempD >= 20 && el.tempD < 40)
                        normal++
                    else
                        warm++;
                })
                circleChart.data.datasets[0].data = [cold, normal, warm]
                circleChart.update();
                flag = false
            }
            else {
                if (data.slice(-1)[0].tempD < 20)
                    circleChart.data.datasets[0].data[0]+= 1
                else if (data.slice(-1)[0].tempD >= 20 && data.slice(-1)[0].tempD < 40)
                    circleChart.data.datasets[0].data[1]+= 1
                else
                    circleChart.data.datasets[0].data[2] += 1
                circleChart.update()
            }
            lineChart.data.datasets[0].data = []
            lineChart.data.labels = []
            data.slice(-10).forEach(el => {
                lineChart.data.datasets[0].data.push(el.tempD)
            })
            data.slice(-10).forEach(el => {
                lineChart.data.labels.push(el.timeD)
            })
            
            lineChart.update();
        }
        await new Promise(res => setTimeout(res, 10000));
        
    }
}



function BuildLine(chartTitle) {
    var data = {
        labels: [],
        datasets: [{
            label: chartTitle, 
            data: [],
        }],
    };
    var ctx = document.getElementById('myChartLine');
    var myChart = new Chart(ctx, {
        type: 'line',
        data: data,
        options: {
            
            scales: {
                y: {
                    display: true
                }
            }
        }
    });
    return myChart;
}

function BuildCircle(chartTitle) {
    var data = {
        labels: ['Cold', 'Normal', 'Warm'],
        datasets: [{
            label: chartTitle,
            data: [],
            backgroundColor: [
                'rgb(54, 162, 235)',
                'rgb(255, 205, 86)',
                'rgb(255, 99, 132)'
            ]
        }],
    };
    var ctx = document.getElementById('myChartCircle');
    var myChart = new Chart(ctx, {
        type: 'pie',
        data: data,
        options: {
            scales: {
                y: {
                    display: true
                }
            }
        }
    });
    return myChart;
}

Work();