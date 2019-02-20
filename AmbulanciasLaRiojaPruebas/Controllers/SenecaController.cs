using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AmbulanciasLaRiojaPruebas.Controllers
{
    /// <summary>
    /// seneca Controller
    /// </summary>
    [Route("api/seneca")]
    [ApiController]
    public class SenecaController : ControllerBase
    {
        /// <summary>
        /// Privadas cargadas con constructor inicialización _ServiceSettings
        /// </summary>
        private readonly IOptions<FerrovialSettings.SenecaServiceSettings> _ServiceSettings;
        private readonly IHttpClientFactory _HttpClientFactory;

        /// <summary>
        /// Constructor con inicialización _ServiceSettings, _httpClientFactory
        /// </summary>
        /// <param name="sServiceSettings"></param>
        /// <param name="shttpClientFactory"></param>

        public SenecaController(IOptions<FerrovialSettings.SenecaServiceSettings> sServiceSettings, IHttpClientFactory shttpClientFactory)
        {
            this._ServiceSettings = sServiceSettings;
            this._HttpClientFactory = shttpClientFactory;
        }

        /// <summary>
        /// Devolverá el último servicio activo, si no tiene fecha de finalización. 
        ///  No JWT authorization
        /// </summary>
        /// <param name="ou">Localización. Formato de cadena</param>
        /// <param name="ou_service">Tipo de Servicio. Formato de cadena</param>
        /// <param name="start_date">Local start date in ISO 8601 format (ex: 2019-01-16T10:15:00)</param>
        /// <returns>Devolverá el último servicio activo, si no tiene fecha de finalización. </returns>
        /// <response code="200">Locations found</response>
        /// <response code="204">No results found</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Server error</response>
        /// <response code="503">Service unavailable</response>
        [HttpGet]
        [RequireHttps]
        [Route("v1/last_services")]
        [ProducesResponseType(typeof(List<FerroFTS.Data.last_service>), StatusCodes.Status200OK)]
        public IActionResult get_last_servicesNoJWT([FromQuery]string ou, [FromQuery] string ou_service, DateTime start_date)
        {
            return GetLastServices(ou, ou_service, start_date);
        }


        /// <summary>
        /// Devuelve Todos los servicios de urgencias en una fecha dada
        /// </summary>
        /// <param name="ou">Localización. Formato de cadena</param>
        /// <param name="ou_service">Tipo de Servicio. Formato de cadena</param>
        /// <param name="ou_start_date">Local start date in ISO 8601 format (ex: 2019-01-16T10:15:00)</param>
        /// <returns></returns>
        /// <returns> Devuelve en formato JSON los datos de las últimas localizaciones de los vehículos.</returns>
        /// <response code="200">Locations found</response>
        /// <response code="204">No results found</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Server error</response>
        /// <response code="503">Service unavailable</response>
        [HttpGet]
        [RequireHttps]
        [Route("v1/last_locations")]
        [ProducesResponseType(typeof(List<FerroFTS.Data.last_location>), StatusCodes.Status200OK)]
        public IActionResult get_last_locations([FromQuery]string ou, [FromQuery] string ou_service, [FromQuery] DateTime ou_start_date)
        {
            return GetLastLocations(ou, ou_service, ou_start_date);
        }

        /// <summary>
        /// Devuelve en formato JSON los datos de las últimas localizaciones de un vehículo en concreto
        /// </summary>
        /// <param name="ou">Localización. Formato de cadena</param>
        /// <param name="ou_service">Tipo de Servicio. Formato de cadena</param>
        /// <param name="vehicle">Vehículo. Formato de cadena</param>
        /// <param name="ou_start_date">Tiempo inicial de la busqueda Formato fecha</param>
        /// <param name="ou_end_date">Tiempo final de la busqueda. Formato fecha</param>
        /// <returns>Devuelve en formato JSON los datos de las últimas localizaciones de un vehículo en concreto.</returns>
        /// <response code="200">Locations found</response>
        /// <response code="204">No results found</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Server error</response>
        /// <response code="503">Service unavailable</response>
        [HttpGet]
        [RequireHttps]
        [Route("v1/last_locations_vehicle")]
        [ProducesResponseType(typeof(List<FerroFTS.Data.last_location>), StatusCodes.Status200OK)]
        public IActionResult get_locations_dates([FromQuery]string ou, [FromQuery] string ou_service, [FromQuery] string vehicle, [FromQuery] DateTime ou_start_date, [FromQuery] DateTime ou_end_date)
        {
            return GetLocationsDates(ou, ou_service, vehicle, ou_start_date, ou_end_date);
        }

        /// <summary>
        /// Devuelve en formato JSON Esta opción devuelve todos los servicios que realiza un vehículo, un indicativo de vehículo o ambos, es decir .
        /// </summary>
        /// <param name="ou">Localización. Formato de cadena</param>
        /// <param name="idVehicle">Identificador del vehículo. Formato de cadena</param>
        /// <param name="vehicle">Tipo de Vehiculo</param>
        /// <param name="ou_start_date">Tiempo inicial de la busqueda Formato fecha</param>
        /// <param name="ou_end_date">Tiempo final de la busqueda. Formato fecha</param>
        /// <returns>Devuelve en formato JSON la ruta de un vehículo, por nombre del vehiculo o por identificacion.</returns>
        /// <response code="200">Locations found</response>
        /// <response code="204">No results found</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Server error</response>
        /// <response code="503">Service unavailable</response>
        [HttpGet]
        [RequireHttps]
        [Route("v1/vehicle_route")]
        [ProducesResponseType(typeof(List<FerroFTS.Data.last_location>), StatusCodes.Status200OK)]
        public IActionResult get_vehicle_route([FromQuery]string ou, [FromQuery] string idVehicle, [FromQuery] string vehicle,  [FromQuery] DateTime ou_start_date, [FromQuery] DateTime ou_end_date)
        {
            return GetVehicleRoute(ou, idVehicle, vehicle, ou_start_date, ou_end_date);
        }


        /// <summary>
        /// Vehiculos que cumplen un servicio en fechas determinadas.
        /// </summary>
        /// <param name="ou">Localización. Formato de cadena</param>
        /// <param name="ou_service">Tipo de Servicio. Formato de cadena</param>
        /// <param name="vehicle">Tipo de Vehiculo</param>
        /// <param name="ou_start_date">Tiempo inicial de la busqueda. Formato fecha</param>
        /// <param name="ou_end_date">Tiempo final de la busqueda. Formato fecha</param>
        /// <returns>Vehiculos que cumplen un servicio en fechas determinadas.</returns>
        /// <response code="200">Locations found</response>
        /// <response code="204">No results found</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Server error</response>
        /// <response code="503">Service unavailable</response>
        [HttpGet]
        [RequireHttps]
        [Route("v1/vehicle_services")]
        [ProducesResponseType(typeof(List<FerroFTS.Data.location>), StatusCodes.Status200OK)]
        public IActionResult get_vehicle_services([FromQuery]string ou, [FromQuery] string ou_service, [FromQuery] string vehicle, [FromQuery] DateTime ou_start_date, [FromQuery] DateTime ou_end_date)
        {
            return GetVehicleRoute(ou, ou_service, vehicle, ou_start_date, ou_end_date);
        }



        #region *** Private Methods *** 
        private IActionResult GetLastServices(string ou, string ou_service, DateTime start_date)
        {
            if (ModelState.IsValid)
            {
                if (ou == null || ou_service == null || start_date == null)
                {
                    return this.BadRequest("Parameters can't be null"); // 400
                }
                else
                {
                    try
                    {
                        var oServiceSettings = this._ServiceSettings.Value;

                        if (oServiceSettings != null)
                        {
                            var oLocationsManager = new FerroFTS.BussinesService(
                                oServiceSettings.MongoDB.ConnectionString,
                                oServiceSettings.MongoDB.Database);

                            
                            List<FerroFTS.Data.last_service> oListLocation = oLocationsManager.GetLastServices(ou, ou_service, start_date);

                            if (oListLocation != null && oListLocation.Count() > 0)
                            {
                                return this.Ok(oListLocation); // 200
                            }
                            else
                            {
                                return this.NoContent(); // 204
                            }
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status503ServiceUnavailable, "Service incorrectly configured"); // 503
                        }
                    }
                    catch (Exception ex)
                    {

                        string sLogPath = this._ServiceSettings.Value.LogPath;
                        FerroLogs.ExceptionLogger.LogError(MethodInfo.GetCurrentMethod().Name, ex, sLogPath);
                        if (_ServiceSettings.Value.ReturnException == true)
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); // 500
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError); // 500
                        }
                    }
                }
            }
            else
            {
                return this.BadRequest("Parameter values incorrect"); // 400
            }
        }
        private IActionResult GetLastLocations(string ou, string ou_service, DateTime ou_start_date)
        {
            if (ModelState.IsValid)
            {
                if (ou == null || ou_service == null || ou_start_date == null)
                {
                    return this.BadRequest("Parameters can't be null"); // 400
                }
                else
                {
                    try
                    {
                        var oServiceSettings = this._ServiceSettings.Value;
                        if (oServiceSettings != null)
                        {
                                var oLocationsManager = new FerroFTS.BussinesLocations(
                                oServiceSettings.MongoDB.ConnectionString,
                                oServiceSettings.MongoDB.Database);

                            List<FerroFTS.Data.last_location> oListLocation = oLocationsManager.GetLastLocations(ou, ou_service, ou_start_date);

                            if (oListLocation != null && oListLocation.Count() > 0)
                            {
                                return this.Ok(oListLocation); // 200
                            }
                            else
                            {
                                return this.NoContent(); // 204
                            }
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError); // 500
                        }
                    }
                    catch (Exception ex)
                    {

                        string sLogPath = this._ServiceSettings.Value.LogPath;
                        FerroLogs.ExceptionLogger.LogError(MethodInfo.GetCurrentMethod().Name, ex, sLogPath);
                        if (_ServiceSettings.Value.ReturnException == true)
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); // 500
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError); // 500
                        }
                    }
                }
            }
            else
            {
                return this.BadRequest("Parameter values incorrect"); // 400
            }
        }
        private IActionResult GetLocationsDates(string ou, string ou_service, string vehicle, DateTime ou_start_date, DateTime ou_end_date)
        {
            if (ModelState.IsValid)
            {
                if (ou == null || ou_service == null || ou_start_date == null || ou_end_date == null)
                {
                    return this.BadRequest("Parameters can't be null"); // 400
                }
                else
                {
                    try
                    {

                        var oServiceSettings = this._ServiceSettings.Value;
                        if (oServiceSettings != null)
                        {
                            var oLocationsManager = new FerroFTS.BussinesLocations(
                            oServiceSettings.MongoDB.ConnectionString,
                            oServiceSettings.MongoDB.Database);

                            List<FerroFTS.Data.last_location> oListLocation = oLocationsManager.GetLocationsDates(ou, ou_service, vehicle, ou_start_date, ou_end_date);

                            if (oListLocation != null && oListLocation.Count() > 0)
                            {
                                return this.Ok(oListLocation); // 200
                            }
                            else
                            {
                                return this.NoContent(); // 204
                            }
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError); // 500
                        }



                    }
                    catch (Exception ex)
                    {

                        string sLogPath = this._ServiceSettings.Value.LogPath;
                        FerroLogs.ExceptionLogger.LogError(MethodInfo.GetCurrentMethod().Name, ex, sLogPath);
                        if (_ServiceSettings.Value.ReturnException == true)
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); // 500
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError); // 500
                        }
                    }
                }
            }
            else
            {
                return this.BadRequest("Parameter values incorrect"); // 400
            }
        }
        private IActionResult GetVehicleRoute(string ou, string idVehicle, string vehicle, DateTime ou_start_date, DateTime ou_end_date)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(ou)  || string.IsNullOrEmpty(ou_start_date.ToString()) || string.IsNullOrEmpty(ou_end_date.ToString()))
                {
                    return this.BadRequest("Parameters can't be null"); // 400
                }
                else
                {
                    try
                    {

                        var oServiceSettings = this._ServiceSettings.Value;
                        if (oServiceSettings != null)
                        {
                            var oLocationsManager = new FerroFTS.BussinesLocations(
                            oServiceSettings.MongoDB.ConnectionString,
                            oServiceSettings.MongoDB.Database);

                            List<FerroFTS.Data.last_location> oListLocation = oLocationsManager.GetVehicleRoute(ou, idVehicle, vehicle, ou_start_date, ou_end_date);

                            if (oListLocation != null && oListLocation.Count() > 0)
                            {
                                return this.Ok(oListLocation); // 200
                            }
                            else
                            {
                                return this.NoContent(); // 204
                            }
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError); // 500
                        }



                    }
                    catch (Exception ex)
                    {

                        string sLogPath = this._ServiceSettings.Value.LogPath;
                        FerroLogs.ExceptionLogger.LogError(MethodInfo.GetCurrentMethod().Name, ex, sLogPath);
                        if (_ServiceSettings.Value.ReturnException == true)
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); // 500
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError); // 500
                        }
                    }
                }
            }
            else
            {
                return this.BadRequest("Parameter values incorrect"); // 400
            }
        }
        private IActionResult GetVehicleServices(string ou, string ou_service, DateTime ou_start_date, DateTime ou_end_date)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(ou) || string.IsNullOrEmpty(ou_start_date.ToString()) || string.IsNullOrEmpty(ou_end_date.ToString())|| string.IsNullOrEmpty(ou_service)) 
                {
                    return this.BadRequest("Parameters can't be null"); // 400
                }
                else
                {
                    try
                    {

                        var oServiceSettings = this._ServiceSettings.Value;
                        if (oServiceSettings != null)
                        {
                            var oLocationsManager = new FerroFTS.BussinesService(
                            oServiceSettings.MongoDB.ConnectionString,
                            oServiceSettings.MongoDB.Database);

                            List<FerroFTS.Data.service> oListLocation = oLocationsManager.GetVehicleServices(ou, ou_service, ou_start_date, ou_end_date);

                            if (oListLocation != null && oListLocation.Count() > 0)
                            {
                                return this.Ok(oListLocation); // 200
                            }
                            else
                            {
                                return this.NoContent(); // 204
                            }
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError); // 500
                        }



                    }
                    catch (Exception ex)
                    {

                        string sLogPath = this._ServiceSettings.Value.LogPath;
                        FerroLogs.ExceptionLogger.LogError(MethodInfo.GetCurrentMethod().Name, ex, sLogPath);
                        if (_ServiceSettings.Value.ReturnException == true)
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); // 500
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError); // 500
                        }
                    }
                }
            }
            else
            {
                return this.BadRequest("Parameter values incorrect"); // 400
            }
        }

        #endregion

    }
}