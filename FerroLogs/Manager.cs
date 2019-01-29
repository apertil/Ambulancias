using System;

namespace FerroLogs
{
    public class Manager : IDisposable
    {
        #region *** Constructors ***
        /// <summary>
        /// Default constructor
        /// </summary>
        public Manager() { }
        /// <summary>
        /// Constructor
        /// Inicializa prop LogPath
        /// </summary>
        /// <param name="sLogPath"></param>
        public Manager(string sLogPath) => LogPath = sLogPath;
        #endregion

        #region *** Local Variables *** 
        #endregion

        #region *** Properties *** 
        public string LogPath { get; set; }
        public bool LogDateTime { get; set; } = true;
        public bool AppendToFile { get; set; } = true;

        #endregion

        #region *** Public Methods *** 
        public void WriteLog(string sMessage) => this.WriteLog(sMessage, "Log_");

        /// <summary>
        /// Escribe a fichero log
        /// </summary>
        /// <param name="sMessage"></param>
        /// <param name="sFilePrefix"></param>
        public void WriteLog(string sMessage, string sFilePrefix)
        {
            System.IO.StreamWriter oSw;
            string sTime;
            DateTime oDt;
            string sFile;
            string sPath = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(LogPath))
                {
                    sPath = System.IO.Directory.GetCurrentDirectory();
                    if (!sPath.EndsWith("\\")) { sPath += "\\"; }
                    sPath += "logs";
                }
                else
                {
                    sPath = this.LogPath;
                }

                if (!System.IO.Directory.Exists(sPath)) { System.IO.Directory.CreateDirectory(sPath); }

                if (!sPath.EndsWith("\\")) { sPath += "\\"; }

                oDt = DateTime.Now;
                sFile = sFilePrefix + oDt.ToString("yyyyMMdd") + ".txt";
                sPath += sFile;

                oSw = new System.IO.StreamWriter(sPath, true);

                try
                {
                    if (this.LogDateTime)
                    {
                        oDt = DateTime.Now;
                        sTime = "[" + oDt.ToString("yyyy/MM/dd HH:mm:ss") + "]";
                    }
                    else
                    {
                        sTime = string.Empty;
                    }

                    oSw.WriteLine(sTime + sMessage);
                    oSw.Close();
                    oSw.Dispose();
                }
                catch (Exception)
                {
                    oSw.Close();
                    oSw.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region *** Private Methods *** 
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: elimine el estado administrado (objetos administrados).
                }
                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.
                disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~Manager() {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
