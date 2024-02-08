using GlobalTicketManagement.Application.Contracts.Persistence;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalTicketManagement.Domain.Entities;
using SharedUtilities;

namespace GlobalTicketManagement.Application.UnitTest.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IAsyncRepository<Category>> GetBaseCategoryRepository()
        {

            FackeDataGenerator.InitializeFackeDataGenerator();
            var categories = FackeDataGenerator.Categories;

            var mockCategoryRepository = new Mock<IAsyncRepository<Category>>();
            mockCategoryRepository.Setup(repo => repo.GetListAsync()).ReturnsAsync(categories);

            mockCategoryRepository.Setup(repo => repo.AddAsync(It.IsAny<Category>())).ReturnsAsync(
                (Category category) =>
                {
                    categories.Add(category);
                    return category;
                });

            return mockCategoryRepository;
        }

        public static Mock<ICategoryRepository> GetCategoryRepository()
        {

            FackeDataGenerator.InitializeFackeDataGenerator();
            var categories = FackeDataGenerator.Categories;

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repo => repo.GetCategoriesWithEvents(true)).ReturnsAsync(categories);
            //mockCategoryRepository.Setup(repo => repo.GetCategoriesWithEvents(false)).ReturnsAsync(categories);
            return mockCategoryRepository;
        }
    }
}
