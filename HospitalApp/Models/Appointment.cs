using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Appointment
{
    public int AppointmentId { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }

    public string Status { get; set; } = "Pending"; // Pending / Approved

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; }
}
