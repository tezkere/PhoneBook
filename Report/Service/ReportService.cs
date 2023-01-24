using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportApi.Entities;
using ReportApi.Helpers;

namespace ReportApi.Service
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAll();
        Task<Report> GetById(Guid id);
        Task<Report> Create(DateTime reportDate);
    }

    public class ReportService : IReportService
    {
        private ReportDbContext _context;
        private readonly IMapper _mapper;

        public ReportService(ReportDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Report> Create(DateTime reportDate)
        {
            // map model to new contact object

            var report = new Report()
            {
                RequestDate = reportDate,
                Status = Shared.Enums.ReportStatus.Inprogress                
            };            

            // save contact

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }


        public async Task<IEnumerable<Report>> GetAll()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<Report> GetById(Guid id)
        {
            return await GetReport(id);
        }

        private async Task<Report> GetReport(Guid id)
        {
            var report = await _context.Reports.Include(x => x.ReportDetail).FirstOrDefaultAsync(x => x.UUID == id);
            if (report == null) throw new KeyNotFoundException("Report not found");
            return report;
        }
    }
}
