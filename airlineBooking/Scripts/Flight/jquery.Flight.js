$(function () {
    $.ajax({
        url: "//partners.api.skyscanner.net/apiservices/pricing/v1.0/webService",
        type: "GET",
        data: "apiKey=7f75b175-ca41-4613-8e84-097d89cbb01b",
        dataType: "xml",
        success: function (result) {
            console.log(result);
        },
        error: function (result) {
            console.log(result);
        }
    });
});