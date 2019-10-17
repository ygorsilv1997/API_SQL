using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Text;

namespace WebApplication1.Controllers
{
    public class HomeController : ApiController
    {
        static getQuery _getquery = new getQuery();

        [HttpGet]
        public List<Home> objsAlt()
        {
            List<Home> home = new List<Home>();
    
            StringBuilder sql_tables = new StringBuilder();
            sql_tables.Append(" SELECT top 1 O.name AS[Nome], O.object_id AS [ID], O.modify_date as [DT_ULT], O.create_date AS[DT_INS] ");
            sql_tables.Append(" FROM sys.objects O ");
            sql_tables.Append(" WHERE O.type_desc = 'USER_TABLE' AND CONVERT(VARCHAR(8),modify_date,112) BETWEEN GETDATE()-30 AND GETDATE() ");

            StringBuilder sql_proc = new StringBuilder();
            sql_proc.Append(" SELECT top 1 O.name AS[Nome], O.modify_date AS [DT_ULT] ");
            sql_proc.Append(" FROM sys.objects O ");
            sql_proc.Append("WHERE O.type_desc = 'SQL_STORED_PROCEDURE' AND modify_date BETWEEN GETDATE()-30 AND GETDATE() ");


            StringBuilder sql_funct= new StringBuilder();
            sql_funct.Append(" SELECT top 1 O.name AS[Nome], O.modify_date AS [DT_ULT] ");
            sql_funct.Append(" FROM sys.objects O ");
            sql_funct.Append(" WHERE O.type_desc = 'SQL_SCALAR_FUNCTION' AND modify_date BETWEEN GETDATE()-30 AND GETDATE()");
           
            home.Add(new Home(_getquery.getTabelas(sql_tables),_getquery.ObjtAndScript(sql_proc),_getquery.ObjtAndScript(sql_funct)));
            //return "Bem vindo ao OpenSourcer";
            return home;
        }


        [HttpPost]
        public string registerCon(string servidor,string banco,string usuario, string senha) {
            try {
                _getquery = new getQuery(servidor, banco, usuario, senha);
                return "ConnectionString cadastrada";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
