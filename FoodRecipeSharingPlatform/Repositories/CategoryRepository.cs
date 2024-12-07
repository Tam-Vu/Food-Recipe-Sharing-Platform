using FoodRecipeSharingPlatform.Dtos.Category;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Interfaces;

namespace FoodRecipeSharingPlatform.Repositories;

public class CategoryRepository
{
    private readonly IBaseRepository<Category, Guid> _categoryRepository;
    public CategoryRepository(IRepositoryFactory repositoryFactory)
    {
        _categoryRepository = repositoryFactory.GetRepository<Category, Guid>();
    }
}