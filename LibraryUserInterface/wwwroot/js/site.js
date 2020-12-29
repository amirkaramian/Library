$(document).ready(function()
{
    $("#categoryItem").on("change", function()
    {
        $.ajax(
            {
                url: '/Home/Book?categoryId=' + $(this).children("option:selected").val(),
                type: 'GET',
                data: "",
                contentType: 'application/json; charset=utf-8',
                success: function(data)
                {
                    console.log(data);
                    $("#partialDiv").html(data);
                },
                error: function()
                {
                    alert("error");
                }
            });
    });
});