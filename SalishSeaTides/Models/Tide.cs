using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Graphics;

namespace SalishSeaTides.Models;

public partial class Tide(string height, DateTime tideDateTime, string tideType)
{
    public double Height { get; set; } = Convert.ToDouble(height);
    public DateTime TideDateTime { get; set; } = tideDateTime;
    public string TideType { get; set; } = tideType; //H or L
    public string DisplayTimeForTable { get; set; }
    
    public Color DisplayColorForTable { get; set; }
}

public class TideGroup : ObservableCollection<Tide>
{
    public string TideDateTime { get; private set; }

    public TideGroup(string dateCategory, IGrouping<string, Tide> tides) : base(tides)
    {
        TideDateTime = dateCategory;
    }
}