using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;

namespace GenshinCharacterBrowserDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var client = new HttpClient();
            //var request = new HttpRequestMessage();
            //request.RequestUri = new Uri(
            //    "https://galaxias-api.lingxigames.com/ds/ajax/endpoint.json"
            //);
            //request.Method = HttpMethod.Post;

            //request.Headers.Add("Accept", "*/*");
            //request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

            //var bodyString =
            //    "{  \"api\": \"/api/l/owresource/getQueryDataInfoListByCategory\",  \"params\": {    \"gameId\": \"10000147\",    \"tbId\": \"452555206502894595\",    \"categoryIds\": \"454680400827016707\",    \"page\": 0,    \"size\": 50  }}";
            //var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            //request.Content = content;

            //var response = await client.SendAsync(request);
            //var result = await response.Content.ReadAsStringAsync();

            //var list = JObject.Parse(result)["result"]["items"];
            //var nameList = list.Select(s => s["priVal"]).ToList();

            //var iconUriList = list.Select(
            //        s => s["details"].First(s => s["key"].ToString() == "PCicon")["val"].ToString()
            //    )
            //    .ToList();
            //var protraitUriList = list.Select(
            //        s => s["details"].First(s => s["key"].ToString() == "PC首页")["val"].ToString()
            //    )
            //    .ToList();
            //for (int i = 0; i < protraitUriList.Count; i++)
            //{
            //    var icon = new Image() { Source = new BitmapImage(new Uri(iconUriList[i])) };
            //    var protrait = new Uri(protraitUriList[i]);
            //    icon.MouseDown += (s, e) =>
            //    {
            //        protraitPic.Source = new BitmapImage(protrait);
            //    };
            //    campListBox.Items.Add(icon);
            //}
        }

        private void charListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
    }
}
