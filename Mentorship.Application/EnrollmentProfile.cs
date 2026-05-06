using System;
using AutoMapper;
using Mentorship.Core.Entities;
using Mentorship.Shared.Contracts.v1.Enrollments;

namespace Mentorship.Application;

public class EnrollmentProfile: Profile
{
    public EnrollmentProfile()
    {
        CreateMap<Enrollment, EnrollmentResponse>()
            .ForMember(dest => dest.ProgramTitle, 
                       opt => opt.MapFrom(src => src.Program != null ? src.Program.Title : "Unknown"));
            // .ForMember(dest => dest.StudentName,
            //            opt => opt.MapFrom(src => src.Student != null ? 
            //                $"{src.Student.FirstName} {src.Student.LastName}" : "Unknown"));
    }
}
