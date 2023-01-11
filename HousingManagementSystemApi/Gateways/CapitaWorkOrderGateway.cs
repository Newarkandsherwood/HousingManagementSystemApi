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
    private string capitaUrlString;
    private string username;
    private string password;
    private string std_job_code;
    private string client_ref;
    private string source;
    private string location;
    private const string quantity = "1";

    public CapitaWorkOrderGateway(ICapitaGatewayHelper capitaGatewayHelper)
    {
        this.capitaGatewayHelper = capitaGatewayHelper;
        capitaUrlString = EnvironmentVariableHelper.GetEnvironmentVariable("CAPITA_URL");
        username = EnvironmentVariableHelper.GetEnvironmentVariable("CAPITA_USERNAME");
        password = EnvironmentVariableHelper.GetEnvironmentVariable("CAPITA_PASSWORD");
        std_job_code = EnvironmentVariableHelper.GetEnvironmentVariable("CAPITA_STDJOBCODE");
        source = EnvironmentVariableHelper.GetEnvironmentVariable("CAPITA_SOURCE");
        location = EnvironmentVariableHelper.GetEnvironmentVariable("CAPITA_SUBLOCATION");
    }
    public async Task<string> CreateWorkOrder(string description, string locationId, string sorCode)
    {
        Guard.Against.NullOrWhiteSpace(description, nameof(locationId));
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

        var restResponse = await restSharp.PostAsync<LogJobResponse>(restRequest);

        return restResponse.Jobs.Job_logged.Job_no;
    }
}
