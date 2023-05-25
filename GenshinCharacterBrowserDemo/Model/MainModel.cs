using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GenshinCharacterBrowserDemo.Model;

public enum ECamp
{
    吴,
    蜀,
    魏,
    群
}

public class CCamp
{
    public static Dictionary<ECamp, long> CampIDs = new Dictionary<ECamp, long>();

    public CCamp(ECamp camp, List<CRole> roles, bool defaultSelected)
    {
        Camp = camp;
        Roles = roles;
        DefaultSelected = defaultSelected;
    }

    static CCamp()
    {
        CampIDs.Add(ECamp.吴, 454680400827016707);
        CampIDs.Add(ECamp.蜀, 454680400827016706);
        CampIDs.Add(ECamp.魏, 454680400827016705);
        CampIDs.Add(ECamp.群, 454680400827016708);
    }

    public ECamp Camp { get; set; }
    public List<CRole> Roles { get; set; }
    public bool DefaultSelected { get; set; }
}

public class CRole
{
    public CRole(string name, string icon, string protrait, string description)
    {
        Name = name;
        Icon = new BitmapImage(new Uri(icon));
        Protrait = new BitmapImage(new Uri(protrait));
        Description = description;
    }

    public string Name { get; set; }

    public BitmapImage Icon { get; set; }
    public BitmapImage Protrait { get; set; }
    public string Description { get; set; }
}

[ValueConversion(typeof(ECamp), typeof(string))]
public class ECampToString : IValueConverter
{
    public object Convert(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        return ((ECamp)value).ToString();
    }

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        return Enum.Parse(typeof(ECamp), (string)value);
    }
}
