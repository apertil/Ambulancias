using System;
using System.Collections.Generic;
using System.Text;

namespace FerroFTS.Data
{
   public class last_location : FerroFTS.Data.location
    {
        public last_location() { }

        public last_location(FerroFTS.Data.location oLocation)
        {
            try
            {
                // Establece propiedades con el mismo nombre (como Inherits son del mismo tipo)
                foreach (System.Reflection.PropertyInfo oServ in oLocation.GetType().GetProperties())
                {
                    System.Reflection.PropertyInfo oPropMe = this.GetType().GetProperty(oServ.Name);
                    if (oPropMe != null)
                        oPropMe.SetValue(this, oServ.GetValue(oLocation));
                }
            }

            catch (Exception ex)
            {
                throw new Exception((ex.StackTrace.Substring((ex.StackTrace.LastIndexOf("\r\n") + 4)) + ("\r\n" + ex.Message)));
            }
        }



    }
}
