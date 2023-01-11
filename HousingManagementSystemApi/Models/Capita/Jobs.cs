namespace HousingManagementSystemApi.Models.Capita;

using System.Xml.Serialization;

[XmlRoot(ElementName = "Jobs")]
public class Jobs
{
    [XmlElement(ElementName = "Job_logged")]
    public Job_logged Job_logged { get; set; }
}
