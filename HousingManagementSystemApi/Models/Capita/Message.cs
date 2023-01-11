namespace HousingManagementSystemApi.Models.Capita;

using System.Xml.Serialization;

[XmlRoot(ElementName = "message")]
public class Message
{
    [XmlElement(ElementName = "Header")]
    public Header Header { get; set; }

    [XmlElement(ElementName = "Body")]
    public Body Body { get; set; }
}
