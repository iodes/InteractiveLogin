using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFExtension;

namespace InteractiveLogin.Controls
{
    /// <summary>
    /// SimpleTextBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SimpleTextBox : UserControl
    {
        #region 속성
        public ImageSource Icon
        {
            get => this.GetValue<ImageSource>(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public string PlaceHolder
        {
            get => this.GetValue<string>(PlaceHolderProperty);
            set => SetValue(PlaceHolderProperty, value);
        }

        public bool IsPassword
        {
            get
            {
                return _IsPassword;
            }
            set
            {
                _IsPassword = value;

                if (IsPassword)
                {
                    textMain.Visibility = Visibility.Collapsed;
                    passMain.Visibility = Visibility.Visible;
                    passMain.IsEnabled = true;
                }
                else
                {
                    textMain.Visibility = Visibility.Visible;
                    passMain.Visibility = Visibility.Collapsed;
                    passMain.IsEnabled = false;
                }
            }
        }
        private bool _IsPassword = false;

        public string Text
        {
            get
            {
                if (IsPassword)
                {
                    return passMain.Password;
                }
                else
                {
                    return textMain.Text;
                }
            }
            set
            {
                if (IsPassword)
                {
                    passMain.Password = value;
                }
                else
                {
                    textMain.Text = value;
                }
            }
        }

        public TextBox MainBox => textMain;
        #endregion

        #region 의존성 속성
        public static readonly DependencyProperty IconProperty = DependencyHelper.Register();
        public static readonly DependencyProperty PlaceHolderProperty = DependencyHelper.Register();
        #endregion

        #region 내부 이벤트
        private void SimpleTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (Icon == null)
            {
                gridHolder.Margin = new Thickness(0);
            }
        }

        private void TextMain_TextChanged(object sender, TextChangedEventArgs e)
        {
            textHolder.Visibility = Visibility.Collapsed;
            TextChanged?.Invoke(textMain.Text);
        }

        private void PassMain_PasswordChanged(object sender, RoutedEventArgs e)
        {
            textHolder.Visibility = Visibility.Collapsed;
            TextChanged?.Invoke(passMain.Password);
        }

        private void SimpleTextBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsPassword)
            {
                passMain.Focus();
                textMain.Visibility = Visibility.Collapsed;
            }
            else
            {
                textMain.Focus();
                passMain.Visibility = Visibility.Collapsed;
            }
        }

        private void TextField_GotFocus(object sender, RoutedEventArgs e)
        {
            textHolder.Visibility = Visibility.Collapsed;
            borderLine.Opacity = 1.0;
        }

        private void TextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Text?.Length <= 0)
            {
                textHolder.Visibility = Visibility.Visible;
            }

            borderLine.Opacity = 0.8;
        }
        #endregion

        #region 사용자 이벤트
        public event TextChangedEventHandler TextChanged;
        public delegate void TextChangedEventHandler(string text);
        #endregion

        #region 생성자
        public SimpleTextBox()
        {
            InitializeComponent();
            DataContext = this;

            Loaded += SimpleTextBox_Loaded;
            textMain.GotFocus += TextField_GotFocus;
            textMain.LostFocus += TextField_LostFocus;
            passMain.GotFocus += TextField_GotFocus;
            passMain.LostFocus += TextField_LostFocus;
            textMain.TextChanged += TextMain_TextChanged;
            passMain.PasswordChanged += PassMain_PasswordChanged;
            PreviewMouseLeftButtonUp += SimpleTextBox_PreviewMouseLeftButtonUp;
        }
        #endregion
    }
}
