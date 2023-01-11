namespace HousingManagementSystemApi.Models.Capita;

using System.Xml.Serialization;

[XmlRoot(ElementName = "Security")]
public class Security
{

    [XmlAttribute(AttributeName = "username")]
    public string Username { get; set; }

    [XmlAttribute(AttributeName = "password")]
    public string Password { get; set; }
}
