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
        private Dictionary<SongParts, string> partNames = new Dictionary<SongParts, string> {
            { SongParts.Chorus1, "Chorus 1" },
            { SongParts.Chorus2, "Chorus 2" },
            { SongParts.Chorus3, "Chorus 3" },
            { SongParts.Chorus4, "Chorus 4" },
            { SongParts.Verse1, "Verse 1" },
            { SongParts.Verse2, "Verse 2" },
            { SongParts.Verse3, "Verse 3" },
            { SongParts.Verse4, "Verse 4" }
        };

        public string Name { get; set; }
        public Dictionary<SongParts, string> Parts { get; set; }

        /// <summary>
        /// Instantiates a new <see cref="Song"/>.
        /// </summary>
        /// <param name="name">The song name.</param>
        public Song(string name)
        {
            Name = name;
            Parts = new Dictionary<SongParts, string>();
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
                }

                writer.WriteLine();
            }
        }
    }

    /// <summary>
    /// List of song parts.
    /// </summary>
    public enum SongParts
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
