Opinion = function () {

}

Opinion.prototype.getViewModel = function () {

    var viewModel = {

        openDialog: function () {
            $("#dialogOpinion").dialog({ autoOpen: true,
                modal: true,
                resizable: false,
                closeOnEscape: true,
                width: 395,
                height: 445,
                position: ['center', 'center']
            });

        }
    }

    $('#opinionForm').submit(function () {
        if ($(this).valid()) {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    //$('#interestcontainer').html(result);
                    if (result.Message) {
                        if ($("div.validation-summary-errors").length == 0) {
                            var html = "<div class='validation-summary-errors'><ul><li>" + result.Message + "</li></ul></div>";
                            $('#opinionForm').append(html).show();
                        }
                        Recaptcha.reload();
                    }
                    else {
                        $("#dialog-message").dialog({
                            modal: true,
                            buttons: {
                                Ok: function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                        $("#dialogOpinion").dialog('close');
                    }
                },
                error: function (result) {
                    //console.log('Error ' + result);
                }
            });
        }
        return false;
    });

    return viewModel;
}


opinion = new Opinion();