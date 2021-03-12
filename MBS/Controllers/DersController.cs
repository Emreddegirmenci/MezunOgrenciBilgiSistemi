using MBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MBS.Controllers
{
    public class DersController : ApiController
    {
        OBSEntities _ent = new OBSEntities();
        OgrenciDersController odc = new OgrenciDersController();
        [HttpGet]
        public List<DersTip> TumDersleriGetir()
        {
            return _ent.Ders.Select(p => new DersTip()
            {
                DersAdi = p.DersAdi,
                DersAkts = p.DersAkts,
                DersID = p.DersID,
                DersKredi = p.DersKredi
            }).ToList();
        }

        [HttpGet]
        public List<DersTip> DersSil(int DersID)
        {
            odc.OgrenciDersSilDersIDyeGore(DersID);
            _ent.Ders.Remove(_ent.Ders.Find(DersID));
            _ent.SaveChanges();
            return TumDersleriGetir();
        }

    }
    public class DersTip
    {
        public int DersID { get; set; }
        public string DersAdi { get; set; }
        public double DersKredi { get; set; }
        public double DersAkts { get; set; }
    }
}