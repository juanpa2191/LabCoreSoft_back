using LabCoreSoft.Application.DTOs;
using MediatR;

namespace LabCoreSoft.Application.Queries
{
    public class GetPatientsQuery : IRequest<PagedResponse<PatientDto>>
    {
        public PagedQueryRequest Request { get; set; } = new();
    }
}