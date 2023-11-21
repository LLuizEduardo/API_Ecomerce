using API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Infraestructure.Data
{
    public class DataContent : DbContext
    {
        public DataContent(DbContextOptions<DataContent> options) : base(options)
        {
        }
        public DbSet<Produto> WeatherForecast { get; set; }
    }
}
