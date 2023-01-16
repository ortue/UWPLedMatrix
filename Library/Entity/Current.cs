namespace Library.Entity
{
  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
  public partial class current
  {
    public currentCity? city { get; set; }
    public currentTemperature? temperature { get; set; }
    public currentFeels_like? feels_like { get; set; }
    public currentHumidity? humidity { get; set; }
    public currentPressure? pressure { get; set; }
    public currentWind? wind { get; set; }
    public currentClouds? clouds { get; set; }
    public currentVisibility? visibility { get; set; }
    public currentPrecipitation? precipitation { get; set; }
    public currentWeather? weather { get; set; }
    public currentLastupdate? lastupdate { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentCity
  {
    public currentCityCoord? coord { get; set; }
    public string? country { get; set; }
    public short timezone { get; set; }
    public currentCitySun? sun { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public uint id { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public string? name { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentCityCoord
  {
    [System.Xml.Serialization.XmlAttribute()]
    public decimal lon { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public decimal lat { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentCitySun
  {
    [System.Xml.Serialization.XmlAttribute()]
    public DateTime rise { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public DateTime set { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentTemperature
  {
    [System.Xml.Serialization.XmlAttribute()]
    public decimal value { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public decimal min { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public decimal max { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public string? unit { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentFeels_like
  {
    [System.Xml.Serialization.XmlAttribute()]
    public decimal value { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public string? unit { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentHumidity
  {
    [System.Xml.Serialization.XmlAttribute()]
    public int value { get; set; }

    [System.Xml.Serialization.XmlAttribute()]
    public string? unit { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentPressure
  {
    [System.Xml.Serialization.XmlAttribute()]
    public ushort value { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public string? unit { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentWind
  {
    public currentWindSpeed? speed { get; set; }
    public object? gusts { get; set; }
    public currentWindDirection? direction { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentWindSpeed
  {
    [System.Xml.Serialization.XmlAttribute()]
    public decimal value { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public string? unit { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public string? name { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentWindDirection
  {
    [System.Xml.Serialization.XmlAttribute()]
    public int value { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public string? code { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public string? name { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentClouds
  {
    [System.Xml.Serialization.XmlAttribute()]
    public int value { get; set; }
    [System.Xml.Serialization.XmlAttribute()]
    public string? name { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentVisibility
  {
    [System.Xml.Serialization.XmlAttribute()]
    public ushort value { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentPrecipitation
  {
    [System.Xml.Serialization.XmlAttribute()]
    public string? mode { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentWeather
  {
    [System.Xml.Serialization.XmlAttribute()]
    public ushort number { get; set; }

    [System.Xml.Serialization.XmlAttribute()]
    public string? value { get; set; }

    [System.Xml.Serialization.XmlAttribute()]
    public string? icon { get; set; }
  }

  [System.Xml.Serialization.XmlType(AnonymousType = true)]
  public partial class currentLastupdate
  {
    [System.Xml.Serialization.XmlAttribute()]
    public DateTime value { get; set; }
  }
}
