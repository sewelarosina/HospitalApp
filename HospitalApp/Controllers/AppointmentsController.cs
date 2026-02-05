using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HospitalApp.Data;
using HospitalApp.Models;

namespace HospitalApp.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================
        // 1. LIST ALL APPOINTMENTS
        // ============================
        public IActionResult Index()
        {
            var appointments = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToList();

            return View(appointments);
        }

        // ============================
        // 2. CREATE APPOINTMENT (GET)
        // ============================
        public IActionResult Create()
        {
            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Doctors = _context.Doctors.ToList();
            return View();
        }

        // ============================
        // 3. CREATE APPOINTMENT (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.Status = "Pending";

                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                return RedirectToAction(nameof(Confirmation));
            }

            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Doctors = _context.Doctors.ToList();
            return View(appointment);
        }

        // ============================
        // 4. CONFIRMATION PAGE
        // ============================
        public IActionResult Confirmation()
        {
            return View();
        }

        // ============================
        // 5. VIEW PENDING APPOINTMENTS
        // ============================
        public IActionResult Pending()
        {
            var pendingAppointments = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.Status == "Pending")
                .ToList();

            return View(pendingAppointments);
        }

        // ============================
        // 6. APPROVE APPOINTMENT
        // ============================
        [HttpPost]
        public IActionResult Approve(int id)
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment != null)
            {
                appointment.Status = "Approved";
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Pending));
        }

        // ============================
        // 7. VIEW APPROVED APPOINTMENTS
        // ============================
        public IActionResult Approved()
        {
            var approvedAppointments = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.Status == "Approved")
                .ToList();

            return View(approvedAppointments);
        }
    }
}
