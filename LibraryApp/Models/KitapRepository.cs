using LibraryApp.Context;
using System.Linq.Expressions;

namespace LibraryApp.Models
{
    public class KitapRepository : Repository<Kitap>, IKitapRepository
    {
        private AppDbContext _context;
        public KitapRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Guncelle(Kitap kitap)
        {
            _context.Update(kitap);
            
        }

        public void Kaydet()
        {
           _context.SaveChanges();
        }

    }
}
