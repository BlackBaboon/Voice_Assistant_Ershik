using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Ershik
{
    public static class Database_interaction
    {
        public static class Add
        {
            public static void Insert_Profile(string profile, string login, string password)
            {
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"exec Insert_Profile '{profile}', '{login}', '{password}'", App.Connection);

                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
            public static void Insert_Phrase(string phrase, string desc)
            {
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"exec Insert_Phrase " +
                    $"'{App.Current_profile_name}','{phrase}','{desc}'", App.Connection);

                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
            public static void Insert_Script(string script, string desc)
            {
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"exec Insert_Script " +
                    $"'{App.Current_profile_name}','{script}','{desc}'", App.Connection);
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }

            public static void Insert_Command(string script,string command)
            {
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"exec Insert_Command " +
                    $"'{App.Current_profile_name}','{script}','{command}'", App.Connection);
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }

            public static void Bind_Phrase_Script(string phrase,string script)
            {
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"exec Bind_Phrase_Script " +
                    $"'{App.Current_profile_name}','{phrase}','{script}'", App.Connection);
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
        }

        public static class Delete
        {
            public static void Delete_Profile(string profile)
            {
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"exec Delete_Profile {profile}'", App.Connection);

                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
            public static void Delete_Phrase(string phrase)
            {
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"exec Delete_Phrase " +
                    $"'{App.Current_profile_name}','{phrase}'", App.Connection);

                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
            public static void Delete_Script(string script)
            {
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"exec Delete_Script " +
                    $"'{App.Current_profile_name}','{script}'", App.Connection);

                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }

            public static void Delete_Commands(string script)
            {
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"exec Delete_Commands " +
                    $"'{App.Current_profile_name}','{script}'", App.Connection);

                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }

            public static void UnBind_Phrase(string phrase)
            {
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"delete from Script_bind where " +
                    $"Profile_name = '{App.Current_profile_name}' and Phrase = '{phrase}'", App.Connection);

                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
        }

        public static class Change
        {
            public static void Change_Profile(string login, string password)
            {
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"exec Change_Profile " +
                    $"'{App.Current_profile_name}','{login}','{password}'", App.Connection);

                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
            public static void Change_Phrase(string old_phrase, string new_phrase, string desc)
            {
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"exec Change_Phrase " +
                    $"'{App.Current_profile_name}','{old_phrase}','{new_phrase}','{desc}'", App.Connection);

                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
            public static void Change_Script(string old_script, string new_script, string desc)
            {
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"exec Change_Script " +
                    $"'{App.Current_profile_name}','{old_script}','{new_script}','{desc}'", App.Connection);

                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
        }
        public static class Get
        {
            public static List<string[]> Get_Scripts()
            {
                List<string[]> Scripts = new List<string[]>();

                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"select * from Script where Profile_name = '{App.Current_profile_name}'"
                    , App.Connection);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Scripts.Add(new string[2] {
                    rdr.GetString(1),
                    rdr.GetString(2)
                });
                }
                App.Connection.Close();
                return Scripts;
            }
            public static List<string[]> Get_Phrases()
            {
                List<string[]> Phrases = new List<string[]>();
                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"select * from Phrase where Profile_name = '{App.Current_profile_name}'"
                                , App.Connection);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Phrases.Add(new string[2] {
                    rdr.GetString(1),
                    rdr.GetString(2)
                });
                }
                App.Connection.Close();
                return Phrases;
            }
            public static List<string> Get_Binds(string phrase)
            {
                List<string> Binds = new List<string>();

                App.Connection.Open();
                SqlCommand cmd = new SqlCommand($"select Script_caption from Script_bind where Profile_name = '{App.Current_profile_name}' and Phrase = '{phrase}'"
                    , App.Connection);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Binds.Add(rdr.GetString(0));
                }
                App.Connection.Close();
                return Binds;
            }
            public static List<string> Get_Command(string script)
            {
                List<string> Commands = new List<string>();
                SqlCommand cmd = new SqlCommand($"select Command from Command_sequence where " +
                    $"Profile_name = '{App.Current_profile_name}' and " +
                    $"Script_Caption = '{script}'", App.Connection);

                App.Connection.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                   Commands.Add(r.GetString(0));
                }
                App.Connection.Close();
                return Commands;
            }
            public static List<string> Get_Credentials()
            {
                List<string> Credentials = new List<string>();
                SqlCommand cmd = new SqlCommand($"select " +
                    $"Login, CONVERT(nvarchar(24), DECRYPTBYASYMKEY(ASYMKEY_ID('LoginInformationKey'), Password, N'14881488')) from Profile where " +
                    $"Profile_name = '{App.Current_profile_name}'", App.Connection);

                App.Connection.Open();
                var r = cmd.ExecuteReader();
                r.Read();
                Credentials.Add(r.GetString(0));
                Credentials.Add(r.GetString(1));
                App.Connection.Close();
                return Credentials;
            }

            public static List<string> Get_Profiles()
            {
                List<string> Profiles = new List<string>();

                SqlCommand cmd = new SqlCommand($"select Profile_name from Profile", App.Connection);

                App.Connection.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Profiles.Add(r.GetString(0));
                }
                App.Connection.Close();
                return Profiles;
            }
        }

        public static class Execute
        {
            public static void Execute_Script(string script, bool byscript)
            {
                List<string> Commands;
                List<string> Credentials;
                Commands = Database_interaction.Get.Get_Command(script);
                Credentials = Database_interaction.Get.Get_Credentials();
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
                if (byscript)
                {
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
            public static void Execute_Phrase(string phrase,bool bytest)
            {
                List<string> Scripts = new List<string>();
                App.Connection.Open();

                SqlCommand cmd = new SqlCommand($"select Script_caption from Script_bind where Phrase = '{phrase}'", App.Connection);

                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Scripts.Add(r.GetString(0));
                }
                App.Connection.Close();
                foreach (string script in Scripts)
                {
                    Execute_Script(script, false);
                }
                if(bytest)
                    MessageBox.Show("Фраза выполнена");
            }
        }
    }
}
