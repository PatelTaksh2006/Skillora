using AutoMapper;
using Skillora.Models.Entities;
using Skillora.Models.ViewModels;

namespace Skillora.Mappings
{
    public class Helper:Profile
    {
        public Helper() {
            CreateMap<Student, StudentViewModel>();
            CreateMap<CreateStudentViewModel, Student>().ReverseMap();
            CreateMap<CreateCompanyViewModel, Company>();
            CreateMap<Company, CompanyViewModel>();
            CreateMap<CreateJobViewModel, Job>().ReverseMap();
            CreateMap<CreateJobViewModel, JobConstraint>().ReverseMap();
            CreateMap<EditStudentViewModel, Student>().ReverseMap();
            CreateMap<EditJobViewModel, Job>().ReverseMap();
            CreateMap<EditJobViewModel, JobConstraint>().ReverseMap();
            CreateMap<Student,DeleteStudentViewModel>().ReverseMap();
            CreateMap<Job, DeleteJobViewModel>();
        }
    }
}
