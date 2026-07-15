using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Doctors.Dtos;
using MediatR;

namespace HMS.Application.Features.Doctors.Queries;

public class GetAllDoctorsQuery : IRequest<List<DoctorDto>>
{
}