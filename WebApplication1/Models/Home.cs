using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Home
    {
        public List<Tabela> Tabelas { get; set; }
        public List<ObjtAndScript> Procs { get; set; }

        public List<ObjtAndScript> Funct { get; set; }

        public Home(List<Tabela> tabelas,List<ObjtAndScript> procs, List<ObjtAndScript> funct)
        {
            this.Tabelas = tabelas;
            this.Procs = procs;
            this.Funct = funct;
        }
    }
}