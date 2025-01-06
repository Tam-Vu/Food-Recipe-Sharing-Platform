using AutoMapper;
using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFood;
using FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFoodIngredient;
using FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandStep;
using FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFood;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Exceptions;
using FoodRecipeSharingPlatform.Interfaces;
using FoodRecipeSharingPlatform.Interfaces.Builder;
using FoodRecipeSharingPlatform.Interfaces.Security;
using Microsoft.EntityFrameworkCore;

namespace FoodRecipeSharingPlatform.Repositories;

public class FoodRepository : BaseRepository<Food, Guid, CommandFood>, IFoodRepository
{
    private readonly IFoodBuilder _foodBuilder;
    private readonly IIdentityService _identityService;
    private readonly IBaseRepository<Food, Guid, CommandFood> _foodRepository;
    private readonly ILogger<FoodRepository> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<FoodIngredient, Guid, CommandFoodIngredients> _foodIngredientRepository;
    private readonly IJwtService _jwtService;
    private readonly IUserServiceRepository _userServiceRepository;

    public FoodRepository(ApplicationDbContext context, IMapper mapper, IRepositoryFactory repositoryFactory,
                            IFoodBuilder foodBuilder, IIdentityService identityService, ILogger<FoodRepository> logger, IUnitOfWork unitOfWork,
                            IJwtService jwtService, IUserServiceRepository userServiceRepository) : base(context, mapper)
    {
        _foodRepository = repositoryFactory.GetRepository<Food, Guid, CommandFood>();
        _foodIngredientRepository = repositoryFactory.GetRepository<FoodIngredient, Guid, CommandFoodIngredients>();
        _foodBuilder = foodBuilder;
        _identityService = identityService;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _userServiceRepository = userServiceRepository;
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
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(FoodRepository), nameof(CreateFoodAsync), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("Failed to create food, please try later");
        }
    }

    public Task<ResponseCommand> DeleteFoodAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return _foodRepository.DeleteByIdAsync(id, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(FoodRepository), nameof(DeleteFoodAsync), e.Message, e.InnerException?.Message);
            throw new BadRequestException("Failed to delete food, please try later");
        }
    }

    public async Task<ResponseFood> GetFoodByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var food = await _foodRepository.GetSingleAsync(c => c.Id == id, i => i.Include(p => p.Category!)
                                                                                .ThenInclude(p => p.Foods!)
                                                                                .Include(p => p.Steps!)
                                                                                .Include(p => p.FoodIngredients!)
                                                                                .ThenInclude(p => p.Ingredient!)
                                                                                .ThenInclude(p => p.FoodIngredients!)
                                                                                .ThenInclude(p => p.Food!)
                                                                                .ThenInclude(p => p.User)
                                                                                , cancellationToken);
            var result = _mapper.Map<ResponseFood>(food);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(FoodRepository), nameof(GetFoodByIdAsync), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("Failed to get food, please try later");
        }
    }

    public async Task<List<ResponseListFood>> GetFoodsByCategory(Guid CategoryId, CancellationToken cancellationToken)
    {
        try
        {
            var foods = await _foodRepository.GetAllAsync(c => c.CategoryId == CategoryId, cancellationToken);
            var result = _mapper.Map<List<ResponseListFood>>(foods);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(FoodRepository), nameof(GetFoodsByCategory), e.Message, e.InnerException?.Message);
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
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(FoodRepository), nameof(GetFoodsByNameAsync), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("Failed to get food, please try later");
        }
    }

    public async Task<List<ResponseListFood>> GetFoodsByUserId(Guid UserId, CancellationToken cancellationToken)
    {
        try
        {
            var foods = await _foodRepository.GetAllAsync(c => c.UserId == UserId, cancellationToken);
            var result = _mapper.Map<List<ResponseListFood>>(foods);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(FoodRepository), nameof(GetFoodsByUserId), e.Message, e.InnerException?.Message);
            throw new BadRequestException("Failed to get food, please try later");
        }
    }

    public async Task<List<ResponseListFood>> GetFoodsOfCurrentUser(CancellationToken cancellationToken)
    {
        try
        {
            var token = _jwtService.GetCurrentToken();
            var user = await _userServiceRepository.GetUserByToken(token!);
            var foods = await _foodRepository.GetAllAsync(c => c.UserId == user!.Id, cancellationToken);
            var result = _mapper.Map<List<ResponseListFood>>(foods);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(FoodRepository), nameof(GetFoodsOfCurrentUser), e.Message, e.InnerException?.Message);
            throw new BadRequestException("Failed to get food, please try later");
        }
    }

    public async Task<ResponseCommand> UpdateFoodAsync(Guid id, CommandFood commandFoodDto, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.CreateTransaction();
            var food = await _foodRepository.GetSingleAsync(c => c.Id == id, i => i.Include(p => p.Category!)
                                                                                .Include(p => p.Steps!)
                                                                                    , cancellationToken);
            var userId = Guid.Parse(_identityService.GetUserId()!);
            if (food.UserId != userId)
            {
                throw new ForbiddenException("You are not allowed to update this food");
            }

            food.Steps?.Clear();
            await _foodIngredientRepository.DeleteByMulti(x => x.FoodId == food.Id, cancellationToken);

            _foodBuilder.SetName(commandFoodDto.Name, commandFoodDto.CategoryId, userId);
            _foodBuilder.SetDescription(commandFoodDto.Description, commandFoodDto.Image);
            var foodIngredients = _mapper.Map<IEnumerable<FoodIngredient>>(commandFoodDto.CommandFoodIngredient);
            _foodBuilder.SetFoodIngredients(foodIngredients);
            var steps = _mapper.Map<IEnumerable<Step>>(commandFoodDto.CommandStep);
            _foodBuilder.SetSteps(steps);
            var updatedFood = _foodBuilder.Build();

            _mapper.Map(updatedFood, food);
            var result = await _foodRepository.UpdateAsync(food, cancellationToken);
            _unitOfWork.Commit();
            return result;
        }
        catch (Exception e)
        {
            _unitOfWork.Rollback();
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(FoodRepository), nameof(UpdateFoodAsync), e.Message, e.InnerException?.Message);
            throw new BadRequestException("Some errors occurred, please try again later");
        }
    }

}