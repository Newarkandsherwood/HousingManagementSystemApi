namespace HousingManagementSystemApi.Helpers;

using System.Collections.Generic;
using System.Xml.Serialization;

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
            RequestType = "logjob", Parameters = new List<Parameters>
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

[XmlRoot(ElementName = "Security")]
public class Security
{

    [XmlAttribute(AttributeName = "username")]
    public string Username { get; set; }

    [XmlAttribute(AttributeName = "password")]
    public string Password { get; set; }
}

[XmlRoot(ElementName = "Header")]
public class Header
{

    [XmlElement(ElementName = "Security")]
    public Security Security { get; set; }
}

[XmlRoot(ElementName = "Parameters")]
public class Parameters
{

    [XmlAttribute(AttributeName = "attribute")]
    public string Attribute { get; set; }

    [XmlAttribute(AttributeName = "attribute_value")]
    public string AttributeValue { get; set; }
}

[XmlRoot(ElementName = "Body")]
public class Body
{

    [XmlElement(ElementName = "Request")]
    public LogJobRequest Request { get; set; }
}

[XmlRoot(ElementName = "message")]
public class Message
{

    [XmlElement(ElementName = "Header")]
    public Header Header { get; set; }

    [XmlElement(ElementName = "Body")]
    public Body Body { get; set; }
}

[XmlRoot(ElementName = "Place")]
public class Place
{

    [XmlAttribute(AttributeName = "address1")]
    public string Address1 { get; set; }

    [XmlAttribute(AttributeName = "address2")]
    public string Address2 { get; set; }

    [XmlAttribute(AttributeName = "address3")]
    public string Address3 { get; set; }

    [XmlAttribute(AttributeName = "address4")]
    public string Address4 { get; set; }

    [XmlAttribute(AttributeName = "address5")]
    public string Address5 { get; set; }

    [XmlAttribute(AttributeName = "address_without_number")]
    public string AddressWithoutNumber { get; set; }

    [XmlAttribute(AttributeName = "date_created")]
    public string DateCreated { get; set; }

    [XmlAttribute(AttributeName = "formatted_addr_1")]
    public string FormattedAddr1 { get; set; }

    [XmlAttribute(AttributeName = "formatted_addr_2")]
    public string FormattedAddr2 { get; set; }

    [XmlAttribute(AttributeName = "formatted_addr_3")]
    public string FormattedAddr3 { get; set; }

    [XmlAttribute(AttributeName = "formatted_addr_county")]
    public string FormattedAddrCounty { get; set; }

    [XmlAttribute(AttributeName = "formatted_addr_parish")]
    public string FormattedAddrParish { get; set; }

    [XmlAttribute(AttributeName = "formatted_addr_pcode")]
    public string FormattedAddrPcode { get; set; }

    [XmlAttribute(AttributeName = "formatted_addr_town")]
    public string FormattedAddrTown { get; set; }

    [XmlAttribute(AttributeName = "formatted_addr_ward")]
    public string FormattedAddrWard { get; set; }

    [XmlAttribute(AttributeName = "full_address")]
    public string FullAddress { get; set; }

    [XmlAttribute(AttributeName = "internal")]
    public string Internal { get; set; }

    [XmlAttribute(AttributeName = "number")]
    public int Number { get; set; }

    [XmlAttribute(AttributeName = "paon")]
    public string Paon { get; set; }

    [XmlAttribute(AttributeName = "paon_end_range")]
    public int PaonEndRange { get; set; }

    [XmlAttribute(AttributeName = "paon_end_suffix")]
    public string PaonEndSuffix { get; set; }

    [XmlAttribute(AttributeName = "parish")]
    public string Parish { get; set; }

    [XmlAttribute(AttributeName = "parish_code")]
    public string ParishCode { get; set; }

    [XmlAttribute(AttributeName = "place_ref")]
    public string PlaceRef { get; set; }

    [XmlAttribute(AttributeName = "place_type")]
    public string PlaceType { get; set; }

    [XmlAttribute(AttributeName = "post_code")]
    public string PostCode { get; set; }

    [XmlAttribute(AttributeName = "post_code_comp")]
    public string PostCodeComp { get; set; }

    [XmlAttribute(AttributeName = "post_town")]
    public string PostTown { get; set; }

    [XmlAttribute(AttributeName = "saon")]
    public string Saon { get; set; }

    [XmlAttribute(AttributeName = "saon_end_range")]
    public int SaonEndRange { get; set; }

    [XmlAttribute(AttributeName = "saon_end_suffix")]
    public string SaonEndSuffix { get; set; }

    [XmlAttribute(AttributeName = "saon_narrative")]
    public string SaonNarrative { get; set; }

    [XmlAttribute(AttributeName = "saon_start_range")]
    public int SaonStartRange { get; set; }

    [XmlAttribute(AttributeName = "saon_start_suffix")]
    public string SaonStartSuffix { get; set; }

    [XmlAttribute(AttributeName = "street")]
    public string Street { get; set; }

    [XmlAttribute(AttributeName = "street_name")]
    public string StreetName { get; set; }

    [XmlAttribute(AttributeName = "suffix")]
    public string Suffix { get; set; }

    [XmlAttribute(AttributeName = "uprn")]
    public string Uprn { get; set; }

    [XmlAttribute(AttributeName = "ward")]
    public string Ward { get; set; }

    [XmlAttribute(AttributeName = "ward_code")]
    public string WardCode { get; set; }

    [XmlText]
    public string Text { get; set; }
}

[XmlRoot(ElementName = "Root")]
public class Root
{

    [XmlElement(ElementName = "Place")]
    public List<Place> Place { get; set; }
}


[XmlRoot(ElementName = "Line")]
public class Line
{
    [XmlElement(ElementName = "Parameters")]
    public List<Parameters> Parameters { get; set; }
}

[XmlRoot(ElementName = "Lines")]
public class Lines
{
    [XmlElement(ElementName = "Line")]
    public Line Line { get; set; }
}

[XmlRoot(ElementName = "Request")]
public class LogJobRequest
{
    [XmlElement(ElementName = "Parameters")]
    public List<Parameters> Parameters { get; set; }
    [XmlElement(ElementName = "Lines")]
    public Lines Lines { get; set; }
    [XmlAttribute(AttributeName = "request_type")]
    public string RequestType { get; set; }
}
[XmlRoot(ElementName = "Job_logged")]
public class Job_logged
{
    [XmlAttribute(AttributeName = "job_no")]
    public string Job_no { get; set; }
    [XmlAttribute(AttributeName = "logged_info")]
    public string Logged_info { get; set; }
    [XmlText]
    public string Text { get; set; }
}

[XmlRoot(ElementName = "Jobs")]
public class Jobs
{
    [XmlElement(ElementName = "Job_logged")]
    public Job_logged Job_logged { get; set; }
}

[XmlRoot(ElementName = "Root")]
public class LogJobResponse
{
    [XmlElement(ElementName = "Jobs")]
    public Jobs Jobs { get; set; }

    [XmlElement(ElementName = "ErrorDetails")]
    public string ErrorDetails { get; set; }
}

