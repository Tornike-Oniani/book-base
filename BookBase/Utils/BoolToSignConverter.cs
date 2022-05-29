using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BookBase.Utils
{
    class BoolToSignConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return new PackIconBootstrapIcons() { Kind = PackIconBootstrapIconsKind.Dash };

            return new PackIconBootstrapIcons() { Kind = PackIconBootstrapIconsKind.Plus };
            //if ((bool)value)
            //    return "-";

            //return "+";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
