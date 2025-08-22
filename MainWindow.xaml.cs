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

namespace DocumentHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : HandyControl.Controls.Window
    {
        private static List<string> _nationList = new List<string> { "汉族",
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

        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void AddNewRowButton_Click(object sender, RoutedEventArgs e)
        {
            AddRowWindow AddNewRowWindow = new AddRowWindow();
            AddNewRowWindow.Owner = this;
            AddNewRowWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            bool ChildDialogResult = (bool)AddNewRowWindow.ShowDialog();
            if (ChildDialogResult)
            {
                StudentDataGrid.Items.Insert(StudentDataGrid.Items.Count, new Student()
                {
                    StudentName = AddNewRowWindow.StudentNameBox.Text,
                    StudentNation = "汉族",//AddNewRowWindow.StudentNameBox.Text,
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
    }
}