namespace ContactApi.Service
{
    using AutoMapper;
    using ContactApi.Entities;
    using ContactApi.Helpers;
    using ContactApi.Model.Contact;
    using ContactApi.Model.ReportInfo;
    using Microsoft.EntityFrameworkCore;

    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAll();
        Task<Contact> GetById(Guid id);
        Task<Contact> Create(CreateRequestForContact model);
        Task Delete(Guid id);
        Task<ContactInfo> CreateContactInfo(Model.ContactInfo.CreateRequestForContactInfo model);
        Task DeleteContactInfo(Guid contactId, Guid id);
        Task<ReportInfo> GetReportInfo();

    }

    //  Rehberde kişi oluşturma ---- ok 
    //• Rehberde kişi kaldırma --- ok 
    //• Rehberdeki kişiye iletişim bilgisi ekleme -- ok 
    //• Rehberdeki kişiden iletişim bilgisi kaldırma -- ok
    //• Rehberdeki kişilerin listelenmesi -- ok
    //• Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin getirilmesi -- ok
    //• Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor talebi
    //• Sistemin oluşturduğu raporların listelenmesi
    //• Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmes


    public class ContactService : IContactService
    {
        private ContactDbContext _context;
        private readonly IMapper _mapper;

        public ContactService(
            ContactDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Contact> Create(CreateRequestForContact model)
        {
            // map model to new contact object
            var contact = _mapper.Map<Contact>(model);

            // save contact

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }       

        public async Task Delete(Guid id)
        {
            var contact = await GetContact(id);

            foreach (var info in contact.ContactInfos)
            {
                _context.ContactInfos.Remove(info);
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetById(Guid id)
        {
            return await GetContact(id);
        }

        public  Task<ReportInfo> GetReportInfo()
        {


            //var res = _context.ContactInfos.Where(x => x.InfoType == Shared.Enums.InfoTypes.Location)
            //                               .GroupBy(x => x.Info).Select(x => x.Key);


                                           

            return null;
        }

        public async Task<ContactInfo> CreateContactInfo(Model.ContactInfo.CreateRequestForContactInfo model)
        {
            var contactInfo = _mapper.Map<ContactInfo>(model);            

            _context.ContactInfos.Add(contactInfo);
            await _context.SaveChangesAsync();
            return contactInfo;
        }

        public async Task DeleteContactInfo(Guid contactId, Guid id)
        {
            var contactInfo = await GetContactInfo(contactId, id);
            _context.ContactInfos.Remove(contactInfo);
            await _context.SaveChangesAsync();
        }

        private async Task<ContactInfo> GetContactInfo(Guid contactId, Guid id)
        {
            var contactInfo = await _context.ContactInfos.FirstOrDefaultAsync(x => x.ContactId == contactId && x.UUID == id);
            if (contactInfo == null) throw new KeyNotFoundException("ContactInfo not found");
            return contactInfo;
        }

        private async Task<Contact> GetContact(Guid id)
        {
            var contact = await _context.Contacts.Include(x => x.ContactInfos).FirstOrDefaultAsync(x => x.UUID == id);                
            if (contact == null) throw new KeyNotFoundException("Contact not found");
            return contact;
        }

        
    }
}
