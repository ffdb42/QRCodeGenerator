using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace QRCodeGenerator
{
    public partial class MainWindow : Window
    {
        QRViewModel qrVM = new QRViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = qrVM;
        }
        
        private void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!qrVM.DataTypeValidation())
                MessageBox.Show("Введенные данные не соответствуют заданному типу.", "Данные не соответствуют типу", MessageBoxButton.OK);
            else if (qrVM.GenerateQRCode(this.TimeCheck.IsChecked.HasValue? this.TimeCheck.IsChecked.Value: false, this.DateCheck.IsChecked.HasValue ? this.DateCheck.IsChecked.Value : false) == -1)
                MessageBox.Show("Введенная версия не может содержать введенное количество информации.", "Версия была изменена.", MessageBoxButton.OK);
        }

        private void VersionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.GenerateButton.IsEnabled = VersionRule.isValid;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!qrVM.SaveImage(DateTime.Now.ToString("ssmmHHdMMyy")+".png"))
                MessageBox.Show("Необходимо сгенерировать изображение.", "Отсутствует изображение.", MessageBoxButton.OK);
        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            string path=string.Empty;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG|*.png|JPEG|*.jpeg";
            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
            }
            if (!qrVM.SaveImage(path))
                MessageBox.Show("Необходимо сгенерировать изображение.", "Отсутствует изображение.", MessageBoxButton.OK);
        }

        private void EncodementTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.DataTextBox.Text = string.Empty;
            this.DateCheck.IsChecked = false;
            this.TimeCheck.IsChecked = false;
            if (this.EncodementTypeComboBox.SelectedIndex == 0)
            {
                this.DateCheck.IsChecked = false;
                this.DateCheck.IsEnabled = false;
                this.TimeCheck.IsChecked = false;
                this.TimeCheck.IsEnabled = false;
            }
            else
            {
                this.DateCheck.IsEnabled = true;
                this.TimeCheck.IsEnabled = true;
            }
        }

        private void ECCComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.DataTextBox.Text = string.Empty;
        }

        private void ReadFromFileButtom_Click(object sender, RoutedEventArgs e)
        {
            this.DataTextBox.Text = string.Empty;
            string path="";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые документы (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
            }
            this.DataTextBox.Text = qrVM.ReadFromFile(path);
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.Owner = this;
            infoWindow.Show();
        }
    }
}
