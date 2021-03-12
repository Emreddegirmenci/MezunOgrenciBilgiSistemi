using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MBS.Controllers
{
    public class NotController : ApiController
    {
        OBSEntities _ent = new OBSEntities();
        public void NotSilOgrenciDersIDyeGore(int OgrenciDersID)
        {
            List<Not> silineceknotlar = _ent.Not.Where(p => p.OgrenciDersID == OgrenciDersID).ToList();

            if (silineceknotlar != null)
            {
                _ent.Not.RemoveRange(silineceknotlar);
                _ent.SaveChanges();
            }
        }
        [HttpGet]
        public bool NotSilNotIDyeGore(int notID)
        {
            try
            {
                _ent.Not.Remove(_ent.Not.Find(notID));
                _ent.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        [HttpGet]
        public List<NotTip> OgrenciveDersinNotlariniGetir(int OgrenciID, int DersID)
        {
            return _ent.Not.Where(p => p.OgrenciDers.DersID == DersID && p.OgrenciDers.OgrenciID == OgrenciID).
                 Select(p => new NotTip()
                 {
                     Final = p.Final,
                     NotID = p.NotID,
                     Odev = p.Odev,
                     OgrenciDersID = p.OgrenciDersID,
                     Vize = p.Vize,
                     HarfNotu = p.HarfNotu

                 }).ToList();
        }

        [HttpGet]
        public List<NotTip> OgrenciDersIDyeGoreNotlariGetir(int OgrenciDersID)
        {
            return _ent.Not.Where(p => p.OgrenciDersID == OgrenciDersID).
                 Select(p => new NotTip()
                 {
                     Final = p.Final,
                     NotID = p.NotID,
                     Odev = p.Odev,
                     OgrenciDersID = p.OgrenciDersID,
                     Vize = p.Vize,
                     HarfNotu = p.HarfNotu

                 }).ToList();
        }
    }
    public class NotTip
    {
        public int NotID { get; set; }
        public int OgrenciDersID { get; set; }
        public Nullable<int> Vize { get; set; }
        public Nullable<int> Final { get; set; }
        public Nullable<int> Odev { get; set; }
        public string HarfNotu { get; set; }
    }
}