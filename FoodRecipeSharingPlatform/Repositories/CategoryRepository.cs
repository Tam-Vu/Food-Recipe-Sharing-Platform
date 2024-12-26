using AutoMapper;
using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Dtos.CategoryDto.CommandCategory;
using FoodRecipeSharingPlatform.Dtos.CategoryDto.ResponseCategory;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Exceptions;
using FoodRecipeSharingPlatform.Interfaces;

namespace FoodRecipeSharingPlatform.Repositories;

public class CategoryRepository : BaseRepository<Category, Guid, CommandCategory>, ICategoryRepository
{
    private readonly IBaseRepository<Category, Guid, CommandCategory> _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryRepository(ApplicationDbContext context, IMapper mapper, IRepositoryFactory repositoryFactory) : base(context, mapper)
    {
        _categoryRepository = repositoryFactory.GetRepository<Category, Guid, CommandCategory>();
        _mapper = mapper;
    }

    public async Task<ResponseCommand> AddCategory(CommandCategory commandCategory, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _categoryRepository.FindOneAsync(x => x.Name == commandCategory.Name, cancellationToken);
            if (category != null)
            {
                throw new BadRequestException($"{commandCategory.Name} is already existed in category");
            }
            return await _categoryRepository.AddAsync(commandCategory, cancellationToken);
        }
        catch (Exception e)
        {
            throw new BadRequestException(e.Message);
        }
    }

    public async Task<ResponseCommand> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            return await _categoryRepository.DeleteAsync(category, cancellationToken);
        }
        catch (Exception e)
        {
            throw new BadRequestException(e.Message);
        }
    }

    public async Task<List<ResponseCategory>> GetAllCategories(CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);
            var result = _mapper.Map<List<ResponseCategory>>(categories);
            return result;
        }
        catch (Exception e)
        {
            throw new BadRequestException(e.Message);
        }
    }

    public Task<List<ResponseCategory>> GetAllCategoriesByName(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseCommand> UpdateCategory(Guid id, CommandCategory commandCategory, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            _mapper.Map(commandCategory, category);
            return await _categoryRepository.UpdateAsync(category, cancellationToken);
        }
        catch (Exception e)
        {
            throw new BadRequestException(e.Message);
        }
    }
}