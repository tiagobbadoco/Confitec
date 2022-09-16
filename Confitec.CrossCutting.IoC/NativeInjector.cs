using Confitec.Application.Interfaces;
using Confitec.Application.Services;
using Confitec.Infrastructure.Data.Interfaces;
using Confitec.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Confitec.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region "Services"
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEscolaridadeService, EscolaridadeService>();
            #endregion

            #region "Repositories"
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEscolaridadeRepository, EscolaridadeRepository>();
            services.AddScoped<IHistoricoEscolarRepository, HistoricoEscolarRepository>();
            #endregion
        }
    }
}