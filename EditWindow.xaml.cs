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
    /// EditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditWindow : HandyControl.Controls.Window
    {

        public static RoutedUICommand CommitCommand = new RoutedUICommand("提交数据", "CommitCommand", typeof(EditWindow));

        public EditWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void CommitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void CommitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
