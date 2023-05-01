using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    internal class SettingsVM
    {
        public RelayCommand ChangeThemeCommand { get; set; }

        public SettingsVM()
        {
            ChangeThemeCommand = new RelayCommand(o => ChangeTheme(o as string));
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
    }
}
