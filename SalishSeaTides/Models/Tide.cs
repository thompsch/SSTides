namespace SalishSeaTides.Models;

public partial class Tide(string height, DateTime tideDateTime, string tideType)
{
    public double Height { get; set; } = Convert.ToDouble(height);
    public DateTime TideDateTime { get; set; } = tideDateTime;
    public string TideType { get; set; } = tideType; //H or L
    public string ShortDate { get; set; }
}