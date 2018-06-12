<?xml version="1.0" encoding="utf-8"?>
<!-- DWXMLSource="A.xml" -->
<!DOCTYPE xsl:stylesheet  [
  <!ENTITY nbsp   "&#160;">
  <!ENTITY copy   "&#169;">
  <!ENTITY reg    "&#174;">
  <!ENTITY trade  "&#8482;">
  <!ENTITY mdash  "&#8212;">
  <!ENTITY ldquo  "&#8220;">
  <!ENTITY rdquo  "&#8221;">
  <!ENTITY pound  "&#163;">
  <!ENTITY yen    "&#165;">
  <!ENTITY euro   "&#8364;">
]>


<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8"/>
  <xsl:template match="/">
    <style type="text/css">
      td {font-family: "Arial"; font-size: 12px;}
      .TdLTBR {font-family: "Arial"; font-size: 12px;border-left: #000000 1px solid;border-top: #000000 1px solid;border-bottom: #000000 1px solid;border-right: #000000 1px solid;}
      .TdTBR {font-family: "Arial"; font-size: 12px;border-left: #000000 0px solid;border-top: #000000 1px solid;border-bottom: #000000 1px solid;border-right: #000000 1px solid;}
      .TdLBR {font-family: "Arial"; font-size: 12px;border-left: #000000 1px solid;border-top: #000000 0px solid;border-bottom: #000000 1px solid;border-right: #000000 1px solid;}
      .TdBR {font-family: "Arial"; font-size: 12px;border-left: #000000 0px solid;border-top: #000000 0px solid;border-bottom: #000000 1px solid;border-right: #000000 1px solid;}

    </style>

    <table border="0" cellspacing="0" width="1800px" cellpadding="2" align="center">

      <tr>
        <td colspan="36" align="center" style="background-color:#4f8edc;color:white;font-size:x-large;">
         财务对账报表<br/> <xsl:value-of select="NewDataSet/Table1/beg"/>——<xsl:value-of select="NewDataSet/Table1/end"/><xsl:value-of select="NewDataSet/Table1/md"/>  
        </td>
        
      </tr>
      <tr>
        <td align="center"  rowspan="2" class="TdLTBR"  >序号</td>
        <td align="center"  rowspan="2" class="TdTBR" >日期　</td>
        <td align="center"  colspan="13" class="TdTBR" >现金流　</td>
        <td align="center"  colspan="2" class="TdTBR" >会员卡消费　</td>
        <td align="center"  colspan="3" class="TdTBR" >会员卡充值　</td>
        <td align="center"  colspan="2" class="TdTBR" >疗程卡消费　</td>
        <td align="center"  rowspan="2" class="TdTBR" >现金券/按摩券　</td>
        <td align="center"  rowspan="2" class="TdTBR">消费券</td>
        <td align="center"  rowspan="2" class="TdTBR" >服务实际营业额　</td>
        <td align="center"  rowspan="2" class="TdTBR" >现金流　</td>
        <td align="center"  colspan="5" class="TdTBR" >客人数量　</td>
        <td align="center"  rowspan="2" class="TdTBR" >客单价　</td>
        <td align="center"  colspan="2" class="TdTBR" >按摩券/现金券　</td>
        <td align="center"  colspan="2" class="TdTBR" >实物产品　</td>
      </tr>
      <tr>
        <td align="center" class="TdBR"  >现金</td>
        <td align="center" class="TdBR"  >银联卡</td>
        <td align="center" class="TdBR"  >外卡</td>
        <td align="center" class="TdBR"  >微信支付</td>
        <td align="center" class="TdBR"  >支付宝</td>
        <td align="center" class="TdBR"  >大众点评团购</td>
        <td align="center" class="TdBR"  >大众点评闪惠</td>
        <td align="center" class="TdBR"  >美团</td>
        <td align="center" class="TdBR"  >糯米</td>
        <td align="center" class="TdBR"  >Emily</td>
        <td align="center" class="TdBR"  >我有礼</td>
        <td align="center" class="TdBR"  >走起</td>
        <td align="center" class="TdBR"  >总计</td>
        <td align="center" class="TdBR"  >会员卡扣款</td>
        <td align="center" class="TdBR"  >会员卡实际扣款</td>
        <td align="center" class="TdBR"  >新客充值</td>
        <td align="center" class="TdBR"  >老客续充</td>
        <td align="center" class="TdBR"  >赠送奖励</td>
        <td align="center" class="TdBR"  >疗程卡购买</td>
        <td align="center" class="TdBR"  >疗程卡消耗</td>
        <td align="center" class="TdBR"  >会员</td>
        <td align="center" class="TdBR"  >老客</td>
        <td align="center" class="TdBR"  >新客</td>
        <td align="center" class="TdBR"  >散客</td>
        <td align="center" class="TdBR"  >总计</td>
        <td align="center" class="TdBR"  >数量</td>
        <td align="center" class="TdBR"  >金额</td>
        <td align="center" class="TdBR"  >数量</td>
        <td align="center" class="TdBR"  >金额</td>
      </tr>
      <xsl:for-each select="NewDataSet/Table1">
        <!--按单位ID循环-->
        <xsl:variable name="ID" select="ID"/>


          <tr>

            <td align="center" class="TdLBR"  >
              　<xsl:number value="position()" />
            </td>

            <td align="center" class="TdBR"  >
              <xsl:value-of select="date"/>　
            </td>
            <td align="right" class="TdBR">
              <xsl:value-of select="format-number(xj,'#0.00')"/>　
            </td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(ylk,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(wk,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(wxzf,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(zfb,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(dzdp,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(dzdpsh,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(mt,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(nm,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(emily,'#0.00')"/>  </td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(woyouli,'#0.00')"/>  </td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(zouqi,'#0.00')"/>  </td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(xjsum,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(hykkk,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(hyksjkk,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(xkcz,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(lkxc,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(zsjl,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(lckgm,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(lckxh,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(xjq,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(xfq,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(fwsjyye,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(xjl,'#0.00')"/>　</td>
            <td align="right" class="TdBR"  ><xsl:value-of select="hy"/></td>
            <td align="right" class="TdBR"  ><xsl:value-of select="lk"/></td>
            <td align="right" class="TdBR"  ><xsl:value-of select="xk"/></td>
            <td align="right" class="TdBR"  ><xsl:value-of select="sk"/></td>
            <td align="right" class="TdBR"  ><xsl:value-of select="zj"/></td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(kdj,'#0.00')"/></td>
            <td align="right" class="TdBR"  ><xsl:value-of select="amqsl"/></td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(amqje,'#0.00')"/></td>
            <td align="right" class="TdBR"  ><xsl:value-of select="swcpsl"/></td>
            <td align="right" class="TdBR"  ><xsl:value-of select="format-number(swcpje,'#0.00')"/></td>
          </tr>

      </xsl:for-each>
      <!--<tr  style="background-color:Gray;">
        <td align="center" class="TdLBR" >
          　&nbsp;
        </td>
        <td align="center"   class="TdBR" >总计　</td>
        <td align="right" class="TdBR" >
            <xsl:value-of select="format-number(sum(NewDataSet/Table1/xj),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/ylk),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/wk),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/wxzf),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/zfb),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/dzdp),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/mt),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/xjsum),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/hykkk),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/hyksjkk),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/xkcz),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/lkxc),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/zsjl),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/lckgm),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/lckxh),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/xjq),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/fwsjyye),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/xjl),'#0.00')"/>　
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/hy),'#0.00')"/>
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/lk),'#0.00')"/>
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/xk),'#0.00')"/>
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/sk),'#0.00')"/>
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/zj),'#0.00')"/>
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/kdj),'#0.00')"/>
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/amqsl),'#0.00')"/>
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/amqje),'#0.00')"/>
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/swcpsl),'#0.00')"/>
        </td>
        <td align="right" class="TdBR"  >
          <xsl:value-of select="format-number(sum(NewDataSet/Table1/swcpje),'#0.00')"/>
        </td>
      </tr>-->
      <tr>
        <td colspan="3">&nbsp;</td>
      </tr>
      
    </table>
  </xsl:template>
</xsl:stylesheet>

