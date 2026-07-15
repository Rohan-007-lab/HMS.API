using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class DeleteAppointmentCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}