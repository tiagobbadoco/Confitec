using Confitec.Application.Interfaces;
using Confitec.Domain.Entities;
using Confitec.Infrastructure.Data.Interfaces;

namespace Confitec.Application.Services
{
    public class EscolaridadeService : IEscolaridadeService
    {
        private readonly IEscolaridadeRepository _escolaridadeRepository;
        public EscolaridadeService(IEscolaridadeRepository escolaridadeRepository)
        {
            _escolaridadeRepository = escolaridadeRepository;
        }
        public List<Escolaridade> Listar()
        {
            return _escolaridadeRepository.Listar();
        }
    }
}
