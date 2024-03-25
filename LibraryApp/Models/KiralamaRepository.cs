using LibraryApp.Context;
using System.Linq.Expressions;

namespace LibraryApp.Models
{
    public class KiralamaRepository : Repository<Kiralama>, IKiralamaRepository
    {
        private AppDbContext _context;
        public KiralamaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Guncelle(Kiralama kiralama)
        {
            _context.Update(kiralama);
            
        }

        public void Kaydet()
        {
           _context.SaveChanges();
        }

    }
}
