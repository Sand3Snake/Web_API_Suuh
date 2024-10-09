using Web_API_Suuh.ORM;

namespace Web_API_Suuh.Model
{
    public class Categoria
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Descricao { get; set; } = null!;
       
    }
}
