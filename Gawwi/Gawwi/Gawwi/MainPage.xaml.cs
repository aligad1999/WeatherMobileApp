using Gawwi.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Gawwi
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            getCountry();
        }


        public async void getCountry()
        {
            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var response = await client.GetAsync("https://api.ipify.org/?format=json"); //Get Ip
            var result = await response.Content.ReadAsStringAsync();
            GPS dataSet = (GPS)JsonConvert.DeserializeObject(result);
            
            response = await client.GetAsync("http://ip-api.com/json/" + dataSet.ip.ToString()); //Get Location
            result = await response.Content.ReadAsStringAsync();
            //DisplayAlert("Result", result.ToString(), "Ok");
            dataSet = (GPS) JsonConvert.DeserializeObject(result);
            Double Lat1 = Convert.ToDouble(dataSet.lat.ToString().Replace(".", ","));
            Double Lng1 = Convert.ToDouble(dataSet.lon.ToString().Replace(".", ","));
            var place = dataSet.country +","+ dataSet.city;
            getWeather(place);
        }
        public async void getWeather(String place)
        {
            string url= "http://api.openweathermap.org/data/2.5/weather?q="+place+"&APPID=6247646ecd029f532cd809d5aab27d48&units=metrix";
            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string result = await client.GetStringAsync(url);
            OpenWeatherMap weather =(OpenWeatherMap) JsonConvert.DeserializeObject(result);

        }

    }
}
