using L2DLib.Utility;
using System.Windows;
using System.Windows.Media;

namespace InteractiveLogin
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string username = "root@admin.com";
        private const string password = "magical";

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            renderView.Model = L2DFunctions.LoadModel(@"shizuku\shizuku.model.json");
            renderView.Model.UseBreath = true;
            renderView.Model.UseEyeBlink = true;
            renderView.Target = textID.MainBox;
        }

        private void BtnSignin_Click(object sender, RoutedEventArgs e)
        {
            if (textID.Text == username && textPW.Text == password)
            {
                labelStatus.Content = "Login Success";
                borderTop.Background = FindResource("Control.Success") as Brush;
                renderView.ShowSuccess();
            }
            else
            {
                labelStatus.Content = "Authentication Error";
                borderTop.Background = FindResource("Control.Error") as Brush;
                renderView.ShowFail();
            }
        }
    }
}
