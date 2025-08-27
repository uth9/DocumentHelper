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

        public static RoutedUICommand CommitCommand = new RoutedUICommand("提交数据", "CommitCommand", typeof(AddRowWindow));

        public enum ExitResults { None, Saved, Canceled };
        public ExitResults ExitResult = ExitResults.None;

        public AddRowWindow()
        {
            InitializeComponent();
        }

        private bool DataChecker()
        {
            /* 此处填写数据校验逻辑 */
            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ExitResult == ExitResults.None)
            {
                MessageBoxResult result = HandyControl.Controls.MessageBox.Show("该条目尚未保存，是否在退出前保存？", "警告", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (DataChecker())
                        {
                            DialogResult = true;
                            e.Cancel = false;
                        } else
                        {
                            HandyControl.Controls.MessageBox.Show("出现数据错误，请修改后重试", "错误", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                            DialogResult = false;
                            e.Cancel = true;
                        }
                        break;
                    case MessageBoxResult.No:
                        DialogResult = false;
                        e.Cancel = false;
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            } else if (ExitResult == ExitResults.Saved)
            {
                DialogResult = true;
                e.Cancel = false;
            } else if (ExitResult == ExitResults.Canceled)
            {
                DialogResult = false;
                e.Cancel = false;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ExitResult = ExitResults.Canceled;
            this.Close();
        }


        // 以下函数将修改为命令形式，理论上不应该再次被引用

        /// <summary>
        /// 数据校验逻辑暂未完成编写
        /// 暂定逻辑如下：
        ///     【姓名】不得少于两个字或多于五个字
        ///     【民族】不得为空
        ///     【身份证号】不得为空且符合对应的RegEx
        ///     两次【身份证号】需一致
        ///     【入团时间】不得为空
        ///     【发展编号】不得为空，长度为8位？12位？
        ///     【手机号码】不得为空且符合对应RegEx
        ///     【户籍地址】不得为空
        ///     若【邮箱】选择填写则不得为空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void CommitButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> errorData = [];
            bool isMatchTelephoneNumber = Regex.IsMatch(TelBox.Text, @"^1[3-9]\d{9}$");
            if (!isMatchTelephoneNumber) errorData.Add("手机号码");
            bool isMatchIdNumber = Regex.IsMatch(PinBox.Text, @"^[1-9]\d{5}(18|19|20)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{3}(\d|X|x)$") &&
                PinBox.Text.Length == 18 &&
                PinBox.Text.ToLower() == ReconfirmedPinBox.Text.ToLower();
            if (!isMatchIdNumber) errorData.Add("身份证号");

            ExitResult = ExitResults.Saved;
            this.Close();
            if (false) // 测试暂时关闭数据校验
            {
                if (this.PinBox.Text.Length == 18 &&
                    this.PinBox.Text == this.ReconfirmedPinBox.Text &&
                    Regex.IsMatch(this.TelBox.Text, @"^1[3-9]\d{9}$") &&
                    Regex.IsMatch(this.PinBox.Text, @"^[1-9]\d{5}(18|19|20)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{3}(\d|X|x)$") 

                    )
                {
                    ExitResult = ExitResults.Saved;
                    this.Close();
                }
                else
                {
                    HandyControl.Controls.MessageBox.Show("请输入符合格式的数据", "错误",  MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        private void CommitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (DataChecker())
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
            e.Handled = true;
            return;
        }

        private void CommitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ExitResult = ExitResults.Saved;
            this.Close();
        }
    }
}
