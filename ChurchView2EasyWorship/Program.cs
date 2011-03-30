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
        static Dictionary<string, SongPart> x = new Dictionary<string, SongPart> {
            { "Song", SongPart.Chorus1 },
            { "Chorus2", SongPart.Chorus2 },
            { "Chorus3", SongPart.Chorus3 },
            { "Chorus4", SongPart.Chorus4 },
            { "Verse1", SongPart.Verse1 },
            { "Verse2", SongPart.Verse2 },
            { "Verse3", SongPart.Verse3 },
            { "Verse4", SongPart.Verse4 }
        };

        static List<string> y = new List<string> {
            "Jesus", "God", "King", "I", "Lord", "You", "Your", "He", "His", "Him", "Spirit",
            "Zion", "King of Kings", "Lord of Lords", "Ancient of Days", "Jireh", "Nissi", "Shalom",
            "Rock of Ages", "Emmanuel"
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

            // ensure database exists
            if (!File.Exists(dbName))
                throw new FileNotFoundException("Could not find database file!", dbName);

            // ensure folder exists (or create it)
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

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
                        currSong = new Song(
                            System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Regex.Replace(reader.GetString(0).ToLower(), @"\s+", " "))
                        );

                        for (int i = 1; i < reader.FieldCount; i++)
                            if ((!reader.IsDBNull(i)) && (!string.IsNullOrWhiteSpace(reader.GetString(i))))
                                currSong.Parts.Add(x[reader.GetName(i)], Normalize(reader.GetString(i), y));

                        songs.Add(currSong);
                    }
                }
            }

            foreach (var song in songs)
                song.Serialize(File.Open(Path.Combine(folderName, MakeValidFileName(song.Name + ".txt")), FileMode.Truncate, FileAccess.Write, FileShare.Write));
        }

        /// <summary>
        /// Returns a case-normalized version of the given string.
        /// </summary>
        /// <param name="input"></param>
        private static string Normalize(string input, List<string> capWords)
        {
            StringBuilder builder = new StringBuilder();
            using(StringReader reader = new StringReader(input))
            using(StringWriter writer = new StringWriter(builder))
            {
                string line;
                while((line=reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.Length == 0) continue;

                    line = line.Substring(0, 1).ToUpper() + Regex.Replace(line.Substring(1).ToLower(), @"\s+", " ");
                    foreach (string word in capWords)
                        line = Regex.Replace(line, string.Format(@"\b{0}\b", word), word, RegexOptions.IgnoreCase);
                    
                    writer.WriteLine(line);
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Sanitizes input so it can be used as a filename.
        /// </summary>
        /// <param name="name">The filename.</param>
        /// <returns>Sanitized filename.</returns>
        /// <remarks>
        /// Lifted verbatim from StackOverflow.
        /// </remarks>
        private static string MakeValidFileName(string name)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"[{0}]+", invalidChars);
            return Regex.Replace(name, invalidReStr, "_");
        }
    }
}
