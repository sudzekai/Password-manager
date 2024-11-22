using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Password_manager
{
    public partial class MainWindow : Window
    {


        // проверка на наличие пароля
        public MainWindow()
        {
            InitializeComponent();

            string path = @"password.txt";
            if (!File.Exists(path))
                File.WriteAllText(path, "");

            path = @"Passwords.txt";
            if (!File.Exists(path))
                File.WriteAllText(path, "");

            string passwordpath = @"password.txt";
            if (!File.Exists(passwordpath))
            {
                PasswordScreen.Visibility = Visibility.Visible;
            }
            if (File.Exists(passwordpath))
            {
                if (File.ReadAllText(passwordpath).Length == 0)
                {
                    PasswordScreen.Visibility = Visibility.Visible;
                }
                else
                {
                    PasswordScreen.Visibility = Visibility.Hidden;
                    LoginScreen.Visibility = Visibility.Visible;
                }
            }


        }

        //скрипт кнопки для входа
        private void EnterButton_Click(object sender, RoutedEventArgs e) { CheckPass(); }

        //функция кнопки, но через enter
        private void PasswordInput_KeyDown(object sender, KeyEventArgs e) { if (e.Key == Key.Enter) CheckPass(); }

        //проверка на валидность пароля + обновление текстового документа
        private void CheckPass()
        {
            string passwordpath = @"password.txt";
            if (PasswordInput.Password == Text_decryptor(File.ReadAllText(passwordpath)))
            {
                LoginScreen.Visibility = Visibility.Hidden;
                MainScreen.Visibility = Visibility.Visible;
                string path = @"Passwords.txt";
                if (File.Exists(path))
                {
                    txt_Output();
                }

            }
            else
            {
                PasswordInput.Password = "";
                WrongInput.Visibility = Visibility.Visible;
            }
        }

        // вывод текста из документа с дешифровкой
        public void txt_Output()
        {
            string path = @"Passwords.txt";
            Passwords_Block.Text = null;
            if (File.Exists(path))
            {
                for (int i = 0; i < File.ReadAllText(path).Split('\n').Count(); i++)
                {
                    Passwords_Block.Text += Text_decryptor(File.ReadAllText(path).Split('\n')[i]) + "\n";
                }
            }
        }

        // создание пароля, его запись и шифрование
        private void gen_password()
        {
            string path = @"password.txt";
            string content = Text_encryptor(GenPasswordBlock.Text);
            
            File.WriteAllText(path, content);
            path = @"Passwords.txt";
            if (File.Exists(path))
            {
                File.WriteAllText(path, "");
            }
            MainScreen.Visibility = Visibility.Visible;
            PasswordScreen.Visibility = Visibility.Hidden;
        }


        // методы шифрования и дешифровки

        public string Text_decryptor(string text)
        {
            string decrypted = "";

            char[] textarr = text.ToCharArray();

            for (int i = 0; i < textarr.Count(); i += 2)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    if (textarr[i] == arr[j])
                    {
                        for (int k = 0; k < arr.Length; k++)
                        {
                            if (textarr[i + 1] == arr[k])
                            {
                                decrypted += Convert.ToString(arr[k - j]);
                                break;
                            }
                        }
                        break;
                    }
                }

            }
            return decrypted;
        }
        public string Text_encryptor(string text)
        {
            Random rand = new Random();
            string encrypted = "";
            char[] textarr = text.ToCharArray();

            for (int i = 0; i < textarr.Count(); i++)
            {

                for (int j = 0; j < arr.Length; j++)
                {

                    int x = rand.Next(j, arr.Count());
                    if (arr[j] == textarr[i]) // PYTHON BLYAT
                    {
                        encrypted += Convert.ToString(arr[x - j]) + Convert.ToString(arr[x]);
                        break;
                    }
                }
            }
            return encrypted;
        }

        // ключ шифрования

        private char[] arr = {'d', 'О', 'щ', '?', '<', 'ь', '6', 'Ъ', 'У', 'А', '-', '$', 'Л', 'n',
        '8', 'k', 'о', ';', 'Q', 'p', 'х', 'К', 'y', 'r', 'ъ', '=', 'X', 'Ю', 'Й', 'Б',
        'Э', 'у', 'x', 'C', 'U', 'Y', 'V', 'т', 'Ф', 'l', 'Т', 'Г', 'o', 'R', 'm', 'З',
        'G', 'W', 'Н', 'g', 'Z', 'е', 'z', ']', 'F', 'A', 'Щ', '9', 'Я', 'н', 'ф', 'B',
        'Д', 'р', 'Е', '&', 'q', 'э', 'д', '*', '5', 'ш', '}', 'М', '~', 'б', '2', 'я',
        'E', '0', 'с', 'В', '_', ':', 'a', 'i', 'Р', 'Ч', 'Х', 'u', 'Ш', 'I', 'И', 'и',
        'г', '>', '^', '{', 'Ц', 'ю', 'Ы', 'й', 'п', 'w', 't', '/', 'f', '@', 'c', 'ы',
        'П', 'e', 'ё', 'S', 'b', '4', 'м', '1', 'L', ' ', 's', '[', 'а', 'в', '+', 'N',
        '7', 'M', '3', 'H', 'ц', 'Ь', 'з', 'л', '`', '|', 'v', 'J', 'P', 'K', 'ч', 'ж',
        'к', 'j', 'Ж', 'Ё', '%', '#', 'h', 'O', 'D', 'С', 'T', '.' };



        // метод вызова функции создания пароля
        private void GenPasswordBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { gen_password(); }
        }

        // обновленный метод ввода информации
        private void Passwords_Block_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                string password = "";
                int i = 0;
                Random x = new Random();
                while (i < 24)
                {
                    password += arr[x.Next(0, arr.Count())];
                    i++;
                }
                Passwords_Block.Text += password;
            }
            File.AppendAllText("Passwords.txt", Passwords_Block.Text);
        }

        // проверка на наличие изменений
        private void Window_Closing_1(object sender, CancelEventArgs e)
        {
            
            string path = @"Passwords.txt";
            File.WriteAllText(path, "");
            if (File.Exists(path))
            {

                for (int i = 0; i < Passwords_Block.LineCount; i++)
                {
                    File.AppendAllText(path, Text_encryptor(Passwords_Block.GetLineText(i)) + "\n");
                }
            }
        }

        
    }
}
