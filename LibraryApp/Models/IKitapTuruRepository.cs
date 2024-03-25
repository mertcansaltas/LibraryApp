namespace LibraryApp.Models
{
    public interface IKitapTuruRepository: IRepository<KitapTuru>
    {
        void Guncelle(KitapTuru kitapturu);
        void Kaydet();
    }
}
