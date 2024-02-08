using AutoMapper;
using GlobalTicketManagement.Application.Contracts.Persistence;
using GlobalTicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using GlobalTicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using GlobalTicketManagement.Application.Profils;
using GlobalTicketManagement.Application.UnitTest.Mocks;
using Moq;
using SharedUtilities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Application.UnitTest.Categories.Queries
{
    public class GetCategoriesListWithEventsQueryHandlerTestes
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        public GetCategoriesListWithEventsQueryHandlerTestes()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCategoriesListWithEventsTest_WithHistory()
        {
            var handler = new GetCategoriesListWithEventsQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetCategoriesListWithEventsQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<CategoryListVm>>();

            result.Count.ShouldBe(FackeDataGenerator.Categories.Count());
        }

    }
}
