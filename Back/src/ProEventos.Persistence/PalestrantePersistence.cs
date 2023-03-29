using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersistence : IPalestrantePersistence
    {
        private readonly ProEventosContext _context;

        public PalestrantePersistence(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);
            if (incluirEventos){
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            
            return await query.OrderBy(p => p.Id).ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
            .Include(p => p.RedesSociais);
            if (incluirEventos){
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            
            return await query.OrderBy(p => p.Id).ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Where(p => p.Id == palestranteId)
            .Include(p => p.RedesSociais);
            if (incluirEventos){
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            
            return await query.FirstOrDefaultAsync();
        }
    }
}