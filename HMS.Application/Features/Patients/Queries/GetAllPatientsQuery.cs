using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Patients.Dtos;
using MediatR;

namespace HMS.Application.Features.Patients.Queries;

public class GetAllPatientsQuery : IRequest<List<PatientDto>>
{
}