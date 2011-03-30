using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.IO;

namespace ChurchView2EasyWorship
{
    class Program
    {
        static Dictionary<string, SongParts> x = new Dictionary<string, SongParts> {
            { "Song", SongParts.Chorus1 },
            { "Chorus2", SongParts.Chorus2 },
            { "Chorus3", SongParts.Chorus3 },
            { "Chorus4", SongParts.Chorus4 },
            { "Verse1", SongParts.Verse1 },
            { "Verse2", SongParts.Verse2 },
            { "Verse3", SongParts.Verse3 },
            { "Verse4", SongParts.Verse4 }
        };

        static void Main(string[] args)
        {
            // set up variables with default values
            string dbName = ConfigurationManager.AppSettings["DefaultDbName"];
            string folderName = ConfigurationManager.AppSettings["DefaultFolderName"];

            // prepare for songs
            List<Song> songs = new List<Song>();

            // get argument list
            if (args.Length > 0)
                dbName = args[0];
            if (args.Length > 1)
                folderName = args[1];

            // build the connection string
            DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
            builder.ConnectionString = ConfigurationManager.ConnectionStrings["Access"].ConnectionString;
            builder.Add("Data Source", dbName);

            // get a new connection
            using (IDbConnection conn = new System.Data.OleDb.OleDbConnection(builder.ConnectionString))
            {
                conn.Open();

                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT SongTitle, Song, Chorus2, Chorus3, Chorus4, Verse1, Verse2, Verse3, Verse4 FROM Songs";
                
                using(IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Song currSong;
                    while (reader.Read())
                    {
                        currSong = new Song(reader.GetString(0));

                        for (int i = 1; i < reader.FieldCount; i++)
                            if (!reader.IsDBNull(i))
                                currSong.Parts.Add(x[reader.GetName(i)], reader.GetString(i));

                        songs.Add(currSong);
                    }
                }
            }

            foreach (var song in songs)
                song.Serialize(File.OpenWrite(Path.Combine(folderName, MakeValidFileName(song.Name))));
        }

        private static string MakeValidFileName(string name)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"[{0}]+", invalidChars);
            return Regex.Replace(name, invalidReStr, "_");
        }
    }
}
