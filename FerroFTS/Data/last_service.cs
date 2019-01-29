using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;


namespace FerroFTS.Data
{
    public class last_service : FerroFTS.Data.service
    {
        public last_service()
        { }
       
        public last_service(FerroFTS.Data.service oService)
        {
            try
            {
                // Establece propiedades con el mismo nombre (como Inherits son del mismo tipo)
                foreach (System.Reflection.PropertyInfo oProp in oService.GetType().GetProperties())
                {
                    System.Reflection.PropertyInfo oPropMe = this.GetType().GetProperty(oProp.Name);
                    if (oPropMe != null)
                        oPropMe.SetValue(this, oProp.GetValue(oService));
                }
            }
        
            catch (Exception ex)
            {
                throw new Exception((ex.StackTrace.Substring((ex.StackTrace.LastIndexOf("\r\n") + 4)) + ("\r\n" + ex.Message)));
            }

        }



    }
}
