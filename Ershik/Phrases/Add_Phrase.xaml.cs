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
    /// Логика взаимодействия для Add_Phrase.xaml
    /// </summary>
    public partial class Add_Phrase : Window
    {
        public Add_Phrase()
        {
            InitializeComponent();
        }

        private void Add_Phrase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database_interaction.Add.Insert_Phrase(Phrase.Text, Desc.Text);
                this.DialogResult = true;
                App.All_Phrases = Database_interaction.Get.Get_Phrases().Select(x => x[0].ToLower()).ToList();
            }
            catch
            {
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
