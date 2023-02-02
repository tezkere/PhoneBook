using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportApi.Entities;
using ReportApi.Helpers;
using ReportApi.Model;

namespace ReportApi.Service
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAll();
        Task<Report> GetById(Guid id);
        Task<Report> Create(DateTime reportDate);
        Task<IEnumerable<ReportDetail>> CreateReportDetail(IEnumerable<ReportInfo> reportInfo);
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
            var report = new Report()
            {
                RequestDate = reportDate,
                Status = Shared.Enums.ReportStatus.Inprogress
            };

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
            var report = await _context.Reports.Include(x => x.ReportDetails).FirstOrDefaultAsync(x => x.UUID == id);
            if (report == null) throw new KeyNotFoundException("Report not found");
            return report;
        }

        public async Task<IEnumerable<ReportDetail>> CreateReportDetail(IEnumerable<ReportInfo> reportInfo)
        {
            var report = await GetReport(reportInfo.First().ReportId);

            report.Status = Shared.Enums.ReportStatus.Complete;
            _context.Reports.Update(report);

            var reportDetail = _mapper.Map<IEnumerable<ReportDetail>>(reportInfo);
            _context.ReportDetails.AddRange(reportDetail);
            await _context.SaveChangesAsync();

            return reportDetail;
        }
    }
}
