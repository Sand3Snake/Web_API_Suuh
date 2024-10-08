using System;
using System.Collections.Generic;

namespace Web_API_Suuh.ORM;

public partial class TbCategoria
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public virtual TbLivro? TbLivro { get; set; }
}
