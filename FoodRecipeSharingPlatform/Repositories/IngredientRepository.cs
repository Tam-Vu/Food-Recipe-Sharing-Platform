using AutoMapper;
using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Dtos.IngredientDto.CommandIngredient;
using FoodRecipeSharingPlatform.Dtos.IngredientDto.ResposeIngredient;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Exceptions;
using FoodRecipeSharingPlatform.Repositories;

namespace FoodRecipeSharingPlatform.Interfaces;

public class IngredientRepository : BaseRepository<Ingredient, Guid, CommandIngredient>, IIngredientRepository
{
    private readonly IBaseRepository<Ingredient, Guid, CommandIngredient> _ingredientRepository;

    public IngredientRepository(ApplicationDbContext context, IMapper mapper, IRepositoryFactory repositoryFactory) : base(context, mapper)
    {
        _ingredientRepository = repositoryFactory.GetRepository<Ingredient, Guid, CommandIngredient>();
    }

    public async Task<ResponseCommand> AddIngredient(CommandIngredient commandIngredient, CancellationToken cancellationToken)
    {
        try
        {
            var checkIngredient = await _ingredientRepository.FindOneAsync(x => x.Name == commandIngredient.Name, cancellationToken);
            if (checkIngredient != null)
            {
                throw new BadRequestException($"{commandIngredient.Name} is already existed in ingredient");
            }
            var result = await _ingredientRepository.AddAsync(commandIngredient, cancellationToken);
            var response = _mapper.Map<ResponseCommand>(result);
            return response;
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new BadRequestException("Some errors occurred, please try again later");
        }
    }

    public async Task<List<ResposeIngredient>> GetAllIngredients(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ingredientRepository.GetAllAsync(cancellationToken);
            var response = _mapper.Map<List<ResposeIngredient>>(result);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new BadRequestException("Some errors occurred, please try again later");
        }
    }

    public async Task<List<ResposeIngredient>> GetAllIngredientsByName(string name, CancellationToken cancellationToken)
    {
        try
        {
            var ingredients = await _ingredientRepository.GetAllAsync(x => x.Name.Contains(name), cancellationToken);
            var result = _mapper.Map<List<ResposeIngredient>>(ingredients);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new BadRequestException("Some errors occurred, please try again later");
        }
    }

    public async Task<ResponseCommand> UpdateIngredient(Guid id, CommandIngredient commandIngredient, CancellationToken cancellationToken)
    {
        try
        {
            var checkIngredient = await _ingredientRepository.FindOneAsync(x => x.Name == commandIngredient.Name, cancellationToken);
            if (checkIngredient != null)
            {
                throw new BadRequestException($"{commandIngredient.Name} already exists");
            }
            var ingredient = await _ingredientRepository.GetByIdAsync(id, cancellationToken);
            if (ingredient == null)
            {
                throw new BadRequestException($"{commandIngredient.Name} not found");
            }
            _mapper.Map(commandIngredient, ingredient);
            return await _ingredientRepository.UpdateAsync(ingredient, cancellationToken);
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new BadRequestException("Some errors occurred, please try again later");
        }
    }


}