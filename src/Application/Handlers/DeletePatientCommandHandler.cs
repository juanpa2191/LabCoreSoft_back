using LabCoreSoft.Application.Commands;
using LabCoreSoft.Application.Interfaces;
using MediatR;

namespace LabCoreSoft.Application.Handlers
{
    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand>
    {
        private readonly IPatientRepository _patientRepository;

        public DeletePatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            await _patientRepository.DeleteAsync(request.Id);
        }
    }
}