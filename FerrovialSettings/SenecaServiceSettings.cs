using System;
using System.Collections.Generic;
using System.Text;

namespace FerrovialSettings
{
    /// <summary>
    /// </summary>
    public class SenecaServiceSettings
    {
        /// <summary>
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// </summary>
        public string LogPath { get; set; }

        /// <summary>
        /// </summary>
        public bool ReturnException { get; set; } = false;

        /// <summary>
        /// </summary>
      //  public FerroServiceSettings.TokenJWT Token { get; set; }

        /// <summary>
        /// </summary>
        public FerrovialSettings.MongoDB MongoDB { get; set; } = null;
    }
}
