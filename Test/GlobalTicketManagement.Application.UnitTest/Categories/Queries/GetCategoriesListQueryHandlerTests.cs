using AutoMapper;
using GlobalTicketManagement.Application.Contracts.Persistence;
using GlobalTicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using GlobalTicketManagement.Application.Profils;
using GlobalTicketManagement.Application.UnitTest.Mocks;
using GlobalTicketManagement.Domain.Entities;
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
    public class GetCategoriesListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Category>> _mockCategoryRepository;

        public GetCategoriesListQueryHandlerTests()
        {
            _mockCategoryRepository = RepositoryMocks.GetBaseCategoryRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCategoriesListTest()
        {
            var handler = new GetCategoryListQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result=await handler.Handle(new GetCategoryListQuery(),CancellationToken.None);

            result.ShouldBeOfType<List<CategoryListVm>>();

            result.Count.ShouldBe(FackeDataGenerator.Categories.Count());
        }
    }
}
