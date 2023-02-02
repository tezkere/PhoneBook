using Microsoft.AspNetCore.Mvc;
using ReportApi.Model;
using ReportApi.Service;

namespace ReportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;
        private readonly IRabbitMQProducer rabbitMQProducer;
        public ReportController(IReportService _reportService, IRabbitMQProducer _rabbitMQProducer)
        {
            reportService = _reportService;
            rabbitMQProducer = _rabbitMQProducer;
        }


        // GET: api/<ReportController>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var reports = await reportService.GetAll();
                return await Task.FromResult(Ok(reports));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET api/<ReportController>/5
        [HttpGet("{id}",Name="ReportId")]
        public async Task<IActionResult> GetReportId(Guid id)
        {
            try
            {
                var report = await reportService.GetById(id);

                if (report is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(report);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST api/<ReportController>
        [HttpPost("createReport")]
        public async Task<IActionResult> CreateReport([FromBody] DateTime reportDate)
        {
            try
            {
                var result = await reportService.Create(reportDate);
                rabbitMQProducer.SendReportCreateMessage($"{result.UUID}");
                return CreatedAtRoute("ReportId", new { id = result.UUID }, result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST api/<ReportController>
        [HttpPost("createReportDetail")]
        public async Task<IActionResult> CreateReportDetail([FromBody] List<ReportInfo> reportInfo)
        {
            try
            {
                var result = await reportService.CreateReportDetail(reportInfo);               
                return CreatedAtRoute("ReportDetails",new { id = result.First().ReportId }, result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
