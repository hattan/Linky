using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Linky.Common
{
    public class GreaterThanZeroToVisibilityConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool visible = false;
            try
            {
                int intValue = int.Parse(value.ToString());
                visible = (intValue == 0);

                if (parameter != null)
                {
                    if (parameter.ToString() == "visibleWithItems")
                    {
                        visible = (intValue > 0);
                    }
                }
            }
            catch (Exception)
            {
                visible = false;
            }

            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
