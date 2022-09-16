using Confitec.Application.ViewModel;
using Microsoft.AspNetCore.Http;

namespace Confitec.Application.Interfaces
{
    public interface IUsuarioService
    {
        List<UsuarioViewModel> Listar();
        UsuarioViewModel GetById(int id);
        int Criar(UsuarioViewModel usuario);
        bool Atualizar(UsuarioViewModel usuario);
        bool Deletar(int id);
        bool EnviarHistoricoEscolar(int usuarioId, IFormFile arquivo);
        HistoricoEscolarModel BaixarHistoricoEscolar(int usuarioId);
    }
}
