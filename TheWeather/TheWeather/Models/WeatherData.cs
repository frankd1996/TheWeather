using System;
using System.Collections.Generic;
using System.Text;

namespace TheWeather.Models
{
    //Los datos int se convirtieron todos a float para evitar excepción
    public class WeatherData
    {
        public List<Datum> data { get; set; }
        public float count { get; set; }
    }
    public class Weather
    {
        public string icon { get; set; }
        public float code { get; set; }
        public string description { get; set; }
    }

    public class Datum
    {
        public float rh { get; set; }
        public string pod { get; set; }
        public double lon { get; set; }
        public double pres { get; set; }
        public string timezone { get; set; }
        public string ob_time { get; set; }
        public string country_code { get; set; }
        public float clouds { get; set; }
        public float ts { get; set; }
        public double solar_rad { get; set; }
        public string state_code { get; set; }
        public string city_name { get; set; }
        public double wind_spd { get; set; }
        public string wind_cdir_full { get; set; }
        public string wind_cdir { get; set; }
        public double slp { get; set; }
        public float vis { get; set; }
        public double h_angle { get; set; }
        public string sunset { get; set; }
        public double dni { get; set; }
        public double dewpt { get; set; }
        public float snow { get; set; }
        public double uv { get; set; }
        public float precip { get; set; }
        public float wind_dir { get; set; }
        public string sunrise { get; set; }
        public double ghi { get; set; }
        public double dhi { get; set; }
        public float aqi { get; set; }
        public double lat { get; set; }
        public Weather weather { get; set; }
        public string datetime { get; set; }
        public float temp { get; set; }
        public string station { get; set; }
        public double elev_angle { get; set; }
        public double app_temp { get; set; }
    }   
}
