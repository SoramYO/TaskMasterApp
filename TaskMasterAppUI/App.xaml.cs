using System.Windows;
using Forms = System.Windows.Forms;
namespace TaskMasterAppUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Forms.NotifyIcon _notifyIcon;
        public App()
        {
            _notifyIcon = new Forms.NotifyIcon();
        }
        protected override void OnStartup(StartupEventArgs e)
        {


            //Forms.NotifyIcon notifyIcon = new Forms.NotifyIcon();
            //_notifyIcon.Icon = new System.Drawing.Icon("Images/logo.ico");
            //_notifyIcon.Text = "Task Master";
            //_notifyIcon.Click += NotifyIcon_Click;
            //_notifyIcon.Visible = true;

            //_notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
            //_notifyIcon.ContextMenuStrip.Items.Add("Open", null, NotifyIcon_Click);

            base.OnStartup(e);
        }
        public void NotifyIcon_Click(object sender, System.EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Activate();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }

    }

}
