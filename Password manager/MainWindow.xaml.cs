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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Console;
using System.Runtime.ExceptionServices;
using System.Security.AccessControl;

namespace Password_manager
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            // Проверка на наличие пароля сделать нада
            //if (password == null)
            //{
            //    PasswordScreen.Visibility = Visibility.Visible;
            //}
            //else 
            //{
            //    LoginScreen.Visibility = Visibility.Collapsed;
            //}

        }

        private void EnterButton_Click(object sender, RoutedEventArgs e) { CheckPass(); }
        private void PasswordInput_KeyDown(object sender, KeyEventArgs e) { if (e.Key == Key.Enter)CheckPass(); }

        public void CheckPass()
        {
            if (PasswordInput.Password == "123")
            {
                LoginScreen.Visibility = Visibility.Hidden;
                MainScreen.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordInput.Password = "";
                WrongInput.Visibility = Visibility.Visible;
            }
        }


        private void Passwords_Box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txt_Input(Passwords_Box.Text);
                Passwords_Box.Text = "";
            }
        }

        public void txt_Input(string text)
        {
            string path = @"Data\Passwords.txt";
            string contents = text + "\n";
            File.AppendAllText(path, contents);
            txt_Output();
        }

        public void txt_Output()
        {
            string path = @"Data\Passwords.txt";
            Passwords_Block.Text = null;
            Passwords_Block.Text = File.ReadAllText(path);
        }

        // создание пароля
        private void gen_password()
        {
            string path = @"C: temp/pass.txt";
            string content = GenPasswordBlock.Text;
            File.WriteAllText(path, content);
            MainScreen.Visibility = Visibility.Visible;
            PasswordScreen.Visibility = Visibility.Hidden;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            gen_password();
        }
    }
}
