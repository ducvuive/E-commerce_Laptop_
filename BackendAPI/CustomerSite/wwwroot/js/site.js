// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var plus = $(".btn-plus");
var minus = $(".btn-minus");
var quality = $("#quantity");
var count = quality.val();
plus.on("click", function (event) {
    if (count + 1 < 100) {
        console.log("count", count);
        quality.val(++count);
    }
});
minus.on("click", function (event) {
    if (count - 1 > 0) {
        console.log("count", count);
        quality.val(--count);
    }
});