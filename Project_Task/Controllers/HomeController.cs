using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Project_Task.Controllers
{
    public class HomeController : Controller
    {
        TasksContext _context = new TasksContext();
        public ActionResult Index(string searchString)
        {
            
            var tasks = _context.Tables.AsQueryable(); 

            if (!string.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(t => t.Name.Contains(searchString) || t.Description.Contains(searchString));
            }

            return View(tasks.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Table model)
        {

            _context.Tables.Add(model);
            _context.SaveChanges();
            ViewBag.Message = "Data inserted successfully";
            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Edit( int id)
        {
            var data = _context.Tables.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Table model)
        {
            var data = _context.Tables.Where(x => x.ID == model.ID).FirstOrDefault();
            if (data != null)
            {
                data.Name = model.Name;
                data.Description = model.Description;
            }
            _context.SaveChanges();
            
            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var data = _context.Tables.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var data = _context.Tables.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(Table model)
        {
            var data = _context.Tables.Where(x => x.ID == model.ID).FirstOrDefault();
            _context.Tables.Remove(data);
            _context.SaveChanges();
            ViewBag.Message = "Data Remove successfully";
            

            return RedirectToAction("index");
        }
    }
}