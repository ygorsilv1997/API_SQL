using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Tabela
    {

        public string Nome { get; set; }
        public List<string> Colunas { get; set; }
        public List<string> Indexes { get; set; }
        public Tabela(string nome, List<string> indexes ,List<string> colunas)
        {
            this.Nome = nome;
            this.Colunas = colunas;
            this.Indexes = indexes;
        }
    }
}