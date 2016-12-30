function isLeapYear(year) {
    return year % 4 == 0 ? year % 100 != 0 : year % 400 == 0;
}
// takes a month & year (as numbers), and a reference to the select list for the days
function changeDaysAvailable(selectedMonth, selectedYear, daySelectList) {
    var thirtyDays = [4, 6, 9, 11]
    if ($.inArray(selectedMonth, thirtyDays) > -1) {
        var end = 30;
    }
    else if (selectedMonth == 2) {
        var end = isLeapYear(selectedYear) ? 29 : 28;
    }
    else {
        var end = 31;
    }
    if (daySelectList.length != end) {
        daySelectList.empty();
        // Keep this select list labeled like the others
        daySelectList.append(new Option("Day", 0))
        for (var i = 1; i <= end; i++) {
            daySelectList.append(new Option(i, i));
        }
    }
}