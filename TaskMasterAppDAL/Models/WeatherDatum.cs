using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class WeatherDatum
{
    public int WeatherId { get; set; }

    public string? Location { get; set; }

    public string? Forecast { get; set; }

    public double? Temperature { get; set; }

    public int? Humidity { get; set; }

    public double? WindSpeed { get; set; }

    public DateOnly? Date { get; set; }

    public DateTime? CreatedAt { get; set; }
}
