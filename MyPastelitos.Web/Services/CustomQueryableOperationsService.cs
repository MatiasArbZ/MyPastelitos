using AutoMapper;
using MyPastelitos.Web.Data;
using MyPastelitos.Web.Core;
using MyPastelitos.Web.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MyPastelitos.Web.Services
{
    public class CustomQueryableOperationsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CustomQueryableOperationsService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<TDto>> CreateAsync<TDto, TEntity>(TDto dto) where TEntity : IID
        {
            try
            {
                TEntity entity = _mapper.Map<TEntity>(dto);

                Guid id = Guid.NewGuid();

                entity.Id = id;

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return Response<TDto>.Success(dto, "Entidad creada con éxito");
            }
            catch (Exception ex)
            {
                return Response<TDto>.Failure(ex)
;
            }
        }

        public async Task<Response<object>> DeleteAsync<TEntity>(Guid id) where TEntity : class, IID
        {
            try
            {
                TEntity? entity = await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

                if (entity == null)
                {
                    return Response<object>.Failure($"No existe registro con el ID {id}");
                }

                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return Response<object>.Success("Registro eliminada con éxito");
            }
            catch (Exception ex)
            {
                return Response<object>.Failure(ex);
            }
        }
    }
}
