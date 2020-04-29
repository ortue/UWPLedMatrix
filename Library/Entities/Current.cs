/// <remarks/>
[System.Xml.Serialization.XmlType(AnonymousType = true)]
[System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
public partial class current
{
  private currentPressure pressureField;

  private currentWind windField;

  private currentClouds cloudsField;

  private currentVisibility visibilityField;

  private currentPrecipitation precipitationField;

  private currentWeather weatherField;

  private currentLastupdate lastupdateField;

  /// <remarks/>
  public currentCity city { get; set; }

  /// <remarks/>
  public currentTemperature temperature { get; set; }

  /// <remarks/>
  public currentFeels_like feels_like { get; set; }

  /// <remarks/>
  public currentHumidity humidity { get; set; }

  /// <remarks/>
  public currentPressure pressure
  {
    get
    {
      return this.pressureField;
    }
    set
    {
      this.pressureField = value;
    }
  }

  /// <remarks/>
  public currentWind wind
  {
    get
    {
      return this.windField;
    }
    set
    {
      this.windField = value;
    }
  }

  /// <remarks/>
  public currentClouds clouds
  {
    get
    {
      return this.cloudsField;
    }
    set
    {
      this.cloudsField = value;
    }
  }

  /// <remarks/>
  public currentVisibility visibility
  {
    get
    {
      return this.visibilityField;
    }
    set
    {
      this.visibilityField = value;
    }
  }

  /// <remarks/>
  public currentPrecipitation precipitation
  {
    get
    {
      return this.precipitationField;
    }
    set
    {
      this.precipitationField = value;
    }
  }

  /// <remarks/>
  public currentWeather weather
  {
    get
    {
      return this.weatherField;
    }
    set
    {
      this.weatherField = value;
    }
  }

  /// <remarks/>
  public currentLastupdate lastupdate
  {
    get
    {
      return this.lastupdateField;
    }
    set
    {
      this.lastupdateField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentCity
{

  private currentCityCoord coordField;

  private string countryField;

  private short timezoneField;

  private currentCitySun sunField;

  private uint idField;

  private string nameField;

  /// <remarks/>
  public currentCityCoord coord
  {
    get
    {
      return this.coordField;
    }
    set
    {
      this.coordField = value;
    }
  }

  /// <remarks/>
  public string country
  {
    get
    {
      return this.countryField;
    }
    set
    {
      this.countryField = value;
    }
  }

  /// <remarks/>
  public short timezone
  {
    get
    {
      return this.timezoneField;
    }
    set
    {
      this.timezoneField = value;
    }
  }

  /// <remarks/>
  public currentCitySun sun
  {
    get
    {
      return this.sunField;
    }
    set
    {
      this.sunField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public uint id
  {
    get
    {
      return this.idField;
    }
    set
    {
      this.idField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string name
  {
    get
    {
      return this.nameField;
    }
    set
    {
      this.nameField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentCityCoord
{

  private decimal lonField;

  private decimal latField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public decimal lon
  {
    get
    {
      return this.lonField;
    }
    set
    {
      this.lonField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public decimal lat
  {
    get
    {
      return this.latField;
    }
    set
    {
      this.latField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentCitySun
{

  private System.DateTime riseField;

  private System.DateTime setField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public System.DateTime rise
  {
    get
    {
      return this.riseField;
    }
    set
    {
      this.riseField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public System.DateTime set
  {
    get
    {
      return this.setField;
    }
    set
    {
      this.setField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentTemperature
{

  private decimal valueField;

  private decimal minField;

  private decimal maxField;

  private string unitField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public decimal value
  {
    get
    {
      return this.valueField;
    }
    set
    {
      this.valueField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public decimal min
  {
    get
    {
      return this.minField;
    }
    set
    {
      this.minField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public decimal max
  {
    get
    {
      return this.maxField;
    }
    set
    {
      this.maxField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string unit
  {
    get
    {
      return this.unitField;
    }
    set
    {
      this.unitField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentFeels_like
{

  private decimal valueField;

  private string unitField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public decimal value
  {
    get
    {
      return this.valueField;
    }
    set
    {
      this.valueField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string unit
  {
    get
    {
      return this.unitField;
    }
    set
    {
      this.unitField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentHumidity
{

  private byte valueField;

  private string unitField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public byte value
  {
    get
    {
      return this.valueField;
    }
    set
    {
      this.valueField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string unit
  {
    get
    {
      return this.unitField;
    }
    set
    {
      this.unitField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentPressure
{

  private ushort valueField;

  private string unitField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public ushort value
  {
    get
    {
      return this.valueField;
    }
    set
    {
      this.valueField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string unit
  {
    get
    {
      return this.unitField;
    }
    set
    {
      this.unitField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentWind
{

  private currentWindSpeed speedField;

  private object gustsField;

  private currentWindDirection directionField;

  /// <remarks/>
  public currentWindSpeed speed
  {
    get
    {
      return this.speedField;
    }
    set
    {
      this.speedField = value;
    }
  }

  /// <remarks/>
  public object gusts
  {
    get
    {
      return this.gustsField;
    }
    set
    {
      this.gustsField = value;
    }
  }

  /// <remarks/>
  public currentWindDirection direction
  {
    get
    {
      return this.directionField;
    }
    set
    {
      this.directionField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentWindSpeed
{

  private decimal valueField;

  private string unitField;

  private string nameField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public decimal value
  {
    get
    {
      return this.valueField;
    }
    set
    {
      this.valueField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string unit
  {
    get
    {
      return this.unitField;
    }
    set
    {
      this.unitField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string name
  {
    get
    {
      return this.nameField;
    }
    set
    {
      this.nameField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentWindDirection
{

  private byte valueField;

  private string codeField;

  private string nameField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public byte value
  {
    get
    {
      return this.valueField;
    }
    set
    {
      this.valueField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string code
  {
    get
    {
      return this.codeField;
    }
    set
    {
      this.codeField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string name
  {
    get
    {
      return this.nameField;
    }
    set
    {
      this.nameField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentClouds
{

  private byte valueField;

  private string nameField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public byte value
  {
    get
    {
      return this.valueField;
    }
    set
    {
      this.valueField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string name
  {
    get
    {
      return this.nameField;
    }
    set
    {
      this.nameField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentVisibility
{

  private ushort valueField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public ushort value
  {
    get
    {
      return this.valueField;
    }
    set
    {
      this.valueField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentPrecipitation
{

  private string modeField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string mode
  {
    get
    {
      return this.modeField;
    }
    set
    {
      this.modeField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentWeather
{

  private ushort numberField;

  private string valueField;

  private string iconField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public ushort number
  {
    get
    {
      return this.numberField;
    }
    set
    {
      this.numberField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string value
  {
    get
    {
      return this.valueField;
    }
    set
    {
      this.valueField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string icon
  {
    get
    {
      return this.iconField;
    }
    set
    {
      this.iconField = value;
    }
  }
}

/// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class currentLastupdate
{

  private System.DateTime valueField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public System.DateTime value
  {
    get
    {
      return this.valueField;
    }
    set
    {
      this.valueField = value;
    }
  }
}

