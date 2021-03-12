using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MBS.Controllers
{
    public class OgrenciController : ApiController
    {
        OBSEntities _ent = new OBSEntities();
        OgrenciDersController odc = new OgrenciDersController();

        [HttpGet]
        public List<OgrenciTip> TumOgrencileriGetir()
        {
            return _ent.Ogrenci.Select(p => new OgrenciTip()
            {
                AdSoyad = p.AdSoyad,
                MezuniyetTarihi = p.MezuniyetTarihi,
                Mail = p.Mail,
                No = p.No,
                OgrenciID = p.OgrenciID,
                TC = p.TC,
                Telefon = p.Telefon

            }).ToList();
        }


        [HttpGet]
        public List<OgrenciTip> OgrenciSil(int OgrenciID)
        {
            odc.OgrenciDersSilOgrenciIDyeGore(OgrenciID);
            _ent.Ogrenci.Remove(_ent.Ogrenci.Find(OgrenciID));
            _ent.SaveChanges();
            return TumOgrencileriGetir();
        }
    }
    public class OgrenciTip
    {
        public int OgrenciID { get; set; }
        public string AdSoyad { get; set; }
        public string No { get; set; }
        public string Mail { get; set; }
        public string Telefon { get; set; }
        public string TC { get; set; }
        public Nullable<System.DateTime> MezuniyetTarihi { get; set; }

        public int DersID { get; set; }
        public int OgrenciDersID { get; set; }
    }
}