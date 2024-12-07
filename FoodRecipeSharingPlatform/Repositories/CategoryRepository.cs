using AutoMapper;
using FoodRecipeSharingPlatform.Dtos.CategoryDto.CommandCategory;
using FoodRecipeSharingPlatform.Dtos.CategoryDto.ResponseCategory;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Exceptions;
using FoodRecipeSharingPlatform.Interfaces;

namespace FoodRecipeSharingPlatform.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IBaseRepository<Category, Guid> _categoryRepository;
    private readonly IMapper _mapper;
    public CategoryRepository(IRepositoryFactory repositoryFactory, IMapper mapper)
    {
        _categoryRepository = repositoryFactory.GetRepository<Category, Guid>();
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
            var newCategory = _mapper.Map<Category>(commandCategory);
            return await _categoryRepository.AddAsync(newCategory, cancellationToken);
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