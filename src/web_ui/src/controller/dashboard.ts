import {DASHBOARD_DATA_API_URL} from "@/constants/endpoints";
import axios from "axios";
import type {SensorType} from "@/controller/sensor_data";
import {parseOccupancy, toFormattedDate} from "@/controller/sensor_data";

export type DashboardType = {
    last_msg: SensorType,
    chest_avg_weight: number,
    biceps_avg_weight: number,
    treadmill_avg_weight: number,
    chest_avg_occupancy: number,
    biceps_avg_occupancy: number,
    treadmill_avg_occupancy: number,
};

export async function get_dashboard_data_from_api() {
    try {
        const response = await axios.get(DASHBOARD_DATA_API_URL);
        return convertToDashboardType(response)
    } catch (error) {
        if (error instanceof Error) {
            console.log('Error message: ', error.message);
            return error.message;
        } else {
            console.log('Unexpected error: ', error);
            return 'An unexpected error occurred';
        }
    }
}

function convertToDashboardType(response: any) {
    const last_msg: SensorType = {
        id: response.data["lastReceivedMessage"]["id"],
        type: response.data["lastReceivedMessage"]["type"],
        weight: response.data["lastReceivedMessage"]["weight"],
        occupied: parseOccupancy(response.data["lastReceivedMessage"]["occupied"]),
        rawDate: response.data["lastReceivedMessage"]["date"],
        formattedDate: toFormattedDate(new Date(response.data["lastReceivedMessage"]["date"])),
    };
    const dashboard_data: DashboardType = {
        last_msg: last_msg,
        biceps_avg_weight: response.data["bicepsAVGweight"],
        biceps_avg_occupancy: response.data["chestAVGoccupancy"],
        chest_avg_weight: response.data["chestAVGweight"],
        chest_avg_occupancy: response.data["chestAVGoccupancy"],
        treadmill_avg_occupancy: response.data["treadmillAVGoccupancy"],
        treadmill_avg_weight: response.data["treadmillAVGweight"],
    }
    return dashboard_data
}
