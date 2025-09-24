using LabCoreSoft.Application.DTOs;
using LabCoreSoft.Application.Interfaces;
using LabCoreSoft.Application.Queries;
using MediatR;

namespace LabCoreSoft.Application.Handlers
{
    public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, PagedResponse<PatientDto>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientsQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PagedResponse<PatientDto>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            return await _patientRepository.GetPatientsAsync(request.Request);
        }
    }
}