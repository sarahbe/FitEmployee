using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApp.Models;
using EmployeeApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeApp.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository repo)
        {
            _employeeRepository = repo;
        }



        [HttpGet]
        public ActionResult<List<EmployeeInfo>> Index()
        {
            return View(_employeeRepository.GetAllEmployees());
        }


        [HttpGet]
        public IActionResult Create()
        {
            var emp = new EmployeeInfo();
            return View(emp);
        }


        [HttpPost]
        public IActionResult AddEmployee(EmployeeInfo model, IFormFile EmployeeImage)
        {

            if (ModelState.IsValid)
            {
                if (EmployeeImage != null)
                {
                    var filePath = Path.GetTempFileName();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        EmployeeImage.CopyToAsync(stream);
                    }
                }
                _employeeRepository.Create(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var emp = _employeeRepository.GetEmployee(ObjectId.Parse(id));
            if (id != emp.Id.ToString())
            {
                return NotFound();
            }
            return View(emp);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EmployeeInfo emp)
        {
            var dbEmp = _employeeRepository.GetEmployee(ObjectId.Parse(id));
            dbEmp.Email = emp.Email;
            dbEmp.Name = emp.Name;
            dbEmp.PhoneNumber = emp.PhoneNumber;
            await _employeeRepository.Update(dbEmp);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("id")]
        public IActionResult Delete(string id)
        {
            _employeeRepository.Delete(ObjectId.Parse(id));
            return RedirectToAction(nameof(Index));
        }
    }
}