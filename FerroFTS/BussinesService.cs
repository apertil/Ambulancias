using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FerroFTS
{
    public class BussinesService : IDisposable
    {
        #region "Variables privadas"
           private FerroData.SessionMongo _Session;
        #endregion
        #region "Constructores"
        public BussinesService(string sConnectionString, string sDatabase)
        {
            this._Session = new FerroData.SessionMongo();
            this._Session.ConnectionString = sConnectionString;
            this._Session.Database = sDatabase;
        }
        #endregion
        #region "Métodos públicos GET"

        //public List<FerroFTS.Data.Last_location> GetLastLocations(string sOu, string sOuService)
        //{
        //    MongoDB.Driver.FilterDefinition<FerroFTS.Data.Last_location> oFilter;
        //    FerroData.Mongo.DataMongo< FerroFTS.Data.Last_location> oData = new FerroData.Mongo.DataMongo<FerroFTS.Data.Last_location>();
        //    MongoDB.Driver.FindOptions<FerroFTS.Data.Last_location> oOptions = new MongoDB.Driver.FindOptions<FerroFTS.Data.Last_location>();
        //    System.Collections.Generic.List<FerroFTS.Data.Last_location> oList;
        //    try
        //    {
        //        oFilter = new MongoDB.Bson.BsonDocument("ou", sOu);
        //        oFilter = (oFilter & new MongoDB.Bson.BsonDocument("ou_service", sOuService));
        //        oData.Session = this._Session;
        //        oList = oData.Find(oFilter, oOptions);
        //        return oList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception((ex.StackTrace.Substring((ex.StackTrace.LastIndexOf("\r\n") + 4)) + ("\r\n" + ex.Message)));
        //    }

        //}


        public List<FerroFTS.Data.last_service> GetLastServices(string sOu, string sOuService, DateTime sStartDate)
        {
            MongoDB.Driver.FilterDefinition<FerroFTS.Data.last_service> oFilter;
            FerroData.Mongo.DataMongo<FerroFTS.Data.last_service> oData = new FerroData.Mongo.DataMongo<FerroFTS.Data.last_service>();
            MongoDB.Driver.FindOptions<FerroFTS.Data.last_service> oOptions = new MongoDB.Driver.FindOptions<FerroFTS.Data.last_service>();
            List<FerroFTS.Data.last_service> oList = new List<Data.last_service>();

            try
            {
                oFilter = new MongoDB.Bson.BsonDocument("ou", sOu);
                oFilter = (oFilter & new MongoDB.Bson.BsonDocument("ou_service", sOuService));
                oFilter = (oFilter & new MongoDB.Bson.BsonDocument("start_date", new MongoDB.Bson.BsonDocument("$gte", sStartDate)));
                
                oData.Session = this._Session;
                oList = oData.Find(oFilter, oOptions);
                return oList;
            }
            catch (Exception ex)
            {

                throw new Exception((ex.StackTrace.Substring((ex.StackTrace.LastIndexOf("\r\n") + 4)) + ("\r\n" + ex.Message)));
            }
        }

        public List<FerroFTS.Data.service> GetVehicleServices(string sOu, string sOuService, DateTime sStartDate, DateTime sEndDate)
        {
            MongoDB.Driver.FilterDefinition<FerroFTS.Data.service> oFilter;
            FerroData.Mongo.DataMongo<FerroFTS.Data.service> oData = new FerroData.Mongo.DataMongo<FerroFTS.Data.service>();
            MongoDB.Driver.FindOptions<FerroFTS.Data.service> oOptions = new MongoDB.Driver.FindOptions<FerroFTS.Data.service>();
            List<FerroFTS.Data.service> oList = new List<Data.service>();

            try
            {
                //*****************************************************************
                //Convertimos fechas
                //*****************************************************************
                sStartDate = Data.Common.ParseUTC(sStartDate);
                sEndDate = Data.Common.ParseUTC(sEndDate);

                /*'*****************************************************************
                *Agregamos datos al filtro 
                ******************************************************************'*/
                oFilter = new MongoDB.Bson.BsonDocument("ou", sOu);
                oFilter = (oFilter & new MongoDB.Bson.BsonDocument("ou_service", sOuService));
                oFilter = (oFilter & new MongoDB.Bson.BsonDocument("utc_datetime", new MongoDB.Bson.BsonDocument("$gte", sStartDate)));
                oFilter = (oFilter & new MongoDB.Bson.BsonDocument("utc_datetime", new MongoDB.Bson.BsonDocument("$gte", sStartDate)));

                oData.Session = this._Session;
                oList = oData.Find(oFilter, oOptions);
                return oList;
            }
            catch (Exception ex)
            {

                throw new Exception((ex.StackTrace.Substring((ex.StackTrace.LastIndexOf("\r\n") + 4)) + ("\r\n" + ex.Message)));
            }
            
        }




        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (!(this._Session == null))
                    {
                        this._Session.Dispose();
                    }

                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~BussinesService() {
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
