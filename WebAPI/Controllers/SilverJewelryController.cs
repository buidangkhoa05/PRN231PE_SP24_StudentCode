using BusinessObject;
using BusinessObject.Dto;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository.Interface;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SilverJewelryController : ControllerBase
    {
        private readonly ISilverJewelryRepository _silverJewelryRepository;

        public SilverJewelryController(ISilverJewelryRepository silverJewelryRepository)
        {
            _silverJewelryRepository = silverJewelryRepository;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Get()
        {
            var result = await _silverJewelryRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _silverJewelryRepository.Get(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateJewelryRequest request)
        {
            try
            {
                var silverJewelry = request.Adapt<SilverJewelry>();
                if (silverJewelry == null)
                    return BadRequest();

                silverJewelry.SilverJewelryId = Guid.NewGuid().ToString();

                await _silverJewelryRepository.Create(silverJewelry);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateJewelryRequest request)
        {
            try
            {
                var silverJewelry = await _silverJewelryRepository.Get(id);
                silverJewelry = request.Adapt(silverJewelry);

                if (silverJewelry == null)
                    return BadRequest();

                await _silverJewelryRepository.Update(silverJewelry);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var silverJewelry = await _silverJewelryRepository.Get(id);

                if (silverJewelry == null)
                    return BadRequest();

                await _silverJewelryRepository.Delete(silverJewelry);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
