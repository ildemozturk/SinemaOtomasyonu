//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proje.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bilet
    {
        public int id { get; set; }
        public string musteri_adi { get; set; }
        public string musteri_soyadi { get; set; }
        public int seans_id { get; set; }
        public string koltuk_numarasi { get; set; }
        public string bilet_tipi { get; set; }
    
        public virtual Seans Seans { get; set; }
    }
}
