namespace HousingManagementSystemApi.Helpers;

using Models.Capita;

public interface ICapitaGatewayHelper
{
    public LogJobRequest CreateLogJobRequest(
        string place_ref,
        string std_job_code,
        string client_ref,
        string source,
        string sor,
        string location,
        string quantity,
        string description);
}
