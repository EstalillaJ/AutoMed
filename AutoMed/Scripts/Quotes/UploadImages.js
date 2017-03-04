function handleFileSelect(evt) {
    var files = evt.target.files; // FileList object

    // Loop through the FileList and render image files as thumbnails.
    for (var i = 0, f; f = files[i]; i++) {

        // Only process image files.
        if (!f.type.match("image.*")) {
            continue;
        }

        var reader = new FileReader();

        // Closure to capture the file information.
        reader.onload = (function (theFile) {
            return function (e) {
                // Render thumbnail.
                var div = document.createElement("div");
                div.className = "images col-md-12";
                div.innerHTML = ["<div class=\"imageContainer col-md-offset-4 col-md-8\">" + '<img class="thumb col-md-12" src="', e.target.result,
                    '" title="', escape(theFile.name), '"/>' +
                    "<br/><span class=\"remove col-md-12\">Remove Image</span>" + "</div>"].join("");

                document.getElementById("list").insertBefore(div, null);

                $(".remove").click(function () {

                    $(this).parent(".imageContainer").parent().remove();
                    document.getElementById("files").value = "";

                });
            };
        })(f);

        var oldInput = $("#files");
        var newInput = oldInput.clone();
        newInput.change(function (evt) {
            handleFileSelect(evt);
        });
        oldInput.attr("id", "files-old");
        oldInput.addClass("hidden");
        oldInput.after(newInput);

        // Read in the image file as a data URL.
        reader.readAsDataURL(f);
    }
}

document.getElementById("files").addEventListener("change", handleFileSelect, false);