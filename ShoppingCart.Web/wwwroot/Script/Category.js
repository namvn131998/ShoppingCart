var CateJS = {
    urls: {
        Delete: SiteConfig.gSiteAdrs + "/Category/Delete",
        List: SiteConfig.gSiteAdrs + "/Category/_List",
    },
    Delete: function (id) {
        $.ajax({
            url: CateJS.urls.Delete,
            type: "POST",
            data: { Id: id },
            success: function (data) {
                if (data.result == "OK")
                    alert(languageScript.Delete);
                CateJS.GetList();
            }
        })
    },
    GetList: function () {
        $.ajax({
            type: "GET",
            url: CateJS.urls.List,
            data: {
                SearchValue: $('#searchData').val(),
                sortBy: "Name",
                sortDirection: "DESC"
            },
            success: function (data) {
                $("#page-data").html(data);
            }
        })
    }
}
