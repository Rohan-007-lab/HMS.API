using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Appointments.Dtos;
using MediatR;

namespace HMS.Application.Features.Appointments.Queries;

public class GetAppointmentByIdQuery : IRequest<AppointmentDto?>
{
    public Guid Id { get; set; }
}
