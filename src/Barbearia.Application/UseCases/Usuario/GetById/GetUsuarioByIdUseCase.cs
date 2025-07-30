using AutoMapper;
using Barbearia.Communication.Request.Usuario;
using Barbearia.Communication.Response.Usuario;
using Barbearia.Domain.Repositories;

namespace Barbearia.Application.UseCases.Usuario.GetById;

public class GetUsuarioByIdUseCase: IGetUsuarioByIdUseCase
{
    private readonly  IUsuarioReadOnlyRepository _repository; 
    private readonly IMapper _mapper;

    public GetUsuarioByIdUseCase(IUsuarioReadOnlyRepository  repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseUsuarioJson> Execute(long id)
    {
        var usuario = await _repository.GetUsuarioById(id);
        var response =  _mapper.Map<ResponseUsuarioJson>(usuario);
        
        return response; 
        // var usuario = _mapper.Map<Usuario>(Id);
    }
    
}