namespace HousingManagementSystemApi.Models.Capita;

using System.Xml.Serialization;

[XmlRoot(ElementName = "Job_logged")]
public class Job_logged
{
    [XmlAttribute(AttributeName = "job_no")]
    public string Job_no { get; set; }
    [XmlAttribute(AttributeName = "logged_info")]
    public string Logged_info { get; set; }
    [XmlText]
    public string Text { get; set; }
}
