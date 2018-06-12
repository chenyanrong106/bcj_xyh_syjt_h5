//服务项目维护js
; (function (window, undefined, $) {


    var validgrid = null;
    var unvalidgrid = null
    InitValidGrid();

    $('#tab_store').on('shown.bs.tab', function (e) {
       
        //if (unvalidgrid == null) { }
            InitUnValidGrid();      
    });
    $('#tab_info').on('shown.bs.tab', function (e) {    
        InitValidGrid();
    });



    //保存未发布
    $("#btnVALIDSave").click(function () {
        $("#un").val("0");
        var valid = $("#un").val();
        var url = options.ValidSaveUrl + "?valid=" + valid;

        var data = validgrid.GetCheckedRowDatas(formatPostStoreData);

        var postData = data.join(",");
        PostSave(postData, url,valid);
    });

    function formatPostStoreData(cell) {
        return [cell[0]];
    }

    function InitValidGrid() {
        $("#un").val("0");
        var valid = $("#un").val();
        var gridopt = {
            url: options.ValidQueryUrl + "?valid=" + valid,
            colModel: [
                    { display: '门店编号', name: 'STORE_ID', hide: true, iskey: true },
                    { display: '时间', name: 'YM', width: "10%", align: 'left' },
                    { display: '门店名称', name: 'STORENAME', align: 'left' },
                    { display: '是否选择', name: 'IsChecked', hide: true }
            ],
            title: false,
            rp: 10,
            localpage: true,
            //rowhanlder: rowStoreBind,
            usepager: true,
            showcheckbox: true
        };
        validgrid = new xjGrid("VALID", gridopt);
    }


    //保存已发布
    $("#btnUNVALIDSave").click(function () {
      
        $("#un").val("1");
        var valid = $("#un").val();
        var url = options.ValidSaveUrl + "?valid=" + valid;
        var data = unvalidgrid.GetCheckedRowDatas(formatPostStoreData);
        var postData = data.join(",");
        PostSave(postData, url,valid);
    });


    function InitUnValidGrid() {
        $("#un").val("1");
        var valid = $("#un").val();
        var gridopt = {
            url: options.ValidQueryUrl + "?valid=" + valid,
            colModel: [
                    { display: '编号', name: 'STORE_ID', hide: true, iskey: true },
                    { display: '时间', name: 'YM', align: 'left' },
                    { display: '门店名称', name: 'STORENAME', align: 'left' },
                    { display: '是否选择', name: 'IsChecked', hide: true }
            ],
            title: false,
            rp: 10,
            localpage: true,
            //rowhanlder: rowStoreBind,
            usepager: true,
            showcheckbox: true
        };
        unvalidgrid = new xjGrid("UNVALID", gridopt);
    }


    var _submiting = false;
    function PostSave(postData, url,valid) {
     
        if (_submiting) {
            showErrorTip("请耐心等待服务端操作结束", { left: 400, top: 450 }, true, 5000);
            return;
        }
        _submiting = true;
        showLoadingMsg("正在努力地请求服务端,请稍后...", { left: 400, top: 450 });
        $.ajax({
            url: url,
            type: "POST",
            data: { "formdata": postData },
            success: function (result) {
                hideLoadingMsg();
             
                if (result.status == 0) {
                 
                    _showInfoMessage('保存成功！', 'success');
                    if (valid == "0") {
                        validgrid.Reload();
                    }
                    else {
                        unvalidgrid.Reload();
                    }

                }
                else {
                    showErrorTip("操作失败！：" + res.message, { left: 400, top: 450 }, true, 5000);
                }
                _submiting = false;
            },
            error: function (result) {
                
                hideLoadingMsg();
                _submiting = false;
                alert("提交表单失败：" + result.message);
            }
        });
    }

    $("#frmVALID").submit(function () {
        xjgrid.Query(this);
        return false;
    });



})(window, undefined, jQuery);