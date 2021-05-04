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
                                 D.DeviceId,
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
                                 D.DeviceId,
                                 D.UniqueCode,
                                 D.DeviceName,
                             }).ToList();
                return Ok(query);
            }
        }

        [HttpGet]
        [Route("[action]")]
        // GET: api/DeviceInfo/categoryList
        public ActionResult categoryList()
        {
            List<CategoryList> objModel = new List<CategoryList>();
            var query = (from DC in _context.DeviceCategory
                         select new
                         {
                             DC.CategoryId,
                             DC.DeviceType
                         });
            foreach (var item in query.ToList())
            {
                objModel.Add(
                    new CategoryList
                    {
                        CategoryId = item.CategoryId,
                        DeviceType = item.DeviceType.ToString(),
                        TotalCount = _context.DeviceInfo.Count(x => x.CategoryId == item.CategoryId),
                        FreeCount = _context.DeviceInfo.Count(x => x.IsAssigned == false && x.CategoryId == item.CategoryId),
                        AllocatedCount = _context.DeviceInfo.Count(x => x.IsAssigned == true && x.CategoryId == item.CategoryId)
                    });

            }
            return Ok(objModel);
        }

        // GET: api/DeviceInfo/auditTrail/IMEI5823972
        [HttpGet]
        [Route("[action]/{uniqueCode}")]
        public ActionResult auditTrail(string uniqueCode)
        {
            var query = (from A in _context.AuditTrail
                         where A.UniqueCode.ToString().Equals(uniqueCode) == false
                         select new
                         {
                             A.EmployeeEmail,
                             A.Date
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
        public int addDevice([FromBody] NewDevice newDevice)
        {
            var uniqueCodeExists = _context.DeviceInfo.Any(x => x.UniqueCode == newDevice.UniqueCode);
            if (uniqueCodeExists){
                return 0;
            }
            DeviceInfo deviceInfo = new DeviceInfo();
            deviceInfo.UniqueCode = newDevice.UniqueCode;
            deviceInfo.DeviceName = newDevice.DeviceName;
            deviceInfo.CategoryId = newDevice.CategoryId;
            _context.DeviceInfo.Add(deviceInfo);
            _context.SaveChanges();
            return 1;
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

        // DELETE: api/DeviceInfo/deleteCategory/5
        [HttpDelete]
        [Route("[action]/{id}")]
        public int deleteCategory(int id)
        {
            int TotalCount = _context.DeviceInfo.Count(x => x.CategoryId == id);
            if (TotalCount == 0){
                DeviceCategory deviceCategory = _context.DeviceCategory.FirstOrDefault(category => category.CategoryId == id);
                _context.DeviceCategory.Remove(deviceCategory);
                _context.SaveChanges();
                return 1;
            }  
            return 0;
        }
    }
}
