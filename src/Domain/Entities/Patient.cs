using System;
using LabCoreSoft.Domain.Enums;

namespace LabCoreSoft.Domain.Entities
{
    public class Patient
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DocumentType DocumentType { get; private set; }
        public string DocumentNumber { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string City { get; private set; }
        public string? Phone { get; private set; }
        public string? Email { get; private set; }
        public bool IsActive { get; private set; }

        // Constructor for creating new patient
        public Patient(string firstName, string lastName, DocumentType documentType, string documentNumber, DateTime birthDate, string city, string? phone, string? email)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required.");
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required.");
            if (string.IsNullOrWhiteSpace(documentNumber)) throw new ArgumentException("Document number is required.");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required.");

            ValidateAge(birthDate);

            FirstName = firstName;
            LastName = lastName;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            BirthDate = birthDate;
            City = city;
            Phone = phone;
            Email = email;
            IsActive = true;
        }

        private Patient() { }

        public void Update(string firstName, string lastName, DateTime birthDate, string city, string? phone, string? email)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required.");
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required.");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required.");

            ValidateAge(birthDate);

            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            City = city;
            Phone = phone;
            Email = email;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        private void ValidateAge(DateTime birthDate)
        {
            var age = DateTime.Now.Year - birthDate.Year;
            if (birthDate > DateTime.Now.AddYears(-age)) age--;

            if (age < 0 || age > 120)
                throw new ArgumentException("Patient age must be between 0 and 120 years.");
        }
    }
}