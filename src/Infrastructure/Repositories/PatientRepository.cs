using LabCoreSoft.Application.DTOs;
using LabCoreSoft.Application.Interfaces;
using LabCoreSoft.Domain.Entities;
using LabCoreSoft.Domain.Enums;
using LabCoreSoft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LabCoreSoft.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<Patient?> GetByDocumentAsync(string documentType, string documentNumber)
        {
            if (!Enum.TryParse<DocumentType>(documentType, out var docType))
                return null;

            return await _context.Patients
                .FirstOrDefaultAsync(p => p.DocumentType == docType && p.DocumentNumber == documentNumber);
        }

        public async Task<PagedResponse<PatientDto>> GetPatientsAsync(PagedQueryRequest request)
        {
            var query = _context.Patients.Where(p => p.IsActive).AsQueryable();

            if (request.Filters != null)
            {
                foreach (var filter in request.Filters)
                {
                    switch (filter.Key.ToLower())
                    {
                        case "documentnumber":
                            query = query.Where(p => p.DocumentNumber.Contains(filter.Value));
                            break;
                        case "firstname":
                            query = query.Where(p => p.FirstName.Contains(filter.Value));
                            break;
                        case "lastname":
                            query = query.Where(p => p.LastName.Contains(filter.Value));
                            break;
                        case "city":
                            query = query.Where(p => p.City.Contains(filter.Value));
                            break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(request.Orden))
            {
                query = request.Asc
                    ? query.OrderBy(p => EF.Property<object>(p, request.Orden))
                    : query.OrderByDescending(p => EF.Property<object>(p, request.Orden));
            }
            else
            {
                query = query.OrderBy(p => p.Id);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    DocumentType = p.DocumentType,
                    DocumentNumber = p.DocumentNumber,
                    BirthDate = p.BirthDate,
                    City = p.City,
                    Phone = p.Phone,
                    Email = p.Email,
                    IsActive = p.IsActive
                })
                .ToListAsync();

            return new PagedResponse<PatientDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

        public async Task AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                patient.Deactivate();
                await _context.SaveChangesAsync();
            }
        }
    }
}