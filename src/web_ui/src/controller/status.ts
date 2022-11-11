import axios from "axios";


export async function get_api_status() {
    //const response_text = ref("")
    const response = await axios.get("https://gourmetapp.net/api/v1/inf_ping");
    //response_text.value = response.data
    return response.data
}
