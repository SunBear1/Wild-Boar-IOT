import axios from "axios";
import {API_URL} from "@/constants/endpoints";

export type SensorType = {
    id: number,
    type: string
    weight: number,
    occupied: boolean
    date: string
};

export async function get_sensor_data_from_api() {
    try {
        const response = await axios.get(API_URL);
        let sensor_data: SensorType[] = []
        for (let i = 0; i < response.data.length; i++) {
            const data: SensorType = {
                id: response.data[i].id,
                type: response.data[i].type,
                weight: response.data[i].weight,
                occupied: response.data[i].occupied,
                date: response.data[i].date,
            };
            sensor_data.push(data)
        }
        return sensor_data
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
