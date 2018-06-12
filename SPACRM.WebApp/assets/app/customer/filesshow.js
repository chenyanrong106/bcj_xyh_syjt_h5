; (function (window, undefined, $) {
    $(document).ready(function () {
        var gridopt = {
            url: options.linkUrl,
            colModel: [
                    { display: '编号', name: 'ID', sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '文件名称', name: 'CONTENT_TYPE', sortable: false, align: 'left' },
                    { display: '后缀名', name: 'Zui', sortable: false, align: 'left' },
                    { display: '文件类型', name: 'FILES_NAME', sortable: false, align: 'left' },
                    { display: '文件大小', name: 'FILE_SIZE', sortable: false, align: 'left' },
                    { display: '查看', name: 'FILES_ID', sortable: false, align: 'left', process: processOp }
            ],
            sortname: "ID",
            sortorder: "desc",
            title: false,
            rp: 20,
            usepager: true,
            showcheckbox: false
        };
        var xjgrid = new xjGrid("gridlist", gridopt);
        function processOp(value, cell) {
            
            var ops = [];
            ops.push("&nbsp;<a title='查看' class='abtn' href='javascript:;'  onclick=\"javascript:window.location='/Customer/ViewImage.do/", value, "'\"><i class='fa fa-search' ></i> 查看</a>");
            return ops.join("");
        }


    });

})(window, undefined, jQuery);
