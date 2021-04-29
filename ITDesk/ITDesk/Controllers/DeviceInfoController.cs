using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITDesk.Models;
using ITDesk.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ITDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceInfoController : ControllerBase
    {
        ITDeskContext _context;
        private IConfiguration _config;

        public DeviceInfoController(IConfiguration config, ITDeskContext context)
        {
            _config = config;
            _context = context;
        }
        // GET: api/DeviceInfo
        [HttpGet]
        public string Get()
        {
            return "DeviceInfo API is Running";
        }

        [HttpGet]
        [Route("[action]")]
        // GET: api/DeviceInfo/data?isAssigned=0&&CategoryId=2
        public ActionResult data(bool isAssigned = false, int CategoryId = 1)
        {
            List<DeviceResponse> objModel = new List<DeviceResponse>();

            var query = (from D in _context.DeviceInfo
                         join E in _context.EmployeeInfo on D.EmployeeId equals E.EmployeeId
                         where D.IsAssigned == isAssigned && D.CategoryId == CategoryId
                         select new
                         {
                             D.UniqueCode,
                             D.DeviceName,
                             D.AssignedDate,
                             E.EmployeeEmail,
                             D.AssignedBy
                         }).ToList();

            foreach (var item in query.ToList())
            {
                objModel.Add(
                    new DeviceResponse
                    {
                        UniqueCode = item.UniqueCode.ToString(),
                        DeviceName = item.UniqueCode.ToString(),
                        AssignedDate = item.AssignedDate,
                        EmployeeEmail = item.EmployeeEmail.ToString(),
                        AssignedBy = item.AssignedBy.ToString()
                    });

            }
            return Ok(objModel);
        }

        [HttpGet]
        [Route("[action]")]
        // GET: api/DeviceInfo/count?CategoryId=2
        public int totalCount(int CategoryId)
        {
            int recordCount = _context.DeviceInfo.Count(x => x.CategoryId == CategoryId);
            return recordCount;
        }

        [HttpGet]
        [Route("[action]")]
        // GET: api/DeviceInfo/freeAssignedCount?isAssigned=0&&CategoryId=2
        public int freeAssignedCount(bool isAssigned, int CategoryId)
        {
            int count = _context.DeviceInfo.Count(x => x.IsAssigned == isAssigned && x.CategoryId == CategoryId);
            return count;
        }

        [HttpGet]
        [Route("[action]")]
        // GET: api/DeviceInfo/employeeEmail
        public ActionResult employeeEmail()
        {
            List<string> employeeEmailList = new List<string>();

            var query = (from E in _context.EmployeeInfo
                         where E.Role == false
                         select new
                         {
                             E.EmployeeEmail
                         }).ToList();

            foreach (var item in query.ToList())
            {
                employeeEmailList.Add(item.EmployeeEmail.ToString());

            }
            return Ok(employeeEmailList);
        }

        // GET: api/DeviceInfo/5
        [HttpGet]
        [Route("[action]/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DeviceInfo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // POST: api/DeviceInfo
        [HttpPost]
        [Route("[action]")]
        public int category([FromBody] DeviceCategory dc)
        {
            var obj = _context.DeviceCategory.FirstOrDefault(x => x.DeviceType == dc.DeviceType);
            if (obj == null)
            {
                return obj.CategoryId;
            }
            return obj.CategoryId;
        }

        // PUT: api/DeviceInfo/5
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
