export interface Weather {
    name: string,
    temp: number,
    wind: Wind,
    iconUrl: string,
    date: Date
}

export interface Wind { 
    speed: number,
    deg: number
}