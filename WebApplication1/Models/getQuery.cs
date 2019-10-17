using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using WebApplication1.Models;
using System.Text;

namespace WebApplication1.Models
{
    public class getQuery
    {
        string servidor_p = "Data Source=";
        string banco_p = "Initial Catalog=";
        string user_p = "user=";
        string Pass_p = "password=";
        public static string Con { get; set; }
        public getQuery()
        {

        }
        public getQuery(string servidor, string banco, string usuario, string senha)
        {
             Con = servidor_p + servidor + ";" +
             banco_p + banco+ ";" +
             user_p + usuario +";" +
             Pass_p + senha + ";";
        }
        public List<Tabela> getTabelas(StringBuilder query)
        {
            List<Tabela> tab = new List<Tabela>();
            List<string> colunas_l = new List<string>();
            List<string> indexes_l = new List<string>();
            String nome, dt_ult, dt_ins, id;

            SqlConnection conn = new SqlConnection(Con.ToString());
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query.ToString(), conn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string tabela_l="";

                    nome = reader["Nome"].ToString();
                    id = reader["ID"].ToString();
                    dt_ult = reader["DT_ULT"].ToString();
                    dt_ins = reader["DT_INS"].ToString();

                    tabela_l = nome +" Data de criacao: "+dt_ins + "  Data de alteracao: "+dt_ult;
                    if (!String.IsNullOrEmpty(id))
                    {
                        indexes_l = popularIndexes(id);
                        colunas_l = popularColunas(id);
                    }
                    tab.Add(new Tabela(tabela_l, indexes_l,colunas_l));
                }
            }
            catch (Exception ex)
            {
                conn.Close();
            }

            return tab;
        }
        public List<string> popularColunas(string IdTabela)
        {

            SqlConnection conn = new SqlConnection(Con.ToString());
            StringBuilder colunas = new StringBuilder();
            List<string> colunas_l = new List<string>();


            String nomeCol, valorCol;

            colunas.Append(" SELECT C.name AS[NOME], CONCAT(T.name, ' (', C.max_length, ')') AS[VALOR] ");
            colunas.Append(" FROM sys.objects O ");
            colunas.Append(" INNER JOIN sys.columns C ON O.object_id = C.object_id ");
            colunas.Append(" INNER JOIN sys.systypes T ON T.xtype = C.system_type_id ");
            colunas.Append(" WHERE O.object_id =");
            colunas.Append(IdTabela);

            try
            {
                conn.Open();
                SqlCommand cmdColunas = new SqlCommand(colunas.ToString(), conn);
                SqlDataReader readerColunas = null;

                readerColunas = cmdColunas.ExecuteReader();

                while (readerColunas.Read())
                {
                    nomeCol = readerColunas["NOME"].ToString();
                    valorCol = readerColunas["VALOR"].ToString();
                    colunas_l.Add(nomeCol+"  "+valorCol);

                }
                conn.Close();
            }
            catch
            {
                conn.Close();
            }

            return colunas_l;
        }
        public List<string> popularIndexes(string IdTabela)
        {
            List<string> indexes_l = new List<string>();
            SqlConnection conn = new SqlConnection(Con.ToString());
            StringBuilder indexes = new StringBuilder();

            indexes.Append(" select name as [NOME],type_desc[Tipo] ");
            indexes.Append(" from sys.indexes ");
            indexes.Append(" WHERE object_id =");
            indexes.Append(IdTabela);
            try
            {
                conn.Open();
                SqlCommand cmdColunas = new SqlCommand(indexes.ToString(), conn);
                SqlDataReader readerIndexes = null;
                readerIndexes = cmdColunas.ExecuteReader();

                while (readerIndexes.Read())
                {
                    indexes_l.Add("Nome: "+readerIndexes["NOME"].ToString() + " Tipo: "+ readerIndexes["Tipo"].ToString());

                }
                conn.Close();
            }
            catch(Exception ex)
            {
                indexes_l.Clear();
                indexes_l.Add(ex.Message);
                conn.Close();
            }

            return indexes_l;
        }
        public List<ObjtAndScript> ObjtAndScript(StringBuilder query)
        {
            List<ObjtAndScript> proc = new List<ObjtAndScript>();
            SqlConnection conn = new SqlConnection(Con.ToString());
            SqlDataReader reader_proc = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query.ToString(), conn);
                reader_proc = cmd.ExecuteReader();
           
                while (reader_proc.Read())
                {
                    string proc_nome = reader_proc["Nome"].ToString();
                    string proc_dt = reader_proc["DT_ULT"].ToString();
                    string proc_script = "";

                    if (!String.IsNullOrEmpty(proc_nome))
                    {
                            proc_script = script_Objetos(proc_nome);
                    }
                    proc.Add(new ObjtAndScript(proc_nome + " Data de alteracao: " + proc_dt, proc_script));
                }
            }
            catch (Exception ex)
            {
                proc.Clear();
                proc.Add(new ObjtAndScript(ex.Message, ""));
            }

            return proc;
        }
        public string script_Objetos(string nome_obj)
        {
            StringBuilder scriptReturn = new StringBuilder();
            SqlConnection conn = new SqlConnection(Con.ToString());
            SqlDataReader reader_procScript = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(" declare @table_proce_script table(query varchar(max)) ");
            sql.Append(" insert into @table_proce_script ");
            sql.Append("exec[sys].[sp_helptext] '");
            sql.Append(nome_obj + "'");
            sql.Append(" select query from @table_proce_script");

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                reader_procScript = cmd.ExecuteReader();

                while (reader_procScript.Read())
                {
                    scriptReturn.Append(reader_procScript["query"].ToString());
                }
            }
            catch(Exception ex)
            {
                scriptReturn.Clear();
                scriptReturn.Append(ex.Message);
            }

         return scriptReturn.ToString();
        }
    }
}