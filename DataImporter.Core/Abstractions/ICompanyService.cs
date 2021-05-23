using DataImporter.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Core.Abstractions
{
    public interface ICompanyService
    {
        Task<CompanyEntity> AddCompany(CompanyEntity companyEntity);
        Task<CompanyEntity> GetCompanyByName(string CompanyName);
    }
}
