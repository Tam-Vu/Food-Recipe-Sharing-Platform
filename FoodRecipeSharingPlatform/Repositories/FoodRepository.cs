using AutoMapper;
using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFood;
using FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFood;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Exceptions;
using FoodRecipeSharingPlatform.Interfaces;
using FoodRecipeSharingPlatform.Interfaces.Builder;
using FoodRecipeSharingPlatform.Interfaces.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace FoodRecipeSharingPlatform.Repositories;

public class FoodRepository : BaseRepository<Food, Guid, CommandFood>, IFoodRepository
{
    private readonly IFoodBuilder _foodBuilder;
    private readonly IIdentityService _identityService;
    private readonly IBaseRepository<Food, Guid, CommandFood> _foodRepository;

    public FoodRepository(ApplicationDbContext context, IMapper mapper, IRepositoryFactory repositoryFactory, IFoodBuilder foodBuilder, IIdentityService identityService) : base(context, mapper)
    {
        _foodRepository = repositoryFactory.GetRepository<Food, Guid, CommandFood>();
        _foodBuilder = foodBuilder;
        _identityService = identityService;
    }

    public async Task<ResponseCommand> CreateFoodAsync(CommandFood commandFood, CancellationToken cancellationToken)
    {
        try
        {
            var userId = Guid.Parse(_identityService.GetUserId()!);
            _foodBuilder.SetName(commandFood.Name, commandFood.CategoryId, userId);
            if (commandFood.Image != null || commandFood.Description != null)
            {
                _foodBuilder.SetDescription(commandFood.Description, commandFood.Image);
            }
            if (commandFood.CommandFoodIngredient != null && commandFood.CommandFoodIngredient.Any())
            {
                var foodIngredients = _mapper.Map<IEnumerable<FoodIngredient>>(commandFood.CommandFoodIngredient);
                _foodBuilder.SetFoodIngredients(foodIngredients);
            }
            if (commandFood.CommandStep != null && commandFood.CommandStep.Any())
            {
                var steps = _mapper.Map<IEnumerable<Step>>(commandFood.CommandStep);
                _foodBuilder.SetSteps(steps);
            }
            var result = await _foodRepository.AddAsync(_foodBuilder.Build(), cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                throw new BadRequestException("Failed to create food, please try later" + ex.InnerException.Message);
            }
            Console.WriteLine(ex.Message);
            throw new BadRequestException("Failed to create food, please try later" + ex.Message);
        }
    }

    public Task<List<ResponseFood>> GetAllFoodsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var foods = _foodRepository.GetAllAsync(i => i.Include(x => x.FoodIngredients!)
                                                            .ThenInclude(x => x.Food!)
                                                            .Include(x => x.Steps!),
                                                        cancellationToken);
            var result = _mapper.Map<List<ResponseFood>>(foods);
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Log GetFoodsByNameAsync error: " + ex.Message);
            throw new BadRequestException("Failed to get food, please try later");
        }
    }

    public async Task<List<ResponseListFood>> GetFoodsByNameAsync(string name, CancellationToken cancellationToken)
    {
        try
        {
            var foods = await _foodRepository.GetAllAsync(c => c.Name.ToLower().Contains(name), cancellationToken);
            var result = _mapper.Map<List<ResponseListFood>>(foods);
            return result;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Log GetFoodsByNameAsync error: " + ex.Message);
            throw new BadRequestException("Failed to get food, please try later");
        }
    }
}