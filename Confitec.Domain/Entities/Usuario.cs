namespace Confitec.Domain.Entities
{
    public class Usuario : EntidadeBase
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public int EscolaridadeId { get; set; }
        public virtual Escolaridade Escolaridade { get; set; }
        public int HistoricoEscolarId { get; set; }
    }
}
