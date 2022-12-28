﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp_TextEdit
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Top = Properties.Settings.Default.Position.Top;
            this.Left = Properties.Settings.Default.Position.Left;
            this.Height = Properties.Settings.Default.Position.Height;
            this.Width = Properties.Settings.Default.Position.Width;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fontName = ((sender as ComboBox).SelectedItem as string);
            if (textBox != null)
            {
                textBox.FontFamily = new FontFamily(fontName);
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (textBox != null)
            {
                double fontSize = Convert.ToDouble((sender as ComboBox).SelectedItem);
                textBox.FontSize = fontSize;
            }
        }

        private void ButtonBold_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.FontWeight == FontWeights.Normal)
            {
                textBox.FontWeight = FontWeights.Bold;
                //double thickness = Convert.ToDouble((sender as Button).BorderThickness);
                //(sender as Button).BorderThickness = new Thickness(5,5,5,5);
                //(sender as Button).BorderBrush = new Brush(Brush.OpacityProperty);
            }
            else
                textBox.FontWeight = FontWeights.Normal;
        }

        private void ButtonItalic_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.FontStyle == FontStyles.Normal)
            {
                textBox.FontStyle = FontStyles.Italic;
            }
            else textBox.FontStyle = FontStyles.Normal;
        }

        private void ButtonUnderline_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.TextDecorations.Count == 0)
                textBox.TextDecorations.Add(TextDecorations.Underline);
            else
            {
                textBox.TextDecorations.RemoveAt(0);
            }

            //if (textBox.TextDecorations == null)
            //    textBox.TextDecorations = TextDecorations.Underline;
            //else
            //{
            //    textBox.TextDecorations = null;
            //}
            ////Работает не стабильно. логика переворачивается и в итоге кнопка подсвечена, а подчеркивания нет
        }

        private void RadioButtonBlack_Checked(object sender, RoutedEventArgs e)
        {
            if (textBox != null)
            {
                textBox.Foreground = Brushes.Black;
            }
        }

        private void RadioButtonRed_Checked(object sender, RoutedEventArgs e)
        {
            if (textBox != null)
            {
                textBox.Foreground = Brushes.Red;
            }

        }

        private void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы(*.txt)|*.txt|Все файлы(*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void Save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы(*.txt)|*.txt|Все файлы(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, textBox.Text);
            }
        }

        private void Info()
        {
            MessageBox.Show("Info");
        }

        private void Exit()
        {
            Properties.Settings.Default.Position = this.RestoreBounds;
            Properties.Settings.Default.Save();
            Application.Current.Shutdown();
        }

        private void ClicOpen(object sender, ExecutedRoutedEventArgs e)
        {
            Open();
        }

        private void ClicSave(object sender, ExecutedRoutedEventArgs e)
        {
            Save();
        }

        private void ClicClose(object sender, ExecutedRoutedEventArgs e)
        {
            Exit();
        }

        private void ClicInfo(object sender, ExecutedRoutedEventArgs e)
        {
            Info();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Exit();  
        }

        private void themes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Uri uriTheme = new Uri(themes.SelectedIndex==0? "LightTeme.xaml": "DarkTeme.xaml",UriKind.Relative);
            ResourceDictionary themeDict = Application.LoadComponent(uriTheme) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(themeDict);

        }
    }
}
