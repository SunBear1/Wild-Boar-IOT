export function convertToChartData(idx: number) {
    let charts_data = {
        labels: ['January', 'February', 'March'],
        datasets: [
            {
                label: 'Data One',
                backgroundColor: '#00C7FD',
                data: [idx, idx * 2, idx * 3]
            }
        ]
    }
    return charts_data
}