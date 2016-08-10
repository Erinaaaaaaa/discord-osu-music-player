using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using osu_Music_Parser.Properties;

namespace osu_Music_Parser
{
    public class osuMusicParser
    {
        static private string databasePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ARFHT\osu!music discord bot\songs.sqlite";
        static private SQLiteConnection songsDatabase;

        static public void LoadDatabase()
        {
            if (File.Exists(databasePath)==false)
            {
                SQLiteConnection.CreateFile("songs.sqlite");
                songsDatabase = new SQLiteConnection("Data Source = " + databasePath + "; Version = 3");
                songsDatabase.Open();
                string sql = "create table songs (audioFile TEXT, beatmaps TEXT, songName TEXT, songArtist TEXT)";
                SQLiteCommand command = new SQLiteCommand(sql, songsDatabase);
                command.ExecuteNonQuery();
            } else
                songsDatabase = new SQLiteConnection("Data Source = "+databasePath+"; Version = 3");
        }
        static public void ScanSongs()
        {
            while (Settings.Default.osuPath=="")
            {
                Settings.Default.osuPath = Prompt.ShowDialog("Enter the path of your osu! folder", "osu! folder path");
            }
            DirectoryInfo osu = new DirectoryInfo(Settings.Default.osuPath + @"\Songs");
            var directories = osu.GetDirectories();
            foreach (var folder in directories)
            {
                
            }
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string content, string caption)
        {
            Form prompt = new Form()
            {
                Width = 490,
                Height = 171,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                Font = new System.Drawing.Font("Segoe UI", 9F),
                MaximizeBox = false,
                MinimizeBox = false,
                ShowInTaskbar = false,
                ShowIcon = false
        };
            Label text = new Label() { Left = 20, Top = 20, Width = 350, Text = content };
            TextBox textBox = new TextBox() { Left = 23, Top = 50, Width = 322, BorderStyle = BorderStyle.Fixed3D };
            Button showPathPrompt = new Button() { Text = "Select folder", Left = 351, Width = 111, Top = 50, FlatStyle = FlatStyle.System};
            Button confirmation = new Button() { Text = "Ok", Left = 387, Width = 75, Top = 97, DialogResult = DialogResult.OK, FlatStyle = FlatStyle.System };
            FolderBrowserDialog pathSelect = new FolderBrowserDialog();
            confirmation.Click += (sender, e) => { prompt.Close(); };
            showPathPrompt.Click += (sender, e) =>
            {
                pathSelect.ShowDialog();
                textBox.Text = pathSelect.SelectedPath;
            };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(text);
            prompt.Controls.Add(showPathPrompt);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
