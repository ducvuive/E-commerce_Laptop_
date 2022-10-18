using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using ShareView.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoNhoRamController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        public BoNhoRamController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<BoNhoRamController>
        [HttpGet]
        public ActionResult<List<BoNhoRamDTO>> Get()
        {
            var boNhoRam = _context.BoNhoRam.ToList();
            return _mapper.Map<List<BoNhoRamDTO>>(boNhoRam);
        }

        // GET api/<BoNhoRamController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BoNhoRamController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BoNhoRamController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BoNhoRamController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
