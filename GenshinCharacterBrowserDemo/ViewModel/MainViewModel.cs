using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GenshinCharacterBrowserDemo.Model;
using Newtonsoft.Json.Linq;

namespace GenshinCharacterBrowserDemo.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<CCamp> camps = new ObservableCollection<CCamp>();

    [ObservableProperty]
    BitmapImage currentProtrait = new BitmapImage(
        new Uri("https://uploadstatic.mihoyo.com/contentweb/20200211/2020021114221470532.jpg")
    );

    [ObservableProperty]
    string currentProtraitName = "琴";

    [ObservableProperty]
    BitmapImage protraitImage = new BitmapImage();

    [RelayCommand]
    void SelectCamp(CCamp obj)
    {
        CurrentProtrait = obj.Roles[0].Protrait;
        CurrentProtraitName = obj.Roles[0].Name;
        ProtraitImage = CurrentProtrait;
    }

    [RelayCommand]
    void SelectProtrait(CRole obj)
    {
        CurrentProtrait = obj.Protrait;
        CurrentProtraitName = obj.Name;
        ProtraitImage = CurrentProtrait;
    }

    [RelayCommand]
    void SelectProtraitaaa()
    {
        CurrentProtrait = camps[0].Roles[2].Protrait;

        ProtraitImage = CurrentProtrait;
    }

    public MainViewModel()
    {
        LoadInfo();
    }

    async Task LoadInfo()
    {
        var client = new HttpClient();

        for (int i = 0; i < CCamp.CampIDs.Count; i++)
        {
            var bodyString =
                "{  \"api\": \"/api/l/owresource/getQueryDataInfoListByCategory\",  \"params\": {    \"gameId\": \"10000147\",    \"tbId\": \"452555206502894595\",    \"categoryIds\": \""
                + CCamp.CampIDs.ElementAt(i).Value.ToString()
                + "\",    \"page\": 0,    \"size\": 50  }}";
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(
                "https://galaxias-api.lingxigames.com/ds/ajax/endpoint.json"
            );
            request.Method = HttpMethod.Post;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add(
                "User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36 Edg/113.0.1774.42"
            );

            var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            var list = JObject.Parse(result)["result"]["items"];
            var nameList = list.Select(s => s["priVal"].ToString()).ToList();

            var iconUriList = list.Select(
                    s => s["details"].First(s => s["key"].ToString() == "PCicon")["val"].ToString()
                )
                .ToList();
            var protraitUriList = list.Select(
                    s => s["details"].First(s => s["key"].ToString() == "PC首页")["val"].ToString()
                )
                .ToList();

            var roles = new List<CRole>();
            for (int j = 0; j < protraitUriList.Count; j++)
            {
                var role = new CRole(nameList[j], iconUriList[j], protraitUriList[j], "描述");
                roles.Add(role);
                if (j == 0 && i == 0)
                {
                    //初始化第一次启动时默认显示第一个
                    CurrentProtrait = new BitmapImage(new Uri(protraitUriList[j]));
                    CurrentProtraitName = role.Name;
                    ProtraitImage = CurrentProtrait;
                }
                //var icon = new Image() { Source = new BitmapImage(new Uri(iconUriList[i])) };
                //var protrait = new Uri(protraitUriList[i]);
            }
            if (i == 0)
            {
                Camps.Add(new CCamp(CCamp.CampIDs.ElementAt(i).Key, roles, true));
            }
            else
            {
                Camps.Add(new CCamp(CCamp.CampIDs.ElementAt(i).Key, roles, false));
            }
        }

        Trace.WriteLine("");
    }
}

public class ImageToGrayImage : IValueConverter
{
    public object Convert(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        BitmapImage image = value as BitmapImage;
        if (image != null)
        {
            FormatConvertedBitmap grayBitmapSource = new FormatConvertedBitmap();
            grayBitmapSource.BeginInit();
            grayBitmapSource.Source = image;
            grayBitmapSource.DestinationFormat = PixelFormats.Gray8;
            grayBitmapSource.EndInit();
            return grayBitmapSource;
        }
        return value;
    }

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        throw new NotImplementedException();
    }
}
