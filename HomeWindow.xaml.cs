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

        private void exit_Kassir_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void poisk_afish_TextChanged(object sender, TextChangedEventArgs e)
        {
            //\\\\ обновление коллекции в соответсвии с контентом строки поиска

            UpdateAfiashaList(poisk_afish.Text);
            
        }

        private void list_afish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //\\\ обращение к бд чтобы взять данные о времени сеансов
            //\\\\ передача названия в NameFillm_Home

            if (list_afish.SelectedItem != null)
                NameFillm_Home.Text = ((TextBlock)list_afish.SelectedItem).Text.ToString();
            else NameFillm_Home.Text = "Выберите фильм";
            CB_Time.Items.Clear();
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

            if(CB_Time.SelectedItem != null)
            {
                var database = App.db.seans.FirstOrDefault(p => p.afish_id == App.db.afish.FirstOrDefault(o => o.name == NameFillm_Home.Text).id
            && p.time == CB_Time.SelectedItem.ToString());

                GoToPlentSelect.IsEnabled = true;
                prise_Home.Text = database.price.ToString();
                OpenPlant_Home.Text = database.openPlant.ToString();
            }
            else
            {
                GoToPlentSelect.IsEnabled = true;
                prise_Home.Text = "";
                OpenPlant_Home.Text = "";
            }
        }

        private void GoToPlentSelect_Click(object sender, RoutedEventArgs e)
        {
            //\\\ название фильма из NameFillm_Home
            //\\\ DB >> свободные
            //\\\ >>занятые  места
            //\\\ >> цена билета
            //\\\ >> номер зала  и количество мест
            Buff.selectPlant = new List<string>();

            Buff.snacs = new List<string>();

            var seans = App.db.seans.FirstOrDefault(p => p.afish_id == App.db.afish.FirstOrDefault(o => o.name == NameFillm_Home.Text).id
            && p.time == CB_Time.SelectedItem.ToString());

            NameFlim_SelectPlant.Text = NameFillm_Home.Text.ToString();
            prise_SelPlant.Text = prise_Home.Text.ToString();
            numZal_SelPlant.Content = seans.zall_id.ToString();

            GreenPlant.Content = seans.openPlant.ToString();

            int j = 0;
            int k = 0;
            List<string> redPlant = App.db.seans.FirstOrDefault(p => p.afish_id == App.db.afish.FirstOrDefault(o => o.name == NameFillm_Home.Text).id &&  p.time == CB_Time.SelectedItem.ToString()).selectPlant.Split('/').ToList();
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
                        Name = "_" + (j + 1).ToString(),
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
                        Name = "_" + (j + 1).ToString(),
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
            //\\\\ add to buff

            SolidColorBrush orang = new SolidColorBrush(Colors.Orange);
            SolidColorBrush green = new SolidColorBrush(Colors.Green);
            SolidColorBrush btnBack = (SolidColorBrush)((Button)sender).Background;

            if (btnBack.Color == orang.Color)
            {
                ((Button)sender).Background = new SolidColorBrush(Colors.Green);
                var button = ((Button)sender);
                Buff.selectPlant.Remove($"{button.Name.Split('_')[1]},{button.Content}/");
                Buff.SumPrise -= Convert.ToInt32(prise_SelPlant.Text);
            }
            else if (btnBack.Color == green.Color)
            {
                ((Button)sender).Background = new SolidColorBrush(Colors.Orange);
                var button = ((Button)sender);
                Buff.SumPrise += Convert.ToInt32(prise_SelPlant.Text);
                Buff.selectPlant.Add($"{button.Name.Split('_')[1]},{button.Content}/");
            }
            endSelectionPlent.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //\\\\ обратно в меню

            SelectPlent.Visibility = Visibility.Hidden;
            Home.Visibility = Visibility.Visible;
            snacks.Visibility = Visibility.Hidden;
            oplata.Visibility = Visibility.Hidden;
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
            if (CountSnacks.Text == "" || CountSnacks.Text == "Count") CountSnacks.Text = "1";
            var content = ((snacks)SnacksGrid.SelectedItem).name + $"  Количество: {CountSnacks.Text} Сумма: "
                + ( ((snacks)SnacksGrid.SelectedItem).price * Convert.ToInt32(CountSnacks.Text) ).ToString()+'\n';
            SnacksList.Items.Add(content);
            Buff.snacs.Add(content);
            Buff.SumPrise += ((snacks)SnacksGrid.SelectedItem).price * Convert.ToInt32(CountSnacks.Text);

        }

        private void DelSnackInList_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ удаление выбранного элемента из списка
            //\\\\ проверка пустого выделения
            if (SnacksList.SelectedItem != null)
            {
                
                Buff.snacs.Remove(SnacksList.SelectedItem.ToString());
                var price = SnacksList.SelectedItem.ToString().Split(new string[] { "Сумма:"},0);
                Buff.SumPrise -= Convert.ToInt32(price[1].Trim());// проверить что возвращает именно сумму!!
                SnacksList.Items.Remove(SnacksList.SelectedItem);
            }
            else MessageBox.Show("Не выбран товар для удаления из списка!");
        }

        private void GoToOpalata_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ очистить и заполнить заново чек оплаты
            //\\\\ "подсчет" суммы
            Chek_oplata.Items.Clear();
            Chek_oplata.Items.Add($"Билеты  Количество: {Buff.selectPlant.Count()} Сумма: {Buff.selectPlant.Count() * Convert.ToInt32(prise_SelPlant.Text)}");
            foreach(var obj in Buff.snacs)
                Chek_oplata.Items.Add(obj);

            finalyPrise.Text = Buff.SumPrise.ToString();

            GoToSelPlent.IsEnabled = true;
            GoToSnacks.IsEnabled = true;

            nalich_btn.IsEnabled = true;
            beznal_btn.IsEnabled = true;

            end_Oplata.Visibility = Visibility.Hidden;

            SelectPlent.Visibility = Visibility.Hidden;
            Home.Visibility = Visibility.Hidden;
            snacks.Visibility = Visibility.Hidden;
            oplata.Visibility = Visibility.Visible;
        }

        //########## Oplata

        //подумать что делать при перевыборе товаров в чеке
        private void GoToSelPlent_Click(object sender, RoutedEventArgs e)
        {
            goToMenu.Visibility = Visibility.Hidden;

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

            GoToSelPlent.IsEnabled = false;
            GoToSnacks.IsEnabled = false;

            nalich_btn.IsEnabled = false;
            beznal_btn.IsEnabled = false;

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
                GoToSelPlent.IsEnabled = false;
                GoToSnacks.IsEnabled = false;

                nalich_btn.IsEnabled = false;
                beznal_btn.IsEnabled = false;

                Sdacha_oplata.Text = sdach.ToString();

                PrintCheck.Visibility = Visibility.Visible;
                PrintTicket.Visibility = Visibility.Visible;
            }
            
        }

        ///\\//\/\/\/\/\/\/

        private void PrintCheck_Click(object sender, RoutedEventArgs e)
        {
            // заметки
            MessageBox.Show("Чек напечатан!");
        }

        private void PrintTicket_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ формирование данных для печати билетов -> название фильма\время начала\номер зала\ряд\место по количеству билетов
            List<string> ticketList = new List<string>();
            string output = "";
            foreach(var plant in Buff.selectPlant.OrderBy(p => p.Split(',')[0]).OrderBy(p => p.Split(',')[1]))// проверить plant - должнобыть ряд/место
            {
                ticketList.Add($"фильм: {NameFlim_SelectPlant.Text}\nВремя начала: {CB_Time.Text.Split('-')[0]}\nНомер зала: {numZal_SelPlant.Content}\n" +
                    $"Ряд: {plant.Split(',')[0]}\nМесто: {plant.Split(',')[1]}"); // проверить вывод - должнобыть ряд/место
                output += $"фильм: {NameFlim_SelectPlant.Text}\nВремя начала: {CB_Time.Text.Split('-')[0]}\nНомер зала: {numZal_SelPlant.Content}\n" +
                    $"Ряд: {plant.Split(',')[0]}\nМесто: {plant.Split(',')[1]}\n\n";
            }
            end_Oplata.Visibility = Visibility.Visible;
            MessageBox.Show(output);
        }

        private void end_Oplata_Click(object sender, RoutedEventArgs e)
        {
            Buff.snacs.Clear();

            var seans = App.db.seans.FirstOrDefault(p => p.afish_id == App.db.afish.FirstOrDefault(o => o.name == NameFillm_Home.Text).id && p.time == CB_Time.SelectedItem.ToString());
            seans.openPlant -= Buff.selectPlant.Count();
            foreach(var plant in Buff.selectPlant)
            {
                seans.selectPlant += plant;
            }
            App.db.SaveChanges();
            Buff.selectPlant.Clear();

            // составление отчета

            Buff.SumPrise = 0;
            finalyPrise.Text = "0";
            Nalich_oplata.Text = "0";
            SnacksList.Items.Clear();
            poisk_afish.Text = "";
            NameFillm_Home.Text = "Выберите фильм из списка!";
            CB_Time.Items.Clear();
            prise_Home.Text = "";
            OpenPlant_Home.Text = "";
            GoToPlentSelect.IsEnabled = false;
            endSelectionPlent.IsEnabled = false;
            end_Oplata.Visibility = Visibility.Hidden;

            goToMenu.Visibility = Visibility.Visible;
            SelectPlent.Visibility = Visibility.Hidden;
            Home.Visibility = Visibility.Visible;
            snacks.Visibility = Visibility.Hidden;
            oplata.Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SelectPlent.Visibility = Visibility.Hidden;
            Home.Visibility = Visibility.Visible;
            snacks.Visibility = Visibility.Hidden;
            oplata.Visibility = Visibility.Hidden;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SelectPlent.Visibility = Visibility.Hidden;
            Home.Visibility = Visibility.Visible;
            snacks.Visibility = Visibility.Hidden;
            oplata.Visibility = Visibility.Hidden;
        }

        private void goToMenu_Click(object sender, RoutedEventArgs e)
        {
            SelectPlent.Visibility = Visibility.Hidden;
            Home.Visibility = Visibility.Visible;
            snacks.Visibility = Visibility.Hidden;
            oplata.Visibility = Visibility.Hidden;
        }

      
    }
}
