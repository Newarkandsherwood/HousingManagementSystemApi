namespace HousingManagementSystemApi.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;

public static class RepairType
{
    public const string Tenant = "TENANT";

    public const string Communal = "COMMUNAL";

    public const string Leasehold = "LEASEHOLD";

    public static readonly IEnumerable<string> All = new[] { Tenant, Communal, Leasehold };

    public static readonly Func<string, bool> IsValidValue = repairType => All.Contains(repairType);
}
