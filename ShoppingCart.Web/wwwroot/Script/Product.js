var ProductJS = {
    urls: {
        Delete: SiteConfig.gSiteAdrs + "Admin/Product/Delete",
        List: SiteConfig.gSiteAdrs + "Admin/Product/_List",
        ShowUploadFile: SiteConfig.gSiteAdrs + "Admin/UploadFile/ShowUploadFile",
        CreateOrUpdate: SiteConfig.gSiteAdrs + "Admin/Product/CreateOrUpdate",
        SubmitAddFileProduct: SiteConfig.gSiteAdrs + "Admin/UploadFile/AddListThumbnail",
        ShowListMedia: SiteConfig.gSiteAdrs + "Admin/UploadFile/ShowListMedia",
        DeleteMedia: SiteConfig.gSiteAdrs + "Admin/UploadFile/DeleteMedia",
    },
    Delete: function (id) {
        $.ajax({
            url: ProductJS.urls.Delete,
            type: "POST",
            data: { Id: id },
            success: function (data) {
                if (data.result == "OK")
                    alert(languageScript.Delete);
                ProductJS.GetList();
            }
        })
    },
    GetList: function () {
        $.ajax({
            type: "GET",
            url: ProductJS.urls.List,
            data: {
                SearchValue: $('#searchData').val(),
                sortBy: "Name",
                sortDirection: "DESC"
            },
            success: function (data) {
                $("#page-data").html(data);
            }
        })
    },
    ShowUploadFile: function () {
        $.ajax({
            type: "GET",
            url: ProductJS.urls.ShowUploadFile,
            data: {
                
            },
            processData: false,
            success: function (data) {
                $("#modalUploadProductImage").html(data);
                $("#modalUploadProductImage").dialog({
                    modal: true,
                    width: 500,
                    lgClass: true
                });
                $("#modalUploadProductImage").dialog("open");
            }
        })
    },
    CreateOrUpdate: function () {
        var data = $("form").serialize();
        var data = new FormData($('#CreateOrUpdate')[0]);
        $.ajax({
            type: "POST",
            url: ProductJS.urls.CreateOrUpdate,
            data: data,
            contentType: false,
            processData: false,
            success: function () {
                alert(1);
            }
        })
    },
    SubmitAddFileProduct: function () {
        var data = new FormData($('#addMediaFileProduct')[0]);
        var productID = $("#ProductID").val();
        data.append("productID", productID);
        if ($("#addMediaFileProduct").valid()) {
            $.ajax({
                url: ProductJS.urls.SubmitAddFileProduct,
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $("#modalUploadProductImage").dialog("close");
                    ProductJS.ShowListMedia(data);
                    var listMeida = $('#MediaIDs').val().split();
                    listMeida.push(data);                  
                    $('#MediaIDs').val(listMeida.toString());
                }
            });
        }

    },
    ShowListMedia: function (mediaid) {
        $.ajax({
            url: ProductJS.urls.ShowListMedia,
            type: "GET",
            data: {
                mediaid: mediaid
            },
            success: function (data) {
                $('#dvListMedia').append(data);
            }
        })
    },
    DeleteMedia: function (meidaid, fileName, productID) {
        var userID = $("#UserID").val();
        $.ajax({
            url: ProductJS.urls.DeleteMedia,
            type: "POST",
            data: {
                mediaid: meidaid,
                UserID: userID,
                FileName: fileName,
                productID: productID
            },
            success: function (data) {
                alert(data.result);
                window.location.reload();
            }
        })
    }
}
