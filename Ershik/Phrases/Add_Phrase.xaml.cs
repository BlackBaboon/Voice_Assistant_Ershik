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
            if (!Phrase.Text.All(x=> Char.IsLetter(x) || Char.IsWhiteSpace(x)))
            {
                MessageBox.Show("Фраза дожна состоять только из букв, цифр или пробелов");
                return;
            } 
            if(Phrase.Text.Trim() == "")
            {
                MessageBox.Show("Фраза не может быть пустой");
                return;
            }
            if (Phrase.Text.Length > 24 || Phrase.Text.Length < 1)
            {
                MessageBox.Show("Длина фразы не может превышать 24 символов и быть короче 1");
                return;
            }
            if (Desc.Text.Length > 200)
            {
                MessageBox.Show("Длина описания не может превышать 200 символов");
                return;
            }
            try
            {
                Database_interaction.Add.Insert_Phrase(Phrase.Text, Desc.Text);
                this.DialogResult = true;
                App.All_Phrases = Database_interaction.Get.Get_Phrases().Select(x => x[0].ToLower()).ToList();
            }
            catch
            {
                MessageBox.Show("Ошибка добавления фразы");
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
