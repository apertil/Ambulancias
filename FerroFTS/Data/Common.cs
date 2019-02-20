using System;
using System.Collections.Generic;
using System.Text;

namespace FerroFTS.Data
{
    public class Common
    {
        internal static DateTime ParseUTC(DateTime dDateTime)
        {
            DateTime dDateTimeUTC;
            try
            {
                switch (dDateTime.Kind)
                {
                    case DateTimeKind.Local:
                        dDateTimeUTC = dDateTime.ToUniversalTime();
                        break;
                    case DateTimeKind.Utc:
                        dDateTimeUTC = dDateTime;
                        break;
                    default:
                        dDateTime = System.DateTime.SpecifyKind(dDateTime, DateTimeKind.Local);
                        dDateTimeUTC = dDateTime.ToUniversalTime();
                        break;
                }
                return dDateTimeUTC;
            }
            catch (Exception ex)
            {
                throw new Exception((ex.StackTrace.Substring((ex.StackTrace.LastIndexOf("\r\n") + 4)) + ("\r\n" + ex.Message)));
            }

        }
    }
}
