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
            {//add try catch
                if(App.DB.admin.FirstOrDefault(p => p.login == login)
                    .password == paswd) return true;
            }else
            {
                if (App.DB.kassir.FirstOrDefault(p => p.login == login)
                    .password == paswd) return true;
            }
            return false;
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
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if( !TryLogin(LoginTextBox.Text.ToString(),
                PasswordTextBox.Text.ToString(),
                Buff.adminLog)
                )
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
            //заполнить список кассиров

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Visible;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
        }

        private void adminOtchot_Click(object sender, RoutedEventArgs e)
        {
            // заполнить список отчетов

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Visible;
        }

        private void afiahRed_Click(object sender, RoutedEventArgs e)
        {
            // показать таблицу афиши
            // заполнить список афиши
        }

        private void adminExit_Click(object sender, RoutedEventArgs e)
        {
            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Visible;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
        }


        //########## list Kassir

        private void listKassirBack_Click(object sender, RoutedEventArgs e)
        {
            // проверка

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Visible;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
        }

        private void listKassirRedact_Click(object sender, RoutedEventArgs e)
        {
            //проверить функционал

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Visible;
            otchot.Visibility = Visibility.Hidden;

            var redKassir = App.DB.kassir.FirstOrDefault(p =>
            p.name == ListBox_Kassir.SelectedItem.ToString());

            Kassir_fio.Text = redKassir.name;
            Kassir_login.Text = redKassir.login;
            Kassir_paswd.Text = redKassir.password;

            Buff.redact = true;
        }

        private void listKassirDelete_Click(object sender, RoutedEventArgs e)
        {
            //проверить функционал

            var delKassir = App.DB.kassir.FirstOrDefault(p =>
            p.name == ListBox_Kassir.SelectedItem.ToString());
            App.DB.kassir.Remove(delKassir);
            App.DB.SaveChanges();

            ListBox_Kassir.Items.Remove(ListBox_Kassir.SelectedItem);
        }

        private void lisatKassirAdd_Click(object sender, RoutedEventArgs e)
        {
            // проверить функционал

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Visible;
            otchot.Visibility = Visibility.Hidden;

            Kassir_fio.Text = "";
            Kassir_login.Text = "";
            Kassir_paswd.Text = "";

            Buff.redact = false;
        }


        //########## Kassir

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // назад

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Visible;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
        }

        private void Kassir_saveButton_Click(object sender, RoutedEventArgs e)
        {
            // проверка на редактирование или создние
            // при создании чистить поля
            // 

            if (Buff.redact)
            {
                var redKassir = App.DB.kassir.FirstOrDefault(p =>
                     p.name == ListBox_Kassir.SelectedItem.ToString());
                redKassir.name = Kassir_fio.Text;
                redKassir.login = Kassir_login.Text;
                redKassir.password = Kassir_paswd.Text;
                App.DB.SaveChanges();

                ListBox_Kassir.Items.Add(Kassir_fio.Text);

            }
            else
            {
                //добавление в бд
            }

            // выходить на предыдущую страницу и обновлять данные в списке

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Visible;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
        }


        //########## otchot

        private void otchotBack_Click(object sender, RoutedEventArgs e)
        {
            // проверить

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Visible;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;

        }

        private void MonthCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // обновлять список по выбранному месяцу

            var otchot = App.DB.otchot.Where(p =>
            p.mes == Convert.ToInt32(MonthCB.SelectedItem)).ToList();

            DataGrid_Othot.ItemsSource = otchot;
        }



        //########## List afish

        private void AfishaBack_Click(object sender, RoutedEventArgs e)
        {
            // проверить правильность

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Visible;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
        }

        private void AfishaRedact_Click(object sender, RoutedEventArgs e)
        {
            // показать форму заполнения
            // передать данные по выбранному элементу
            // передавть идентификатор элемента
        }

        private void AfishaDelete_Click(object sender, RoutedEventArgs e)
        {
            // сделать проверку push + удалить из бд

            ListBox_Afisha.Items.Remove(ListBox_Afisha.SelectedItem);
        }

        private void AfishaAdd_Click(object sender, RoutedEventArgs e)
        {
            // очистить и показать форму заполнения
        }


        //########## afish

        private void agish_back_Click(object sender, RoutedEventArgs e)
        {
            // проверить правильность

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Visible;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
        }

        private void afish_Save_Click(object sender, RoutedEventArgs e)
        {
            // сделать ветвление для редактирования и создания
            // при редактировании искать объект по индентификатору
            // при создании создавть объект и сохранять в базе
            // выходить на предыдущую страницу и обновлять данные в списке
        }
    }
}
