using Microsoft.AspNetCore.Mvc;

public class PatientsController : Controller
{
    private readonly ApplicationDbContext _context;

    public PatientsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Patients.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Patient patient)
    {
        _context.Patients.Add(patient);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
