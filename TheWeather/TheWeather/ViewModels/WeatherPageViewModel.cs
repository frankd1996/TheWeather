using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheWeather.Models;
using Xamarin.Forms;

namespace TheWeather.ViewModels
{
    public class WeatherPageViewModel :Page, INotifyPropertyChanged
    {        
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private WeatherData data;
        public WeatherData Data
        {
            get => data; 
            set
            {
                data = value;
                OnPropertyChanged();
            }
        }
        //Nos permitirá llevar a cabo la solicitud del servicio REST
        public ICommand SearchCommand { get; set; }
        public Page page;

        public WeatherPageViewModel()
        {
            //searchTerm es el parámetro enviado desde el xaml del view
            //a través del atributo SearchCommandParameter, el cual manda
            //el texto del propio SearchBar.
            SearchCommand = new Command(async (searchTerm) =>
            {
                try
                {
                    //x,y . Donde x es latitud e y es longitud
                    var entrada = searchTerm as string;
                    var datos = entrada.Split(',');
                    var lat = datos[0];
                    var lon = datos[1];
                    //el url ingresado ya se sabe que es válido (se obtuvo directamente de la página)
                    //con latitud y longitud de la ciudad de México, así que el usuario no debe ingresar
                    //latitud ni longitud
                    //url ciudad de mexico en ingles https://api.weatherbit.io/v2.0/current?lat=19.431&lon=-99.132&key=f985837c99cd44e28f7b40f37d501474
                    //la url usada permite, a través de uso de llave, usar la latitud y longitud
                    //ingresadas por el usuario, y al final del url se añadió el idioma español
                    await GetData($"https://api.weatherbit.io/v2.0/current?lat={lat}&lon={lon}&key=f985837c99cd44e28f7b40f37d501474&lang=es");
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error"
                        , "Ocurrió un error en la consulta. " +
                        "Por favor, verifique que las coordenadas ingresadas sean correctas"
                        , "Ok");
                }
            });
        }
       
		 private async Task GetData(string url)
         {
            //Esta clase nos va a permitir ejecutar solicitudes 
            // a algún servicio o sitio web (servidor)
            var client = new HttpClient();

            //Operación asíncrona que ejecuta una petición asíncrona al servidor
            //a través de un url. GetAsync devuelve una clase HttpResponseMessage
            var response = await client.GetAsync(url);

            //lanza un excepción si ocurre un problema con la petición al servidor
            response.EnsureSuccessStatusCode();

            //si la solicitud es correcta, el servidor nos devolverá un archivo Json
            //como respuesta, el cual debemos descerializar 

            //realizamos una lectura del archivo JSON que nos devuelve el servidor
            var jsonResult = await response.Content.ReadAsStringAsync();

            //Ahora llevamos la conversión del archivo Json leído a objetos .NET.
            //a esto se llama descerealización. Para esto debemos instalar el 
            //paquete de nuget NewtonSoft
            var result = JsonConvert.DeserializeObject<WeatherData>(jsonResult);

            //Guaradamos el resultado en campo Data
            Data = result;
         }
    }    
}
