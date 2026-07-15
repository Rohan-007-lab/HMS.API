using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Prescriptions.Dtos;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Queries;

public class GetAllPrescriptionsQuery : IRequest<List<PrescriptionDto>>
{
}