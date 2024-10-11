using Microsoft.EntityFrameworkCore;
using Web_API_Suuh.Model;
using Web_API_Suuh.ORM;
using Web_API_Suuh.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Adicione o contexto do banco de dados
builder.Services.AddDbContext<BancoBibliotecaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicione o repositório FuncionarioR
builder.Services.AddScoped<FuncionarioRepositorio>();
builder.Services.AddScoped<CategoriaRepositorio>();
builder.Services.AddScoped<EmprestimoRepositorio>();
builder.Services.AddScoped<LivroRepositorio>();
builder.Services.AddScoped<MembroRepositorio>();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
