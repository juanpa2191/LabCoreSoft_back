using LabCoreSoft.Application.DTOs;
using LabCoreSoft.Domain.Entities;

namespace LabCoreSoft.Application.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient?> GetByIdAsync(int id);
        Task<Patient?> GetByDocumentAsync(string documentType, string documentNumber);
        Task<PagedResponse<PatientDto>> GetPatientsAsync(PagedQueryRequest request);
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(int id);
    }
}