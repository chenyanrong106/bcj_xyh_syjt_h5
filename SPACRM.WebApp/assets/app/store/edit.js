; (function (window, undefined, $) {
    $(document).ready(function () {

        $('#CLOSE_DATE_Str').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd"

        })

        $('#OPEN_DATE_Str').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd"
        })

        //初始化    //服务类别  保留
        InintSerItem();
        function InintSerItem() {
            $.post(options.querySerItemUrl, { code: "STORE_SERVICETYPE", parentCode: 0 },
                     function (res) {
                         //alert(res.status);
                         if (res.status >= 0) {
                             var items = eval("(" + res.data + ")");
                             $("#SERVICE_TYPE").select2({ width: "resolve", tags: items });
                         }
                         else {
                             $("#SERVICE_TYPE").select2({ width: "resolve", tags: "" });
                         }
                     },
                     "json"
                    );
        }




      
        $("#btnCancel").click(function (e) {
          
            window.location.href = "index.do";
       
        });
    

        $("#btnSave").click(function (e) {
          
            var check = "";

            $('[type="checkbox"]').each(function () {
                if (true == $(this).prop("checked")) {
                    check += $(this).val() + ",";               
                }
            })
            $("#SERVICE_TYPE").val(check);
           
            $("#frmSave").submit();
        });
       
        function GetFeature() {
            var count = $("#count").val();
            var feature = "";
            for (var i = 0; i < count; i++) {
                var cname = "#CheckBox" + i;
                var sname = "#SERVICE_TYPE" + i;

                var c = $(cname).prop("checked");
                var s = $(sname).text();
                if (c == true) {
                    feature += s;
                }
            }
            $("#SERVICE_TYPE").val(feature);
          
        }


        $('#frmSave').validator({
            rules: {

            },
            fields: {
                '#NAME': 'required',
                '#STORE_NO': 'required',
                '#TYPE': 'required',
                '#OPEN_DATE_Str': 'required',
                '#REGION_ID': 'required',

            },
            valid: function (form) {

                FormSubmit(form, function (res) {
                    if (res.status == 0) {
                        _showInfoMessage("操作成功！", 'success');
                        window.location.href = "index.do";
                    }
                    else {
                        _showInfoMessage("操作失败：" + res.message, 'error');
                    }
                })
            }
        });


    });
})(window, undefined, jQuery);

$("#biaozhu").click(function () {
    if ($("#CITY").val() == "") {
        alert("请输入所属城市");
        return;
    } else {
        window.open("GetLocation.do?nicename=小时光&city=" + $("#CITY").val() + "&Lng=" + $("#Lng").val() + "&Lat=" + $("#Lat").val() + "&ADDRESS=" + $("#ADDRESS").val());
       // $("#biaozhu").attr("href", "GetLocation.do?nicename=小时光&city=" + $("#CITY").val() + "&Lng=" + $("#Lng").val() + "&Lat=" + $("#Lat").val() + "&ADDRESS=" + $("#ADDRESS").val());
    }
});