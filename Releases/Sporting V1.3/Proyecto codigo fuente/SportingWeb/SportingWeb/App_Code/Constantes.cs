using System;
using System.Configuration;

public class Constantes
{
    public static String CONNECTION_STRING = ConfigurationManager.ConnectionStrings["sportingCn"].ConnectionString.ToString();
}
