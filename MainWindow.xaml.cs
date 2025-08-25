using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HandyControl;
using System.Collections;
using System.Collections.ObjectModel;

namespace DocumentHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 主程序
    /// </summary>


    public partial class MainWindow : HandyControl.Controls.Window
    {
        /// <summary>
        /// 数据视图的数据源，存储学生数据
        /// </summary>
        public ObservableCollection<Student> StudentCollection { get; set; } = new ObservableCollection<Student>();

        /// <summary>
        /// 存储民族列表
        /// </summary>
        public List<string> _NationList = new List<string> { "汉族",
            "壮族",
            "回族",
            "满族",
            "维吾尔族",
            "苗族",
            "彝族",
            "土家族",
            "藏族",
            "蒙古族",
            "侗族",
            "布依族",
            "瑶族",
            "白族",
            "朝鲜族",
            "哈尼族",
            "黎族",
            "哈萨克族",
            "傣族",
            "畲族",
            "傈僳族",
            "东乡族",
            "仡佬族",
            "拉祜族",
            "水族",
            "佤族",
            "纳西族",
            "羌族",
            "土族",
            "仫佬族",
            "锡伯族",
            "柯尔克孜族",
            "景颇族",
            "达斡尔族",
            "撒拉族",
            "布朗族",
            "毛南族",
            "塔吉克族",
            "普米族",
            "阿昌族",
            "怒族",
            "鄂温克族",
            "京族",
            "基诺族",
            "德昂族",
            "保安族",
            "俄罗斯族",
            "裕固族",
            "乌孜别克族",
            "门巴族",
            "鄂伦春族",
            "独龙族",
            "赫哲族",
            "高山族",
            "珞巴族",
            "塔塔尔族" };

        // 日期列表
        public string[] _YearList = ["2025", "2026"]

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

        }

        /// <summary>
        /// 在数据视图上双击，开启数据修改页面
        /// TODO: 待完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// 【新建】按钮单击，开启新建页面
        /// 需求：
        ///     若档案条数不为零且设置中开启【发展编号自动递增】（暂时用true代替因为设置还没写）
        ///     则点击时获取最后一条档案的MemberId自动加1后填入新建条目窗口的发展编号框
        ///     
        ///     入团时间默认为设置中的【DefaultRegDate】（还没写，用2025/1代替）
        ///     
        ///     【是否注册志愿者】的默认IsChecked设置为设置中的【DefaultVolunteerState】（还没写，用true吧）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewRowButton_Click(object sender, RoutedEventArgs e)
        {
            AddRowWindow AddNewRowWindow = new AddRowWindow();
            AddNewRowWindow.Owner = this;
            AddNewRowWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            AddNewRowWindow.DataContext = this;
            AddNewRowWindow.NationBox.ItemsSource = _NationList; // 设置下拉列表默认选中项
            AddNewRowWindow.NationBox.SelectedIndex = 0;
            bool ChildDialogResult = (bool)AddNewRowWindow.ShowDialog();
            if (ChildDialogResult) // 如果退出前选择保存数据
            {
                this.StudentCollection.Add( new Student()
                {
                    StudentName = AddNewRowWindow.StudentNameBox.Text,
                    StudentNation = "汉族",//AddNewRowWindow.StudentNameBox.Text, //测试用数据
                    Pin = AddNewRowWindow.PinBox.Text,
                    ReconfirmedPin = AddNewRowWindow.ReconfirmedPinBox.Text,
                    MemberId = AddNewRowWindow.MemberIdBox.Text,
                    RegDate = "2025/01",//string.Concat(AddNewRowWindow.RegYearBox.Text, "/", AddNewRowWindow.RegMonthBox.Text),
                    Tel = AddNewRowWindow.TelBox.Text,
                    Address = AddNewRowWindow.AddressBox.Text,
                    VolunteerState = (bool)AddNewRowWindow.VolunteerBox.IsChecked,
                });
            }
        }

        /// <summary>
        /// 点按【设置】，开启设置界面
        /// TODO: 设置项暂未完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            CustomSettings SettingWindow = new CustomSettings();
            SettingWindow.Owner = this;
            SettingWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SettingWindow.ShowDialog();
        }

        /// <summary>
        /// 点按【启动服务】，启动悬浮窗
        /// 预期效果：
        ///     保持前台，不显示标题栏及关闭按钮（NonClientArea），不可改变大小，不可拖动
        ///     显示启动时选中条目的详细信息以及上一条和下一条的姓名
        ///     下方显示【上一条】【下一条】【退出】三个按钮
        ///     右上角显示【帮助】按钮，按下弹窗提示用法及注意事项
        ///     左上角显示当前注册的热键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartServiceButton_Click(object sender, RoutedEventArgs e)
        {
            AssistWindow ServiceWindow = new AssistWindow();
            ServiceWindow.Owner = this;
            ServiceWindow.DataContext = this.StudentDataGrid.Items.Count switch
            {
                >0 => this.StudentDataGrid.Items[0], // 占位逻辑，后期修改
                _ => null,
            };
            ServiceWindow.ShowDialog();
        }
    }
}