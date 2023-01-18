using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            int j = 0;
            int k = 0;
            for (int i = 0; i < 10; ++i)
            {
                Button button = new Button()
                {
                    Content = $"11:{i}0 - 1{i}:50",
                    Height = 25,
                    Width = 75,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                if (i % 3 == 0) { j++; k = 0; }
                button.Click += new RoutedEventHandler(button_Click);
                button.Margin = new Thickness(0 + 80 * k, 30 * j, 0, 0);
                this.grid.Children.Add(button);
                k++;
            }

        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{((Button)sender).Content.ToString()}");
        }
    }
}
