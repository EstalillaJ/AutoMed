var table = $("#mappings-table").DataTable({
    "paging": false,
    "searching": false,
    "info": false,
    "ordering": false
});
var deletedMappings = 0;


function addRow() {
    var count = table.data().length;
    table.row.add([
        '<input class="form-control text-box single-line"' +
                       'data-val="true"' +
                       'data-val-number="Must be a number."' +
                       'id="BracketMappings_' + count + '__PovertyLevel" name="BracketMappings[' + count + '].PovertyLevel" type="text" value="">',
        '<input class="form-control text-box single-line discount"' +
                       'data-val="true"' +
                       'data-val-number="Must be a number."' +
                       'id="BracketMappings_' + count + '__Discount" name="BracketMappings[' + count + '].Discount" type="text" value="">'

    ]).draw(false);
    $("#remove-row").removeClass("disabled");
}

function markRowAsDeleted() {
    var count = table.data().length;
    var idInput = $("#BracketMappings_" + (count - 1) + "__Id");
    if (idInput.length == 1) {
        idInput.attr("name", "deletedMappings[" + deletedMappings + "].Id")
               .attr("id", "")
               .insertAfter($("#mappings-table"));
        deletedMappings++;
    }
}

function removeRow() {
    var count = table.data().length;
    if (count > 1) {
        table.row(count - 1).remove().draw(false);
        if (count == 2)
            $("#remove-row").addClass("disabled");
    }
}

function validateLocationForm(event) {
    var hasZero = false;
    $(".discount").each(function (i, object) {
        if (object.value === "0") {
            hasZero = true;
        }
    }
    );

    if (!hasZero) {
        event.preventDefault();
        alert("Please provide a poverty level with zero discount.");
    }
}

$('form').submit(function (event) {
    validateLocationForm(event);
});