using LibraryApp.Context;
using System.Linq.Expressions;

namespace LibraryApp.Models
{
    public class KitapTuruRepository : Repository<KitapTuru>, IKitapTuruRepository
    {
        private AppDbContext _context;
        public KitapTuruRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Guncelle(KitapTuru kitapTuru)
        {
            _context.Update(kitapTuru);
            
        }

        public void Kaydet()
        {
           _context.SaveChanges();
        }

    }
}
