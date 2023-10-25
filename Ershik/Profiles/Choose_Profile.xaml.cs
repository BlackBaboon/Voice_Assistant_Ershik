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
    /// Логика взаимодействия для Choose_Profile.xaml
    /// </summary>
    public partial class Choose_Profile : Window
    {
        public Choose_Profile()
        {
            InitializeComponent();
            Load_Profiles();
        }
        public void Load_Profiles()
        {
            Profiles.ItemsSource = Database_interaction.Get.Get_Profiles()
                .Select(p => new TextBlock() { Text = p, FontSize = 20 });
            Profiles.SelectedIndex = 0;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            App.Current_profile_name = ((TextBlock)Profiles.SelectedItem).Text.ToString();
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Insert_Profile_Click(object sender, RoutedEventArgs e)
        {
            Insert_Profile insert = new Insert_Profile();
            insert.ShowDialog();
            if (insert.DialogResult == true)
                MessageBox.Show("Профиль успешно добавлен"); 
            Load_Profiles();
        }
    }
}
