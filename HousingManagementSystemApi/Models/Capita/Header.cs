namespace HousingManagementSystemApi.Models.Capita;

using System.Xml.Serialization;

[XmlRoot(ElementName = "Header")]
public class Header
{
    [XmlElement(ElementName = "Security")]
    public Security Security { get; set; }
}
