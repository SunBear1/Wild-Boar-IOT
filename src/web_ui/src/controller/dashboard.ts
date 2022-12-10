import {DASHBOARD_DATA_API_URL} from "@/constants/endpoints";
import axios from "axios";
import type {SensorType} from "@/controller/sensor_data";

export type DashboardType = {
    last_msg: SensorType,
    chest_avg_weight: number,
    biceps_avg_weight: number,
    treadmill_avg_weight: number,
    chest_avg_occupancy: number,
    biceps_avg_occupancy: number,
    treadmill_avg_occupancy: number,
};

export async function get_dashboard_data_from_api(data_format: string, url_parameters?: string) {
    try {
        const response = await axios.get(DASHBOARD_DATA_API_URL);
        let dashboard_data: DashboardType
        dashboard_data.last_msg = SensorType()
        return response.data
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

function convertToDashboardType(response: any): SensorType[] {
    const last_msg: SensorType = {
        id: response.data["lastReceivedMessage"]["id"],
        type: response.data["lastReceivedMessage"]["type"],
        weight: response.data["lastReceivedMessage"]["weight"],
        occupied: response.data["lastReceivedMessage"]["occupied"],
        date: response.data["lastReceivedMessage"]["date"],
    };
    const dashboard_data: DashboardType = {
        last_msg: last_msg,
        biceps_avg_weight: response.data["chest"]
    }
    return sensor_data
}
