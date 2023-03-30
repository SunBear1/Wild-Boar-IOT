namespace WebApi.Model;

public class dashboardData
{
    public WildBoarIotData lastReceivedMessage { get; set; }
    public double chestAVGweight { get; set; }
    public double bicepsAVGweight { get; set; }
    public double treadmillAVGweight { get; set; }
    public int chestAVGoccupancy { get; set; }
    public int bicepsAVGoccupancy { get; set; }
    public int treadmillAVGoccupancy { get; set; }
}