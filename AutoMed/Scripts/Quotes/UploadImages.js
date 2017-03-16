var fileCount = 0;

function handleFileSelect(evt) {
    var files = evt.target.files; // FileList object

    // Loop through the FileList and render image files as thumbnails.
    for (var i = 0, f; f = files[i]; i++) {

        // Only process image files.
        if (!f.type.match("image.*")) {
            continue;
        }
        fileCount++;
        var reader = new FileReader();

        // Closure to capture the file information.
        reader.onload = (function (theFile) {
            return function (e) {
                // Render thumbnail.
                var div = document.createElement("div");
                div.className = "images col-md-6";
                div.innerHTML = ['<img class="thumb col-md-12" src="', e.target.result,
                    '" title="', escape(theFile.name), '"/>' +
                    "<br/><span id=\"remove-"+ (fileCount - 1) + "\" class=\"remove col-md-12\">Remove <span class=\"glyphicon glyphicon-remove\"></span></span>"].join("");

                document.getElementById("list").insertBefore(div, null);
                var current = fileCount - 1;
                var myFileInput = $("#files-" + current);
                $("#remove-"+ current).click(function () {

                    $(this).parent().remove();
                    myFileInput.remove();
                });
            };
        })(f);

        var oldInput = $("#files-" + (fileCount - 1));
        var newInput = document.createElement("label");
        newInput.innerHTML = ["Upload Picture(s) <input type=\"file\" id=\"files-"+ (fileCount) + "\" name=\"files\" multiple />"];
        newInput.className = "btn btn-default btn-file text-right";
        newInput.onchange = function (evt) {
            handleFileSelect(evt);
        };
        oldInput.parent().addClass("hidden");
        oldInput.addClass("hidden");
        document.getElementById("input-div").insertBefore(newInput, null);

        // Read in the image file as a data URL.
        reader.readAsDataURL(f);
    }
}

document.getElementById("files-0").addEventListener("change", handleFileSelect, false);