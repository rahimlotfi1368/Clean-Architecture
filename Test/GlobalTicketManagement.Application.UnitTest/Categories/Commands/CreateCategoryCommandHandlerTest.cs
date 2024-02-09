using AutoMapper;
using GlobalTicketManagement.Application.Contracts.Persistence;
using GlobalTicketManagement.Application.Features.Categories.Commands.CreateCateogry;
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

namespace GlobalTicketManagement.Application.UnitTest.Categories.Commands
{
    public class CreateCategoryCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Category>> _mockCategoryRepository;

        public CreateCategoryCommandHandlerTest()
        {
            _mockCategoryRepository = RepositoryMocks.GetBaseCategoryRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task CreateNewCategoryCommandTest()
        {
            var countBefor= FackeDataGenerator.Categories.Count();
            var handler= new CreateCategoryCommandHandler(_mapper,_mockCategoryRepository.Object);
            var result = await handler.Handle(new CreateCategoryCommand { Name="Test"}, CancellationToken.None);

            result.ShouldBeOfType <CreateCategoryCommandResponse>();
            result.Category.ShouldNotBeNull();

            var allCategories = await _mockCategoryRepository.Object.GetListAsync();
            allCategories.Count.ShouldBe(countBefor + 1);
        }
    }
}
