namespace HousingManagementSystemApi.Models.Capita;

using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot(ElementName = "Line")]
public class Line
{
    [XmlElement(ElementName = "Parameters")]
    public List<Parameters> Parameters { get; set; }
}
