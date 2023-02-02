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
            var database = App.DB.afisha.ToList();
            if(poisk != "") database = App.DB.afisha.Where(p => p.name.Contains(poisk)).ToList();
            foreach(var db in database)
            {
                list_afish.Items.Add( new TextBlock(){
                  HorizontalAlignment = HorizontalAlignment.Left,
                  TextWrapping = TextWrapping.Wrap,
                  Text = db.name,//доаолнять тут через 'перренос строки?'
                   TextAlignment = TextAlignment.Left
              }
              );
            }
        }


       
        public void UpdateSnacksList()
        {
            list_afish.Items.Clear();
            var database = App.DB.snack.ToList();
            SnacksGrid.ItemsSource = database;
        }

        //########## Home
        private void poisk_afish_TextChanged(object sender, TextChangedEventArgs e)
        {
            //\ обновление коллекции в соответсвии с контентом строки поиска

            UpdateAfiashaList(poisk_afish.Text);
            
        }

        private void list_afish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //// обращение к бд чтобы взять данные о времени сеансов
            ///передача названия в NameFillm_Home

            var database = App.DB.seansi.ToList();
            foreach(var time in database)
            {
                CB_Time.Items.Add(time.ToString());
            }

            NameFillm_Home.Text = list_afish.SelectedItem.ToString();//настроить, возможно придется выирать именно текст, либо разбивать строку
        }

        private void CB_Time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///// Обращение к бд чтобы взять цену билета и количество свободных мест

            var database = App.DB.seansi.FirstOnDefault(p => p.seans == NameFillm_Home.Text).ToList();//паправить where

            prise_Home.Text = database.prise.ToString();
            OpenPlant_Home.Text = database.OpenPlant.ToString();
        }

        private void GoToPlentSelect_Click(object sender, RoutedEventArgs e)
        {
            //\ название фильма из NameFillm_Home
            // DB >> свободные и занятые  места
            //\ >> цена билета
            //\ >> номер зала  и количество мест
            //передача в буффер выбранных мест (клик по месту)

            NameFlim_SelectPlant.Text = NameFillm_Home.Text.ToString();
            prise_SelPlant.Text = prise_Home.Text.ToString();
            numZal_SelPlant.Content = "5";//from db

            int j = 0;
            int k = 0;
            // in Buff Static class //var numZall = App.DB.seans.FirstOrDefault(p => p.afish_id == Buff.SelectAfish).numZal;
            //var zall = App.DB.zal.FirstOnDefault(p => p.num == Buff.numZall); zall.rad * zall.plantInRad
            for (int i = 0; i < 20 * 9; ++i)
            {
                if (i % 20 == 0 && i != 0) { j++; k = 0; }

                Button button = new Button()
                {
                    Content = k + 1,
                    Height = 25,
                    Width = 25,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Background = new SolidColorBrush(Colors.Green)
                };

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
            SolidColorBrush btnBack = (SolidColorBrush)((Button)sender).Background;
            if (btnBack.Color == orang.Color) ((Button)sender).Background = new SolidColorBrush(Colors.Red);
            else ((Button)sender).Background = new SolidColorBrush(Colors.Orange);
        }

        private void endSelectionPlent_Click(object sender, RoutedEventArgs e)
        {
            // заполнение чека оплаты, данные из буфера выбранных мест + (int)prise_SelPlant
            //\/\/ заполнение таблицы со снеками из бд (updateFunction)
            // заполнение чека оплаты в виде: "билеты" - количество - сумма

            UpdateSnacksList();


            SelectPlent.Visibility = Visibility.Hidden;
            Home.Visibility = Visibility.Hidden;
            snacks.Visibility = Visibility.Visible;
            oplata.Visibility = Visibility.Hidden;
        }




        //########## Snacks

        private void AddSnackToList_Click(object sender, RoutedEventArgs e)
        {
            // пере давть выбранный элемент SnacksGrid в SnacksList с учетом количества
            //\ (int)CountSnacks (для рассчта цены)
            // попробовать реализовать кастомную таблицу (name\count\SumPrise)
            // вид чека как для оплаты снек - количество - сумма
            var content = ((snack)SnacksGrid.SelectedItem).name + $"   Количество: {CountSnacks.Text} "
                + (((snack)SnacksGrid.SelectedItem).prise * Convert.ToInt32(CountSnacks.Text)).ToString();
            SnacksList.Items.Add(Content);

        }

        private void DelSnackInList_Click(object sender, RoutedEventArgs e)
        {
            //\ удвление выбранного элемента из списка
            // проверка пустого выделения
            SnacksList.Items.Remove(SnacksList.SelectedItem);
        }

        private void GoToOpalata_Click(object sender, RoutedEventArgs e)
        {
            //\ дополнить чек оплаты
            //\ "подсчет" суммы
            foreach(var obj in SnacksList.Items)
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
            //\ активация кнопок печати

            Nalich_oplata.Text = "0";
            Sdacha_oplata.Text="0";

            PrintCheck.Visibility = Visibility.Visible;
            PrintTicket.Visibility = Visibility.Visible;

            MessageBox.Show("Оплата прошла успешно!");
        }

        private void nalich_btn_Click(object sender, RoutedEventArgs e)
        {
            // рассчет разницы чека и(<=) оплаты, вывод сдачи
            // Если все ок => активация кнопок печати
        }

        ///\\//\/\/\/\/\/\/

        private void PrintCheck_Click(object sender, RoutedEventArgs e)
        {
            // заметки
        }

        private void PrintTicket_Click(object sender, RoutedEventArgs e)
        {
            // формирование данных для печати билетов -> название фильма\время начала\номер зала\ряд\место 
        }

        
    }
}
