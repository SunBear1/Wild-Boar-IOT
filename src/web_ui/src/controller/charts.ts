import type {SensorType} from "@/controller/sensor_data";

export function prepareBarChartData(raw_data: SensorType[]) {
    let chest_avg_weight: number = 0
    let biceps_avg_weight: number = 0
    let treadmill_avg_weight: number = 0
    for (let i = 0; i < raw_data.length; i++) {
        if (raw_data[i].type == "BICEPS_MACHINE") {
            biceps_avg_weight += raw_data[i].weight
        } else if (raw_data[i].type == "CHEST_MACHINE") {
            chest_avg_weight += raw_data[i].weight
        } else if (raw_data[i].type == "TREADMILL") {
            treadmill_avg_weight += raw_data[i].weight
        }
    }
    chest_avg_weight /= raw_data.length
    biceps_avg_weight /= raw_data.length
    treadmill_avg_weight /= raw_data.length
    return {
        labels: ['Chest machine', 'Biceps machine', 'Treadmill'],
        datasets: [
            {
                label: 'Average weight on the machine over all months',
                backgroundColor: '#00C7FD',
                data: [chest_avg_weight, biceps_avg_weight, treadmill_avg_weight]
            }
        ]
    }
}

export function prepareDoughnutChartData(raw_data: SensorType[]) {
    let avg_occupancy = 0
    for (let i = 0; i < raw_data.length; i++) {
        if (raw_data[i].occupied == "Occupied") {
            avg_occupancy++
        }
    }
    avg_occupancy /= raw_data.length
    return {
        labels: ['Occupied', 'Available'],
        datasets: [
            {
                label: 'Average occupancy of the machine(s) over all months',
                backgroundColor: ['#F84F31', '#23C552'],
                data: [avg_occupancy, 1 - avg_occupancy]
            }
        ]
    }
}

export function prepareLineChartData(raw_data: SensorType[]) {
    const months: string[] = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'November', 'December']
    let chest_avg_weights: number[] = []
    let biceps_avg_weights: number[] = []
    let treadmill_avg_weights: number[] = []
    let chest_avg_counter: number[] = []
    let biceps_avg_counter: number[] = []
    let treadmill_avg_counter: number[] = []

    for (let i = 0; i < months.length; i++) {
        chest_avg_weights.push(0)
        biceps_avg_weights.push(0)
        treadmill_avg_weights.push(0)
        chest_avg_counter.push(0)
        biceps_avg_counter.push(0)
        treadmill_avg_counter.push(0)
    }

    for (let i = 0; i < raw_data.length; i++) {
        if (raw_data[i].type == "CHEST_MACHINE") {
            chest_avg_weights[raw_data[i].rawDate.getMonth()] += raw_data[i].weight
            chest_avg_counter[raw_data[i].rawDate.getMonth()]++
        } else if (raw_data[i].type == "BICEPS_MACHINE") {
            biceps_avg_weights[raw_data[i].rawDate.getMonth()] += raw_data[i].weight
            biceps_avg_counter[raw_data[i].rawDate.getMonth()]++
        } else if (raw_data[i].type == "TREADMILL") {
            treadmill_avg_weights[raw_data[i].rawDate.getMonth()] += raw_data[i].weight
            treadmill_avg_counter[raw_data[i].rawDate.getMonth()]++
        }
    }

    for (let i = 0; i < months.length; i++) {
        chest_avg_weights[i] /= chest_avg_counter[i]
        biceps_avg_weights[i] /= biceps_avg_counter[i]
        treadmill_avg_weights[i] /= treadmill_avg_counter[i]
    }

    return {
        labels: months,
        datasets: [
            {
                label: 'Chest machine average weight',
                backgroundColor: '#00C7FD',
                data: chest_avg_weights
            },
            {
                label: 'Biceps machine average weight',
                backgroundColor: '#FCEB19',
                data: biceps_avg_weights
            },
            {
                label: 'Treadmill average weight',
                backgroundColor: '#FC1962',
                data: treadmill_avg_weights
            },
        ]
    }
}