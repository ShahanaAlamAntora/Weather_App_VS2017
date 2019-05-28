using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        const string APPID = "542ffd081e67f4512b705f89d2a611b2";
        string cityName = "Colombo";

        public Form1()
        {
            InitializeComponent();
            getWeather("America");
            getForcast("America");
        }

        void getWeather(string city)
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format(format: " http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric&cnt=6 ",city,APPID);

                string json = web.DownloadString(url);

                WeatherInfo.root result = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                WeatherInfo.root outPut = result;

                lbl_cityName.Text = string.Format(" {0}", outPut.name);
                lbl_country.Text = string.Format(" {0}", outPut.sys.country);
                lbl_Temp.Text = string.Format(" {0} \u00B0C" , outPut.main.temp);

            }
        }
        void getForcast(string city)
        {
            int day = 5;
            string url = string.Format(format: " http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt={1}&APPID={2}",city,day,APPID);

            using (WebClient web = new WebClient())
                {

                string json = web.DownloadString(url);

                weatherForcast Object = JsonConvert.DeserializeObject<weatherForcast>(json);

                    

                    weatherForcast forcast = Object;
                    
                    //next 2nd day
                    lbl_day_2.Text = string.Format(" {0}", getDate(forcast.list[1].dt).DayOfWeek); //returning day
                    lbl_cond_2.Text = string.Format(" {0}", forcast.list[1].weather[0].main); //weather condition
                    lbl_des_2.Text = string.Format(" {0}", forcast.list[1].weather[0].description); //weather description
                    lbl_temp_2.Text = string.Format(" {0} \u00B0C", forcast.list[1].temp.day); //weather temp
                    lbl_wind_2.Text = string.Format(" {0} km/h", forcast.list[1].speed); //weather  wind speed


                //next 3rd day
                lbl_day_3.Text = string.Format(" {0}", getDate(forcast.list[2].dt).DayOfWeek); //returning day
                lbl_cond_3.Text = string.Format(" {0}", forcast.list[2].weather[0].main); //weather condition
                lbl_des_3.Text = string.Format(" {0}", forcast.list[2].weather[0].description); //weather description
                lbl_temp_3.Text = string.Format(" {0} \u00B0C", forcast.list[2].temp.day); //weather temp
                lbl_wind_3.Text = string.Format(" {0} km/h", forcast.list[2].speed); //weather  wind speed



                //next 4th day
                lbl_day_4.Text = string.Format(" {0}", getDate(forcast.list[3].dt).DayOfWeek); //returning day
                lbl_cond_4.Text = string.Format(" {0}", forcast.list[3].weather[0].main); //weather condition
                lbl_des_4.Text = string.Format(" {0}", forcast.list[3].weather[0].description); //weather description
                lbl_temp_4.Text = string.Format(" {0} \u00B0C", forcast.list[3].temp.day); //weather temp
                lbl_wind_4.Text = string.Format(" {0} km/h", forcast.list[3].speed); //weather  wind speed

                //next 5th day
                lbl_day_5.Text = string.Format(" {0}", getDate(forcast.list[4].dt).DayOfWeek); //returning day
                lbl_cond_5.Text = string.Format(" {0}", forcast.list[4].weather[0].main); //weather condition
                lbl_des_5.Text = string.Format(" {0}", forcast.list[4].weather[0].description); //weather description
                lbl_temp_5.Text = string.Format(" {0} \u00B0C", forcast.list[4].temp.day); //weather temp
                lbl_wind_5.Text = string.Format(" {0} km/h", forcast.list[4].speed); //weather  wind speed

                /*   //next 6th day
                    lbl_day_6.Text = string.Format(" {0}", getDate(forcast.list[5].dt).DayOfWeek); //returning day
                    lbl_cond_6.Text = string.Format(" {0}", forcast.list[5].weather[0].main); //weather condition
                    lbl_des_6.Text = string.Format(" {0}", forcast.list[5].weather[0].description); //weather description
                    lbl_temp_6.Text = string.Format(" {0} \u00B0C", forcast.list[5].temp.day); //weather temp
                    lbl_wind_6.Text = string.Format(" {0} km/h", forcast.list[5].speed); //weather  wind speed

                    //next 7th day
                    lbl_day_7.Text = string.Format(" {0}", getDate(forcast.list[6].dt).DayOfWeek); //returning day
                    lbl_cond_7.Text = string.Format(" {0}", forcast.list[6].weather[0].main); //weather condition
                    lbl_des_7.Text = string.Format(" {0}", forcast.list[6].weather[0].description); //weather description
                    lbl_temp_7.Text = string.Format(" {0} \u00B0C", forcast.list[6].temp.day); //weather temp
                    lbl_wind_7.Text = string.Format(" {0} km/h", forcast.list[6].speed); //weather  wind speed


        */



                picture_main.Image = setIcon();
                


            }


        }

        // millisecound to DateTime methode
        DateTime getDate(double millisecound)
        {

            DateTime day = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(millisecound).ToLocalTime();

            return day;
        }


        
        Image setIcon()
        {
            string url = "http://openweathermap.org/img/w/{0}.png ";  //weather icon resource
            WebRequest request = WebRequest.Create(url);
            return NewMethod(request);

        }

        private static Image NewMethod(WebRequest request)
        {
            using (WebResponse response = request.GetResponse())
            using (System.IO.Stream weatherIcon = response.GetResponseStream())
            {
                Image weatherImg = Bitmap.FromStream(weatherIcon);
                return weatherImg;



            }
        }

        /*   Image setIcon(string iconID)
           {
               string url = string.Format("http://openweathermap.org/img/w/{0}.png", iconID); // weather icon resource 
               var request = WebRequest.Create(url);
               using (var response = request.GetResponse())
               using (var weatherIcon = response.GetResponseStream())
               {
                   Image weatherImg = Bitmap.FromStream(weatherIcon);
                   return weatherImg;
               }
           }

       */
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
