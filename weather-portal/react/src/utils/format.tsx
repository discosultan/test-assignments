export function dateToHourMinuteStr(date: Date) {
    return numberToZeroPaddedStr(date.getHours()) + ":" + numberToZeroPaddedStr(date.getMinutes());
}

function numberToZeroPaddedStr(value: number) {
    return ("0" + value).slice(-2);
}