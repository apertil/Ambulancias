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
        /// Returns a Service JSON collection.
        ///  No JWT authorization
        /// </summary>
        /// <param name="ou">Localización. Formato de cadena</param>
        /// <param name="ou_service">Tipo de Servicio. Formato de cadena</param>
        /// <param name="start_date">Local start date in ISO 8601 format (ex: 2019-01-16T10:15:00)</param>
        /// <returns></returns>
        /// <returns>A Location JSON collection</returns>
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
        /// Devuelve en formato JSON los datos de las últimas localizaciones de los vehículos.
        /// </summary>
        /// <param name="ou">Localización. Formato de cadena</param>
        /// <param name="ou_service">Tipo de Servicio. Formato de cadena</param>
        /// <param name="ou_start_date">Local start date in ISO 8601 format (ex: 2019-01-16T10:15:00)</param>
        /// <returns></returns>
        /// /// <returns> Devuelve en formato JSON los datos de las últimas localizaciones de los vehículos.</returns>
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

           #endregion
        
    }
}