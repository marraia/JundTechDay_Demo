using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSP_Demo.Application.Abstraction;
using MSP_Demo.Application.Models;
using MSP_Demo.Domain;
using MSP_Demo.Domain.ValueObject;

namespace MSP_Demo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/GasStation")]
    public class GasStationController : Controller
    {
        private readonly IGasStationAppService _gasStationService;
        public GasStationController(IGasStationAppService gasStationService)
        {
            _gasStationService = gasStationService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GasStation), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] GasStationInput input)
        {
            var obj = await _gasStationService.AddAsync(input);
            return Created(nameof(Get), obj);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GasStation), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var obj = await _gasStationService.GetByIdAsync(id);
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GasStation), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _gasStationService.GetAllAsync());
        }

        [HttpGet]
        [Route("Proximity")]
        [ProducesResponseType(typeof(GasStationLocationValueObject), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProximity([FromQuery]double latitude, [FromQuery]double longitude, [FromQuery]string name)
        {
            return Ok(await _gasStationService.GetProximityAsync(latitude, longitude, name));
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(GasStation), 202)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Put([FromRoute]Guid id, [FromBody] GasStationInput input)
        {
            var obj = _gasStationService.GetByIdAsync(id);
            if (obj == null)
                return NotFound();

            return Accepted(await _gasStationService.UpdateAsync(id, input));
        }

        /// <summary>
        /// Method that excludes a doctor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(GasStation), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var obj = _gasStationService.GetByIdAsync(id);
            if (obj == null)
                return NotFound();

            await _gasStationService.DeleteAsync(id);

            return Ok();
        }
    }
}