namespace Web_API_Suuh.Model
{
    public class MembroDto
    {
        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Telefone { get; set; } = null!;

        public DateTime DataCadastro { get; set; }

        public string TipoMembro { get; set; } = null!;
    }
}
