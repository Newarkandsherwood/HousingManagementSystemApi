namespace HousingManagementSystemApi.Models.Capita;

using System.Collections.Generic;
using System.Xml.Serialization;

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
