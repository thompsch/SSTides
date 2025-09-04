using System.Xml.Serialization;

namespace SalishSeaTides.Models;
using System;  
using System.Collections.Generic;  
using System.Xml.Serialization;  

[XmlRoot("datainfo")]  
public class TideModel
{
    [XmlElement("origin")]  
    public string Origin { get; set; }  
  
    [XmlElement("disclaimer")]  
    public string Disclaimer { get; set; }  
  
    [XmlElement("datarange")]  
    public string DataRange { get; set; }  
  
    [XmlElement("producttype")]  
    public string ProductType { get; set; }  
  
    [XmlElement("stationname")]  
    public string StationName { get; set; }  
  
    [XmlElement("state")]  
    public string State { get; set; }  
  
    [XmlElement("stationid")]  
    public string StationId { get; set; }  
  
    [XmlElement("stationtype")]  
    public string StationType { get; set; }  
  
    [XmlElement("referencedToStationName")]  
    public string ReferencedToStationName { get; set; }  
  
    [XmlElement("referencedToStationId")]  
    public string ReferencedToStationId { get; set; }  
  
    [XmlElement("HeightOffsetLow")]  
    public string HeightOffsetLow { get; set; }  
  
    [XmlElement("HeightOffsetHigh")]  
    public string HeightOffsetHigh { get; set; }  
  
    [XmlElement("TimeOffsetLow")]  
    public string TimeOffsetLow { get; set; }  
  
    [XmlElement("TimeOffsetHigh")]  
    public string TimeOffsetHigh { get; set; }  
  
    [XmlElement("BeginDate")]  
    public string BeginDate { get; set; }  
  
    [XmlElement("EndDate")]  
    public string EndDate { get; set; }  
  
    [XmlElement("dataUnits")]  
    public string DataUnits { get; set; }  
  
    [XmlElement("Timezone")]  
    public string Timezone { get; set; }  
  
    [XmlElement("Datum")]  
    public string Datum { get; set; }  
  
    [XmlElement("IntervalType")]  
    public string IntervalType { get; set; }  
  
    [XmlArray("data")]  
    [XmlArrayItem("item")]  
    public List<DataItem> Items { get; set; }  
}  
  
public class DataItem  
{  
    [XmlElement("date")]  
    public string Date { get; set; }  
  
    [XmlElement("day")]  
    public string Day { get; set; }  
  
    [XmlElement("time")]  
    public string Time { get; set; }  
  
    [XmlElement("pred_in_ft")]  
    public string PredInFt { get; set; }  
  
    [XmlElement("pred_in_cm")]  
    public string PredInCm { get; set; }  
  
    [XmlElement("highlow")]  
    public string HighLow { get; set; }  
}  