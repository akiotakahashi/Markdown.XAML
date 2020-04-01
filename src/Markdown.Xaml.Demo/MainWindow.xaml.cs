﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Markdown.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CommandBindings.Add(new CommandBinding(NavigationCommands.GoToPage, (sender, e) => Process.Start((string)e.Parameter)));

            this.DataContext = new DemoViewModel();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            var sampleMarkdown = LoadSample();
            demoSource.Text = sampleMarkdown;
            editSource.Text = sampleMarkdown;
            editSource2.Text = sampleMarkdown;
        }

        private string LoadSample()
        {
            var subjectType = GetType();
            var subjectAssembly = subjectType.Assembly;
            using (Stream stream = subjectAssembly.GetManifestResourceStream(subjectType.FullName + ".md"))
            {
                if (stream is null)
                {
                    return string.Format(
                        CultureInfo.InvariantCulture,
                        "Could not find sample text *{0}*.md", 
                        subjectType.FullName);
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Manual preview markdown.
        /// </summary>
        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            ((DemoViewModel)this.DataContext).TextPreview = ((DemoViewModel)this.DataContext).TextMarkdown;
        }
    }
}
