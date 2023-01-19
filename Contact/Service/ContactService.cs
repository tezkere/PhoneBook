namespace ContactApi.Service
{
    using ContactApi.Entities;
    using ContactApi.Model.Contact;
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAll();
        Task<Contact> GetById(Guid id);
        Task Create(CreateRequest model);        
        Task Delete(Guid id);
        Task CreateLocationReport();

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

    }
}
