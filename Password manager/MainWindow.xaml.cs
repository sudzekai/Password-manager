using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Password_manager
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            string passwordpath = @"data/password.txt";
            if (!File.Exists(passwordpath))
            {
                PasswordScreen.Visibility = Visibility.Visible;
            }
            if (File.Exists(passwordpath))
            {
                if (File.ReadAllText(passwordpath) == null || File.ReadAllText(passwordpath)=="")
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

        //скрипт кнопки для входа, существует для красоты
        private void EnterButton_Click(object sender, RoutedEventArgs e) { CheckPass(); }

        //функция кнопки, но через enter
        private void PasswordInput_KeyDown(object sender, KeyEventArgs e) { if (e.Key == Key.Enter) CheckPass(); }

        //проверка на валидность пароля + обновление текстового документа
        private void CheckPass()
        {
            string passwordpath = @"Data/password.txt";
            if (PasswordInput.Password == Text_decryptor(File.ReadAllText(passwordpath)))
            {
                LoginScreen.Visibility = Visibility.Hidden;
                MainScreen.Visibility = Visibility.Visible;
                string path = @"Data\Passwords.txt";
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

        // ввод паролей в главном окне
        private void Passwords_Box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txt_Input(Passwords_Box.Text);
                Passwords_Box.Text = "";
            }
        }

        // ввод введенного текста в документ
        // необходимо сделать шифрование паролей
        private void txt_Input(string text)
        {
            string path = @"Data\Passwords.txt";

            if (File.Exists(path))
            {
                string contents = Text_encryptor(text) + "\n";
                File.AppendAllText(path, contents);
                txt_Output();
            }
            if (!File.Exists(path))
            {
                string contents = Text_encryptor(text) + "\n";
                File.WriteAllText(path, contents);
                txt_Output();
            }
        }

        // вывод текста из документа
        // необходимо сделать дешифрование паролей
        public void txt_Output()
        {
            string path = @"Data\Passwords.txt";
            Passwords_Block.Text = null;
            if (File.Exists(path))
            {   
                for (int i = 0; i < File.ReadAllText(path).Split('\n').Count(); i++)
                Passwords_Block.Text += Text_decryptor(File.ReadAllText(path).Split('\n')[i])+"\n";
            }
        }

        // создание пароля
        private void gen_password()
        {
            string path = @"Data/password.txt";
            string content = Text_encryptor(GenPasswordBlock.Text);
            File.WriteAllText(path, content);
            MainScreen.Visibility = Visibility.Visible;
            PasswordScreen.Visibility = Visibility.Hidden;
        }


        // шифрование паролей

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

            string encrypted = "";
            char[] textarr = text.ToCharArray();

            for (int i = 0; i < textarr.Count(); i++)
            {

                for (int j = 0; j < arr.Length; j++)
                {

                    int x = new Random().Next(j, arr.Count());
                    if (arr[j] == textarr[i]) // PYTHON BLYAT
                    {
                        encrypted += Convert.ToString(arr[x - j]) + Convert.ToString(arr[x]);
                        break;
                    }
                }
            }
            return encrypted;
        }

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

        private void GenPasswordBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { gen_password(); }
        }
    }
}
