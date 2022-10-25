namespace HousingManagementSystemApi.Tests.TestHelpers;

using System;
using Helpers;
using Xunit;

public class RepairTypeTestData
{
    public static TheoryData<Exception, string> InvalidRepairTypeArgumentTestData() =>
        new()
        {
            { new ArgumentNullException(), null },
            { new ArgumentException(), "" },
            { new ArgumentException(), " " },
            { new ArgumentException(), "non-repair-type-value" },
        };

    public static TheoryData<string> ValidRepairTypeArgumentTestData()
    {
        var result = new TheoryData<string>();
        foreach (var repairType in RepairType.All)
        {
            result.Add(repairType);
        }
        return result;
    }
}

