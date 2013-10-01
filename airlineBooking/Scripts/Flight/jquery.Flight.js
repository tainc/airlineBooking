$(function () {
    $("#fromCity").autocomplete({
        source: function( request, response ) {
            $.ajax({
                url: '/autosuggest?query=' + $("#fromCity").val(),
                dataType: "json",
                success: function (data) { 
                    response($.map(data['Places'], function (item) {
                        return {
                            label: item.PlaceName + " ( " + item.PlaceId.split("-sky",1) + " )",
                            value: item.PlaceId
                        }
                    }));
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            $("#fromCity").val(ui.item.label);
            $("#fromCityCode").val(ui.item.value);
            return false;
            //console.log( ui.item ?
            //  "Selected: " + ui.item.label :
            //  "Nothing selected, input was " + this.value);
        },
        open: function() {
            $( this ).removeClass( "ui-corner-all" ).addClass( "ui-corner-top" );
        },
        close: function () {
            
            $( this ).removeClass( "ui-corner-top" ).addClass( "ui-corner-all" );
        }
    });


});