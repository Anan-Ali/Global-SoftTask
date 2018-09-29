using DataModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _EmployeeService;

        public EmployeeController()
        {
            _EmployeeService = new EmployeeService();
        }
        // GET: Employee
        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(EmployeeModel Employee)
        {
            if (ModelState.IsValid)
            {
                _EmployeeService.Create(Employee);
                RedirectToAction("Index");
;
            }
            else
            {
                if (Employee.Name.Length < 3)
                {
                    ModelState.AddModelError("Name", "Name can't be less than 3");

                }

            }
            return Json(Employee);
        }
        [HttpPost]
        public JsonResult GetAll([DataSourceRequest]DataSourceRequest request)
        {
            var alldata = _EmployeeService.GetAll();
            var result = alldata.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(EmployeeModel Employee)
        {
            if (ModelState.IsValid)
            {
                _EmployeeService.Edit(Employee);
                RedirectToAction("Index");
            }
            return Json(Employee);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            _EmployeeService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}