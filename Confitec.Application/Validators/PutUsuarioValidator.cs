using FluentValidation;

namespace Confitec.Application.Validators
{
    internal class PutUsuarioValidator : PostUsuarioValidator
    {
        public PutUsuarioValidator() : base()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("É preciso informar um id válido");
        }
    }
}
