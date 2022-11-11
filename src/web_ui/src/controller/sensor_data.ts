import axios from "axios";

export type SensorType = {
    id: string
    weight: number,
    occupancy: boolean
};

export async function get_sensor_data_from_api() {
    try {
        const response = await axios.get("https://mocki.io/v1/62f644b3-1e57-4401-b0ae-5693b98be671");
        let sensor_data: SensorType[] = []
        for (let i = 0; i < response.data.length; i++) {
            const data: SensorType = {
                id: response.data[i].id,
                weight: response.data[i].weight,
                occupancy: response.data[i].occupancy
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