namespace HousingManagementSystemApi.Models.Capita;

using System.Xml.Serialization;

[XmlRoot(ElementName = "Root")]
public class LogJobResponse
{
    [XmlElement(ElementName = "Jobs")]
    public Jobs Jobs { get; set; }

    [XmlElement(ElementName = "ErrorDetails")]
    public string ErrorDetails { get; set; }
}
