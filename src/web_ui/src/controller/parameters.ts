import moment from "moment";

export function parse_parameters(url_parameters_list: string[]): string {
    let url_string = ""

    for (let i = 0; i < url_parameters_list.length; i++) {
        if (i === 0) {
            url_string = url_string + "?" + url_parameters_list[i]
        } else {
            url_string = url_string + "&" + url_parameters_list[i]
        }
    }
    return url_string;
}

export function collect_parameters(sort: string, type: string, occupancy: string, weight: string, order: string, date_start?: string, date_end?: string,): string[] {
    let parameters: string[] = []
    parameters.push("sort=" + sort)
    if (type !== "all") {
        parameters.push("type=" + type)
    }
    if (occupancy !== "all") {
        parameters.push("occupied=" + occupancy)
    }
    if (date_start !== undefined && date_start !== "") {
        parameters.push("date_start=" + moment(date_start).format('MM-DD-YYYY'))
    }
    if (date_end !== undefined && date_end !== "") {
        parameters.push("date_end=" + moment(date_end).format('MM-DD-YYYY'))
    }
    if (weight !== "") {
        parameters.push("weight=" + weight)
    }
    if (order !== "") {
        parameters.push("order=" + order)
    }
    return parameters;
}