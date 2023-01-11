namespace HousingManagementSystemApi.Models.Capita;

using System.Xml.Serialization;
using Helpers;

[XmlRoot(ElementName = "Body")]
public class Body
{
    [XmlElement(ElementName = "Request")]
    public LogJobRequest Request { get; set; }
}
