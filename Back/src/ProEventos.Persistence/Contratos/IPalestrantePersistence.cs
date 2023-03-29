using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IPalestrantePersistence
    {
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool incluirEventos);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool incluirEventos);
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool incluirEventos);
    }
}