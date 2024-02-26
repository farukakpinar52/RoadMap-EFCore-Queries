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

#region Diğer sorgulama fonksiyonları
//BU FONKSIYONLARDA ŞARTLARI WHERE İLE OLUŞTURDUKTAN SONRA BU FONKSIYONLARI ÇAĞIRMAK YERİNE, ŞART CÜMLELERİNİ DİREK BU FONKSİYONLARIN İÇİNDE VEREBLİLİRİZ.


#region CountAsync
//varolan bir sorgunun execute neticesinde kaç adet olacağını geri döndürür
//şimdi yanlış bir yöntem ile başlayalım bu maliyetli bir hesaplama oldu.
//int adet = (await context.Set<Urun>().ToListAsync()).Count();

//IQueryable kısmında sayarak sorgu execute edilmeden adedi bulabiliriz.
//int adet2 = await context.Set<Urun>().CountAsync();

//int adet2 = await context.Set<Urun>().CountAsync(u=>u.UrunAdi.Contains("A"));
//Console.WriteLine( "bu countasync fonksiyonu sonucu "+adet2.ToString());

#endregion

#region LongCountAsync
//CountAsync int olduğu için 2.4milyar veriyi okur o yüzden 10 milyar veri falan varsa bu fonksiyon çalıştırılır
//long adet2 = await context.Set<Urun>().LongCountAsync();

#region -------Şartlı sorgu sayısı elde etmek
//long adet2 = await context.Set<Urun>().LongCountAsync(u => u.Fiyat<500);

#endregion

#endregion

#region AnyAsync
//Sorgu neticesinde AnyAsync ile verinin gelip gelmediğini bool olarak verir
//bool urunVarMı = await context.Set<Urun>().Where(u=>u.Fiyat==444).AnyAsync();

//bool urunVarMı = await context.Set<Urun>().Where(u => u.UrunAdi.Contains("A")).AnyAsync();
//bool urunVarMı2 = await context.Set<Urun>().AnyAsync(u => u.UrunAdi.Contains("A"));

//Console.WriteLine(urunVarMı);
//Console.WriteLine(urunVarMı2);

#endregion

#region MaxAsync
//sorguda dönen sayısal veriler arasındaki en büyük değeri getirir
//var maxFiyat = await context.Set<Urun>().MaxAsync(u=>u.Fiyat);
//Console.WriteLine(maxFiyat.ToString());
#endregion

#region MinAsync
//gelen sorgudaki en küçük değeri verir
//var maxFiyat2 = await context.Set<Urun>().MinAsync(u => u.Fiyat);
//Console.WriteLine(maxFiyat2.ToString());
#endregion

#region Distinct
//sorguda mükerrer kayıtlar varsa bunları tekilleştiren bir işleve sahip fonksiyonlardır
//distinct sorguyu execute etmez o yüzden tolistasync ile çalışırız.
//var urunler = await context.Set<Urun>().Distinct().ToListAsync();
#endregion

#region AllAsync
//bir sorgu neticesinde gelen verilerin TAMAMI şartı sağlıyorsa True döner, değilse false.
//bool a1 = await context.Set<Urun>().AllAsync(u=>u.Fiyat<5000);
//bool a2 = await context.Set<Urun>().AllAsync(u => u.UrunAdi.Contains("a"));

//Console.WriteLine(a1);
//Console.WriteLine(a2);

#endregion

#region SumAsync
//Toplamı veren fonksiyondur sayısal kolonda çalışır
//var toplamFiyat = await context.Set<Urun>().SumAsync(u=>u.Fiyat);

#endregion

#region AverageAsync
//sayısal değer içeren kolonda ortalamayı verir
//float ortalamaFiyat = await context.Set<Urun>().AverageAsync(u => u.Fiyat);

#endregion

#region ContainsAsync
//Like '%...%'  şeklindeki like sorgusu oluşturmamızı sağlar
//sql de sorgu yazarken  ... where KolonAdı Like '%abc%' şeklinde olduğu için where şartından sonra gelir
//List<Urun> urunHasAWord = await context.Set<Urun>().Where(u=>u.UrunAdi.Contains("A")).ToListAsync();

#endregion

#region StartsWith
//Like sorgusu oluşturmamızı sağlar '...%' şeklindeki sorguları getirir
//List<Urun> urunStartAWord = await context.Set<Urun>().Where(u => u.UrunAdi.StartsWith("A")).ToListAsync();
//foreach (var item in urunStartAWord)
//{
//    Console.WriteLine(  item.UrunAdi);
//}

#endregion

#region EndsWith
//Like sorgusu oluşturmamızı sağlar '%...' şeklindeki sorguları getirir
//List<Urun> urunStartAWord = await context.Set<Urun>().Where(u => u.UrunAdi.EndsWith("A")).ToListAsync();
//foreach (var item in urunStartAWord)
//{
//    Console.WriteLine(item.UrunAdi);
//}
#endregion
#endregion

#region SORGU SONUCU ELDE EDİLEN VERİLERİ FARKLI TÜRLERE(ToDictionary, ToArray, Select, SelectMany) DÖNÜŞTÜRMEK
//bu fonksiyonlar ile sorgu neticesinde elde edilen verilerin istediğimiz kolonlarını çekerek farklı türlerde projeksiyon edebiliyoruz

#region a.ToDictionaryAsync
//sorgu neticesinde gelecek olan veriyi bir dictionary olarak elde etmek/karşılamak istiyorsak kullanırız
//var mydic = await context.Set<Urun>().ToDictionaryAsync(u=>u.UrunAdi,u=>u.Fiyat);

//oluşturulan sorguyu ToListAsync gibi execute eder fakat 
//ToList : entity türünde bir koleksiyondur List<TEntity> döndürür
//ToDictionary : dictionary türünden bir koleksiyondur Dictionary<Key,Value>
#endregion

#region b.ToArrayAsync
//query'yi execute eder ama gelen verileri Array(dizi) içinde tutar.
//Urun[] myArr = await context.Set<Urun>().ToArrayAsync();

#endregion

#region c.Select
//işlevsel olarak birden fazla davranışı vardır
//1. generate edilecek fonksiyonun çekilecek olan kolonlarını ayarlamayı sağlar
//var urunler1 = await context.Set<Urun>().Select(u=> new Urun { Id=u.Id, Fiyat=u.Fiyat}).ToListAsync();

//2. generate edilecek fonksiyonun çekilecek olan kolonlarına özel bir anonim nesne üreterek çağırabilir
//var urunler2 = await context.Set<Urun>().Select(u => new { Id = u.Id, Fiyat = u.Fiyat }).ToListAsync();

//3. çekilecek olan verinin kolonlarını karşılayacak bir sınıfımız varsa o türden karşılayabiliriz
//burada sanki UrunDetay isimli id ve fiyat prop'larına sahip bir sınıfımız var gibi düşündük
//var urunler3 = await context.Set<Urun>().Select(u => new UrunDetay{ Id = u.Id, Fiyat = u.Fiyat }).ToListAsync();


#endregion

#region d.SelectMany
//ilgili tablonun içinde ilişkisel veriyi (bire çok) barındıran bir ilişki olabilir
//bu ilişkiye ait kolonlar çekmek için SelectMany kullanırız
//ilişkisel tablolar neticesinde gelen koleksiyonel verileri de tekilleştirip projeksiyon eder
var urunler = await context.Set<Urun>().Include(u=> u.Parcalar).SelectMany(u=>u.Parcalar, (u, p) => new
{
    u.Id,
    u.UrunAdi,
    p.ParcaAdi
}).ToListAsync();
#endregion








#endregion

#region GROUPBY FONKSIYONU

#endregion

#region FOREACH FONKSIYONU

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

