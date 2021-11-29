using Microsoft.EntityFrameworkCore;
using OLM.Services.DailyReport.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.DailyReport.API.Data
{
    public class DailyReportDbContext : DbContext
    {
        public DailyReportDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DailyReportDataModel> ReportData { get; set; }
    }
}
