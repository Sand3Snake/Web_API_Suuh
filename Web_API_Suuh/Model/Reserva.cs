using Web_API_Suuh.ORM;

namespace Web_API_Suuh.Model
{
    public class Reserva
    {
        public int Id { get; set; }

        public DateTime DataReserva { get; set; }

        public int FkMembro { get; set; }

        public int FkLivro { get; set; }

    }
}
