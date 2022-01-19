export interface sensor{
    key: string,
    value: number,
    unit: string
}
  
export interface Msg {
    recordID: string,
    sessionID: string,
    timeStamp: string,
    data: sensor[]
}

export interface Dev {
    device_uuid: string,
    timeStamp: string,
    status: number
}