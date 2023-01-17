namespace HousingManagementSystemApi.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Models.Capita;
using RestSharp;

public class CapitaService : ICapitaService
{
    private readonly RestClient restClient;
    private IOptions<CapitaOptions> capitaOptions;
    private string client_ref;
    private const string quantity = "1";

    public CapitaService(RestClient restClient, IOptions<CapitaOptions> capitaOptions)
    {
        this.restClient = restClient;
        this.capitaOptions = capitaOptions;
    }

    public async Task<string> LogJob(string description, string locationId, string sorCode)
    {
        Guard.Against.NullOrWhiteSpace(description, nameof(description));
        Guard.Against.NullOrWhiteSpace(locationId, nameof(locationId));
        Guard.Against.NullOrWhiteSpace(sorCode, nameof(sorCode));

        var restRequest = new RestRequest { Method = Method.Post };

        var logJobRequest = CreateLogJobRequest(locationId, capitaOptions.Value.StandardJobCode, client_ref,
            capitaOptions.Value.Source, sorCode, capitaOptions.Value.Sublocation, quantity, description);

        var body = new Message
        {
            Header = new Header { Security = new Security { Username = capitaOptions.Value.Username, Password = capitaOptions.Value.Password } },
            Body = new Body
            {
                Request = logJobRequest,
            }
        };
        restRequest.AddXmlBody(body);

        var restResponse = await this.restClient.PostAsync<LogJobResponse>(restRequest);

        if (!string.IsNullOrEmpty(restResponse.ErrorDetails))
        {
            throw new SystemException(restResponse.ErrorDetails);
        }

        return restResponse.Jobs.Job_logged.Job_no;
    }

    private LogJobRequest CreateLogJobRequest(
        string place_ref,
        string std_job_code,
        string client_ref,
        string source,
        string sorCode,
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
                new() { Attribute = "long_description", AttributeValue = description },
            },
            Lines = new Lines
            {
                Line = new Line
                {
                    Parameters = new List<Parameters>
                    {
                        new() { Attribute = "sor", AttributeValue = sorCode},
                        new() { Attribute = "location", AttributeValue = location },
                        new() { Attribute = "quantity", AttributeValue = quantity },
                    }
                }
            }
        };
}
