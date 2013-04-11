using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using PurpleCubed.Domain;
using PurpleCubed.Domain.Entities;

namespace PurpleCubed.UI.Controllers
{
    public class TeamsController : Controller
    {
        // Constructor Injection through IoC container of our repositories
        private readonly IEmployeeRepository employeeRepository;
        private readonly ITeamRepository teamRepository;

        [Inject]
        public TeamsController(IEmployeeRepository employeeRepository, ITeamRepository teamRepository)
        {
            if (employeeRepository == null)
                throw new ArgumentNullException("employeeRepository");

            if (teamRepository == null)
                throw new ArgumentNullException("teamRepository");

            this.employeeRepository = employeeRepository;
            this.teamRepository = teamRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Team> teams = teamRepository.GetAll();
            return View(teams);
        }

        public ActionResult Details(int id)
        {
            var team = teamRepository.GetById(id);
            return View(team);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Team team)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    teamRepository.Create(team);
                    // Bootstrap Alert
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Handle
                }
            }

            return View(team);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var team = teamRepository.GetById(id);

            if (team == null) 
                return HttpNotFound();

            return View(team);
        }

        [HttpPost]
        public ActionResult Edit(Team team)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    teamRepository.Update(team);
                    // Bootstrap Alert
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    // Handle
                }
            }

            ViewBag.Employees = new MultiSelectList(employeeRepository.GetAll(), "Id", "Email", team.Employees.Select(employee => employee.Id));
            return View(team);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                teamRepository.Delete(id);
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
