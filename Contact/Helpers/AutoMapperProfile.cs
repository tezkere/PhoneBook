using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateRequest -> Contact
        CreateMap<ContactApi.Model.Contact.CreateRequestForContact, ContactApi.Entities.Contact>();

        // CreateRequest -> ContactInfo
        CreateMap<ContactApi.Model.ContactInfo.CreateRequestForContactInfo, ContactApi.Entities.ContactInfo>();
        
    }
}
