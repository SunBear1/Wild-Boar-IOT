import type {SensorType} from "@/controller/sensor_data";

export function prepareBarChartData(raw_data: SensorType[]) {
    let chest_avg_weight = 0
    let biceps_avg_weight = 0
    let treadmill_avg_weight = 0
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
                label: 'Avarage weight attached to machine',
                backgroundColor: '#00C7FD',
                data: [chest_avg_weight, biceps_avg_weight, treadmill_avg_weight]
            }
        ]
    }
}

export function prepareDoughNutChartData(raw_data: SensorType[]) {
    let avg_occupancy = 0
    for (let i = 0; i < raw_data.length; i++) {
        if (raw_data[i].occupied) {
            avg_occupancy++
        }
    }
    avg_occupancy /= raw_data.length
    return {
        labels: ['Occupied', 'Non occupied'],
        datasets: [
            {
                label: 'Avarage occupancy of the machine(s)',
                backgroundColor: ['#F84F31', '#23C552'],
                data: [avg_occupancy, 1 - avg_occupancy]
            }
        ]
    }
}