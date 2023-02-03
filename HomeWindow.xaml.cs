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
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            UpdateAfiashaList();
        }
        //########## Update
        public void UpdateAfiashaList(string poisk = "")
        {
            list_afish.Items.Clear();
            var database = App.db.afish.ToList();
            if(poisk != "") database = App.db.afish.Where(p => p.name.Contains(poisk)).ToList();
            foreach(var db in database)
            {
                list_afish.Items.Add( new TextBlock(){
                  HorizontalAlignment = HorizontalAlignment.Left,
                  TextWrapping = TextWrapping.Wrap,
                  Text = db.name,
                   TextAlignment = TextAlignment.Left,
                   FontSize = 20
              }
              );
            }
        }


       
        public void UpdateSnacksList()
        {
            var database = App.db.snacks.OrderBy(p => p.name).ToList();
            SnacksGrid.ItemsSource = database;
        }

        //########## Home
        private void poisk_afish_TextChanged(object sender, TextChangedEventArgs e)
        {
            //\\\\ обновление коллекции в соответсвии с контентом строки поиска

            UpdateAfiashaList(poisk_afish.Text);
            
        }

        private void list_afish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //\\\ обращение к бд чтобы взять данные о времени сеансов
            //\\\\ передача названия в NameFillm_Home

            NameFillm_Home.Text = list_afish.SelectedItem.ToString();

            var database = App.db.seans.Where(p => p.afish_id == App.db.afish.FirstOrDefault(o => o.name == NameFillm_Home.Text.ToString()).id).OrderBy(p => p.time).ToList();
            foreach(var time in database)
            {
                CB_Time.Items.Add(time.time);
            }

        }

        private void CB_Time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //\\\\ Обращение к бд чтобы взять цену билета
            //\\\\ количество свободных мест

            var database = App.db.seans.FirstOrDefault(p => p.afish_id == App.db.afish.FirstOrDefault(o => o.name == NameFillm_Home.Text).id 
            && p.time == CB_Time.SelectedItem.ToString());

            prise_Home.Text = database.price.ToString();
            OpenPlant_Home.Text = database.openPlant.ToString();
        }

        private void GoToPlentSelect_Click(object sender, RoutedEventArgs e)
        {
            //\\\ название фильма из NameFillm_Home
            //\\\ DB >> свободные
            //\\\ >>занятые  места
            //\\\ >> цена билета
            //\\\ >> номер зала  и количество мест

            var seans = App.db.seans.FirstOrDefault(p => p.afish_id == App.db.afish.FirstOrDefault(o => o.name == NameFillm_Home.Text).id);

            NameFlim_SelectPlant.Text = NameFillm_Home.Text.ToString();
            prise_SelPlant.Text = prise_Home.Text.ToString();
            numZal_SelPlant.Content = seans.zall_id.ToString();

            GreenPlant.Content = seans.openPlant.ToString();

            int j = 0;
            int k = 0;
            List<string> redPlant = App.db.seans.FirstOrDefault(p => p.afish_id == App.db.afish.FirstOrDefault(o => o.name == NameFillm_Home.Text).id).selectPlant.Split('/').ToList();
            var zall = App.db.zall.FirstOrDefault(p => p.id == seans.zall_id); 
            for (int i = 0; i < zall.countRyd * zall.countPlant; ++i)
            {
                if (i % zall.countPlant == 0 && i != 0) { j++; k = 0; }
                
                Button button = new Button();
                string plant = $"{j+1},{k+1}";
                if (redPlant.Contains(plant))
                {
                    button = new Button()
                    {
                        Name = (j+1).ToString(),
                        Content = k + 1,
                        Height = 25,
                        Width = 25,
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Background = new SolidColorBrush(Colors.Red)
                    };
                }else{
                    button = new Button()
                    {
                        Name = (j + 1).ToString(),
                        Content = k + 1,
                        Height = 25,
                        Width = 25,
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Background = new SolidColorBrush(Colors.Green)
                    };
                }
                button.Click += new RoutedEventHandler(button_Click);
                button.Margin = new Thickness(20 + (30 * k), 20 + (30 * j), 0, 0);
                this.grid.Children.Add(button);
                k++;
            }


            SelectPlent.Visibility = Visibility.Visible;
            Home.Visibility = Visibility.Hidden;
            snacks.Visibility = Visibility.Hidden;
            oplata.Visibility = Visibility.Hidden;
        }


        //########## Seleckt Plant

        // клик по месту в зале
        void button_Click(object sender, RoutedEventArgs e)
        {
            //add to buff

            SolidColorBrush orang = new SolidColorBrush(Colors.Orange);
            SolidColorBrush green = new SolidColorBrush(Colors.Green);
            SolidColorBrush btnBack = (SolidColorBrush)((Button)sender).Background;

            if (btnBack.Color == orang.Color)
            {
                ((Button)sender).Background = new SolidColorBrush(Colors.Green);
                Buff.selectPlant.Remove($"{((Button)sender).Name},{((Button)sender).Content}");
                Buff.SumPrise -= Convert.ToInt32(prise_SelPlant.Text);
            }
            else if (btnBack.Color == green.Color)
            {
                ((Button)sender).Background = new SolidColorBrush(Colors.Orange);
                Buff.selectPlant.Add($"{((Button)sender).Name},{((Button)sender).Content}");
                Buff.SumPrise += Convert.ToInt32(prise_SelPlant.Text);
            }
        }

        private void endSelectionPlent_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ заполнение таблицы со снеками из бд (updateFunction)

            UpdateSnacksList();




            SelectPlent.Visibility = Visibility.Hidden;
            Home.Visibility = Visibility.Hidden;
            snacks.Visibility = Visibility.Visible;
            oplata.Visibility = Visibility.Hidden;
        }




        //########## Snacks

        private void AddSnackToList_Click(object sender, RoutedEventArgs e)
        {
            //\\\ передавть выбранный элемент SnacksGrid в SnacksList с учетом количества
            //\\\\ (int)CountSnacks (для рассчта цены)
            //\\\\ вид чека как для оплаты снек - количество - сумма
            var content = ((snacks)SnacksGrid.SelectedItem).name + $"  Количество: {CountSnacks.Text} Сумма: "
                + ( ((snacks)SnacksGrid.SelectedItem).price * Convert.ToInt32(CountSnacks.Text) ).ToString();
            SnacksList.Items.Add(content);
            Buff.snacs.Add(content);
            Buff.SumPrise += ((snacks)SnacksGrid.SelectedItem).price * Convert.ToInt32(CountSnacks.Text);

        }

        private void DelSnackInList_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ удвление выбранного элемента из списка
            //\\\\ проверка пустого выделения
            if (SnacksList.SelectedItem != null)
            {
                SnacksList.Items.Remove(SnacksList.SelectedItem);
                Buff.snacs.Remove(SnacksList.SelectedItem.ToString());
                Buff.SumPrise -= Convert.ToInt32(SnacksList.SelectedItem.ToString().Split("Сумма:".ToCharArray())[1].Trim());
            }
            else MessageBox.Show("Не выбран товар для удаления из списка!");
        }

        private void GoToOpalata_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ заполнить чек оплаты
            //\\\\ "подсчет" суммы
            Chek_oplata.Items.Add($"Билеты  Количество: {Buff.selectPlant.Count()} Сумма: {Buff.selectPlant.Count() * Convert.ToInt32(prise_SelPlant.Text)}");
            foreach(var obj in Buff.snacs)
                Chek_oplata.Items.Add(obj);

            finalyPrise.Text = Buff.SumPrise.ToString();

            SelectPlent.Visibility = Visibility.Hidden;
            Home.Visibility = Visibility.Hidden;
            snacks.Visibility = Visibility.Hidden;
            oplata.Visibility = Visibility.Visible;
        }

        //########## Oplata

        //подумать что делать при перевыборе товаров в чеке
        private void GoToSelPlent_Click(object sender, RoutedEventArgs e)
        {
            SelectPlent.Visibility = Visibility.Visible;
            Home.Visibility = Visibility.Hidden;
            snacks.Visibility = Visibility.Hidden;
            oplata.Visibility = Visibility.Hidden;
        }

        private void GoToSnacks_Click(object sender, RoutedEventArgs e)
        {
            SelectPlent.Visibility = Visibility.Hidden;
            Home.Visibility = Visibility.Hidden;
            snacks.Visibility = Visibility.Visible;
            oplata.Visibility = Visibility.Hidden;
        }

        ///\\//\/\/\/\/\/\/

        private void beznal_btn_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ активация кнопок печати

            Nalich_oplata.Text = "0";
            Sdacha_oplata.Text= "0";

            PrintCheck.Visibility = Visibility.Visible;
            PrintTicket.Visibility = Visibility.Visible;

            MessageBox.Show("Оплата прошла успешно!");
        }

        private void nalich_btn_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ рассчет разницы чека и(<=) оплаты, вывод сдачи
            //\\\\ Если все ок => активация кнопок печати

            var sdach = Convert.ToInt32(Nalich_oplata.Text) - Convert.ToInt32(finalyPrise.Text);
            if(sdach < 0)
            {
                MessageBox.Show("Недостаточно средств!");
            }
            else
            {
                Sdacha_oplata.Text = sdach.ToString();

                PrintCheck.Visibility = Visibility.Visible;
                PrintTicket.Visibility = Visibility.Visible;
            }
            
        }

        ///\\//\/\/\/\/\/\/

        private void PrintCheck_Click(object sender, RoutedEventArgs e)
        {
            // заметки
        }

        private void PrintTicket_Click(object sender, RoutedEventArgs e)
        {
            // формирование данных для печати билетов -> название фильма\время начала\номер зала\ряд\место по количеству билетов
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
