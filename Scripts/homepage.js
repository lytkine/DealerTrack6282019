var PC = {

    UploadDialog: function () {
        $("#fileupload").click();
    },

    UploadFile: function (ev) {
        var files = ev.target.files;

        if (files.length > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("fileupload" + x, files[x]);
                }

                $.ajax({
                    type: "POST",
                    url: '/home/uploadfile',
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (result) {
                        document.getElementById("fileup").reset();
                        PC.DisplaySalesReport(result);
                    },
                    error: function (xhr, status, p3, p4) {
                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                        if (xhr.responseText && xhr.responseText[0] === "{")
                            err = JSON.parse(xhr.responseText).Message;
                        console.error(err);
                    }
                });
            } else {
                alert("This browser doesn't support HTML5 file uploads!");
            }
        }
    },

    DisplaySalesReport: function (report) {
        console.log("Dispay");
        console.log(report);


        var headers = ["Deal Number", "Customer Name", "Dealership", "Vehicle", "Price", "Date"];
        var c = '<tr><th>' + headers.join('</th><th>') + '</th></tr>';

        $.each(report.Sales, function (i, v) {
            c += '<tr>';
            c += '<td class="cnt">' + v.DealNumber + '</td>';
            c += '<td>' + v.CustomerName + '</td>';
            c += '<td>' + v.DealershipName + '</td>';
            c += '<td>' + v.Vehicle + '</td>';
            c += '<td>' + PC.FormatMoney(v.Price) + '</td>';
            c += '<td class="cnt">' + v.Date + '</td>';
            c += '</tr>';
        });

        $("#lblReportName").empty().append(report.ReportName);
        $("#detailRecNumber").empty().append(report.Sales.length);

        var bestttl = "N/A";

        if (report.Best !== null) {
            bestttl = report.Best.Vehicle + ' (' + report.Best.NumberSold + ' sold at average price of ' + PC.FormatMoney(report.Best.AveragePrice) + ')';
        }

        $("#detailBestVehicle").empty().append(bestttl);

        $("#tbldata").empty().append(c);
        $(".paneltwo").removeClass("noshow");
    },

    FormatMoney: function(x) {
        return 'CAD$ ' + x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }




};


$(function () {

    
    $("#btnUpload").click(PC.UploadDialog);
    $("#fileupload").on('change', PC.UploadFile);
});
