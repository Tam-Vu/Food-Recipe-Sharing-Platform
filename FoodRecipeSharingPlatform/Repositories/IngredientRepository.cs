using AutoMapper;
using FoodRecipeSharingPlatform.Dtos.IngredientDto.CommandIngredient;
using FoodRecipeSharingPlatform.Dtos.IngredientDto.ResposeIngredient;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Exceptions;

namespace FoodRecipeSharingPlatform.Interfaces;

public class IngredientRepository : IIngredientRepository
{
    private readonly IBaseRepository<Ingredient, Guid, CommandIngredient> _ingredientRepository;
    private readonly IMapper _mapper;

    public IngredientRepository(IRepositoryFactory repositoryFactory, IMapper mapper)
    {
        _ingredientRepository = repositoryFactory.GetRepository<Ingredient, Guid, CommandIngredient>();
        _mapper = mapper;
    }

    public async Task<ResponseCommand> AddIngredient(CommandIngredient commandIngredient, CancellationToken cancellationToken)
    {
        try
        {
            // var ingredient = _mapper.Map<Ingredient>(commandIngredient);
            var result = await _ingredientRepository.AddAsync(commandIngredient, cancellationToken);
            var response = _mapper.Map<ResponseCommand>(result);
            return response;
        }
        catch (Exception e)
        {
            throw new BadRequestException(e.Message);
        }
    }

    public async Task<ResponseCommand> DeleteIngredient(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id, cancellationToken);
            return await _ingredientRepository.DeleteAsync(ingredient, cancellationToken);
        }
        catch (Exception e)
        {
            throw new BadRequestException(e.Message);
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
            throw new BadHttpRequestException(e.Message);
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
            throw new BadRequestException(e.Message);
        }
    }

    public async Task<ResponseCommand> UpdateIngredient(Guid id, CommandIngredient commandIngredient, CancellationToken cancellationToken)
    {
        try
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id, cancellationToken);
            _mapper.Map(commandIngredient, ingredient);
            return await _ingredientRepository.UpdateAsync(ingredient, cancellationToken);
        }
        catch (Exception e)
        {
            throw new BadRequestException(e.Message);
        }
    }


}