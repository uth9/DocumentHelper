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
            else { e.Cancel = false; }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DataCanceled = true;
            this.Close();
        }

        private void CommitButton_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                //TODO: 添加数据验证
                DataSaved = true;
                this.Close();
            }
        }
    }
}
