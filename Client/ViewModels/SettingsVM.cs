using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client.ViewModels
{
    internal class SettingsVM
    {
        private Frame _frame;
        private Socket _socket;

        public RelayCommand ChangeThemeCommand { get; set; }
        public RelayCommand GoBackCommand { get; set; }

        public SettingsVM(Frame frame, Socket socket)
        {
            _frame = frame;
            _socket = socket;

            ChangeThemeCommand = new RelayCommand(o => ChangeTheme(o as string));
            GoBackCommand = new RelayCommand(o => GoBack());
        }

        private async void ChangeTheme(string? theme)
        {
            await Task.Run(() =>
            {
                if (theme == null)
                    return;

                Theme.ChangeTheme(theme);
            });
        }

        private void GoBack()
        {
            _frame.GoBack();
        }
    }
}
