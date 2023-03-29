using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersistence
    {
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool incluirPalestrantes);
        Task<Evento[]> GetAllEventosAsync(bool incluirPalestrantes);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool incluirPalestrantes);
    }
}