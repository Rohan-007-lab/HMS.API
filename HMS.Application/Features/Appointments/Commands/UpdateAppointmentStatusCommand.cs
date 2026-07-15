using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class UpdateAppointmentStatusCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty; // Scheduled, Completed, Cancelled, NoShow
    public string Notes { get; set; } = string.Empty;
}
