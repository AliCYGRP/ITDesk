using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITDesk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ITDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        ITDeskContext _context;
        private IConfiguration _config;

        public EmployeeController(IConfiguration config, ITDeskContext context)
        {
            _config = config;
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Employee/devices/5
        [HttpGet]
        [Route("[action]/{id}")]
        public ActionResult devices(int id)
        {
            var query = (from D in _context.DeviceInfo
                         where D.EmployeeId == id
                         select new
                         {
                             D.DeviceId,
                             D.UniqueCode,
                             D.DeviceName,
                             D.AssignedDate,
                             D.AssignedBy
                         }).ToList();
            return Ok(query);
        }

        // GET: api/employee/profile/5
        [HttpGet]
        [Route("[action]/{id}")]
        public ActionResult profile(int id)
        {
            var query = _context.EmployeeInfo
                            .Where(v => v.EmployeeId == id)
                            .Select(v => new { v.EmployeeName, v.EmployeeEmail, v.Designation }).ToList();
            return Ok(query);
        }

        [HttpGet]
        [Route("[action]")]
        // GET: api/employee/employeeEmail
        public ActionResult employeeEmail()
        {
            var query = (from E in _context.EmployeeInfo
                         where E.Role == false
                         select new
                         {
                             E.EmployeeId,
                             E.EmployeeEmail
                         }).ToList();
            return Ok(query);
        }

        // POST: api/Employee
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPost]
        [Route("[action]")]
        // POST: api/employee/getEmployeeId
        public int getEmployeeId([FromBody] string email)
        {
            var queryList = _context.EmployeeInfo
                            .Where(v => v.EmployeeEmail == email)
                            .Select(v => v.EmployeeId).ToList();
            if (queryList.Count() == 0)
                return 0;
            return queryList[0];
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
