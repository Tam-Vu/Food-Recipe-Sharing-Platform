using AutoMapper;
using FoodRecipeSharingPlatform.Dtos.Gredient.CommandIngredient;
using FoodRecipeSharingPlatform.Dtos.Gredient.ResposeIngredient;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Exceptions;

namespace FoodRecipeSharingPlatform.Interfaces;

public class IngredientRepository : IIngredientRepository
{
    private readonly IBaseRepository<Ingredient, Guid> _ingredientRepository;
    private readonly IMapper _mapper;

    public IngredientRepository(IRepositoryFactory repositoryFactory, IMapper mapper)
    {
        _ingredientRepository = repositoryFactory.GetRepository<Ingredient, Guid>();
        _mapper = mapper;
    }

    public async Task<ResponseCommand> AddIngredient(CommandIngredient commandIngredient, CancellationToken cancellationToken)
    {
        try
        {
            var ingredient = _mapper.Map<Ingredient>(commandIngredient);
            var result = await _ingredientRepository.AddAsync(ingredient, cancellationToken);
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