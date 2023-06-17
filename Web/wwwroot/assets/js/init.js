(function ($) {
    // ui
 
    $('.datetime').attr('autocomplete', 'off');
    // global form modal
    initFormModal();

    // custom validation
   

    // delete item by id
    initDeleteItemById();

    //Spinner
    var $loading = $("#blockUISpinner").hide();

    $(document)
        .ajaxStart(function () {
            $loading.show();
        })
        .ajaxStop(function () {
            $loading.hide();
        });

})(jQuery);

function initFormModal() {
    debugger;
    $('#popupForm').on('hidden.bs.modal',
        function (e) {
            location.reload();
        });

    $(".popupFormBtn").click(function () {
        debugger;
        var url = $(this).attr("data-url");
        var title = $(this).attr("data-title");
        var reload = $(this).attr("data-reload");
        if (url) {
            $("#popupFormBody").load(url, function (data, status, jqXGR) {
                $("#form").attr("action", url);
                $("#formTitle").text(title);
             
            });



        }
    });

    $("form.popup").submit(function (e) {

        e.preventDefault();

        var $form = $("form.popup");
        $.validator.unobtrusive.parse($form);

        $(this).validate();

        if ($(this).valid()) {
            var data = new FormData(this);
            var url = $(this).attr("action");
            $(':input[type="submit"]').prop('disabled', true);
            $.ajax({
                type: "post",
                url: url,

                contentType: false,
                data: data,
                processData: false,
                success: function (result) {
                    $("#popupFormBody").html(result);
                    $(':input[type="submit"]').prop('disabled', false);
                },
                error: function (request, error) {
                    $(':input[type="submit"]').prop('disabled', false);
                    console.log(arguments);
                    console.log(request.responseText);
                    alert(" Can't do because: " + error);
                }
            });
        }
    });
}

function openFormModal(url, title) {
    if (url) {
        $("#popupFormBody").load(url,
            function () {
                $("#form").attr("action", url);
                $("#formTitle").text(title);
                $('#popupForm').modal('show');
            });
    }
}



function initDeleteItemById() {

    $(".deleteItem").click(function () {
        var id = $(this).data("id");
        var action = $(this).data("url");
        var message = $(this).data("message");
        var confirmMessage = "هل انت متأكد من هذا الإجراء للحذف";
        if (message)
            confirmMessage = message;

        if (confirm(confirmMessage) == true) {
            $.post(action,
                { id: id },
                function (data) {
                    if (data.success) {
                        location.reload();
                    } else {
                        alert(data.message);
                    }
                },
                "json");
        }
    });
}

