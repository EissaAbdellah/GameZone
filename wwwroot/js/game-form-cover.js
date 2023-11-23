
    $(document).ready(function () {
        $("#Cover").change(function (event) {
            previewImage(event);
        });

    function previewImage(event) {
                var input = event.target;
    var preview = $('#Image');

    var reader = new FileReader();
    reader.onload = function () {
        preview.attr('src', reader.result);
    preview.show(); // Display the image preview
                };

    if (input.files && input.files[0]) {
        reader.readAsDataURL(input.files[0]);
                }
            }
});


