using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReportApi.Controllers;
using ReportApi.Entities;
using ReportApi.Service;
using System.Net;

namespace Test_PhoneBook
{
    public class ReportServiceTest
    {
        [Fact]
        public async Task WhenGettingAllReports_ThenAllReportsReturn()
        {
            var reportController = MockReport.Setup();

            var result = await reportController.GetAll();
            var okObject = result as OkObjectResult;

            Assert.NotNull(okObject);
            Assert.Equal(StatusCodes.Status200OK, okObject.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<Report>>(okObject.Value);
            Assert.NotEmpty(okObject.Value as IEnumerable<Report>);
        }

        [Fact]
        public async Task WhenGettingReportsById_ThenReportReturn()
        {

            var reportController = MockReport.Setup();
            var id = Guid.Parse("4db7b504-024d-40f4-9a45-5bb00502f56c");
            var result = await reportController.GetReportId(id);
            var okObject = result as OkObjectResult;

            Assert.NotNull(okObject);
            Assert.Equal(StatusCodes.Status200OK, okObject.StatusCode);
            Assert.IsAssignableFrom<Report>(okObject.Value);
            Assert.NotNull(okObject.Value as Report);
        }

        [Fact]
        public async Task WhenGettingReportsById_ThenNotFoundReturns()
        {
            var reportController = MockReport.Setup();
            var id = Guid.Parse("f4f4e3bf-afa6-4399-87b5-a3fe17572c4d");
            var result = await reportController.GetReportId(id);
            var statusResult = result as StatusCodeResult;
            Assert.NotNull(statusResult);
            Assert.Equal(StatusCodes.Status404NotFound, statusResult.StatusCode);
        }

        [Fact]
        public async Task WhenPostReportsByCreateRequest_ThenCreatedIdReturn()
        {
            var reportController = MockReport.Setup();

            var result = await reportController.CreateReport(DateTime.Now);
            var objectRes = result as ObjectResult;

            Assert.NotNull(objectRes);
            Assert.IsAssignableFrom<CreatedAtRouteResult>(objectRes);
            Assert.Equal((int)HttpStatusCode.Created, objectRes!.StatusCode);
            Assert.Equal("ReportId", (objectRes as CreatedAtRouteResult)!.RouteName);
        }

        internal static class MockReport
        {
            private static ReportController reportController;
            private static Mock<IReportService> reportServiceMock;
            private static Mock<IRabbitMQProducer> rabbitMQMock;
            private static List<Report> reports;

            public static ReportController Setup()
            {
                if (reportController != null)
                {
                    return reportController;
                }


                reportServiceMock = new Mock<IReportService>();
                rabbitMQMock = new Mock<IRabbitMQProducer>();

                BindReports();

                reportServiceMock.Setup(m => m.GetAll().Result).Returns(() => reports);
                reportServiceMock.Setup(m => m.GetById(It.IsAny<Guid>()).Result)
                    .Returns((Guid id) => reports.FirstOrDefault(o => o.UUID == id));

                reportServiceMock.Setup(m => m.Create(It.IsAny<DateTime>()).Result)
                    .Returns(() => reports.FirstOrDefault());

                return reportController = new ReportController(reportServiceMock.Object, rabbitMQMock.Object);
            }

            private static void BindReports()
            {
                reports = new List<Report>()
        {
            new Report()
            {
                UUID = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Status = ReportApi.Shared.Enums.ReportStatus.Inprogress,
                RequestDate = DateTime.Now,
                ReportDetails = new List<ReportDetail>() {
                new ReportDetail() {
                    ContactCount = 5,
                    LocationName = "Adana",
                    PhoneCount = 2,
                    ReportId =  Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")
                    }
                }
            },
            new Report()
            {
                UUID = Guid.Parse("4db7b504-024d-40f4-9a45-5bb00502f56c"),
                Status = ReportApi.Shared.Enums.ReportStatus.Complete,
                RequestDate = DateTime.Now.AddDays(-1),
                ReportDetails = new List<ReportDetail>(){
                    new ReportDetail() {
                    ContactCount = 1,
                    LocationName = "Istanbul",
                    PhoneCount = 0,
                    ReportId =  Guid.Parse("4db7b504-024d-40f4-9a45-5bb00502f56c")
                               }
                          }
                        }

                  };
            }
        }
    }
}
