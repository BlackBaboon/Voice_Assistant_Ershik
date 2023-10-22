﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Diagnostics;
using System.Security;

namespace Ershik
{
    /// <summary>
    /// Логика взаимодействия для Script_Editor.xaml
    /// </summary>
    public partial class Script_Editor : Page
    {
        public Script_Editor()
        {
            InitializeComponent();
            Load_Scripts();
            Load_Page();
        }
        List<string[]> Scripts = new List<string[]>();
        int Page = 0;

        private void Load_Scripts()
        {
            Scripts.Clear();
            SqlCommand cmd = new SqlCommand($"select * from Script where Profile_name = '{App.Current_profile_name}'"
                , App.Connection);

            App.Connection.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Scripts.Add(new string[2] {
                    rdr.GetString(1),
                    rdr.GetString(2)
                });
            }
            App.Connection.Close();
            Page = 0;
        }

        private void Load_Page()
        {
            if (Scripts.Count == 0)
            {
                Forward_Button.Visibility = Visibility.Hidden;
                Back_Button.Visibility = Visibility.Hidden;

                Edit_Button.Visibility = Visibility.Hidden;
                Delete_Button.Visibility = Visibility.Hidden;
                Script.Visibility = Visibility.Hidden;
                Description.Visibility = Visibility.Hidden;
                Open_Editor_Button.Visibility = Visibility.Hidden;
                Test_Button.Visibility = Visibility.Hidden;

                Edit_Button1.Visibility = Visibility.Hidden;
                Delete_Button1.Visibility = Visibility.Hidden;
                Script1.Visibility = Visibility.Hidden;
                Description1.Visibility = Visibility.Hidden;
                Open_Editor_Button1.Visibility = Visibility.Hidden;
                Test_Button1.Visibility = Visibility.Hidden;

                return;
            }
            else
            {
                Forward_Button.Visibility = Visibility.Visible;
                Back_Button.Visibility = Visibility.Visible;

                Edit_Button.Visibility = Visibility.Visible;
                Delete_Button.Visibility = Visibility.Visible;
                Script.Visibility = Visibility.Visible;
                Description.Visibility = Visibility.Visible;
                Open_Editor_Button.Visibility = Visibility.Visible;
                Test_Button.Visibility = Visibility.Visible;
            }

            Script.Text = Scripts[Page * 2][0];
            Description.Text = Scripts[Page * 2][1];

            if (Page * 2 + 1 > Scripts.Count - 1)
            {
                Edit_Button1.Visibility = Visibility.Hidden;
                Delete_Button1.Visibility = Visibility.Hidden;
                Script1.Visibility = Visibility.Hidden;
                Description1.Visibility = Visibility.Hidden;
                Open_Editor_Button1.Visibility = Visibility.Hidden;
                Test_Button1.Visibility = Visibility.Hidden;

                Script1.Text = "";
                Description1.Text = "";
            }
            else
            {
                Edit_Button1.Visibility = Visibility.Visible;
                Delete_Button1.Visibility = Visibility.Visible;
                Script1.Visibility = Visibility.Visible;
                Description1.Visibility = Visibility.Visible;
                Open_Editor_Button1.Visibility = Visibility.Visible;
                Test_Button1.Visibility = Visibility.Visible;

                Script1.Text = Scripts[Page * 2 + 1][0];
                Description1.Text = Scripts[Page * 2 + 1][1];
            }
        }

        private void GoBack_Button_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.GoBack();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            Database_interaction.Delete.Delete_Script(Script.Text);

            Load_Scripts();
            Load_Page();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Edit_Script edit_Script = new Edit_Script(Script.Text, Description.Text);
            if (edit_Script.ShowDialog() == true)
            {
                MessageBox.Show("Фраза успешно изменена");
                Load_Scripts();
                Load_Page();
            }
        }

        private void Delete_Button1_Click(object sender, RoutedEventArgs e)
        {
            Database_interaction.Delete.Delete_Script(Script1.Text);

            Load_Scripts();
            Load_Page();
        }

        private void Edit_Button1_Click(object sender, RoutedEventArgs e)
        {
            Edit_Script edit_Script = new Edit_Script(Script1.Text, Description1.Text);
            if (edit_Script.ShowDialog() == true)
            {
                MessageBox.Show("Фраза успешно изменена");
                Load_Scripts();
                Load_Page();
            }
        }

        private void Forward_Button_Click(object sender, RoutedEventArgs e)
        {
            Page++;
            if (Page * 2 > Scripts.Count - 1)
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

        private void Insert_Script_btn_Click(object sender, RoutedEventArgs e)
        {
            Add_Script add_Script = new Add_Script();
            if (add_Script.ShowDialog() == true)
            {
                MessageBox.Show("Скрипт успешно добавлен");
                Load_Scripts();
                Load_Page();
            }
        }
        private void Open_Editor_Button_Click(object sender, RoutedEventArgs e)
        {
            Command_Editor edit = new Command_Editor(Script.Text);
            edit.ShowDialog();
        }
        private void Open_Editor_Button1_Click(object sender, RoutedEventArgs e)
        {
            Command_Editor edit = new Command_Editor(Script1.Text);
            edit.ShowDialog();
            if (edit.DialogResult == true)
                MessageBox.Show("Изменения успешно внесены");
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            List<string> Commands;
            List<string> Credentials;
            if (((Button)sender).Name == "Test_Button")
            {
                Commands = Database_interaction.Get.Get_Command(Script.Text);
                Credentials = Database_interaction.Get.Get_Credentials();
            }
            else
            {
                Commands = Database_interaction.Get.Get_Command(Script1.Text);
                Credentials = Database_interaction.Get.Get_Credentials();
            }
            string output_errors = "";
            if (Credentials[0] == "")
            {

                foreach (var cmd_command in Commands)
                {
                    using (Process P = new Process())
                    {
                        P.StartInfo.FileName = "cmd.exe";
                        P.StartInfo.RedirectStandardInput = true;
                        P.StartInfo.RedirectStandardOutput = true;
                        P.StartInfo.CreateNoWindow = true;
                        P.StartInfo.UseShellExecute = false;
                        P.StartInfo.RedirectStandardOutput = true;
                        P.StartInfo.RedirectStandardError = true;
                        P.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
                        P.StartInfo.StandardErrorEncoding = Encoding.GetEncoding(866);

                        P.Start();

                        P.StandardInput.WriteLine(cmd_command);
                        P.StandardInput.Flush();
                        P.StandardInput.Close();
                        while (!P.StandardError.EndOfStream)
                        {
                            output_errors += cmd_command + " " + P.StandardError.ReadLine() + "\n";
                        }
                        P.WaitForExit();
                    }
                }
            }
            else
            {
                foreach (var cmd_command in Commands)
                {
                    SecureString password = new SecureString();
                    foreach (char ch in Credentials[1]) password.AppendChar(ch);
                    using (Process P = new Process())
                    {
                        P.StartInfo.UserName = Credentials[0];
                        P.StartInfo.Password = password;
                        P.StartInfo.FileName = "cmd.exe";
                        P.StartInfo.RedirectStandardInput = true;
                        P.StartInfo.RedirectStandardOutput = true;
                        P.StartInfo.CreateNoWindow = true;
                        P.StartInfo.UseShellExecute = false; 
                        P.StartInfo.RedirectStandardOutput = true;
                        P.StartInfo.RedirectStandardError = true;
                        P.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
                        P.StartInfo.StandardErrorEncoding = Encoding.GetEncoding(866);
                        P.Start();

                        P.StandardInput.WriteLine(cmd_command);
                        P.StandardInput.Flush();
                        P.StandardInput.Close();
                        while (!P.StandardError.EndOfStream)
                        {
                            output_errors += cmd_command + " " + P.StandardError.ReadLine() + "\n";
                        }
                        P.WaitForExit();
                    }
                }
            }

            if (output_errors == "")
                MessageBox.Show("Скрипт выполнен");
            else
            {
                MessageBoxResult result = MessageBox.Show("При выполнении скрипта были обнаружены ошибки, желаете их посмотреть?", "Найдены ошибки",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show(output_errors);
                }
            }

        }
    }
}
