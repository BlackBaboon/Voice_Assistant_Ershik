using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ershik
{
    public static class Database_interaction
    {
        public static class Add
        {
            public static void Insert_Profile(string profile, string login, string password)
            {
                SqlCommand cmd = new SqlCommand($"exec Insert_Profile " +
                    $"'{profile}', {login}', {password}'", App.Connection);

                App.Connection.Open();
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
            public static void Insert_Phrase(string phrase, string desc)
            {
                SqlCommand cmd = new SqlCommand($"exec Insert_Phrase " +
                    $"'{App.Current_profile_name}','{phrase}','{desc}'", App.Connection);

                App.Connection.Open();
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
            public static void Insert_Script(string script, string desc)
            {
                SqlCommand cmd = new SqlCommand($"exec Insert_Script " +
                    $"'{App.Current_profile_name}','{script}','{desc}'", App.Connection);

                App.Connection.Open();
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }

            public static void Insert_Command(string script,string command)
            {
                SqlCommand cmd = new SqlCommand($"exec Insert_Command " +
                    $"'{App.Current_profile_name}','{script}','{command}'", App.Connection);

                App.Connection.Open();
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }

            public static void Bind_Phrase_Script(string phrase,string script)
            {
                SqlCommand cmd = new SqlCommand($"exec Bind_Phrase_Script " +
                    $"'{App.Current_profile_name}','{phrase}','{script}'", App.Connection);

                App.Connection.Open();
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
        }

        public static class Delete
        {
            public static void Delete_Profile(string profile)
            {
                SqlCommand cmd = new SqlCommand($"exec Delete_Profile {profile}'", App.Connection);

                App.Connection.Open();
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
            public static void Delete_Phrase(string phrase)
            {
                SqlCommand cmd = new SqlCommand($"exec Delete_Phrase " +
                    $"'{App.Current_profile_name}','{phrase}'", App.Connection);

                App.Connection.Open();
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
            public static void Delete_Script(string script)
            {
                SqlCommand cmd = new SqlCommand($"exec Delete_Script " +
                    $"'{App.Current_profile_name}','{script}'", App.Connection);

                App.Connection.Open();
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }

            public static void Delete_Command(string script, int number)
            {
                SqlCommand cmd = new SqlCommand($"exec Delete_Command " +
                    $"'{App.Current_profile_name}','{script}','{number}'", App.Connection);

                App.Connection.Open();
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }

            public static void UnBind_Phrase_Script(string phrase, string script, int number)
            {
                SqlCommand cmd = new SqlCommand($"exec UnBind_Phrase_Script " +
                    $"'{App.Current_profile_name}','{phrase}','{script}','{number}'", App.Connection);

                App.Connection.Open();
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
        }

        public static class Change
        {
            public static void Change_Profile(string login, string password)
            {
                SqlCommand cmd = new SqlCommand($"exec Change_Profile " +
                    $"'{App.Current_profile_name}','{login}','{password}'", App.Connection);

                App.Connection.Open();
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
            public static void Change_Phrase(string old_phrase, string new_phrase, string desc)
            {
                SqlCommand cmd = new SqlCommand($"exec Change_Phrase " +
                    $"'{App.Current_profile_name}','{old_phrase}','{new_phrase}','{desc}'", App.Connection);

                App.Connection.Open();
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
            public static void Change_Script(string old_script, string new_script, string desc)
            {
                SqlCommand cmd = new SqlCommand($"exec Change_Script " +
                    $"'{App.Current_profile_name}','{old_script}','{new_script}','{desc}'", App.Connection);

                App.Connection.Open();
                cmd.ExecuteNonQuery();
                App.Connection.Close();
            }
        }
    }
}
