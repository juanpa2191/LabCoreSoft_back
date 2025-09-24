using LabCoreSoft.Application.Commands;
using LabCoreSoft.Application.Interfaces;
using MediatR;

namespace LabCoreSoft.Application.Handlers
{
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand>
    {
        private readonly IPatientRepository _patientRepository;

        public UpdatePatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.Id);
            if (patient == null)
                throw new KeyNotFoundException("Patient not found.");

            patient.Update(
                request.FirstName,
                request.LastName,
                request.BirthDate,
                request.City,
                request.Phone,
                request.Email
            );

            await _patientRepository.UpdateAsync(patient);
        }
    }
}