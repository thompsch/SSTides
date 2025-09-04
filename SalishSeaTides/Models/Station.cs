namespace SalishSeaTides.Models;

public class Station
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; } = "";
    public int StationId { get; set; }

    private Station(double latitude, double longitude, string name, int stationId)
    {
        this.Latitude = latitude;
        this.Longitude = longitude;    
        this.Name = name;
        this.StationId = stationId;
    }
    public static List<Station> Stations { get; set; } = new List<Station>()
    {
new Station(48.035,-122.377,"9447856", 9447856)
    };
}