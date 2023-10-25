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
            if (Password.Password == Password_1.Password)
                if (Login.Text != "")
                {
                    Database_interaction.Add.Insert_Profile(Profile_name.Text, Login.Text, Password.Password);
                }
                else
                {
                    Database_interaction.Add.Insert_Profile(Profile_name.Text, "","");
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
