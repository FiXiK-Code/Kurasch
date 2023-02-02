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
                if(App.db.admin.FirstOrDefault(p => p.login == login)
                    .password == paswd) return true;
            }else
            {
                if (App.db.kassir.FirstOrDefault(p => p.login == login)
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

            DGKassir.ItemsSource = App.db.kassir;

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

            DGOthot.ItemsSource = App.db.otchot;


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
                TextBlock item = new TextBlock()
                {
                    Text = elem.name,
                    FontSize = 20
                };
                ListBox_Afisha.Items.Add(item);
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
            // MB "Точно?"

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

            var delKassir = (kassir)DGKassir.SelectedItem;
            App.db.kassir.Remove(delKassir);
            App.db.SaveChanges();

            DGKassir.ItemsSource = App.db.kassir;
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
                kassir item = new kassir()
                {name = Kassir_fio.Text,
                login = Kassir_login.Text,
                password = Kassir_paswd.Text
                };
                App.db.kassir.Add(item);
                App.db.SaveChanges();
            }

            //\\\\\\ выходить на предыдущую страницу и обновлять данные в списке

            DGKassir.ItemsSource = App.db.kassir;

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
            // MB "Точно?"

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

            var otchot = App.db.otchot.Where(p => p.mes == MonthCB.SelectedIndex + 1);

            DGOthot.ItemsSource = otchot;
        }

        private void YearCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var otchot = ((List<otchot>)DGOthot.ItemsSource).Where(p => p.year == YearCB.SelectedIndex + 1);

            //DGOthot.ItemsSource = otchot;
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
            // передать данные по выбранному элементу
            // передавть идентификатор элемента

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

            ListBox_Afisha.Items.Remove(ListBox_Afisha.SelectedItem);

            var elem = App.db.afish.FirstOrDefault(p => p.name == ((TextBlock)ListBox_Afisha.SelectedItem).Text.ToString());
            App.db.afish.Remove(elem);
        }

        private void AfishaAdd_Click(object sender, RoutedEventArgs e)
        {
            // очистить поля
            //\\\\ показать форму заполнения


            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Hidden;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Visible;
            ListAfiah.Visibility = Visibility.Hidden;
        }


        //########## afish

        private void agish_back_Click(object sender, RoutedEventArgs e)
        {
            // MB "Точно?"

            logined.Visibility = Visibility.Hidden;
            selector.Visibility = Visibility.Hidden;
            admin.Visibility = Visibility.Visible;
            listKassir.Visibility = Visibility.Hidden;
            kassir.Visibility = Visibility.Hidden;
            otchot.Visibility = Visibility.Hidden;
            afish.Visibility = Visibility.Hidden;
            ListAfiah.Visibility = Visibility.Hidden;
        }

        private void afish_Save_Click(object sender, RoutedEventArgs e)
        {
            //\\\ сделать ветвление для редактирования и создания
            //\\\\ выходить на предыдущую страницу
            // обновлять данные в списке по завершении

            // при редактировании искать объект по индентификатору
            if (Buff.redact)
            {
                
            }
            // при создании создавть объект и сохранять в базе
            else
            {
                
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

        
    }
}
