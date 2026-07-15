using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Domain.Common;

namespace HMS.Domain.Entities;

public enum PaymentStatus
{
    Pending,
    Paid,
    Cancelled
}

public class Bill : BaseEntity
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public decimal ConsultationFee { get; set; }
    public decimal MedicineFee { get; set; }
    public decimal OtherCharges { get; set; }
    public decimal TotalAmount { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public DateTime BillDate { get; set; } = DateTime.UtcNow;
}