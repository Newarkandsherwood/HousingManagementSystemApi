namespace HousingManagementSystemApi.Models.Capita;

using System.Xml.Serialization;

[XmlRoot(ElementName = "Parameters")]
public class Parameters
{

    [XmlAttribute(AttributeName = "attribute")]
    public string Attribute { get; set; }

    [XmlAttribute(AttributeName = "attribute_value")]
    public string AttributeValue { get; set; }
}
