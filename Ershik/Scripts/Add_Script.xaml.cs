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
    /// Логика взаимодействия для Add_Script.xaml
    /// </summary>
    public partial class Add_Script : Window
    {
        public Add_Script()
        {
            InitializeComponent();
        }
        private void Add_Script_Click(object sender, RoutedEventArgs e)
        {
            if(Script.Text.Length > 24 || Script.Text.Trim().Length < 4)
            {
                MessageBox.Show("Длина названия скрипта не может превышать 24 символов и быть короче 4");
                return;
            }
            if (Desc.Text.Length > 200)
            {
                MessageBox.Show("Длина описания скрипта не может превышать 200");
                return;
            }
            try
            {
                Database_interaction.Add.Insert_Script(Script.Text, Desc.Text);
                this.DialogResult = true;
            }
            catch
            {
                MessageBox.Show("Ошибка добавления скрипта");
                this.DialogResult = false;
                this.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
