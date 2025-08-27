﻿using System.Text;
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
using System.Runtime.CompilerServices;

namespace DocumentHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 主程序
    /// </summary>


    public partial class MainWindow : HandyControl.Controls.Window
    {
        /// <summary>
        /// 屏幕数据
        /// </summary>
        private static double ScreenWidth = SystemParameters.WorkArea.Width;
        private static double ScreenHeight = SystemParameters.WorkArea.Height;

        /// <summary>
        /// 命令
        /// </summary>
        public static RoutedUICommand StartServiceCommand = new RoutedUICommand("启动服务", "StartService", typeof(MainWindow));
        public static RoutedUICommand DeleteRowCommand = new RoutedUICommand("删除条目", "Delete", typeof(MainWindow));
        public static RoutedUICommand SaveToCommand = new RoutedUICommand("保存到", "SaveTo", typeof(MainWindow));


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
        public string[] _YearList = ["2025", "2026"];

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
            int SelectIndex = this.StudentDataGrid.SelectedIndex;
            if (SelectIndex != -1)
            {
                EditWindow EditRowWindow = new EditWindow();
                EditRowWindow.Owner = this;
                EditRowWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                Student OldStudentData = this.StudentCollection[SelectIndex];
                Student NewStudentData = new Student()
                {
                    StudentName = OldStudentData.StudentName,
                    StudentNation = OldStudentData.StudentNation,
                    Pin = OldStudentData.Pin,
                    RegMonth = OldStudentData.RegMonth,
                    RegYear = OldStudentData.RegYear,
                    MemberId = OldStudentData.MemberId,
                    Tel = OldStudentData.Tel,
                    Address = OldStudentData.Address,
                    Mail = OldStudentData.Mail,
                    MailState = OldStudentData.MailState,
                    VolunteerState = OldStudentData.VolunteerState,
                };
                EditRowWindow.DataContext = NewStudentData;
                EditRowWindow.NationBox.ItemsSource = this._NationList;
                EditRowWindow.ShowDialog();
                this.StudentCollection[SelectIndex] = (Student)EditRowWindow.DataContext;
            }
            else
            {
                HandyControl.Controls.MessageBox.Show("索引出现错误，请重新尝试或联系开发者", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            bool ChildDialogResult = AddNewRowWindow.ShowDialog() switch { true => true, false => false, null => false };
            if (ChildDialogResult) // 如果退出前选择保存数据
            {
                this.StudentCollection.Add( new Student()
                {
                    StudentName = AddNewRowWindow.StudentNameBox.Text,
                    StudentNation = AddNewRowWindow.NationBox.SelectedItem switch { null => "", _ => AddNewRowWindow.NationBox.SelectedItem.ToString()}, //测试用数据
                    Pin = AddNewRowWindow.PinBox.Text,
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
        private void StartServiceButton_Click()
        {
            AssistWindow ServiceWindow = new AssistWindow();
            ServiceWindow.Owner = this;
            ServiceWindow.SelectIndex = this.StudentDataGrid.SelectedIndex switch { < 0 => 0, >= 0 => this.StudentDataGrid.SelectedIndex };
            ServiceWindow.DataContext = this.StudentCollection;
            ServiceWindow.Left = ScreenWidth - ServiceWindow.Width - 20;
            ServiceWindow.Top = 20;
            ServiceWindow.Hide();
            ServiceWindow.ShowDialog();
            ServiceWindow.Show();
        }

        private void StartService_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.StudentCollection.Count != 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
            e.Handled = true;
        }

        private void StartService_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.StartServiceButton_Click();
        }

        private void DeleteRowCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.StudentCollection.Count != 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
            e.Handled = true;
        }

        private void DeleteRowCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult result = HandyControl.Controls.MessageBox.Show("是否确认删除选中项？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes && this.StudentDataGrid.SelectedIndex != -1)
            {
                this.StudentCollection.RemoveAt(this.StudentDataGrid.SelectedIndex);
            }
        }
    }
}