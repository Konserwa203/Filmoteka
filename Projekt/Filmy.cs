//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPF_Projekt_Filmy
{
    using System;
    using System.Collections.Generic;
    
    public partial class Filmy
    {
        public int IdFilmu { get; set; }
        public string Tytuł { get; set; }
        public Nullable<int> RokWydania { get; set; }
        public Nullable<int> CzasTrwania { get; set; }
        public Nullable<int> Ograniczenia { get; set; }
        public Nullable<double> OcenaIMDB { get; set; }
        public Nullable<bool> Obejrzany { get; set; }
        public string Fabuła { get; set; }
        public int IdUzytkownika { get; set; }
        public string LinkDoObrazka { get; set; }
    
        public virtual Uzytkownik Uzytkownik { get; set; }
    }
}