using ContactApi.Controllers;
using ContactApi.Entities;
using ContactApi.Model.Contact;
using ContactApi.Service;
using ContactApi.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Test_PhoneBook
{
    public class ContactServiceTest
    {
        [Fact]
        public async Task WhenGettingAllContacts_ThenAllContactsReturn()
        {
            var serviceMock = MockIContactService.GetMock();           
            var contactController = new ContactController(serviceMock.Object);

            var result = await contactController.GetAll();
            var okObject = result as OkObjectResult;

            Assert.NotNull(okObject);
            Assert.Equal(StatusCodes.Status200OK, okObject.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<Contact>>(okObject.Value);
            Assert.NotEmpty(okObject.Value as IEnumerable<Contact>);
        }

        [Fact]
        public async Task WhenGettingContactsById_ThenContactReturn()
        {
            var serviceMock = MockIContactService.GetMock();
            var contactController = new ContactController(serviceMock.Object);
            var id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
            var result = await contactController.Get(id);
            var okObject = result as OkObjectResult;            

            Assert.NotNull(okObject);
            Assert.Equal(StatusCodes.Status200OK, okObject.StatusCode);
            Assert.IsAssignableFrom<Contact>(okObject.Value);
            Assert.NotNull(okObject.Value as Contact);
        }

        [Fact]
        public async Task WhenGettingContactsById_ThenNotFoundReturns()
        {
            var serviceMock = MockIContactService.GetMock();
            var contactController = new ContactController(serviceMock.Object); 
            var id = Guid.Parse("f4f4e3bf-afa6-4399-87b5-a3fe17572c4d");
            var result = await contactController.Get(id);
            var statusResult = result as StatusCodeResult;
            Assert.NotNull(statusResult);
            Assert.Equal(StatusCodes.Status404NotFound, statusResult.StatusCode);
        }

        [Fact]
        public async Task WhenPostContactsByCreateRequest_ThenCreatedIdReturn()
        {
            var serviceMock = MockIContactService.GetMock();
            var contactController = new ContactController(serviceMock.Object);

            var createRequest = new CreateRequest()
            {
                Name = "TestName",
                Surname = "TestSurName",
                Company = "TestCompany"
            };
            
            var result = await contactController.Post(createRequest);
            var objectRes = result as ObjectResult;

            Assert.NotNull(objectRes);
            Assert.IsAssignableFrom<CreatedAtRouteResult>(objectRes);
            Assert.Equal((int)HttpStatusCode.Created, objectRes!.StatusCode);
            Assert.Equal("ContactId", (objectRes as CreatedAtRouteResult)!.RouteName);
        }

        [Fact]
        public async Task WhenDeleteContactsById_ThenSuccessReturn()
        {
            var serviceMock = MockIContactService.GetMock();
            var contactController = new ContactController(serviceMock.Object);
            var id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
            var result = await contactController.Delete(id);
            var okObject = result as OkObjectResult;

            Assert.NotNull(okObject);
            Assert.Equal(StatusCodes.Status200OK, okObject.StatusCode);
            Assert.IsAssignableFrom<bool>(okObject.Value);            
            Assert.True((bool)okObject.Value); 
        }

        internal class MockIContactService
        {
            public static Mock<IContactService> GetMock()
            {
                var mock = new Mock<IContactService>();

                var contacts = new List<Contact>()
        {
            new Contact()
            {
                UUID = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Name = "John",
                Surname = "Surname",
                ContactInfos = new List<ContactInfo>()
                {
                    new ContactInfo()
                    {
                        UUID = new Guid(),
                        Info = "123456789",
                        InfoType = Enums.InfoTypes.Phone,
                        ContactId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")                        
                    },
                    new ContactInfo()
                    {
                        UUID = new Guid(),
                        Info = "Adana",
                        InfoType = Enums.InfoTypes.Location,
                        ContactId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")
                    },
                    new ContactInfo()
                    {
                        UUID = new Guid(),
                        Info = "john_Surname@gmail.com",
                        InfoType = Enums.InfoTypes.Email,
                        ContactId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")
                    },
                }
            },
            new Contact()
            {
                UUID = Guid.Parse("4db7b504-024d-40f4-9a45-5bb00502f56c"),
                Name = "Caldwell",
                Surname = "Roullier",
                ContactInfos = new List<ContactInfo>()
                {
                    new ContactInfo()
                    {
                        UUID = new Guid(),
                        Info = "212321312312",
                        InfoType = Enums.InfoTypes.Phone,
                        ContactId = Guid.Parse("4db7b504-024d-40f4-9a45-5bb00502f56c")
                    },
                    new ContactInfo()
                    {
                        UUID = new Guid(),
                        Info = "Ýstanbul",
                        InfoType = Enums.InfoTypes.Location,
                        ContactId = Guid.Parse("4db7b504-024d-40f4-9a45-5bb00502f56c")
                    },
                    new ContactInfo()
                    {
                        UUID = new Guid(),
                        Info = "caldwell_roullier@gmail.com",
                        InfoType = Enums.InfoTypes.Email,
                        ContactId = Guid.Parse("4db7b504-024d-40f4-9a45-5bb00502f56c")
                    },
                }
            }

        };

                mock.Setup(m => m.GetAll().Result).Returns(() => contacts);

                mock.Setup(m => m.GetById(It.IsAny<Guid>()).Result)
                    .Returns((Guid id) => contacts.FirstOrDefault(o => o.UUID == id));

                mock.Setup(m => m.Create(It.IsAny<CreateRequest>()))
                    .Callback(() => { return; });

                mock.Setup(m => m.Delete(It.IsAny<Guid>()))
                   .Callback(() => { return; });

                return mock;
            }
        }
    }
}
