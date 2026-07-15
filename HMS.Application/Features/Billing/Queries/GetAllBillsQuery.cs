using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Billing.Dtos;
using MediatR;

namespace HMS.Application.Features.Billing.Queries;

public class GetAllBillsQuery : IRequest<List<BillDto>>
{
}