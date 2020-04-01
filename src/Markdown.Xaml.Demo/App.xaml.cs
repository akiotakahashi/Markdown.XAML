using System;
using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace Markdown.Demo
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoadStyles();
        }

        private void LoadStyles()
        {
            Resources.MergedDictionaries.Add((ResourceDictionary)Application.Current.Resources["styles1"]);
        }
    }
}
