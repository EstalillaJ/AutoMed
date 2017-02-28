
$("#reports-link").addClass("active");
        
var numLocations = @Model.Locations.Count;        

function toggleCheckboxes(idOfMaster, classOfSlaves, cb){
    $(idOfMaster).change(
        function () {
            var checked = $(this).is(":checked");
            $(classOfSlaves).prop('checked', checked);
        });

    $(classOfSlaves).change(
            function(){
                var checked = $(this).is(":checked");
                if (!checked){
                    $(idOfMaster).prop('checked', false);
                }
                else{
                    var allchecked = $(classOfSlaves).not(":checked").length === 0;
                    $(idOfMaster).prop('checked', allchecked);
                }
                cb();
            });
}

$("#form-create").submit(function (event) {
    if (!$(".location_checkbox").is(":checked")) {
        $("#location-checkbox-error").removeClass('hidden');
        event.preventDefault();
    }

    if (!$(".column_checkbox").is(":checked")) {
        $('#data-field-error').removeClass('hidden');
        event.preventDefault();
    }
});

toggleCheckboxes("#AllLocations", ".location_checkbox", function() {
    $("#location-checkbox-error").addClass("hidden");
});
toggleCheckboxes("#AllColumns", ".column_checkbox", function() {
    $("#data-field-error").addClass("hidden");   
});

  
$("#AnyDiscountDollars").change(
    function () {
        var checked = $(this).is(":checked");
        $("#MaxDiscountDollars").prop('disabled', checked);
        $("#MinDiscountDollars").prop('disabled', checked);
    });
        
$("#AnyDiscountPercentage").change(
    function () {
        var checked = $(this).is(":checked");
        $("#MaxDiscountPercentage").prop('disabled', checked);
        $("#MinDiscountPercentage").prop('disabled', checked);
    });
$("#AnyDate").change(
    function () {
        var checked = $(this).is(":checked");
        $("#StartDay").prop('disabled', checked);
        $("#EndDay").prop('disabled', checked);
        $("#StartMonth").prop('disabled', checked);
        $("#EndMonth").prop('disabled', checked);
        $("#StartYear").prop('disabled', checked);
        $("#EndYear").prop('disabled', checked);
    });
        
$("#EndMonth").change(function(){
    changeDaysAvailable(this.value, $("#EndYear").value, $("#EndDay"))
})
$("#StartMonth").change(function(){
    changeDaysAvailable(this.value, $("#StartYear").value, $("#StartDay"))
})