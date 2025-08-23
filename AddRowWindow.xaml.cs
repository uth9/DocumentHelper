using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HandyControl;
using System.Text.RegularExpressions;

namespace DocumentHelper
{
    /// <summary>
    /// AddRowWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    public partial class AddRowWindow : HandyControl.Controls.Window
    {
        public bool IfSaveBeforeExit = false;
        public bool DataSaved = false;
        public bool DataCanceled = false;
        public AddRowWindow()
        {
            InitializeComponent();
            IfSaveBeforeExit = false;
            DataSaved = false;
            DataCanceled = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!DataSaved && !DataCanceled)
            {
                MessageBoxResult result = HandyControl.Controls.MessageBox.Show("该条目尚未保存，是否在退出前保存？", "警告", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        IfSaveBeforeExit = true;
                        DialogResult = true;
                        e.Cancel = false;
                        break;
                    case MessageBoxResult.No:
                        IfSaveBeforeExit = false;
                        DialogResult = false;
                        e.Cancel = false;
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            else
            {
                DialogResult = true;
                e.Cancel = false;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DataCanceled = true;
            this.Close();
        }

        private void CommitButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> errorData = [];
            bool isMatchTelephoneNumber = Regex.IsMatch(TelBox.Text, @"^1[3-9]\d{9}$");
            if (!isMatchTelephoneNumber) errorData.Add("身份证号");
            bool isMatchIdNumber = Regex.IsMatch(PinBox.Text, @"^[1-9]\d{5}(18|19|20)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{3}(\d|X|x)$") && PinBox.Text.Length == 18;
            if (!isMatchIdNumber) errorData.Add("手机号");
            if (true)
            {
                if ( isMatchIdNumber && isMatchTelephoneNumber )
                {
                    DataSaved = true;
                    this.Close();
                }
                else
                {
                    string _ = String.Join("，", errorData);
                    HandyControl.Controls.MessageBox.Show($"请输入符合格式的数据\n出错的数据包括：{_}", "错误",  MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }
    }
}
