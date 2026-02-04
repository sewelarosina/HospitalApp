using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class AppointmentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AppointmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var appointments = _context.Appointments
            .Select(a => new
            {
                a.Id,
                Patient = _context.Patients.FirstOrDefault(p => p.Id == a.PatientId),
                Doctor = _context.Doctors.FirstOrDefault(d => d.Id == a.DoctorId),
                a.AppointmentDate
            })
            .ToList();

        return View(appointments);
    }

    public IActionResult Create()
    {
        ViewBag.Patients = _context.Patients.ToList();
        ViewBag.Doctors = _context.Doctors.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
