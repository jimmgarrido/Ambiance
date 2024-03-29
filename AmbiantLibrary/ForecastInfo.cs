﻿using System;
using System.Collections.Generic;

namespace AmbiantLibrary
{
	public class ForecastInfo
	{
		public float Latitude { get; set; }

		public float Longitude { get; set; }

		public string TimeZone { get; set; }

		public DataPoint Currently { get; set; }

		public DataBlock Daily { get; set; }

	}

	public class DataPoint
	{
		public float ApparentTemperature { get; set; }

		public float ApparentTemperatureHigh { get; set; }

		public float Temperature { get; set; }

		public float TemperatureHigh { get; set; }

		public float TemperatureLow { get; set; }

		public long Time { get; set; }
	}

	public class DataBlock
	{
		public List<DataPoint> Data { get; set; }

		public string Summary { get; set; }
	}
}
