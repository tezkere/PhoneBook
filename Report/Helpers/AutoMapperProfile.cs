using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // ReportInfo -> ReportDetail
        CreateMap<ReportApi.Model.ReportInfo, ReportApi.Entities.ReportDetail>();
    }
}
