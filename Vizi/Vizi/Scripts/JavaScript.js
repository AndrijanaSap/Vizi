
    $(document).ready(function () {
        $("#SearchBtn").click(function () {
            var SearchValue = $("#Search").val();
            var SetData = $("#DataSearching");
            SetData.html("");
            $.ajax({
                type: "post",
                url: "/Home/GetSearchingData?SearchValue=" + SearchValue,
                contentType: "html",
                success: function (result) {
                    if (result.length == 0) {
                        SetData.append('<tr style="color:red"><td colspan="3">No Match Data</td></tr>')
                    }
                    else {
                        $.each(result, function (index, value) {
                            var Data = "<tr class='col-lg-2 col-md-4 col-sm-6 col-xs-12' >" +
                                "<span>" + value.Name + "</span>" +
                             "</tr>";
                            SetData.append(Data);

                        });
                    }
                }
            });
        });
    });


