using Confitec.Application.ViewModel;
using FluentValidation;

namespace Confitec.Application.Validators
{
    public class PostUsuarioValidator : AbstractValidator<UsuarioViewModel>
    {
        public PostUsuarioValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Por favor inserir um e-mail válido")
                .NotNull().WithMessage("Por favor inserir um e-mail válido")
                .EmailAddress().WithMessage("Por favor inserir um e-mail válido");

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("Por favor inserir uma data de nascimento válida")
                .NotNull().WithMessage("Por favor inserir uma data de nascimento válida")
                .Must(ValidarDataDeNascimentoMenorQueHoje).WithMessage("Por favor inserir uma data de nascimento válida");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Por favor inserir um nome válido")
                .NotNull().WithMessage("Por favor inserir um nome válido")
                .MaximumLength(100).WithMessage("Por favor inserir um nome válido");

            RuleFor(x => x.Sobrenome)
                .NotEmpty().WithMessage("Por favor inserir um sobrenome válido")
                .NotNull().WithMessage("Por favor inserir um sobrenome válido")
                .MaximumLength(200).WithMessage("Por favor inserir um sobrenome válido");
        }

        protected bool ValidarDataDeNascimentoMenorQueHoje(string dataNascimento) => DateTime.Parse(dataNascimento) <= DateTime.Now;
    }
}
