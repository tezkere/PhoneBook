using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateRequest -> Contact
        CreateMap<ContactApi.Model.Contact.CreateRequest, ContactApi.Entities.Contact>();

        // CreateRequest -> ContactInfo
        CreateMap<ContactApi.Model.ContactInfo.CreateRequest, ContactApi.Entities.ContactInfo>();
        
    }
}
