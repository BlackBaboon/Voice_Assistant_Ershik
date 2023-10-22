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
using System.Data.SqlClient;

namespace Ershik
{
    /// <summary>
    /// Логика взаимодействия для Phrase_Editor.xaml
    /// </summary>
    public partial class Phrase_Editor : Page
    {        
        List<string[]> Phrases = new List<string[]>();
        int Page = 0;
        public Phrase_Editor()
        {
            InitializeComponent();
            Load_Phrases();
            Load_Page();
        }
        private void Load_Phrases()
        {
            Phrases.Clear();
            SqlCommand cmd = new SqlCommand($"select * from Phrase where Profile_name = '{App.Current_profile_name}'"
                , App.Connection);

            App.Connection.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Phrases.Add(new string[2] {
                    rdr.GetString(1),
                    rdr.GetString(2)
                });
            }
            App.Connection.Close();
            Page = 0;
        }

        private void Load_Page()
        {
            if (Phrases.Count == 0)
            {
                Forward_Button.Visibility = Visibility.Hidden;
                Back_Button.Visibility= Visibility.Hidden;

                Edit_Button.Visibility = Visibility.Hidden;
                Delete_Button.Visibility = Visibility.Hidden;
                Phrase.Visibility = Visibility.Hidden;
                Description.Visibility = Visibility.Hidden;

                Edit_Button1.Visibility = Visibility.Hidden;
                Delete_Button1.Visibility = Visibility.Hidden;
                Phrase1.Visibility = Visibility.Hidden;
                Description1.Visibility = Visibility.Hidden;

                return;
            }
            else
            {
                Forward_Button.Visibility = Visibility.Visible;
                Back_Button.Visibility = Visibility.Visible;

                Edit_Button.Visibility = Visibility.Visible;
                Delete_Button.Visibility = Visibility.Visible;
                Phrase.Visibility = Visibility.Visible;
                Description.Visibility = Visibility.Visible;
            }

            Phrase.Text = Phrases[Page * 2][0];
            Description.Text = Phrases[Page * 2][1];

            if (Page * 2 + 1 > Phrases.Count - 1)
            {
                Edit_Button1.Visibility = Visibility.Hidden;
                Delete_Button1.Visibility = Visibility.Hidden;
                Phrase1.Visibility = Visibility.Hidden;
                Description1.Visibility = Visibility.Hidden;

                Phrase1.Text = "";
                Description1.Text = "";
            }
            else
            {
                Edit_Button1.Visibility = Visibility.Visible;
                Delete_Button1.Visibility = Visibility.Visible;
                Phrase1.Visibility = Visibility.Visible;
                Description1.Visibility = Visibility.Visible;
                
                Phrase1.Text = Phrases[Page * 2 + 1][0];
                Description1.Text = Phrases[Page * 2 + 1][1];                
            }
        }

        private void GoBack_Button_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.GoBack();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            Database_interaction.Delete.Delete_Phrase(Phrase.Text);

            Load_Phrases();
            Load_Page();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Edit_Phrase edit_Phrase = new Edit_Phrase(Phrase.Text, Description.Text);
            if (edit_Phrase.ShowDialog() == true)
            {
                MessageBox.Show("Фраза успешно изменена");
                Load_Phrases();
                Load_Page();
            }
        }

        private void Delete_Button1_Click(object sender, RoutedEventArgs e)
        {
            Database_interaction.Delete.Delete_Phrase(Phrase1.Text);

            Load_Phrases();
            Load_Page();
        }

        private void Edit_Button1_Click(object sender, RoutedEventArgs e)
        {
            Edit_Phrase edit_Phrase = new Edit_Phrase(Phrase1.Text, Description1.Text);
            if (edit_Phrase.ShowDialog() == true)
            {
                MessageBox.Show("Фраза успешно изменена");
                Load_Phrases();
                Load_Page();
            }
        }

        private void Forward_Button_Click(object sender, RoutedEventArgs e)
        {
            Page++;
            if (Page * 2 > Phrases.Count - 1)
                Page--;
            Load_Page();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Page--;
            if (Page < 0)
                Page = 0;
            Load_Page();
        }

        private void Insert_Phrase_btn_Click(object sender, RoutedEventArgs e)
        {
            Add_Phrase add_Phrase = new Add_Phrase();
            if (add_Phrase.ShowDialog() == true)
            {
                MessageBox.Show("Фраза успешно добавлена");
                Load_Phrases();
                Load_Page();
            }
        }
    }
}
