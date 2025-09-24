using MediatR;

namespace LabCoreSoft.Application.Commands
{
    public class DeletePatientCommand : IRequest
    {
        public int Id { get; set; }
    }
}