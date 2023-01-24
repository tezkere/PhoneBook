namespace ContactApi.Service
{
    using AutoMapper;
    using ContactApi.Entities;
    using ContactApi.Helpers;
    using ContactApi.Model.Contact;
    using Microsoft.EntityFrameworkCore;

    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAll();
        Task<Contact> GetById(Guid id);
        Task<Guid> Create(CreateRequest model);
        Task Delete(Guid id);        

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

        public async Task<Guid> Create(CreateRequest model)
        {
            // map model to new contact object
            var contact = _mapper.Map<Contact>(model);

            // save contact

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact.UUID;
        }       

        public async Task Delete(Guid id)
        {
            var contact = await getContact(id);

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
            return await getContact(id);
        }       

        private async Task<Contact> getContact(Guid id)
        {
            var contact = await _context.Contacts.Include(x => x.ContactInfos).FirstOrDefaultAsync(x => x.UUID == id);                
            if (contact == null) throw new KeyNotFoundException("Contact not found");
            return contact;
        }
    }
}
