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

        /// <summary>
        /// 外部改变该窗口的DataContext时自动更新窗体内所有数据显示组件的DataContext
        /// 避免在外部做过多操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimpleStackPanel_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.NameShower.DataContext = this.DataContext;
            this.NationShower.DataContext = this.DataContext;
            // 尚未全部完成
        }
    }
}
