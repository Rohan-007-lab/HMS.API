using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace HMS.Application.Features.Patients.Commands;

public class DeletePatientCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
