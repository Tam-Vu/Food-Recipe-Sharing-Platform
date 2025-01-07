using AutoMapper;
using FluentValidation;
using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Dtos.RatingDto.CommandRating;
using FoodRecipeSharingPlatform.Dtos.RatingDto.ResponseRating;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Exceptions;
using FoodRecipeSharingPlatform.Interfaces;
using FoodRecipeSharingPlatform.Interfaces.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodRecipeSharingPlatform.Repositories;

public class RatingRepository : BaseRepository<Rating, Guid, CommandRating>, IRatingRepository
{
    private IBaseRepository<Rating, Guid, CommandRating> _ratingRepository;
    private IJwtService _jwtService;
    private IUserServiceRepository _userServiceRepository;
    private readonly ILogger<RatingRepository> _logger;
    private readonly UserManager<User> _userManager;
    public RatingRepository(ApplicationDbContext context, IMapper mapper, IRepositoryFactory repositoryFactory,
                                IJwtService jwtService, IUserServiceRepository userServiceRepository,
                                ILogger<RatingRepository> logger, UserManager<User> userManager) : base(context, mapper)
    {
        _ratingRepository = repositoryFactory.GetRepository<Rating, Guid, CommandRating>();
        _jwtService = jwtService;
        _userServiceRepository = userServiceRepository;
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<ResponseCommand> AddRating(Guid FoodId, CommandRating commandRating, CancellationToken cancellationToken)
    {
        try
        {
            var token = _jwtService.GetCurrentToken();
            var user = await _userServiceRepository.GetUserByToken(token!);
            var rating = _mapper.Map<Rating>(commandRating);
            rating.UserId = user!.Id;
            rating.FoodId = FoodId;
            var result = await _ratingRepository.AddAsync(rating, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(RatingRepository), nameof(AddRating), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("System error, please try later");
        }
    }

    public async Task<ResponseCommand> RemoveRating(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var token = _jwtService.GetCurrentToken();
            var user = await _userServiceRepository.GetUserByToken(token!);
            var role = await _userManager.GetRolesAsync(user!);
            var rating = await _ratingRepository.GetByIdAsync(id, cancellationToken);
            if (rating.UserId != user!.Id && !role.Contains("Admin"))
            {
                throw new ForbiddenException("You are not allowed to delete this rating");
            }
            var result = await _ratingRepository.DeleteAsync(rating, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(RatingRepository), nameof(RemoveRating), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("System error, please try later");
        }
    }

    public async Task<ResponseCommand> UpdateRating(Guid id, CommandRating commandRating, CancellationToken cancellationToken)
    {
        try
        {
            var token = _jwtService.GetCurrentToken();
            var user = await _userServiceRepository.GetUserByToken(token!);
            var rating = await _ratingRepository.GetByIdAsync(id, cancellationToken);
            if (rating.UserId != user!.Id)
            {
                throw new ForbiddenException("You are not allowed to update this rating");
            }
            _mapper.Map(commandRating, rating);
            var result = await _ratingRepository.UpdateAsync(rating, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(RatingRepository), nameof(UpdateRating), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("System error, please try later");
        }
    }

    public async Task<List<ResponseRating>> GetRatingsByFoodId(Guid FoodId, CancellationToken cancellationToken)
    {
        try
        {
            var ratings = await _ratingRepository.GetAllAsync(x => x.FoodId == FoodId,
                                                                p => p.Include(x => x.User),
                                                                cancellationToken);
            var result = _mapper.Map<List<ResponseRating>>(ratings);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(RatingRepository), nameof(GetRatingsByFoodId), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("System error, please try later");
        }
    }

    public async Task<List<ResponseRating>> GetRatingsByFoodIdAndStar(Guid FoodId, int Star, CancellationToken cancellationToken)
    {
        try
        {
            var ratings = await _ratingRepository.GetAllAsync(x => x.FoodId == FoodId && x.Star == Star,
                                                    p => p.Include(x => x.User),
                                                    cancellationToken);
            var result = _mapper.Map<List<ResponseRating>>(ratings);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(RatingRepository), nameof(GetRatingsByFoodIdAndStar), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("System error, please try later");
        }
    }

    public async Task<List<ResponseRating>> GetRatingsByFoodIdOrderByStar(Guid FoodId, string Order, CancellationToken cancellationToken)
    {
        try
        {
            var ratings = Order switch
            {
                "asc" => await _ratingRepository.GetAllAsync(x => x.FoodId == FoodId,
                                                        p => p.Include(x => x.User).OrderBy(x => x.Star),
                                                        cancellationToken),
                "desc" => await _ratingRepository.GetAllAsync(x => x.FoodId == FoodId,
                                                        p => p.Include(x => x.User).OrderByDescending(x => x.Star),
                                                        cancellationToken),
                _ => throw new BadRequestException("Invalid order")
            };
            var result = _mapper.Map<List<ResponseRating>>(ratings);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(RatingRepository), nameof(GetRatingsByFoodIdOrderByStar), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("System error, please try later");
        }
    }
}