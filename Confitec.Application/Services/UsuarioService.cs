using Confitec.Application.Interfaces;
using Confitec.Application.Validators;
using Confitec.Application.ViewModel;
using Confitec.Domain.Entities;
using Confitec.Infrastructure.Data.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System.Net.Http.Headers;

namespace Confitec.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEscolaridadeRepository _escolaridadeRepository;
        private readonly IHistoricoEscolarRepository _historicoEscolarRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository, IEscolaridadeRepository escolaridadeRepository, IHistoricoEscolarRepository historicoEscolarRepository)
        {
            _usuarioRepository = usuarioRepository;
            _escolaridadeRepository = escolaridadeRepository;
            _historicoEscolarRepository = historicoEscolarRepository;
        }
        public List<UsuarioViewModel> Listar()
        {
            return _usuarioRepository.Listar()
                .Select(usuario => new UsuarioViewModel {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email,
                    DataNascimento = usuario.DataNascimento.ToString("yyyy-MM-dd"),
                    Escolaridade = usuario.Escolaridade.Descricao
                }).ToList();
        }

        public UsuarioViewModel GetById(int id)
        {
            Usuario? usuario = _usuarioRepository.GetById(id);

            if (usuario == null)
                throw new Exception("Não foi possível encontrar o usuário");

            return new UsuarioViewModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                DataNascimento = usuario.DataNascimento.ToString("yyyy-MM-dd"),
                Escolaridade = usuario.Escolaridade.Descricao
            };
        }

        public bool Deletar(int id)
        {
            Usuario? usuario = _usuarioRepository.GetById(id);

            if (usuario == null)
                throw new Exception("Não foi encontrado um usuário com o Id informado");

            _usuarioRepository.Deletar(usuario);
            _usuarioRepository.SaveChanges();
            return true;
        }

        public int Criar(UsuarioViewModel usuario)
        {
            PostUsuarioValidator validator = new PostUsuarioValidator();
            validator.ValidateAndThrow(usuario);

            Escolaridade? escolaridade = _escolaridadeRepository.GetByDescricao(usuario.Escolaridade);

            if (escolaridade == null)
                throw new Exception("Escolaridade não encontrada.");

            Usuario _usuario = new Usuario
            {
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                DataNascimento = DateTime.Parse(usuario.DataNascimento),
                EscolaridadeId = escolaridade.Id
            };

            _usuarioRepository.Criar(_usuario);
            _usuarioRepository.SaveChanges();

            return _usuario.Id;
        }

        public bool Atualizar(UsuarioViewModel usuario)
        {
            PutUsuarioValidator validator = new PutUsuarioValidator();
            validator.ValidateAndThrow(usuario);

            Escolaridade? escolaridade = _escolaridadeRepository.GetByDescricao(usuario.Escolaridade);

            if (escolaridade == null)
                throw new Exception("Escolaridade não encontrada.");

            Usuario? _usuario = _usuarioRepository.GetById(usuario.Id);

            if (_usuario == null)
                throw new Exception("Usuário não encontrado.");


            _usuario.Nome = usuario.Nome;
            _usuario.Sobrenome = usuario.Sobrenome;
            _usuario.Email = usuario.Email;
            _usuario.DataNascimento = DateTime.Parse(usuario.DataNascimento);
            _usuario.EscolaridadeId = escolaridade.Id;


            _usuarioRepository.Atualizar(_usuario);
            _usuarioRepository.SaveChanges();

            return true;
        }

        public bool EnviarHistoricoEscolar(int usuarioId, IFormFile arquivo)
        {
            Usuario? usuario = _usuarioRepository.GetById(usuarioId);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            var folderName = Path.Combine("Resources", "HistoricoEscolar");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (arquivo.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(arquivo.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);

                if (usuario.HistoricoEscolarId != 0)
                {
                    HistoricoEscolar? historicoEscolar = _historicoEscolarRepository.GetById(usuario.HistoricoEscolarId);

                    if (historicoEscolar == null)
                        throw new Exception("Não foi encontrado Histórico Escolar para esse usuário");

                    string historicoAntigo = Path.Combine(pathToSave, historicoEscolar.Nome + '.' + historicoEscolar.Formato);

                    if (File.Exists(historicoAntigo))
                    {
                        File.Delete(historicoAntigo);
                        _historicoEscolarRepository.Deletar(historicoEscolar);
                    }
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    arquivo.CopyTo(stream);
                }

                List<string> historicoNovo = fileName.Split(".").ToList();

                HistoricoEscolar _historicoEscolar = new HistoricoEscolar
                {
                    Nome = historicoNovo[0],
                    Formato = historicoNovo[1]
                };

                _historicoEscolarRepository.Criar(_historicoEscolar);
                _historicoEscolarRepository.SaveChanges();

                usuario.HistoricoEscolarId = _historicoEscolar.Id;
                _usuarioRepository.Atualizar(usuario);
                _usuarioRepository.SaveChanges();
            }

            return true;
        }

        public HistoricoEscolarModel BaixarHistoricoEscolar(int usuarioId)
        {
            Usuario? usuario = _usuarioRepository.GetById(usuarioId);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            HistoricoEscolar? historicoEscolar = _historicoEscolarRepository.GetById(usuario.HistoricoEscolarId);

            if (historicoEscolar == null)
                throw new Exception("Não foi encontrado Histórico Escolar para esse usuário");

            var folderName = Path.Combine("Resources", "HistoricoEscolar");
            var pathToFile = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(pathToFile, historicoEscolar.Nome + '.' + historicoEscolar.Formato);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Arquivo não encontrado");

            var memory = new MemoryStream();
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return new HistoricoEscolarModel { Data = memory, ContentType = GetContentType(fullPath) };
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
