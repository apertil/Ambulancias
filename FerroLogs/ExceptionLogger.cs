using System;
using System.Collections.Generic;
using System.Text;

namespace FerroLogs
{
    public class ExceptionLogger
    {
        #region *** Properties *** 
        #endregion

        #region *** Local Variables *** 
        #endregion

        #region *** Public Methods *** 
        public static void LogError(string sProcedure, Exception oEx, string sPath)
        {
            FerroLogs.Manager oLog = new FerroLogs.Manager();

            try
            {
                if (sPath != null && sPath != string.Empty)
                {
                    //Localización fichero logs
                    oLog.LogPath = sPath;

                    lock (oLog)
                    {
                        oLog.WriteLog("START " + sProcedure + " error...");
                        oLog.WriteLog(oEx.Message);
                        oLog.WriteLog("END error");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            finally
            {
                oLog.Dispose();
            }
        }

        #endregion

        #region *** Private Methods *** 
        #endregion
    }
}
