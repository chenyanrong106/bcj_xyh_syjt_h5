SELECT * FROM dbo.ORG_WX_QRCode c RIGHT JOIN (
SELECT COUNT(1) cnum,qid FROM (
SELECT MIN(QRCodeID) qid,FromUserName FROM dbo.ORG_WX_QRLog GROUP BY FromUserName)t GROUP BY qid)a ON c.QID=a.qid


--SELECT * FROM dbo.ORG_WX_QRCode

--UPDATE dbo.ORG_WX_QRCode SET qid=3 WHERE id=4


--SELECT * FROM dbo.OAauth_Log WHERE SurplusMoney IS NOT NULL
