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
    /// 设置边框控件，命名空间为DocumentHelper
    /// </summary>
    public partial class SettingsBorder : Border
    {
        // SettingText 依赖属性
        public static readonly DependencyProperty SettingTextProperty =
            DependencyProperty.Register("SettingText", typeof(string), typeof(SettingsBorder),
                new PropertyMetadata("", OnSettingTextChanged));

        // IsChecked 依赖属性
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool?), typeof(SettingsBorder),
                new PropertyMetadata(false, OnIsCheckedChanged));

        /// <summary>
        /// 获取或设置SimpleText的文本内容
        /// </summary>
        public string SettingText
        {
            get { return (string)GetValue(SettingTextProperty); }
            set { SetValue(SettingTextProperty, value); }
        }

        /// <summary>
        /// 获取或设置CheckBox的选中状态
        /// </summary>
        public bool? IsChecked
        {
            get { return (bool?)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        /// <summary>
        /// 获取SimpleText控件引用
        /// </summary>
        public HandyControl.Controls.SimpleText TextControl => CustomText;

        /// <summary>
        /// 获取CheckBox控件引用
        /// </summary>
        public CheckBox CheckBoxControl => CustomCheckBox;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SettingsBorder()
        {
            InitializeComponent();
            DefaultStyleKey = typeof(Border);
        }

        /// <summary>
        /// CheckBox状态改变事件处理
        /// </summary>
        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            // 更新IsChecked属性
            SetCurrentValue(IsCheckedProperty, CustomCheckBox.IsChecked);

            // 触发IsCheckedChanged事件
            IsCheckedChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// SettingText属性变化时的回调方法
        /// </summary>
        private static void OnSettingTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SettingsBorder;
            if (control != null && e.NewValue is string newText)
            {
                // 确保CustomText控件的文本与SettingText属性同步
                if (control.CustomText != null)
                {
                    control.CustomText.Text = newText;
                }
            }
        }

        /// <summary>
        /// IsChecked属性变化时的回调方法
        /// </summary>
        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SettingsBorder;
            if (control != null && control.CustomCheckBox != null)
            {
                // 确保CustomCheckBox控件的选中状态与IsChecked属性同步
                control.CustomCheckBox.IsChecked = (bool?)e.NewValue;

                // 触发IsCheckedChanged事件
                // control.IsCheckedChanged?.Invoke(control, EventArgs.Empty);
            }
        }

        /// <summary>
        /// IsChecked属性改变时触发的事件
        /// </summary>
        public event EventHandler IsCheckedChanged;
    }
}