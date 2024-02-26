// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

Console.WriteLine("Hello, World!");

ETicaretDbContext context = new ETicaretDbContext();

#region En temelde sorgu oluşturmak

#region 1.yöntem : Method syntax
////ToList metodu ile liste halinde veri çekmek
//List<Urun> urunler1 = await context.Set<Urun>().ToListAsync();
//#endregion
//#region 2.yöntem : Query syntax
////veritabanındaki ürünlerimizi şimdi sql sorgusu yazarak çekelim
//List<Urun> urunler2 = await (from urun in context.Set<Urun>()
//                     select urun).ToListAsync();

//parantez içindeki kısım bizim Query dediğimiz sorgumuzu temsil ediyor.
//En sonra eklediğimiz ToListAsync ile sorgumuzu sql de execute etmiş ve verileri çekmiş oluyoruz.
#endregion
#endregion

#region Sorguyu execute etmek için neler yapmalıyız ?
#region   ___1. ToListAsync() kullanımı___
#region __a.Metot Syntax__
//var urunler = context.Set<Urun>().ToListAsync();
#endregion
#region __b.Query Syntax__
//var urunler2 = (from urun in context.Set<Urun>()
//               select urun).ToListAsync();
#endregion
#endregion

#region ___2.Forech Kullanımı___
//int urunId = 5;
//var urunler = from urun in context.Set<Urun>()
//              where urun.Id >= urunId && urun.UrunAdi.Contains("K")
//              select urun;

//urunId = 1; //foreach henüz tetiklenmediği için sorguya urunId değeri 1 olarak gider. buna DEFERED EXECUTION denir.
//////sorgu döngüye girdiğinde execute edilir ve veriler ide 'ye getirilir.
//foreach (Urun urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);
//}
#region a.DEFERED EXECUTION : ERTELENMİŞ ÇALIŞMA
// IQueryable çalışmalarında ilgili kod yazıldığı noktada çalıştırılmaz/generate etmez
//execute edildiği fonksiyonda tetiklenir.
#endregion
#endregion

#endregion

#region IQueryable ve IEnumerable nedir? 
////sorgunun atanmasından ötürü urunler değişkeni IQueryable'dır.
//var urunler = from urun in context.Set<Urun>()
//              select urun;
////Burada urunler2 değişkenimiz bir List<> yani IEnumerable oldu çünkü ToListAsyn Query'i execute eder.
//var urunler2 = (from urun in context.Set<Urun>()
//                select urun).ToListAsync();
//#region IQueryable
////IQueryable sorguya karşılık gelen cümledir. Execute edilmemiş halidir.
//#endregion
//#region IEnumerable
////Sorgunun çalıştırılıp yani execute edilip in memory'e yüklenmiş halini ifade eder.

//#endregion

#endregion

#region Çoğul veri getiren şartlı ve sıralı sorgulama fonksiyonları

#region a.ToListAsync
//üretilen sorguyu IQueryable'ı  IEnumerable yapar yani execute eder.
#region a.1.Method
//var urunler = context.Set<Urun>().ToListAsync();
#endregion
#region a.2.Query
//var urunler2 = (from urun in context.Set<Urun>()
//               select urun).ToListAsync();
#endregion

#endregion
#region b.where fonksiyonu
// sorguya şart yazmak için kullanıyoruz sorgunun devamında yazılmalı
#region b.1.Method
//var urunler = await context.Set<Urun>().Where(u=>u.Id>3).ToListAsync();
//foreach (var u in urunler) 
//{
//    Console.WriteLine(u.Id);

//}
#endregion
#region b.2.Query
//var urunler2 = await (from urun in context.Set<Urun>()
//                where urun.Id>3 || urun.UrunAdi.EndsWith("K")
//                select urun).ToListAsync();
#endregion
#endregion
#region c.OrderBy ile sıralama(Default  : Ascending )
#region c.1.Method
//ürün adına göre alfebetik olarak sıralama
//var urunler = await context.Set<Urun>().Where(u => u.Id > 3).OrderBy(u=>u.UrunAdi).ToListAsync();
//foreach (var u in urunler)
//{
//    Console.WriteLine(u.UrunAdi);

//}
#endregion
#region c.2.Query
//var urunler2 = await (from urun in context.Set<Urun>()
//                      where urun.Id > 3 || urun.UrunAdi.EndsWith("K")
//                      orderby urun.UrunAdi descending //alfabetik olarak tersten sıralar
//                      select urun).ToListAsync();
//foreach (var u in urunler2)
//{
//    Console.WriteLine(u.UrunAdi);

//}
#endregion
#endregion
#region d.ThenBy
#region d.1.Method
//OrderBy üzerinde sıralama yaptığımızda 1 nolu üründen 15 çeşit varsa, thenby ile o 15 çeşit alt kategoriyi de sıralayabiliriz
//var urunler = await context.Set<Urun>().Where(u => u.Id > 3).OrderBy(u => u.UrunAdi).ThenBy(u=>u.Fiyat).ThenBy(u=>u.Id).ToListAsync();
//foreach (var u in urunler)
//{
//    Console.WriteLine(u.UrunAdi);

//}
#endregion
#region d.2.Query
//var urunler2 = await (from urun in context.Set<Urun>()
//                      where urun.Id > 3 || urun.UrunAdi.EndsWith("K")
//                      orderby urun.UrunAdi descending //alfabetik olarak tersten sıralar
//                      select urun).ToListAsync();
//foreach (var u in urunler2)
//{
//    Console.WriteLine(u.UrunAdi);

//}
#endregion
#endregion
#region e.OrderByDescending
//orderby'ın tam tersidir. Sıralama işleminde artandan azalana doğru sıralar.

#endregion
#region f.ThenByDescending
//sıralama sonrası benzer nesnelerin sıralanmasını büyükten küçüğe doğru yapar.
//ör : Yoğurt  52
//     Yoğurt  25
//     Yoğurt  7
//     Zeytin  54
//     Zeytin  34
#endregion
#endregion

#region Tekil veri getiren şartlı ve sıralı sorgulama fonksiyonları
//yapılan sorguda sade ve sadece tek bir veri gelmesi isteniyorsa Single yada SingleOrDefault kullanılır

#region a.SingleAsync
//birden fazla verinin geldiği ya da hiç verinin gelmediği durumlarda exception furlatır.sorguda top(2) komutu kullanır
#region a.1.Tek Kayıt Geldiğinde
//var urun = await context.Set<Urun>().SingleAsync(u=>u.Id==3);
//Console.WriteLine( urun.UrunAdi);
#endregion
#region a.2.Hiç Kayıt Gelmediğinde
//33 id'si ile kayıt yok hata verecek
//var urun = await context.Set<Urun>().SingleAsync(u => u.Id == 33);
//Console.WriteLine(urun.UrunAdi);
#endregion
#region a.3.Çok Kayıt Geldiğinde
//id'si 3'ten büyük çok veri var hata verecek
//var urun = await context.Set<Urun>().SingleAsync(u => u.Id > 3);
//Console.WriteLine(urun.UrunAdi);
#endregion

#endregion

#region b.SingleOrDefaultAsync
//birden fazla veri geliyorsa hata fırlatır, veri gelmiyorsa default değer olarak NULL döner.
#region b.1.Tek Kayıt Geldiğinde
//bu değeri getirir sıkıntı çıkmaz
//var urun = await context.Set<Urun>().SingleAsync(u=>u.Id==3);
//Console.WriteLine( urun.UrunAdi);
#endregion
#region b.2.Hiç Kayıt Gelmediğinde
//33 id'si ile kayıt yok NULL DÖNER
//var urun = await context.Set<Urun>().SingleAsync(u => u.Id == 33);
//Console.WriteLine(urun.UrunAdi);
#endregion
#region b.3.Çok Kayıt Geldiğinde
//id'si 3'ten büyük çok veri var hata verecek
//var urun = await context.Set<Urun>().SingleAsync(u => u.Id > 3);
//Console.WriteLine(urun.UrunAdi);
#endregion
#endregion

#region c.FirstAsync
//sorgu netincesinde çoklu veri gelebilir bu veriler arasından ilkini alır.
#region c.1.Tek Kayıt Geldiğinde
//var urun = await context.Set<Urun>().FirstAsync(u => u.Id == 3);
//Console.WriteLine(urun.UrunAdi);
#endregion
#region c.2.Hiç Kayıt Gelmediğinde
//33 id'si ile kayıt yoksa hata verir.
//var urun = await context.Set<Urun>().FirstAsync(u => u.Id == 33);
//Console.WriteLine(urun.UrunAdi);
#endregion
#region c.3.Çok Kayıt Geldiğinde
//id'si 3'ten büyük çok veri varsa bunlardan ilkini getirir.
//var urun = await context.Set<Urun>().FirstAsync(u => u.Id > 3);
//Console.WriteLine(urun.UrunAdi);
#endregion
#endregion

#region d.FirstOrDefaultAsync
//sorgu netincesinde çoklu veri gelebilir bu veriler arasından ilkini alır.eğerki hiç veri gelmezse null değerini alır
#region d.1.Tek Kayıt Geldiğinde
//var urun = await context.Set<Urun>().FirstOrDefaultAsync(u => u.Id == 3);
//Console.WriteLine( urun.UrunAdi);
#endregion
#region d.2.Hiç Kayıt Gelmediğinde
//33 id'si ile kayıt yok NULL DÖNER
//var urun = await context.Set<Urun>().FirstOrDefaultAsync(u => u.Id == 33);
//Console.WriteLine(urun.UrunAdi);
#endregion
#region d.3.Çok Kayıt Geldiğinde
//id'si 3'ten büyük çok veri gelecek ve ilkini alacak
//var urun = await context.Set<Urun>().FirstOrDefaultAsync(u => u.Id > 3);
//Console.WriteLine(urun.UrunAdi);
#endregion
#endregion


#region SingleAsync , SingleOrDefaultAsync , FirstAsync , FirstOrDefaultAsync KARŞILAŞTIRMA
//single fonksiyonları uniq id değerlerine sahip kolon sorgularında kullanmak önemlidir
//diğer durumlar için first yapısı kullanılır
#endregion

#region e.FindAsync
//var urun = await context.Set<Urun>().FirstOrDefaultAsync(u=>u.Id==5);
////biz bu aramayı primarykey ile yapacaksak FindAsync ile direk veriyi getirtebiliriz
//var urun2= await context.Set<Urun>().FindAsync(5);
//Console.WriteLine("FirstOrDefaultAsync ile çekilen veri : " + urun.UrunAdi);
//Console.WriteLine("Find ile çekilen veri : " + urun2.UrunAdi);

//var urun3 = await context.Set<UrunParca>().FindAsync(2,5);
//Console.WriteLine("Find ile çekilen veri : " + urun3.UrunId);
//Console.WriteLine("Find ile çekilen veri : " + urun3.ParcaId);



#endregion

#region FindAsync ile SingleAsync , SingleOrDefaultAsync , FirstAsync , FirstOrDefaultAsync KARŞILAŞTIRMA
//Find fonksiyonu önce belleğe yani contexte bakar onu getirir yoksa veritabanına gider sorguyu gerçekleştirir
//Find ile sadece primary key olurken diğerleri her kolon için where şartı ile özel aramalar yapar
//Diğerleri her zaman veritabanına sorguyu gönderir ve execute ettirir.
//Find kayıt bulunamazsa null döndürür
#endregion

#region f.LastAsync
//First fonksiyonu gelen verilerin ilkini alırken Last fonksiyonu sondaki veriyi alır
//bu fonksiyonu kullanmak için sıralamak gerektiğinden orderby gerekir
////veri gelmezse hata verir
//var sonUrun1 = await context.Set<Urun>().OrderBy(u => u.UrunAdi).LastAsync(u => u.Id > 466);

//Console.WriteLine("Gelen son veri : " + sonUrun1.UrunAdi);

#endregion

#region g.LastOrDefaultAsync
//eğer ki hiç veri gelmiyorsa null döner
//var sonUrun1 = await context.Set<Urun>().OrderBy(u => u.UrunAdi).LastAsync(u => u.Id > 466);
//if (sonUrun1 == null)
//{
//    Console.WriteLine("null değer.");

//}
//else
//{
//    Console.WriteLine("Gelen son veri : " + sonUrun1.UrunAdi);

//}
#endregion

#endregion



public class ETicaretDbContext : DbContext
{
    DbSet<Urun> Uruns { get; set; }
    DbSet<Parca> Parcas { get; set; }

    DbSet<UrunParca> UrunParcas { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=DESKTOP-P7KA77K\\SQLEXPRESS;Database=QueriesDB;User Id=sa;Password=1234; ;TrustServerCertificate=true");
        

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrunParca>().HasKey(up => new {up.UrunId,up.ParcaId });
    }

}

public partial class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }

    public ICollection<Parca> Parcalar { get; set; }
}

public partial class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }

}

public partial class UrunParca
{
    public int UrunId { get; set; }
    public int ParcaId { get; set; }

    public Urun Urun { get; set; }
    public Parca Parca { get; set; }

}

