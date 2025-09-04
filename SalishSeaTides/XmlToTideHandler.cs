using System.Xml.Serialization;
using SalishSeaTides.Models;

namespace SalishSeaTides;

public class XmlToTideHandler
{
    public static async Task<TideModel> GetAllTides(int selectedStationID)
    {
        var year = DateTime.Now.Year;
        var fileName = $"{selectedStationID}_2025.xml";
        await using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
        using var reader = new StreamReader(stream);
        var serializer = new XmlSerializer(typeof(TideModel));
        var data = (TideModel)serializer.Deserialize(stream);

        return data;
    }
}