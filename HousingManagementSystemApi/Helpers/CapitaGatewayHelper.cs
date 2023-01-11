namespace HousingManagementSystemApi.Helpers;

using System.Collections.Generic;
using System.Xml.Serialization;
using Models.Capita;

public static class CapitaGatewayHelper
{
    public static LogJobRequest CreateLogJobRequst(
        string place_ref,
        string std_job_code,
        string client_ref,
        string source,
        string sor,
        string location,
        string quantity,
        string description) =>
        new()
        {
            RequestType = "logjob",
            Parameters = new List<Parameters>
            {
                new() { Attribute = "place_ref", AttributeValue = place_ref },
                new() { Attribute = "std_job_code", AttributeValue = std_job_code },
                new() { Attribute = "client_ref", AttributeValue = client_ref },
                new() { Attribute = "source", AttributeValue = source },
            },
            Lines = new Lines
            {
                Line = new Line
                {
                    Parameters = new List<Parameters>
                    {
                        new() { Attribute = "sor", AttributeValue = sor},
                        new() { Attribute = "location", AttributeValue = location },
                        new() { Attribute = "quantity", AttributeValue = quantity },
                        new() { Attribute = "description", AttributeValue = description },
                    }
                }
            }
        };
}

