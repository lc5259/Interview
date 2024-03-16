using demo3.Models;
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
    public class IsShowEditButton : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                //true.编辑模式。Edit 按钮不展示出来
                if ((bool)value == true)
                {

                    return Visibility.Collapsed;
                }
                else  //退出编辑模式.Edit按钮显示
                    return Visibility.Visible;
            }
            return Visibility.Collapsed;

            //if (value is UserInfo userInfo && parameter is UserInfo selectedUserInfo)
            //{
            //    return userInfo == selectedUserInfo ? Visibility.Visible : Visibility.Collapsed;
            //}
            //return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
