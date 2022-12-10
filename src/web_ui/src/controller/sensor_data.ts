import axios from "axios";
import {SENSOR_DATA_API_URL} from "@/constants/endpoints";

export type SensorType = {
    id: number,
    type: string
    weight: number,
    occupied: boolean
    date: string
};

export async function get_sensor_data_from_api(data_format: string, url_parameters?: string) {
    let URL = SENSOR_DATA_API_URL
    if (typeof url_parameters !== 'undefined') {
        URL = URL + url_parameters
    }
    try {
        const response = await axios.get(URL, {
            headers: {
                'Accept': data_format,
            }
        });
        return convertToSensorType(response)
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


function convertToSensorType(response: any): SensorType[] {
    let sensor_data: SensorType[] = []
    if (response.headers['content-type'] == "application/json; charset=utf-8") {
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
    } else if (response.headers['content-type'] == "text/csv; charset=utf-8") {
        let arrayFromCSV = CSVtoArray(response.data)
        for (let i = 1; i < arrayFromCSV.length; i++) {
            const data: SensorType = {
                id: Number(arrayFromCSV[i][0]),
                type: arrayFromCSV[i][1],
                weight: Number(arrayFromCSV[i][2]),
                occupied: Boolean(arrayFromCSV[i][3]),
                date: arrayFromCSV[i][4],
            };
            sensor_data.push(data)
        }
    }
    return sensor_data
}

function CSVtoArray(text: string) {
    let p = '', row = [''], ret = [row], i = 0, r = 0, s = !0, l;
    for (l of text) {
        if ('"' === l) {
            if (s && l === p) row[i] += l;
            s = !s;
        } else if (',' === l && s) l = row[++i] = '';
        else if ('\n' === l && s) {
            if ('\r' === p) row[i] = row[i].slice(0, -1);
            row = ret[++r] = [l = ''];
            i = 0;
        } else row[i] += l;
        p = l;
    }
    return ret;
}