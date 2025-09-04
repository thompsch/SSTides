using System.Xml.Serialization;
using SalishSeaTides.Models;
using UserNotifications;

namespace SalishSeaTides;

public class XmlToTideHandler
{
    public static async Task<TideModel> GetAllTides()
    {
        var year = DateTime.Now.Year;
        var fileName = "9447856_2025.xml"; //$"{year}.xml";
        await using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
        using var reader = new StreamReader(stream);
        var serializer = new XmlSerializer(typeof(TideModel));
        var data = (TideModel)serializer.Deserialize(stream);

        return data;
    }
}