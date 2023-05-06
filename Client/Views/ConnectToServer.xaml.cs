using Client.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Client
{
    public partial class ConnectToServer : Window
    {
        public ConnectToServer()
        {
            InitializeComponent();
            this.DataContext = new ConnectToServerVM(this);
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch {; }
        }
    }
}
