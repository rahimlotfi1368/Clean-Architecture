using AutoMapper;
using GlobalTicketManagement.Application.Features.Categories.Commands.CreateCateogry;
using GlobalTicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using GlobalTicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using GlobalTicketManagement.Application.Features.Events.Commands.CreateEvent;
using GlobalTicketManagement.Application.Features.Events.Commands.UpdateEvent;
using GlobalTicketManagement.Application.Features.Events.Queries.GetEventDetail;
using GlobalTicketManagement.Application.Features.Events.Queries.GetEventList;
using GlobalTicketManagement.Application.Features.Orders.Queries;
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
                CreateMap<Event,EventListVm>().ReverseMap();
                CreateMap<Event,EventDetailVm>().ReverseMap();
                CreateMap<Event, CreateEventCommand>().ReverseMap();
                CreateMap<Event, UpdateEventCommand>().ReverseMap();
                CreateMap<Event,CreateEventCommand>().ReverseMap(); //ToDo Test to be ok
                CreateMap<Event,CategoryEventDto>().ReverseMap(); //ToDo Test to be ok

                CreateMap<Category, CategoryDto>();
                CreateMap<Category, CategoryListVm>();
                CreateMap<Category, CategoryEventListVm>().ReverseMap();
                CreateMap<Category, CreateCategoryCommand>();
                CreateMap<Category, CreateCategoryDto>();
           
                CreateMap<Order, OrdersForMonthDto>();

        }
    }
}
