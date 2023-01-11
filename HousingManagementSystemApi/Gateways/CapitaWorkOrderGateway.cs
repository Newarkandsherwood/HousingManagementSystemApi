using System;
using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HousingManagementSystemApi.Helpers;
using RestSharp;

namespace HousingManagementSystemApi.Gateways;

using Models.Capita;

public class CapitaWorkOrderGateway : IWorkOrderGateway
{
    private readonly ICapitaGatewayHelper capitaGatewayHelper;
    const string capitaUrlString = "https://test";
    const string username = "test";
    const string password = "test";
    private const string std_job_code = "WEB";
    private const string client_ref = "abc125";
    private const string source = "ONREP";
    private const string location = "NA";
    private const string quantity = "1";
    private const string description = "Testing web services";

    public CapitaWorkOrderGateway(ICapitaGatewayHelper capitaGatewayHelper)
    {
        this.capitaGatewayHelper = capitaGatewayHelper;
    }
    public Task<string> CreateWorkOrder(string locationId, string sorCode)
    {
        Guard.Against.NullOrWhiteSpace(locationId, nameof(locationId));
        Guard.Against.NullOrWhiteSpace(sorCode, nameof(sorCode));

        var restSharp = new RestClient(new HttpClient { BaseAddress = new Uri(capitaUrlString) });
        var restRequest = new RestRequest { Method = Method.Post };

        var logJobRequest = capitaGatewayHelper.CreateLogJobRequest(locationId, std_job_code, client_ref,
            source, sorCode, location, quantity, description);

        var body = new Message
        {
            Header = new Header { Security = new Security { Username = username, Password = password } },
            Body = new Body
            {
                Request = logJobRequest
            }
        };
        restRequest.AddXmlBody(body);

        var restResponse = restSharp.Post<LogJobResponse>(restRequest);

        return Task.FromResult(restResponse.Jobs.Job_logged.Job_no);
    }
}
