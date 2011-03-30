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
            throw new NotImplementedException();
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
