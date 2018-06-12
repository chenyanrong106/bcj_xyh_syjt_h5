<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="SPACRM.WebApp.wechat.ceshi.Map" %>
<!DOCTYPE html>
<html>
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
	<style type="text/css">
		body, html,#allmap {width: 100%;height: 100%;overflow: hidden;margin:0;font-family:"微软雅黑";}
	</style>
	<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=0bFDc7N7CprDGUMBEG2tTV1btAh8HgId"></script>
	<title>地图测试</title>
</head>
<body>
	<div id="allmap"></div>
</body>
</html>
<script type="text/javascript">
    // 百度地图API功能
    var map = new BMap.Map("allmap");
    var point = new BMap.Point(121.438879, 31.201975);
    map.centerAndZoom(point, 20);
    var marker = new BMap.Marker(point);  // 创建标注
    marker.addEventListener("click", getAttr);
    function getAttr() {
        var p = marker.getPosition();       //获取marker的位置
        window.location = "map2.aspx?wz=扑满总部";
    }
    map.addOverlay(marker);              // 将标注添加到地图中


    var point2 = new BMap.Point(121.480988, 31.20494);
    map.centerAndZoom(point2, 20);
    var marker2 = new BMap.Marker(point2);  // 创建标注
    marker2.addEventListener("click", getAttr2);
    function getAttr2() {
        var p = marker2.getPosition();       //获取marker的位置
        window.location = "map2.aspx?wz=鲁班路";
    }
    map.addOverlay(marker2);

    var label = new BMap.Label("这里是扑满总部", { offset: new BMap.Size(20, -10) });
    marker.setLabel(label);

    map.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放
    var label2 = new BMap.Label("这里是鲁班路", { offset: new BMap.Size(20, -10) });
    marker2.setLabel(label2);
</script>
