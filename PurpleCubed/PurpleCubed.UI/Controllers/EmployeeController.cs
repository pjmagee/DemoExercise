using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using PurpleCubed.Domain;
using PurpleCubed.Domain.Entities;

namespace PurpleCubed.UI.Controllers
{

    /// <summary>
    /// Should remain ORM agnostic. Loosly coupled design meaning
    /// the ORM could be switched out and injected with another implementation
    /// meaning that either Entity Framework, NHibernate or other like Linq to entities
    /// could be used, if the buisness requirements changed or a client
    /// specifically wanted one type to be used (other factors to consider can be ORM performance)
    /// 
    /// </summary>
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly ITeamRepository teamRepository;

        [Inject]
        public EmployeeController(IEmployeeRepository employeeRepository, ITeamRepository teamRepository)
        {
            if (employeeRepository == null)
                throw new ArgumentNullException("employeeRepository");

            if (teamRepository == null)
                throw new ArgumentNullException("teamRepository");

            this.employeeRepository = employeeRepository;
            this.teamRepository = teamRepository;
        }
        
        public ActionResult Index()
        {
            IEnumerable<Employee> employees = employeeRepository.GetAll();
            return View(employees);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Teams = new SelectList(teamRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createdEmployee = employeeRepository.Create(employee);
                    // Bootstrap Alert
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Handle
                }
            }

            ViewBag.Teams = new SelectList(teamRepository.GetAll(), "Id", "Name");
            return View(employee);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var employee = employeeRepository.GetById(id);

            if (employee == null) 
                return HttpNotFound();

            ViewBag.Teams = new SelectList(teamRepository.GetAll(), "Id", "Name", employee.TeamId);

            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedEmployee = employeeRepository.Update(employee);
                    // Bootstrap Alert
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    // Handle
                }
            }

            ViewBag.Teams = new SelectList(teamRepository.GetAll(), "Id", "Name", employee.TeamId);

            return View(employee);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                employeeRepository.Delete(id);
                // Bootstrap Alert
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                // Handle
            }

            return RedirectToAction("Index");
        }
    }
}
