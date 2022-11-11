import axios from "axios";


export async function get_api_status() {
    const response = await axios.get("https://gourmetapp.net/api/v1/status");
    return response.data
}
