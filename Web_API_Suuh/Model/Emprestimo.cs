namespace Web_API_Suuh.Model
{
    public class Emprestimo
    {
        public int Id { get; set; }

        public DateTime DataEmprestimo { get; set; }

        public DateTime DataEvolucao { get; set; }

        public int FkMembro { get; set; }

        public int FkLivro { get; set; }
    }
}

