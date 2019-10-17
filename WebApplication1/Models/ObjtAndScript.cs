using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ObjtAndScript
    {
        public string objtAndScript_nome { get; set; }
        public string objtAndScript_script { get; set; }
        public ObjtAndScript()
        {

        }
        public ObjtAndScript(string nome, string script)
        {
            this.objtAndScript_nome = nome;
            this.objtAndScript_script = script;
        }
    }
}