namespace ContactApi.Service
{
    using AutoMapper;
    using ContactApi.Entities;
    using ContactApi.Helpers;
    using ContactApi.Model.ContactInfo;
    using Microsoft.EntityFrameworkCore;

    public interface IContactInfoService
    {

        Task Create(Guid contactId, CreateRequest model);
        Task Delete(Guid contactId, Guid id);

    }


    public class ContactInfoService : IContactInfoService
    {
        private ContactDbContext _context;
        private readonly IMapper _mapper;

        public ContactInfoService(ContactDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task Create(Guid contactId, CreateRequest model)
        {
            var contactInfo = _mapper.Map<ContactInfo>(model);
            contactInfo.ContactId = contactId;

            _context.ContactInfos.Add(contactInfo);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid contactId, Guid id)
        {
            var contactInfo = await getContactInfo(contactId, id);
            _context.ContactInfos.Remove(contactInfo);
            await _context.SaveChangesAsync();

        }

        private async Task<ContactInfo> getContactInfo(Guid contactId, Guid id)
        {
            var contactInfo = await _context.ContactInfos.FirstOrDefaultAsync(x => x.ContactId == contactId && x.UUID == id);
            if (contactInfo == null) throw new KeyNotFoundException("ContactInfo not found");
            return contactInfo;
        }
    }
}
