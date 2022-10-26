namespace HousingManagementSystemApi.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;

public static class RepairType
{
    public const string Tenant = "TENANT";

    public const string Communal = "COMMUNAL";

    public static readonly IEnumerable<string> All = new[] { Tenant, Communal };

    public static readonly Func<string, bool> IsValidValue = repairType => All.Contains(repairType);
}