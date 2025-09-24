using LabCoreSoft.Application.Commands;
using LabCoreSoft.Application.Interfaces;
using LabCoreSoft.Domain.Entities;
using MediatR;

namespace LabCoreSoft.Application.Handlers
{
    public class RegisterPatientCommandHandler : IRequestHandler<RegisterPatientCommand, int>
    {
        private readonly IPatientRepository _patientRepository;

        public RegisterPatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<int> Handle(RegisterPatientCommand request, CancellationToken cancellationToken)
        {
            // Check uniqueness
            var existing = await _patientRepository.GetByDocumentAsync(request.DocumentType.ToString(), request.DocumentNumber);
            if (existing != null)
                throw new InvalidOperationException("Patient with this document already exists.");

            var patient = new Patient(
                request.FirstName,
                request.LastName,
                request.DocumentType,
                request.DocumentNumber,
                request.BirthDate,
                request.City,
                request.Phone,
                request.Email
            );

            await _patientRepository.AddAsync(patient);
            return patient.Id;
        }
    }
}