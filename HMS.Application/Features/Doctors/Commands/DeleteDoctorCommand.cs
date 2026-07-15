using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

public class DeleteDoctorCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
