//Get states as per country
const getstates = () => {
    var country = $('#CountryId').find(":selected").val()
    if (parseInt(country) != 0) {
        $.ajax({
            url: '/Home/getstates',
            type: 'POST',
            data: { country: country },
            success: function (result) {
                var s = `<option value = "">Select State</option >`
                $.each(result, function (index, value) {
                    s += `<option value = "${value.stateId}">${value.statename}</option >`
                });
                $("#StateId").html(s);
            },
            error: function () {
                console.log("Error updating variable");
            }
        })
    }
}
//Get cities as per state
const getcities = () => {
    var states = $('#StateId').find(":selected").val()
    if (parseInt(states) != 0) {
        $.ajax({
            url: '/Home/city',
            type: 'POST',
            data: { states: states },
            success: function (result) {
                var s = `<option value = "">Select City</option >`
                $.each(result, function (index, value) {
                    s += `<option value = "${value.cityId}">${value.cityName}</option >`
                });
                $("#CityId").html(s);
            },
            error: function () {
                console.log("Error updating variable");
            }
        })
    }
}