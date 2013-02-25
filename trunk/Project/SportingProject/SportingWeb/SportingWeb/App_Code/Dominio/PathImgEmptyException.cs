using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PathImgEmptyException : Exception
{
    private String message;

    public PathImgEmptyException(String message)
    {
        this.Message = message;
    }

    public String Message
    {
        get { return this.message; }
        set { this.message = value; }
    }
}
