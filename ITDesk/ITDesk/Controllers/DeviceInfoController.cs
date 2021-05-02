using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITDesk.Models;
using ITDesk.Models.Request;
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

        [HttpGet]
        [Route("[action]")]
        // GET: api/DeviceInfo/data?isAssigned=0&&CategoryId=2
        public ActionResult data(bool isAssigned, int CategoryId)
        {
            if (isAssigned == true)
            {
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
                return Ok(query);
            }
            else
            {
                var query = (from D in _context.DeviceInfo
                             where D.IsAssigned == isAssigned && D.CategoryId == CategoryId
                             select new
                             {
                                 D.UniqueCode,
                                 D.DeviceName,
                             }).ToList();
                return Ok(query);
            }
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
        // GET: api/DeviceInfo/categoryList
        public ActionResult categoryList()
        {
            var query = (from DC in _context.DeviceCategory
                         select new
                         {
                             DC.DeviceType
                         }).ToList();
            return Ok(query);
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

        // POST: api/DeviceInfo/category
        [HttpPost]
        [Route("[action]")]
        public int category([FromBody] DeviceCategory dc)
        {
            var obj = _context.DeviceCategory.FirstOrDefault(x => x.DeviceType == dc.DeviceType);
            if (obj == null)
            {
                _context.DeviceCategory.Add(dc);
                _context.SaveChanges();
            }
            return _context.DeviceCategory.FirstOrDefault(x => x.DeviceType == dc.DeviceType).CategoryId;
        }

        // POST: api/DeviceInfo/addDevice
        [HttpPost]
        [Route("[action]")]
        public ActionResult addDevice([FromBody] NewDevice newDevice)
        {
            DeviceInfo deviceInfo = new DeviceInfo();
            deviceInfo.UniqueCode = newDevice.UniqueCode;
            deviceInfo.DeviceName = newDevice.DeviceName;
            deviceInfo.CategoryId = newDevice.CategoryId;
            _context.DeviceInfo.Add(deviceInfo);
            _context.SaveChanges();
            return Ok(deviceInfo);
        }

        // PUT: api/DeviceInfo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // PUT: api/DeviceInfo/allocate/5
        [HttpPut]
        [Route("[action]/{id}")]
        public ActionResult allocate(int id, [FromBody] DeviceInfo device)
        {
            DeviceInfo deviceInfo = _context.DeviceInfo.FirstOrDefault(d => d.DeviceId == id);
            deviceInfo.AssignedDate = device.AssignedDate;
            deviceInfo.EmployeeId = device.EmployeeId;
            deviceInfo.AssignedBy = device.AssignedBy;
            deviceInfo.IsAssigned = device.IsAssigned;
            _context.DeviceInfo.Update(deviceInfo);
            _context.SaveChanges();
            return Ok(deviceInfo);
        }

        // PUT: api/DeviceInfo/deallocate/5
        [HttpPut]
        [Route("[action]/{id}")]
        public ActionResult deallocate(int id)
        {
            DeviceInfo deviceInfo = _context.DeviceInfo.FirstOrDefault(d => d.DeviceId == id);
            var employeeId = deviceInfo.EmployeeId;
            deviceInfo.AssignedDate = null;
            deviceInfo.EmployeeId = null;
            deviceInfo.AssignedBy = null;
            deviceInfo.IsAssigned = false;
            _context.DeviceInfo.Update(deviceInfo);
            _context.SaveChanges();

            //Update Deallocated device in audit trail
            AuditTrail auditTrailInfo = new AuditTrail();
            auditTrailInfo.AuditId = 0;
            auditTrailInfo.UniqueCode = deviceInfo.UniqueCode;
            var query = _context.EmployeeInfo
                            .Where(v => v.EmployeeId == employeeId)
                            .Select(v => v.EmployeeEmail).ToList();
            auditTrailInfo.EmployeeEmail = query[0];
            auditTrailInfo.Date = DateTime.Now;
            _context.AuditTrail.Update(auditTrailInfo);
            _context.SaveChanges();
            return Ok(deviceInfo);
        }

        // DELETE: api/DeviceInfo/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            DeviceInfo deviceInfo = _context.DeviceInfo.FirstOrDefault(device => device.DeviceId == id);
            _context.DeviceInfo.Remove(deviceInfo);
            _context.SaveChanges();
            return Ok(deviceInfo);
        }
    }
}
