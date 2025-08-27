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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DocumentHelper
{
    /// <summary>
    /// StudentDataShower.xaml 的交互逻辑
    /// 用于在点按【启动服务】弹出的提示窗口中显示当前条目的详细数据
    /// </summary>
    public partial class StudentDataShower : UserControl
    {
        /// <summary>
        /// 定义依赖属性【DataHint】，绑定于SimpleText的Text属性
        /// </summary>
        public static readonly DependencyProperty DataHintProperty =
            DependencyProperty.Register("DataHint", typeof(string), typeof(StudentDataShower),
                new PropertyMetadata("提示："));

        // 创建CLR属性包装器
        public string DataHint
        {
            get { return (string)GetValue(DataHintProperty); }
            set { SetValue(DataHintProperty, value); }
        }

        /// <summary>
        /// 定义依赖属性【DataResource】，绑定于SimpleText的Text属性
        /// </summary>
        public static readonly DependencyProperty DataResourceProperty =
            DependencyProperty.Register("DataResource", typeof(string), typeof(StudentDataShower),
                new PropertyMetadata("提示："));

        // 创建CLR属性包装器
        public string DataResource
        {
            get { return (string)GetValue(DataResourceProperty); }
            set { SetValue(DataResourceProperty, value); }
        }

        public StudentDataShower()
        {
            InitializeComponent();
        }
    }
}
