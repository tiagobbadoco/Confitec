using Confitec.Domain.Entities;
using Confitec.Infrastructure.Data.Context;
using Confitec.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confitec.Tests.Unit.Repositories
{
    public class UsuarioRepositoryTests
    {
        private DbContextOptions<ConfitecContext> dbContextOptions;

        public UsuarioRepositoryTests()
        {
            var dbName = $"ConfitecDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<ConfitecContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        [Fact]
        public void ListarTodosOsUsuarios()
        {
            var usuarioRepository = CriarRepositorio();

            var usuarios = usuarioRepository.Listar();

            Assert.Single(usuarios);
        }

        [Fact]
        public void AdicionarUmNovoUsuario()
        {
            var usuarioRepository = CriarRepositorio();
            usuarioRepository.Criar(new Usuario
            {
                Id = 2,
                Nome = "Maria",
                Sobrenome = "Pereira",
                Email = "maria@email.com.br",
                DataNascimento = DateTime.Parse("2001-01-01"),
                EscolaridadeId = 1
            });

            usuarioRepository.SaveChanges();

            var usuarios = usuarioRepository.Listar();
            Assert.Equal(2, usuarios.Count);
        }

        [Fact]
        public void DeletarUmUsuario()
        {
            var usuarioRepository = CriarRepositorio();
            var usuario = usuarioRepository.GetById(1);
            usuarioRepository.Deletar(usuario);
            usuarioRepository.SaveChanges();

            var usuarios = usuarioRepository.Listar();
            Assert.Empty(usuarios);
        }

        [Fact]
        public void AtualizarUmUsuario()
        {
            var usuarioRepository = CriarRepositorio();
            var usuario = usuarioRepository.GetById(1);

            usuario.Nome = "Teste";
            usuarioRepository.Atualizar(usuario);
            usuarioRepository.SaveChanges();

            usuario = usuarioRepository.GetById(1);

            Assert.Equal("Teste", usuario.Nome);
        }

        [Fact]
        public void BuscarUmUsuarioQueNaoExiste()
        {
            var usuarioRepository = CriarRepositorio();
            var usuario = usuarioRepository.GetById(10);

            Assert.Null(usuario);
        }


        private UsuarioRepository CriarRepositorio()
        {
            ConfitecContext context = new ConfitecContext(dbContextOptions);
            PopularDados(context);
            return new UsuarioRepository(context);
        }

        private void PopularDados(ConfitecContext context)
        {
            Usuario usuario = new Usuario
            {
                Id = 1,
                Nome = "João",
                Sobrenome = "Silva",
                Email = "joao@email.com.br",
                DataNascimento = DateTime.Parse("2001-01-01"),
                Escolaridade = new Escolaridade
                {
                    Id = 1,
                    Descricao = "Teste"
                }
            };

            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }
    }
}
