using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Client
{
    internal static class Theme
    {
        public static void ChangeTheme(string theme)
        {
            Dictionary<string, string> themes = new Dictionary<string, string>
            {
                { "Light", @"Resources\Themes\LightTheme.xaml" },
                { "Dark", @"Resources\Themes\DarkTheme.xaml" }
            };


            var uri = new Uri(themes[theme], UriKind.Relative);

            ResourceDictionary? resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);

            Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Theme\RealTime-Chat").SetValue("Theme", theme);
        }
    }
}
