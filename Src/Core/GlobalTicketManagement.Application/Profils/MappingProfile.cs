using AutoMapper;
using GlobalTicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using GlobalTicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using GlobalTicketManagement.Application.Features.Events.Commands.CreateEvent;
using GlobalTicketManagement.Application.Features.Events.Queries.GetEventDetail;
using GlobalTicketManagement.Application.Features.Events.Queries.GetEventList;
using GlobalTicketManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Application.Profils
{
    public class MappingProfile:Profile
    {
        /// <summary>
        /// make pssible ReverseMap if property names are the same
        /// Event <=> EventViewModel
        /// </summary>
        public MappingProfile()
        {
                CreateMap<Event,EventViewModel>().ReverseMap();
                CreateMap<Event,EventDetailVm>().ReverseMap();
                CreateMap<Category, CategoryDto>();
                CreateMap<Category, CategoryListVm>();
                CreateMap<Category, CategoryEventListVm>();
                CreateMap<Category, CategoryDto>();
                
                CreateMap<CreateEventCommand, Event>().ReverseMap(); //ToDo Test to be ok
        }
    }
}
