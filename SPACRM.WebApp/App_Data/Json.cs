using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// JsonSMsg 的摘要说明
/// </summary>
public class Json
{
    private int _status;

    public int Status
    {
        get { return _status; }
        set { _status = value; }
    }

    private string _message;

    public string Message
    {
        get { return _message; }
        set { _message = value; }
    }

    private string _data;

    public string Data
    {
        get { return _data; }
        set { _data = value; }
    }
    
}