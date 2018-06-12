$(document).ready(
  function () {


      $("#btnSave").click(function (e) {
         
          //$('[type="radio"]').each(function ()
          //{
          //    alert($('[type="checkbox"]').val());
          //})
          //$("[type='checkbox']").click(function(){ 
          //    var ids = ''; 
          //    var flag = 0; 
          //    $("#ids").attr("value",ids); 
          //    $("[type='checkbox']").each(function(){ 
          //        if (true == $(this).attr("checked")) { 
          //            ids += $(this).attr('value')+','; 
          //            flag += 1; 
          //        } 
          //    }); 
          var check = "";
          var radio = "";
          $('[type="checkbox"]').each(function () {
              if (true == $(this).prop("checked"))
              {
                  check += $(this).val() + ",";
                  //alert($(this).next().text());
                
              }
              
          })
          $('[type="radio"]').each(function () {
              if (true == $(this).prop("checked")) {
                  //alert($(this).next().text());
                  radio += $(this).val() + ",";
                  //alert(radio);
              }
          })
          $("#check").val(check);
          $("#radio").val(radio);

         
          $.post("SaveExpandInfo.do", { check: check,radio:radio,CUST_ID:$("#CUST_ID").val() },
        function (res) {
            
            //alert(res);
            if (res.status == 0) {
              
                _showInfoMessage("保存成功！", 'success');            
            }
            else {
                showErrorTip("保存失败！：" + res.message, { left: 100, top: 10 }, true, 5000);
            }
        },
        "json"
  );

          //$("#frmExpand").submit();
        
      });
    

      



  });


function search() {
    
    $.post("CustExinfoList.do", { null: null },
          function (result) {
              var cuAllPs = result.data;

              var cuAllPsArray = new Array();
              if (cuAllPs != "") {
                  cuAllPsArray = cuAllPs.split(",");

              }

              var arr = new Array();
              var h = "";
              if (cuAllPs != "") {

                  for (var i = 0; i < cuAllPsArray.length; i++) {
                      var re = cuAllPsArray[i].split("_");
                      //alert(re[0]);
                      if (re[0] == "1")
                      {
                          h += "<h5>" + re[2] + "</h5>";
                      }
                      //h+="<div>";
                      else if (re[0] == "2")
                      {
                          h += "<div class='form-horizontal'>&nbsp;&nbsp; " + re[2] + ":&nbsp;  ";
                      }
                      else if (re[0] == "3") {
                          h += " <input type='checkbox' id=''/> <span id='" + re[1] + "'>" + re[2] + " </span><span> &nbsp; </span> </div> ";
                      }                 
                      //h += "<div class='form-horizontal'>&nbsp;&nbsp; " + re[4] + ":&nbsp;   <input type='checkbox' id=''/> <span id='" + re[5] + "'>" + re[6] + " </span><span> &nbsp; </span> </div> ";

                  }
                
              }           
              $("#content").html(h);
          },
          "json"
    );
}
//function GetCustExpandInfomation() {
//    var cust_id = $("#cust_id").val();
//    alert(cust_id);
//    //$.ajax({
//    //    url: options.getcustexpandInfo,
//    //    type: "POST",
//    //    data: { "cust_id": cust_id },
//    //    success: function (result) {
//    //        if (result.Status == 1) {
//    //            var ExpandItemList = result.Data;
//    //            alert(ExpandItemList.length);
//    //            //for (var i = 0; i < ExpandItemList.length;i++)
//    //            //{

//    //            //}
//    //        }
//    //    }
//    //});
//}