using AutoMapper;
using GlobalTicketManagement.Application.Contracts.Persistence;
using MediatR;
using SharedUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    public class GetCategoriesListWithEventsQueryHandler : IRequestHandler<GetCategoriesListWithEventsQuery, List<CategoryEventListVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesListWithEventsQueryHandler(IMapper mapper,ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<List<CategoryEventListVm>> Handle(GetCategoriesListWithEventsQuery request, CancellationToken cancellationToken)
        {
            FackeDataGenerator.InitializeFackeDataGenerator();
            var categories = FackeDataGenerator.Categories;
            var list = await _categoryRepository.GetCategoriesWithEvents(request.IncludeHistory);

            var result= _mapper.Map<List<CategoryEventListVm>>(list);

            return result;
        }
    }
}
