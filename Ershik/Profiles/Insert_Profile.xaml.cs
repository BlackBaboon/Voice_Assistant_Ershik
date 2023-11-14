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

namespace Ershik
{
    /// <summary>
    /// Логика взаимодействия для Insert_Profile.xaml
    /// </summary>
    public partial class Insert_Profile : Window
    {
        public Insert_Profile()
        {
            InitializeComponent();
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if ( !(Password.Password.All(x=>Char.IsLetterOrDigit(x) || Char.IsWhiteSpace(x) || x == '_' || x == '-') 
                &
                Profile_name.Text.All(x => Char.IsLetterOrDigit(x) || Char.IsWhiteSpace(x) || x == '_' || x == '-')
                &
                Login.Text.All(x => Char.IsLetterOrDigit(x) || Char.IsWhiteSpace(x) || x == '_' || x == '-')) )
            {
                MessageBox.Show("Текстовые поля могут содержать только буквы и цифры, а также символы '-, _'");
                return;
            }

            if(Profile_name.Text.Length > 16 || Profile_name.Text.Length < 4)
            {
                MessageBox.Show("Длина профиля не может превышать 16 символов и быть короче 4");
                return;
            }

            if (Login.Text.Length > 24)
            {
                MessageBox.Show("Длина логина для выхода не может превышать 24 символов");
                return;
            }

            if (Password.Password == Password_1.Password)
                if (Login.Text != "")
                {
                    Database_interaction.Add.Insert_Profile(Profile_name.Text, Login.Text, Password.Password);
                }
                else
                {
                    Database_interaction.Add.Insert_Profile(Profile_name.Text, "", "");
                }
            else
                MessageBox.Show("Пароли не совпадают");
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
