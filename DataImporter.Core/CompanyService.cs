using DataImporter.Core.Abstractions;
using DataImporter.Data.Entities;
using DataImporter.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Core
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _CompanyRepository;

        public CompanyService(ICompanyRepository CompanyRepository)
        {
            _CompanyRepository = CompanyRepository;
        }

        public async Task<CompanyEntity> AddCompany(CompanyEntity companyEntity)
        {
            return await _CompanyRepository.Add(companyEntity);
        }

        public async Task<CompanyEntity> GetCompanyByName(string CompanyName)
        {
            return await _CompanyRepository.FirstOrDefault(a => a.Name == CompanyName);
        }
    }
}
