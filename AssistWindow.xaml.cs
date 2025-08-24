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
    /// AssistWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AssistWindow : HandyControl.Controls.Window
    {
        public AssistWindow()
        {
            InitializeComponent();
        }

        private void SimpleStackPanel_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.NameShower.DataContext = this.DataContext;
            this.NationShower.DataContext = this.DataContext;
        }
    }
}
