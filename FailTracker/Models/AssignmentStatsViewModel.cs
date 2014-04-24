using AutoMapper;
using FailTracker.Domain;
using FailTracker.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FailTracker.Models
{
    public class AssignmentStatsViewModel : IHaveCustomMappings
    {
        public string UserName { get; set; }
        public int Enhancements { get; set; }
        public int Bugs { get; set; }
        public int Support { get; set; }
        public int Other { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            Mapper.CreateMap<Domain.ApplicationUser, Models.AssignmentStatsViewModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(i => i.UserName))
                .ForMember(m => m.Enhancements, opt => opt.MapFrom(i => i.Assignments.Count(r => r.IssueType == IssueType.Enhancement)))
                .ForMember(m => m.Bugs, opt => opt.MapFrom(i => i.Assignments.Count(r => r.IssueType == IssueType.Bug)))
                .ForMember(m => m.Support, opt => opt.MapFrom(i => i.Assignments.Count(r => r.IssueType == IssueType.Support)))
                .ForMember(m => m.Other, opt => opt.MapFrom(i => i.Assignments.Count(r => r.IssueType == IssueType.Other)));
        }
    }
}