using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ChurchView2EasyWorship
{
    /// <summary>
    /// Defines a single song.
    /// Associates a song name with its constituent parts.
    /// </summary>
    public class Song
    {
        private Dictionary<SongPart, string> partNames = new Dictionary<SongPart, string> {
            { SongPart.Chorus1, "Chorus 1" },
            { SongPart.Chorus2, "Chorus 2" },
            { SongPart.Chorus3, "Chorus 3" },
            { SongPart.Chorus4, "Chorus 4" },
            { SongPart.Verse1, "Verse 1" },
            { SongPart.Verse2, "Verse 2" },
            { SongPart.Verse3, "Verse 3" },
            { SongPart.Verse4, "Verse 4" }
        };

        public string Name { get; set; }
        public Dictionary<SongPart, string> Parts { get; set; }

        /// <summary>
        /// Instantiates a new <see cref="Song"/>.
        /// </summary>
        /// <param name="name">The song name.</param>
        public Song(string name)
        {
            Name = name;
            Parts = new Dictionary<SongPart, string>();
        }

        /// <summary>
        /// Returns a string representation of the song.
        /// </summary>
        public override string ToString()
        {
            return string.Format("<Song(\"{0}\")>", Name);
        }

        /// <summary>
        /// Outputs a text representation of the song to the given <see cref="System.IO.Stream"/>
        /// </summary>
        /// <param name="s">The stream.</param>
        public void Serialize(Stream s)
        {
            using (var writer = new StreamWriter(s))
            {
                writer.WriteLine(Name);
                writer.WriteLine();

                foreach (var part in Parts)
                {
                    writer.WriteLine(partNames[part.Key]);
                    writer.WriteLine(part.Value);
                    writer.WriteLine();
                }

                writer.WriteLine();
            }
        }
    }

    /// <summary>
    /// List of song parts.
    /// </summary>
    public enum SongPart
    {
        Chorus1,
        Chorus2,
        Chorus3,
        Chorus4,
        Verse1,
        Verse2,
        Verse3,
        Verse4
    }
}
