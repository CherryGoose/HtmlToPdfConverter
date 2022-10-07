$(function ()
{
    function saveFile(name, type, data) {
        if (data !== null && navigator.msSaveBlob)
            return navigator.msSaveBlob(new Blob([data], { type: type }), name);
        var a = $("<a style='display: none;'/>");
        var url = window.URL.createObjectURL(new Blob([data], { type: type }));
        a.attr("href", url);
        a.attr("download", name);
        $("body").append(a);
        a[0].click();
        window.URL.revokeObjectURL(url);
        a.remove();
    }
    var downloadForm = $("form#DownloadFile");

    downloadForm.submit(function (event) {
        event.preventDefault();

        var fileName = $('#GeneratedFileId').val().toString();
       
        var downloadWindow = window.open(abp.appPath + 'api/conventer/download?fileName=' + fileName.toString(),
            "_blank"
        );
        downloadWindow.focus();

    });



    $('#ConvertButton').click(function (event) {
        event.preventDefault();


        var fileName = $('#GeneratedFileId').val().toString();

        abp.ajax({
            dataType: 'text',
            type: 'POST',
            contentType: 'application/json',
            url: abp.appPath + 'api/conventer/convert?taskId=' + fileName,
            abpHandleError: false,
        }).then(function (result) {

            console.log(result);

        }).catch(function (error) {

            console.log(result);

        });


    });

});