using AutoMapper;
using GlobalTicketManagement.Application.Contracts.Persistence;
using GlobalTicketManagement.Domain.Entities;
using MediatR;
using SharedUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, List<CategoryListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Category> _categoryRepository;

        public GetCategoryListQueryHandler(IMapper mapper, IAsyncRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<List<CategoryListVm>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {            
            var allCategories = (await _categoryRepository.GetListAsync()).OrderBy(x => x.Name);
            return _mapper.Map<List<CategoryListVm>>(allCategories);
        }
    }
}
