using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace demo3.Common.Converts
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                //true.编辑模式。Save 按钮展示出来
                if ((bool)value == true)
                {
                    
                    return Visibility.Visible;
                }
                else  //退出编辑模式.save按钮折叠
                    return Visibility.Collapsed;
            }
            return "空";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
