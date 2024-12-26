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

    public CategoryRepository(ApplicationDbContext context, IMapper mapper, IRepositoryFactory repositoryFactory) : base(context, mapper)
    {
        _categoryRepository = repositoryFactory.GetRepository<Category, Guid, CommandCategory>();
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
            Console.WriteLine(e.Message);
            throw new BadRequestException("Some errors occurred, please try again later");
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
            Console.WriteLine(e.Message);
            throw new BadRequestException("Some errors occurred, please try again later");
        }
    }

    public async Task<List<ResponseCategory>> GetAllCategoriesByName(string name, CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync(x => x.Name.Contains(name), cancellationToken);
            var result = _mapper.Map<List<ResponseCategory>>(categories);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new BadRequestException("Some errors occurred, please try again later");
        }
    }

    public async Task<ResponseCommand> UpdateCategory(Guid id, CommandCategory commandCategory, CancellationToken cancellationToken)
    {
        try
        {
            var checkCategory = await _categoryRepository.FindOneAsync(x => x.Name == commandCategory.Name, cancellationToken);
            if (checkCategory != null)
            {
                throw new BadRequestException($"{commandCategory.Name} is already existed in category");
            }
            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            if (category == null)
            {
                throw new BadRequestException("Category not found");
            }
            _mapper.Map(commandCategory, category);
            return await _categoryRepository.UpdateAsync(category, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new BadRequestException("Some errors occurred, please try again later");
        }
    }
}