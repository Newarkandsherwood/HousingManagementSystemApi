namespace HousingManagementSystemApi.Models.Capita;

using System.Xml.Serialization;

[XmlRoot(ElementName = "Lines")]
public class Lines
{
    [XmlElement(ElementName = "Line")]
    public Line Line { get; set; }
}
