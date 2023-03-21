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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        //########## castom function
        private bool TryLogin(string login, string paswd, bool admin)
        {
            if (admin)
            {//\\\\add try catch
                try
                {
                    if (App.db.admin.FirstOrDefault(p => p.login == login).password == paswd) return true;
                }
                catch (Exception)
                {
                    return false;
                }
                
            }else
            {
                try
                {
                    if (App.db.kassir.FirstOrDefault(p => p.login == login)
                    .password == paswd) return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public void UpdateAfishList()
        {
            ListBox_Afisha.Items.Clear();
            foreach(var film in App.db.afish.OrderBy(p => p.name))
            {
                ListBox_Afisha.Items.Add(film.name);
            }
        }


        //########## selector
        private void kassirBtn_Click(object sender, RoutedEventArgs e)
        {
            Buff.adminLog = false;
            logined.Visibility =    Visibility.Visible;
            selector.Visibility =   Visibility.Hidden;
            admin.Visibility =      Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility =     Visibility.Hidden;
            otchot.Visibility =     Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;
        }

        private void adminBtn_Click(object sender, RoutedEventArgs e)
        {
            Buff.adminLog = true;
            logined.Visibility = Visibility.Visible;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;
        }

        //########## loign
        private void selectorBack_Click(object sender, RoutedEventArgs e)
        {
            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Visible;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;

        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if( !TryLogin(LoginTextBox.Text.ToString(),
                PasswordTextBox.Text.ToString(),
                Buff.adminLog))
            {
                MessageBox.Show("Неверные данные для входа!\nПовторите попытку.");
            }
            else
            {
                if (Buff.adminLog)
                {
                    logined.Visibility = Visibility.Hidden;
                    selector.Visibility = Visibility.Hidden;
                    admin.Visibility = Visibility.Visible;
                    listKassir.Visibility = Visibility.Hidden;
                    kassir.Visibility = Visibility.Hidden;
                    otchot.Visibility = Visibility.Hidden;
                    afish.Visibility = Visibility.Hidden;
                    ListAfiah.Visibility = Visibility.Hidden;
                }
                else
                {
                    HomeWindow homeWindow = new HomeWindow();
                    homeWindow.Show();
                    this.Close();
                }
            }
        }

        //########## admin

        private void adminKassiri_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ заполнить список кассиров

            DGKassir.ItemsSource = App.db.kassir.OrderBy(p => p.name).ToList();

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Visible;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;
        }

        private void adminOtchot_Click(object sender, RoutedEventArgs e)
        {
            //\\\ заполнить список отчетов

            DGOthot.ItemsSource = App.db.otchot.ToList();


            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Visible;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;
        }

        private void afiahRed_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ показать таблицу афиши
            //\\\\ заполнить список афиши
            foreach(var elem in App.db.afish.OrderBy(p => p.name))
            {
                
                ListBox_Afisha.Items.Add(elem.name);
            }
            


            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Visible;
        }

        private void adminExit_Click(object sender, RoutedEventArgs e)
        {
            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Visible;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;
        }


        //########## list Kassir

        private void listKassirBack_Click(object sender, RoutedEventArgs e)
        {

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Visible;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;
        }

        private void listKassirRedact_Click(object sender, RoutedEventArgs e)
        {
            //\\\\проверить функционал

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Visible;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;

            var redKassir = (kassir)DGKassir.SelectedItem;

            Kassir_fio.Text = redKassir.name;
            Kassir_login.Text = redKassir.login;
            Kassir_paswd.Text = redKassir.password;

            Buff.redact = true;
        }

        private void listKassirDelete_Click(object sender, RoutedEventArgs e)
        {
            //\\\\\\\\\проверить функционал
            // "точно удалять?"
            var delKassir = (kassir)DGKassir.SelectedItem;
            App.db.kassir.Remove(delKassir);
            App.db.SaveChanges();

            DGKassir.ItemsSource = App.db.kassir.ToList();

            MessageBox.Show($"{delKassir.name} - Удален из базы сотрудников!");
        }

        private void lisatKassirAdd_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ проверить функционал

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Visible;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;

            Kassir_fio.Text = "";
            Kassir_login.Text = "";
            Kassir_paswd.Text = "";

            Buff.redact = false;
        }


        //########## Kassir

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Visible;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;
        }

        private void Kassir_saveButton_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ проверка на редактирование или создние
            // сделать валидацию пароля

            if (Buff.redact)
            {
                var redKassir = (kassir)DGKassir.SelectedItem;
                redKassir.name = Kassir_fio.Text;
                redKassir.login = Kassir_login.Text;
                redKassir.password = Kassir_paswd.Text;
                App.db.SaveChanges();
            }
            else
            {
                kassir item = new kassir
                {name = Kassir_fio.Text,
                login = Kassir_login.Text,
                password = Kassir_paswd.Text
                };
                App.db.kassir.Add(item);
                App.db.SaveChanges();
            }

            //\\\\\\ выходить на предыдущую страницу и обновлять данные в списке

            DGKassir.ItemsSource = App.db.kassir.ToList();

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Visible;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;
        }


        //########## otchot

        private void otchotBack_Click(object sender, RoutedEventArgs e)
        {
            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Visible;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;

        }

        private void MonthCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //\\\\ обновлять список по выбранному месяцу

            try
            {
                var otchot = App.db.otchot.Where(p => p.date.Year == new DateTime(YearCB.SelectedIndex + 2022, MonthCB.SelectedIndex + 1, 1).Year
                    && p.date.Month == new DateTime(YearCB.SelectedIndex + 2022, MonthCB.SelectedIndex + 1, 1).Month);// проверить что индекс с нуля
                DGOthot.ItemsSource = otchot;
            }
            catch(Exception)
            {

            }
        }

        private void YearCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var otchot = App.db.otchot.Where(p => p.date.Year == new DateTime(YearCB.SelectedIndex + 2022, MonthCB.SelectedIndex + 1, 1).Year
                    && p.date.Month == new DateTime(YearCB.SelectedIndex + 2022, MonthCB.SelectedIndex + 1, 1).Month);// проверить что индекс с нуля

                DGOthot.ItemsSource = otchot;
            }
            catch (Exception)
            {

            }
        }



        //########## List afish

        private void AfishaBack_Click(object sender, RoutedEventArgs e)
        {
            // MB "точно?"

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Visible;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;
        }

        private void AfishaRedact_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ показать форму заполнения
            //\\\\ делать выборку по имени и заполнять поля ввода
            //\\\\ передавать данные в поля

            var afishFilm = App.db.afish.FirstOrDefault(p => p.name == (ListBox_Afisha.SelectedItem).ToString());
            CBSeans_Afish.Items.Clear();
            nameFilm_Afish.Text = afishFilm.name;
            length_Afish.Text = afishFilm.length.ToString();
            newSeans_Afish.Text = "00:00 - 00:00";

            List<seans> seansi = new List<seans>();
            try
            {
                seansi = App.db.seans.Where(p => p.afish_id == afishFilm.id).ToList();
                foreach (var time in seansi.OrderBy(p => p.time))
                {
                    CBSeans_Afish.Items.Add(time.time);
                }
                numZll_Afish.Text = seansi[0].zall_id.ToString();
                prise_Afish.Text = seansi[0].price.ToString();
            }
            catch (Exception)
            {
                CBSeans_Afish.Items.Clear();
                numZll_Afish.Text = "";
                prise_Afish.Text = "";
            }
           
            
            Buff.redact = true;

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Visible;
            ListAfiah.Visibility = Visibility.Hidden;
        }

        private void AfishaAdd_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ очистить поля
            //\\\\ показать форму заполнения

            nameFilm_Afish.Text = "";
            numZll_Afish.Text = "";
            prise_Afish.Text = "";
            length_Afish.Text = "";
            newSeans_Afish.Text = "00:00 - 00:00";
            CBSeans_Afish.Items.Clear();

            Buff.redact = false;

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Visible;
            ListAfiah.Visibility = Visibility.Hidden;
        }

        private void AfishaDelete_Click(object sender, RoutedEventArgs e)
        {
            // MB "Точно удалть?"
            //\\\\ удалить из бд

            
            var elem = App.db.afish.FirstOrDefault(p => p.name == ListBox_Afisha.SelectedItem.ToString());
            ListBox_Afisha.Items.Remove(ListBox_Afisha.SelectedItem);
            App.db.afish.Remove(elem);
            App.db.SaveChanges();
        }

        //########## afish

        private void agish_back_Click(object sender, RoutedEventArgs e)
        {
            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Visible;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;
        }

        private void delSeans_btn_Click(object sender, RoutedEventArgs e)
        {
            if (CBSeans_Afish.SelectedIndex == -1)
            {
                MessageBox.Show("Для удаления необходимо выбрать элемент!");
            }
            else
            {
                CBSeans_Afish.Items.Remove(CBSeans_Afish.SelectedItem);
            }
        }

        private void addSeans_btn_Click(object sender, RoutedEventArgs e)
        {

            if (true) //валидация ввода сеанса
            {
                CBSeans_Afish.Items.Add(newSeans_Afish.Text);
            }
            else
            {
                MessageBox.Show("Проверьте данные ввода!\nФормат времени сеанса: <00:00 - 00:00>");
            } 
        }

        private void afish_Save_Click(object sender, RoutedEventArgs e)
        {
            //\\\\ сделать ветвление для редактирования и создания
            //\\\\ выходить на предыдущую страницу
            //\\\\ обновлять данные в списке по завершении

            //\\\\ при редактировании искать объект по индентификатору
            if (Buff.redact)
            {
                var film = App.db.afish.FirstOrDefault(p => p.name == (ListBox_Afisha.SelectedItem).ToString());
                film.name = nameFilm_Afish.Text;
                film.length = new TimeSpan(Convert.ToInt32(length_Afish.Text.Split(':')[0]), Convert.ToInt32(length_Afish.Text.Split(':')[1]), 0);
                App.db.SaveChanges();

                var seansi = App.db.seans.Where(p => p.afish_id == film.id);
                foreach(var del in seansi)
                {
                    App.db.seans.Remove(del);
                    
                }
                App.db.SaveChanges();

                int numZal = Convert.ToInt32(numZll_Afish.Text.ToString());
                var zall = App.db.zall.FirstOrDefault(p => p.id == numZal);
                int openPlant = zall.countRyd * zall.countPlant;
                foreach (var time in CBSeans_Afish.Items)
                {
                    var newSeans = new seans{
                        afish_id = film.id,
                        zall_id = Convert.ToInt32(numZll_Afish.Text),
                        selectPlant = "",
                        time = time.ToString(),
                        price = Convert.ToInt32(prise_Afish.Text),
                        openPlant = openPlant
                    };
                    App.db.seans.Add(newSeans);
                    App.db.SaveChanges();
                }
                
                UpdateAfishList();
                CBSeans_Afish.Items.Clear();
            }
            //\\\\ при создании создавть объект и сохранять в базе
            else
            {
                var newFilm = new afish()
                {
                    name = nameFilm_Afish.Text,
                    length = new TimeSpan(Convert.ToInt32(length_Afish.Text.Split(':')[0]), Convert.ToInt32(length_Afish.Text.Split(':')[1]), 0)
                };
                App.db.afish.Add(newFilm);
                App.db.SaveChanges();
                var numZal = Convert.ToInt32(numZll_Afish.Text.ToString());
                var zall = App.db.zall.FirstOrDefault(p => p.id == numZal);
                int openPlant = zall.countRyd * zall.countPlant;
                foreach (var time in CBSeans_Afish.Items)
                {
                    seans newSeans = new seans()
                    {
                        afish_id = App.db.afish.FirstOrDefault(p => p.name == newFilm.name).id,
                        zall_id = Convert.ToInt32(numZll_Afish.Text),
                        selectPlant = "",
                        time = time.ToString(),
                        price = Convert.ToInt32(prise_Afish.Text),
                        openPlant= openPlant
                    };
                    App.db.seans.Add(newSeans);
                   
                }
                App.db.SaveChanges();
                UpdateAfishList();

                CBSeans_Afish.Items.Clear();
            }


            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Visible;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;
        }
    }
}
