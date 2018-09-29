using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace UI_Project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _EmployeeService;

        public EmployeeController()
        {
            _EmployeeService = new EmployeeService();
        }

        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var alldata = _EmployeeService.GetAll();
            alldata = alldata.OrderByDescending(i => i.Id);
            //RedirectToAction("Index");
            return Json(alldata, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult Search(string name)
        //{

        //    var data = _ProductService.GetByName(name);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult Create(ProductModel product)
        //{
        //    string message = "";
        //    if (ModelState.IsValid)
        //    {
        //        var p = _ProductService.GetById(product.id);
        //        if (p == null)
        //        {
        //            _ProductService.Create(product);
        //            message = "success";
        //        }
        //        else
        //        {
        //            message = "this name is already exists";
        //        }

        //        //GetAll();
        //        //RedirectToAction("Index");
        //        //return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        message = "model state is not valid";
        //    }
        //    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        //[HttpPost]
        //public JsonResult Update(ProductModel product)
        //{
        //    string message = "";
        //    if (ModelState.IsValid)
        //    {
        //        _ProductService.Edit(product);
        //        message = "success";
        //        //return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        message = "model state is not valid";
        //    }
        //    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}



        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var data = _ProductService.GetById(id);
        //    if (data == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(data);
        //}



       

        //[HttpGet]
        //public JsonResult Delete(int id)
        //{
        //    var data = _ProductService.GetById(id);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}



        //[HttpPost]
        //public JsonResult DeleteAngular(int id)
        //{
        //    _ProductService.Delete(id);
        //    return Json(id);
        //}
    }
}