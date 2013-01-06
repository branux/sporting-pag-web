using System;
using System.Data;
using System.Collections.Generic;

public class GestorPlantel
{
    public static Plantel getPlantelActual()
    {
        return PlantelDAL.getPlantelActual();
    }
}