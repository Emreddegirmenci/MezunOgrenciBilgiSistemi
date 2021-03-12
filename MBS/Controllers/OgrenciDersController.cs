using MBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MBS.Controllers
{
    public class OgrenciDersController : ApiController
    {
        OBSEntities _ent = new OBSEntities();
        NotController nc = new NotController();
        public void OgrenciDersSilDersIDyeGore(int DersID)
        {
            List<OgrenciDers> silinecekler = _ent.OgrenciDers.Where(p => p.DersID == DersID).ToList();
            foreach (OgrenciDers item in silinecekler)
            {
                nc.NotSilOgrenciDersIDyeGore(item.OgrenciDersID);
                _ent.OgrenciDers.Remove(item);
                _ent.SaveChanges();
            }
        }
        public void OgrenciDersSilOgrenciIDyeGore(int OgrenciID)
        {
            List<OgrenciDers> silinecekler = _ent.OgrenciDers.Where(p => p.OgrenciID == OgrenciID).ToList();
            foreach (OgrenciDers item in silinecekler)
            {
                nc.NotSilOgrenciDersIDyeGore(item.OgrenciDersID);
                _ent.OgrenciDers.Remove(item);
                _ent.SaveChanges();
            }
        }
        [HttpGet]
        public bool OgrenciDersSilOgrenciDersIDyeGore(int OgrenciDersID)
        {
            try
            {
                _ent.OgrenciDers.Remove(_ent.OgrenciDers.Find(OgrenciDersID));
                _ent.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        public List<OgrenciTip> DersiAlanOgrencileriGetir(int DersID)
        {
            List<OgrenciTip> dersinlistesi = _ent.OgrenciDers.Where(p => p.DersID == DersID).Select(p => new OgrenciTip()
            {
                AdSoyad = p.Ogrenci.AdSoyad,
                MezuniyetTarihi = p.Ogrenci.MezuniyetTarihi,
                Mail = p.Ogrenci.Mail,
                No = p.Ogrenci.No,
                OgrenciID = p.OgrenciID,
                TC = p.Ogrenci.TC,
                Telefon = p.Ogrenci.Telefon,
                DersID = p.DersID,
                OgrenciDersID = p.OgrenciDersID
            }).ToList();

            return dersinlistesi;
        }
    }
    public class OgrenciDersTip
    {
        public int OgrenciDersID { get; set; }
        public int OgrenciID { get; set; }
        public int DersID { get; set; }
        public System.DateTime DersAlmaZamani { get; set; }


    }
}