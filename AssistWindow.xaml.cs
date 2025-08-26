using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public int SelectIndex;

        /// <summary>
        /// 命令
        /// </summary>
        public static RoutedUICommand LastPageCommand = new RoutedUICommand("上一页", "LastPage", typeof(AssistWindow));
        public static RoutedUICommand NextPageCommand = new RoutedUICommand("下一页", "NextPage", typeof(AssistWindow));

        public AssistWindow()
        {
            InitializeComponent();
        }

        private void ChangeSelectItem(int Index)
        {
            if (Index <= ((ObservableCollection<Student>)this.DataContext).Count && Index >= 0)
            {
                this.NameShower.DataContext = ((ObservableCollection<Student>)this.DataContext)[SelectIndex];
                this.NationShower.DataContext = ((ObservableCollection<Student>)this.DataContext)[SelectIndex];
            }
        }

        /// <summary>
        /// 外部改变该窗口的DataContext时自动更新窗体内所有数据显示组件的DataContext
        /// 避免在外部做过多操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimpleStackPanel_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // 尚未全部完成
            this.ChangeSelectItem(SelectIndex);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = HandyControl.Controls.MessageBox.Show("是否确认要停止服务并返回主窗口？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void LastPageCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.DataContext != null && SelectIndex != null && SelectIndex > 0 )
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
            e.Handled = true;
        }

        private void LastPageCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ChangeSelectItem(--SelectIndex);
        }

        private void NextPageCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if( this.DataContext != null && SelectIndex != null && SelectIndex < ((ObservableCollection<Student>)this.DataContext).Count - 1 )
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute= false;
            }
            e.Handled = true;
        }

        private void NextPageCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ChangeSelectItem(++SelectIndex);
        }
    }
}
