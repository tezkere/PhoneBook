namespace ContactApi.Service
{
    using ContactApi.Model.ContactInfo;
    public interface IContactInfoService {

        Task Create(Guid contactId,CreateRequest model);
        Task Delete(Guid contactId,Guid id);

    }


    public class ContactInfoService : IContactInfoService
    {
    }
}
