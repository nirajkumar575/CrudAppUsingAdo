using CrudAppUsingAdo.Controllers;
using CrudAppUsingAdo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class DashboardController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly EmployeeDbContext _employeeDbContext;
    public DashboardController(ILogger<HomeController> logger, EmployeeDbContext dbContext)
    {
        _logger = logger;
        _employeeDbContext = dbContext;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View(_employeeDbContext.GetEmployees());
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Employee model)
    {

        try
        {
            if (ModelState.IsValid == true)
            {
                bool check = _employeeDbContext.AddEmployee(model);
                if (check == true)
                {
                    TempData["InsertMessage"] = "Data has been inserted successfully !!";
                    ModelState.Clear();
                    return RedirectToAction("Index","Dashboard");
                }

            }
        }
        catch
        {
            return View();
        }


        return View();
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        Employee row = _employeeDbContext.GetEmployees().Find(x => x.id == id);
        return View(row);
    }
    [HttpPost]
    public IActionResult Edit(int id, Employee model)
    {
        if (ModelState.IsValid == true)
        {
            bool check = _employeeDbContext.UpdateEmployee(model);
            if (check == true)
            {
                TempData["UpdateMessage"] = "Data has been updated successfully !!";
                ModelState.Clear();
                return RedirectToAction("Index","Dashboard");
            }

        }
        return View();
    }
    [HttpGet]
    public IActionResult Details(int id)
    {
        Employee row = _employeeDbContext.GetEmployees().Find(x => x.id == id);
        return View(row);
    }
    public IActionResult Delete(int id)
    {
        Employee row = _employeeDbContext.GetEmployees().Find(x => x.id == id);
        return View(row);
    }
    [HttpPost]
    public IActionResult Delete(int id, Employee model)
    {
        bool check = _employeeDbContext.DeleteEmployee(id);
        if (check == true)
        {
            TempData["DeleteMessage"] = "Data has been deleted successfully !!";
            ModelState.Clear();
            return RedirectToAction("Index", "Dashboard");
        }

        return View();
    }
}
