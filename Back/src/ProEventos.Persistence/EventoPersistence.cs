using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersistence : IEventoPersistence
    {
        private readonly ProEventosContext _context;

        public EventoPersistence(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool incluirPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
            if (incluirPalestrantes){
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool incluirPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Where(e => e.Tema.ToLower().Contains(tema.ToLower()))
            .Include(e => e.Lotes).Include(e => e.RedesSociais);
            if (incluirPalestrantes){
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            
            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool incluirPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Where(e => e.Id == eventoId)
            .Include(e => e.Lotes).Include(e => e.RedesSociais);
            if (incluirPalestrantes){
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            
            return await query.FirstOrDefaultAsync();
        }
    }
}