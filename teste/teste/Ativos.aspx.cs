using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;
using MySql.Data.MySqlClient;

namespace teste
{
    public partial class Ativos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label lbl = this.Master.FindControl("lblMasterPage") as Label;
         //   string user = lbl.Text;
         //   if (user == "")
         //   {
         //       Response.Redirect("Loginuser.aspx", false);
         //   }
           
            if (!IsPostBack)
            {
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
               // Validate();          
       
                prencherGrid();

                DropDownList1.SelectedIndex = -1;

             
            }

            MySqlConnection conn4 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
            //definição do comando sql
           
           
            string sql4 = "DELETE FROM CLL";

            try
            {

                MySqlCommand comando4 = new MySqlCommand(sql4, conn4);
                //Adicionando o valor das textBox nos parametros do comando



                //abre a conexao
                conn4.Open();
                //executa o comando com os parametros que foram adicionados acima
                comando4.ExecuteNonQuery();
                //fecha a conexao
                conn4.Close();
            }
            catch
            {

            }

            string compra = (Server.MapPath("~/compra.txt"));
            string venda = (Server.MapPath("~/venda.txt"));
            string mensagem = "";
            try
            {
                if (new FileInfo(compra).Length > 0)
                {
                    using (StreamReader texto = new StreamReader(compra))
                    {
                        while ((mensagem = texto.ReadLine()) != null)
                        {
                            if (lstcompra.Items.Contains(lstcompra.Items.FindByText(mensagem)))
                            //string found
                            {                               //string not found

                               
                            }
                            else
                            {
                                lstcompra.Items.Add(mensagem);
                            }
                           
                           
                        }
                    }
                }
            }
            catch
            {
            }
            try
            {
                if (new FileInfo(venda).Length > 0)
                {
                    using (StreamReader texto = new StreamReader(venda))
                    {
                        while ((mensagem = texto.ReadLine()) != null)
                        {
                            if (lstvenda.Items.Contains(lstvenda.Items.FindByText(mensagem)))
                            //string found
                            {                               //string not found


                            }
                            else
                            {
                                lstvenda.Items.Add(mensagem);
                            }
                           
                        }
                    }
                }
            }
            catch
            {

            }

            //atribui o datatable ao datagridview para exibir o resultado
           
        }
 

        public static List<string> GetCompletionList(string prefixText, int count)
        {
             string strSql2 = "SELECT * From Ativos  ";
                //cria a conexão com o banco de dados
             MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
                //cria o objeto command para executar a instruçao sql
                MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
                //abre a conexao
                con.Open();
                
                
                using (MySqlCommand com = new MySqlCommand())
                {
                    com.CommandText = "SELECT * From Ativos  ";

              //      com.Parameters.AddWithValue("@Search", prefixText);
                    com.Connection = con;
                    con.Open();
                    List<string> countryNames = new List<string>();
                    using (MySqlDataReader sdr = com.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["fname"].ToString());
                        }
                    }
                    con.Close();
                    return countryNames;
                }
           
        } 
        string foi  ;
        public  void prencherGrid()
        {
            //define a string de conexao com provedor caminho e nome do banco de dados
            gridcheck.DataSource = null; 
            gridcheck.DataBind(); ;
            //define a instrução SQL
           
            gridcheck.AutoGenerateColumns = false;
               int contagem = gridcheck.Columns.Count;

               if (contagem <= 0)
               {
                   //define e realiza a formatação de cada coluna
                   BoundField coluna1 = new BoundField();
                   coluna1.DataField = "Id_ativo";
                   coluna1.HeaderText = "Código";
                   coluna1.Visible = false;
                   gridcheck.Columns.Add(coluna1);
                   BoundField coluna2 = new BoundField();
                   coluna2.DataField = "fname";
                   coluna2.HeaderText = "ATIVO";
                   coluna2.ReadOnly = true;
                   coluna2.HtmlEncode = false;

                   gridcheck.Columns.Add(coluna2);
                   BoundField coluna3 = new BoundField();
                   coluna3.DataField = "Id_tipoativo";
                   coluna3.HeaderText = "Tipo";
                   coluna3.HtmlEncode = false;
                   coluna3.Visible = false;
                   
                   CheckBoxField coluna4 = new CheckBoxField();
                   coluna4.DataField = "Compra";
                   coluna4.HeaderText = "Compra";
                   coluna4.InsertVisible = true;
                   coluna4.Visible = false;
                   gridcheck.Columns.Add(coluna4);

                   CheckBoxField coluna5 = new CheckBoxField();
                   coluna5.DataField = "Venda";
                   coluna5.HeaderText = "Venda";
                   coluna5.Visible = false;

                   gridcheck.Columns.Add(coluna5);

                   BoundField coluna6 = new BoundField();
                   coluna6.DataField = "descricao";
                   coluna6.HeaderText = "Descrição";
                   coluna6.HtmlEncode = false;
                   
                   gridcheck.Columns.Add(coluna6);
               }
            string strSql2 = "SELECT * From Ativos";
            //cria a conexão com o banco de dados
            MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
            //abre a conexao
            con.Open();


            //define o tipo do comando
           

            
            //cria um dataadapter
            MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
            DataSet dataset = new DataSet();
            da.Fill(dataset);
            //cria um objeto datatable
            DataTable clientes = new DataTable();

            //preenche o datatable via dataadapter
            da.Fill(clientes);
            da.Fill(dataset, "Ativos");

            this.gridcheck.DataSource = dataset ;

            gridcheck.DataBind();

            gridcheck.Enabled = true;
            //atribui o datatable ao datagridview para exibir o resultado
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            gridcheck.DataSource = null;
            gridcheck.DataBind();


            int contagem = gridcheck.Columns.Count;

            if (contagem <= 0)
            {

                BoundField coluna1 = new BoundField();
                coluna1.DataField = "Id_ativo";
                coluna1.HeaderText = "Código";
                coluna1.Visible = false;
                gridcheck.Columns.Add(coluna1);
                BoundField coluna2 = new BoundField();
                coluna2.DataField = "fname";
                coluna2.HeaderText = "Nome-Resul";
                coluna2.ReadOnly = true;
                coluna2.HtmlEncode = false;
                gridcheck.Columns.Add(coluna2);
                BoundField coluna3 = new BoundField();
                coluna3.DataField = "Id_tipoativo";
                coluna3.HeaderText = "Tipo";
                coluna3.HtmlEncode = false;
                coluna3.Visible = false;
                gridcheck.Columns.Add(coluna3);
                CheckBoxField coluna4 = new CheckBoxField();
                coluna4.DataField = "Compra";
                coluna4.HeaderText = "Compra";
                coluna4.InsertVisible = true;
                coluna4.Visible = false;
                gridcheck.Columns.Add(coluna4);

                CheckBoxField coluna5 = new CheckBoxField();
                coluna5.DataField = "Venda";
                coluna5.HeaderText = "Venda";
                coluna5.Visible = false;

                gridcheck.Columns.Add(coluna5);
                 

                BoundField coluna6 = new BoundField();
                coluna6.DataField = "descricao";
                coluna6.HeaderText = "Descrição";
                coluna6.HtmlEncode = false;

                gridcheck.Columns.Add(coluna6);
            }
            
            
            int tipo = 0;
            string nome = DropDownList1.SelectedValue.ToString();
            //define a string de conexao com provedor caminho e nome do banco de dados

            //define a instrução SQL
            string strSql = "SELECT id_tipoativo From Tipos_ativo WHERE nome LIKE'" + nome + "'";



            //cria a conexão com o banco de dados
            MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true;");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd = new MySqlCommand(strSql, con);

            //abre a conexao
            con.Open();


            //define o tipo do comando

            MySqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                string tipao = sdr["id_tipoativo"].ToString();
                tipo = Convert.ToInt32(tipao);
            }

            sdr.Close();

            string strSql2 = "SELECT * From Ativos WHERE id_tipoativo ='" + tipo + "'";
            MySqlCommand cmd1 = new MySqlCommand(strSql2, con);

            //cria um dataadapter
            MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
            DataSet dataset = new DataSet();
            da.Fill(dataset);
            //cria um objeto datatable
            DataTable clientes = new DataTable();

            //preenche o datatable via dataadapter
            da.Fill(clientes);
            da.Fill(dataset, "Ativos");

            this.gridcheck.DataSource = dataset;

            gridcheck.DataBind();
            Button1.Text = "OK";
            Button2.Text = "Filtrar";
        }
         

        protected void gridcheck_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gridcheck.PageIndex = e.NewPageIndex;
           
             if (Button1.Text == "OK")
            {
                gridcheck.DataSource = null;
                gridcheck.DataBind();


                int contagem = gridcheck.Columns.Count;

                if (contagem <= 0)
                {

                    BoundField coluna1 = new BoundField();
                    coluna1.DataField = "Id_ativo";
                    coluna1.HeaderText = "Código";
                    coluna1.Visible = false;
                    gridcheck.Columns.Add(coluna1);
                    BoundField coluna2 = new BoundField();
                    coluna2.DataField = "fname";
                    coluna2.HeaderText = "Nome";
                    coluna2.ReadOnly = true;
                    coluna2.HtmlEncode = false;
                    gridcheck.Columns.Add(coluna2);
                    BoundField coluna3 = new BoundField();
                    coluna3.DataField = "Id_tipoativo";
                    coluna3.HeaderText = "Tipo";
                    coluna3.HtmlEncode = false;
                    coluna3.Visible = false;
                    gridcheck.Columns.Add(coluna3);
                    CheckBoxField coluna4 = new CheckBoxField();
                    coluna4.DataField = "Compra";
                    coluna4.HeaderText = "Compra";
                    coluna4.InsertVisible = true;
                    coluna4.Visible = false;
                    gridcheck.Columns.Add(coluna4);

                    CheckBoxField coluna5 = new CheckBoxField();
                    coluna5.DataField = "Venda";
                    coluna5.HeaderText = "Venda";
                    coluna5.Visible = false;

                    gridcheck.Columns.Add(coluna5);
               

                    BoundField coluna6 = new BoundField();
                    coluna6.DataField = "descricao";
                    coluna6.HeaderText = "Descrição";
                    coluna6.HtmlEncode = false;

                    gridcheck.Columns.Add(coluna6);
                }


                int tipo = 0;
                string nome = DropDownList1.SelectedValue.ToString();
                //define a string de conexao com provedor caminho e nome do banco de dados

                //define a instrução SQL
                string strSql = "SELECT id_tipoativo From Tipos_ativo WHERE nome LIKE'" + nome + "'";



                //cria a conexão com o banco de dados
                MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //cria o objeto command para executar a instruçao sql
                MySqlCommand cmd = new MySqlCommand(strSql, con);

                //abre a conexao
                con.Open();


                //define o tipo do comando

                MySqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    string tipao = sdr["id_tipoativo"].ToString();
                    tipo = Convert.ToInt32(tipao);
                }

                sdr.Close();

                string strSql2 = "SELECT * From Ativos WHERE id_tipoativo ='" + tipo + "'";
                MySqlCommand cmd1 = new MySqlCommand(strSql2, con);

                //cria um dataadapter
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataSet dataset = new DataSet();
                da.Fill(dataset);
                //cria um objeto datatable
                DataTable clientes = new DataTable();

                //preenche o datatable via dataadapter
                da.Fill(clientes);
                da.Fill(dataset, "Ativos");

                this.gridcheck.DataSource = dataset;

                gridcheck.DataBind();
                foi = "1";
            }
            else if (Button2.Text == "OK")
           {
               gridcheck.DataSource = null;
               gridcheck.DataBind(); ;
               int contagem = gridcheck.Columns.Count;

               if (contagem <= 0)
               {
                   BoundField coluna1 = new BoundField();
                   coluna1.DataField = "Id_ativo";
                   coluna1.HeaderText = "Código";
                   gridcheck.Columns.Add(coluna1);
                   BoundField coluna2 = new BoundField();
                   coluna2.DataField = "fname";
                   coluna2.HeaderText = "ATIVO";
                   coluna2.HtmlEncode = false;
                   gridcheck.Columns.Add(coluna2);
                   BoundField coluna3 = new BoundField();
                   coluna3.DataField = "Id_tipoativo";
                   coluna3.HeaderText = "Tipo";
                   coluna3.HtmlEncode = false;
                   gridcheck.Columns.Add(coluna3);
                   BoundField coluna4 = new BoundField();
                   coluna4.DataField = "Compra";
                   coluna4.HeaderText = "Compra";
                   coluna4.HtmlEncode = false;
                   coluna4.HtmlEncode = false;
                   coluna4.Visible = false;
                   gridcheck.Columns.Add(coluna4);
                   BoundField coluna5 = new BoundField();
                   coluna5.DataField = "Venda";
                   coluna5.HeaderText = "Venda";
                   coluna5.HtmlEncode = false;
                   coluna5.HtmlEncode = false;
                   coluna5.Visible = false;
                   gridcheck.Columns.Add(coluna5);
                   BoundField coluna6 = new BoundField();
                   coluna6.DataField = "descricao";
                   coluna6.HeaderText = "Descrição";
                   coluna6.HtmlEncode = false;

                   gridcheck.Columns.Add(coluna6);
               }
               string strSql2 = "SELECT * From Ativos WHERE fname LIKE'" + txtnome.Text + "' ";
               //cria a conexão com o banco de dados
               MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
               //cria o objeto command para executar a instruçao sql
               MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
               //abre a conexao
               con.Open();


               //define o tipo do comando



               //cria um dataadapter
               MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
               DataSet dataset = new DataSet();
               da.Fill(dataset);
               //cria um objeto datatable
               DataTable clientes = new DataTable();

               //preenche o datatable via dataadapter
               da.Fill(clientes);
               da.Fill(dataset, "Ativos");

               this.gridcheck.DataSource = dataset;

               gridcheck.DataBind();

               foi = "2";
           }

             else
             {
                 prencherGrid();
             }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            gridcheck.DataSource = null;
            gridcheck.DataBind(); ;
               int contagem = gridcheck.Columns.Count;

               if (contagem <= 0)
               {
                   BoundField coluna1 = new BoundField();
                   coluna1.DataField = "Id_ativo";
                   coluna1.HeaderText = "Código";
                   gridcheck.Columns.Add(coluna1);
                   BoundField coluna2 = new BoundField();
                   coluna2.DataField = "fname";
                   coluna2.HeaderText = "ATIVO";
                   coluna2.HtmlEncode = false;
                   gridcheck.Columns.Add(coluna2);
                   BoundField coluna3 = new BoundField();
                   coluna3.DataField = "Id_tipoativo";
                   coluna3.HeaderText = "Tipo";
                   coluna3.HtmlEncode = false;
                   gridcheck.Columns.Add(coluna3);
                   BoundField coluna4 = new BoundField();
                   coluna4.DataField = "Compra";
                   coluna4.HeaderText = "Compra";
                   coluna4.HtmlEncode = false;
                   coluna4.HtmlEncode = false;
                   coluna4.Visible = false;
                   gridcheck.Columns.Add(coluna4);
                   BoundField coluna5 = new BoundField();
                   coluna5.DataField = "Venda";
                   coluna5.HeaderText = "Venda";
                   coluna5.HtmlEncode = false;
                   coluna5.HtmlEncode = false;
                   coluna5.Visible = false;
                   gridcheck.Columns.Add(coluna5);
                   BoundField coluna6 = new BoundField();
                   coluna6.DataField = "descricao";
                   coluna6.HeaderText = "Descrição";
                   coluna6.HtmlEncode = false;

                   gridcheck.Columns.Add(coluna6);
               }
               string strSql2 = "SELECT * From Ativos WHERE fname LIKE'" + txtnome.Text + "%'";
            //cria a conexão com o banco de dados
               MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
            //abre a conexao
            con.Open();


            //define o tipo do comando



            //cria um dataadapter
            MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
            DataSet dataset = new DataSet();
            da.Fill(dataset);
            //cria um objeto datatable
            DataTable clientes = new DataTable();

            //preenche o datatable via dataadapter
            da.Fill(clientes);
            da.Fill(dataset, "Ativos");

            this.gridcheck.DataSource = dataset;

            gridcheck.DataBind();

            foi = "2";
            Button2.Text = "OK";
            Button1.Text = "Filtrar";

            string tipo = DropDownList3.SelectedItem.ToString();
            //     if (e.Row.RowType == DataControlRowType.DataRow)
            //      {
            //          // Display the company name in italics.
            //          foi = "<i>" + e.Row.Cells[2].Text + "</i>";
            
            
        
        }

        protected void gridcheck_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Display the company name in italics.
               foi = "<i>" + e.Row.Cells[2].Text + "</i>";
               string valor = e.Row.Cells[2].Text.ToString();
            }


        }

        protected void gridcheck_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridcheck.EditIndex = e.NewEditIndex;


            if (Button1.Text == "OK")
            {
                gridcheck.DataSource = null;
                gridcheck.DataBind();


                int contagem = gridcheck.Columns.Count;

                if (contagem <= 0)
                {

                    BoundField coluna1 = new BoundField();
                    coluna1.DataField = "Id_ativo";
                    coluna1.HeaderText = "Código";
                    coluna1.Visible = false;
                    gridcheck.Columns.Add(coluna1);
                    BoundField coluna2 = new BoundField();
                    coluna2.DataField = "fname";
                    coluna2.HeaderText = "ATIVO";
                    coluna2.ReadOnly = true;
                    coluna2.HtmlEncode = false;
                    
                    gridcheck.Columns.Add(coluna2);
                    BoundField coluna3 = new BoundField();
                    coluna3.DataField = "Id_tipoativo";
                    coluna3.HeaderText = "Tipo";
                    coluna3.HtmlEncode = false;
                    coluna3.Visible = false;
                    gridcheck.Columns.Add(coluna3);
                    CheckBoxField coluna4 = new CheckBoxField();
                    coluna4.DataField = "Compra";
                    coluna4.HeaderText = "Compra";
                    coluna4.InsertVisible = true;
                    gridcheck.Columns.Add(coluna4);
                    coluna4.Visible = false;

                    CheckBoxField coluna5 = new CheckBoxField();
                    coluna5.DataField = "Venda";
                    coluna5.HeaderText = "Venda";
                    coluna5.Visible = false;

                    gridcheck.Columns.Add(coluna5);

                    BoundField coluna6 = new BoundField();
                    coluna6.DataField = "descricao";
                    coluna6.HeaderText = "Descrição";
                    coluna6.HtmlEncode = false;

                    gridcheck.Columns.Add(coluna6);

                }


                int tipo = 0;
                string nome = DropDownList1.SelectedValue.ToString();
                //define a string de conexao com provedor caminho e nome do banco de dados

                //define a instrução SQL
                string strSql = "SELECT id_tipoativo From Tipos_ativo WHERE nome LIKE'" + nome + "'";



                //cria a conexão com o banco de dados
                MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //cria o objeto command para executar a instruçao sql
                MySqlCommand cmd = new MySqlCommand(strSql, con);

                //abre a conexao
                con.Open();


                //define o tipo do comando

                MySqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    string tipao = sdr["id_tipoativo"].ToString();
                    tipo = Convert.ToInt32(tipao);
                }

                sdr.Close();

                string strSql2 = "SELECT * From Ativos WHERE id_tipoativo ='" + tipo + "'";
                MySqlCommand cmd1 = new MySqlCommand(strSql2, con);

                //cria um dataadapter
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataSet dataset = new DataSet();
                da.Fill(dataset);
                //cria um objeto datatable
                DataTable clientes = new DataTable();

                //preenche o datatable via dataadapter
                da.Fill(clientes);
                da.Fill(dataset, "Ativos");

                this.gridcheck.DataSource = dataset;

                gridcheck.DataBind();
                foi = "1";
            }
            else if (Button2.Text == "OK")
            {
                gridcheck.DataSource = null;
                gridcheck.DataBind(); ;
                int contagem = gridcheck.Columns.Count;

                if (contagem <= 0)
                {
                    BoundField coluna1 = new BoundField();
                    coluna1.DataField = "Id_ativo";
                    coluna1.HeaderText = "Código";
                    gridcheck.Columns.Add(coluna1);
                    BoundField coluna2 = new BoundField();
                    coluna2.DataField = "fname";
                    coluna2.HeaderText = "ATIVO";
                    coluna2.HtmlEncode = false;
                    gridcheck.Columns.Add(coluna2);
                    BoundField coluna3 = new BoundField();
                    coluna3.DataField = "Id_tipoativo";
                    coluna3.HeaderText = "Tipo";
                    coluna3.HtmlEncode = false;
                    gridcheck.Columns.Add(coluna3);
                    CheckBoxField coluna4 = new CheckBoxField();
                    coluna4.DataField = "Compra";
                    coluna4.HeaderText = "Compra";
                    coluna4.InsertVisible = true;
                    coluna4.Visible = false;
                    gridcheck.Columns.Add(coluna4);

                    CheckBoxField coluna5 = new CheckBoxField();
                    coluna5.DataField = "Venda";
                    coluna5.HeaderText = "Venda";
                    coluna5.Visible = false;

                    BoundField coluna6 = new BoundField();
                    coluna6.DataField = "descricao";
                    coluna6.HeaderText = "Descrição";
                    coluna6.HtmlEncode = false;

                    gridcheck.Columns.Add(coluna6);
                }
                string strSql2 = "SELECT * From Ativos WHERE fname LIKE'" + txtnome.Text + "%'";
                //cria a conexão com o banco de dados
                MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true; ");
                //cria o objeto command para executar a instruçao sql
                MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
                //abre a conexao
                con.Open();


                //define o tipo do comando



                //cria um dataadapter
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataSet dataset = new DataSet();
                da.Fill(dataset);
                //cria um objeto datatable
                DataTable clientes = new DataTable();

                //preenche o datatable via dataadapter
                da.Fill(clientes);
                da.Fill(dataset, "Ativos");

                this.gridcheck.DataSource = dataset;

                gridcheck.DataBind();

                foi = "2";
            }

            else
            {
                prencherGrid();
            }

           
           
        }

        protected void gridcheck_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridcheck.Rows[e.RowIndex];
       
            if (row.RowType == DataControlRowType.DataRow)
            {
                // Display the company name in italics.
                foi =  row.Cells[2].Text ;
               
            }
            
            int column = gridcheck.Columns.Count;

           
          
            TextBox tname = (TextBox)row.FindControl("coluna2");
   
            TextBox txtBox1 = (TextBox)(row.Cells[1].Controls[0]);
            //TextBox txtBox2 = (TextBox)(row.Cells[2].Controls[0]);
         //   String txtProdID = (String)gridcheck.Rows[e.RowIndex].FindControl("coluna2").ToString();
            TextBox txtBox3 = (TextBox)(row.Cells[3].Controls[0]);
            CheckBox compra = (CheckBox)(row.Cells[4].Controls[0]);
            CheckBox venda = (CheckBox)(row.Cells[5].Controls[0]);
            
                String str = foi;
           

            if (compra.Checked == true)
            {

                if (lstcompra.Items.Contains(lstcompra.Items.FindByText(foi)))
                //string found
                {                               //string not found


                }
                else
                {
                    lstcompra.Items.Add(foi);

                    System.IO.StreamWriter SaveFile = new StreamWriter(Server.MapPath("~/compra.txt"), true);


                    SaveFile.WriteLine(foi);


                    SaveFile.Close();
                }
            }
            if (venda.Checked == true)
            {

                if (lstvenda.Items.Contains(lstvenda.Items.FindByText(foi)))
                //string found
                {                               //string not found


                }
                else
                {
                    lstvenda.Items.Add(foi);
                    System.IO.StreamWriter SaveFile = new StreamWriter(Server.MapPath("~/venda.txt"), true);

                    SaveFile.WriteLine(foi);


                    SaveFile.Close();
                }

            }

            if (Button1.Text == "OK")
            {
                gridcheck.DataSource = null;
                gridcheck.DataBind();


                int contagem = gridcheck.Columns.Count;

                if (contagem <= 0)
                {

                    BoundField coluna1 = new BoundField();
                    coluna1.DataField = "Id_ativo";
                    coluna1.HeaderText = "Código";
                    coluna1.Visible = false;
                    gridcheck.Columns.Add(coluna1);
                    BoundField coluna2 = new BoundField();
                    coluna2.DataField = "fname";
                    coluna2.HeaderText = "ATIVO";
                    coluna2.ReadOnly = true;
                    coluna2.HtmlEncode = false;

                    gridcheck.Columns.Add(coluna2);
                    BoundField coluna3 = new BoundField();
                    coluna3.DataField = "Id_tipoativo";
                    coluna3.HeaderText = "Tipo";
                    coluna3.HtmlEncode = false;
                    coluna3.Visible = false;
                    gridcheck.Columns.Add(coluna3);
                    CheckBoxField coluna4 = new CheckBoxField();
                    coluna4.DataField = "Compra";
                    coluna4.HeaderText = "Compra";
                    coluna4.InsertVisible = true;
                    coluna4.Visible = false;
                    gridcheck.Columns.Add(coluna4);

                    CheckBoxField coluna5 = new CheckBoxField();
                    coluna5.DataField = "Venda";
                    coluna5.HeaderText = "Venda";
                    coluna5.Visible = false;

                    gridcheck.Columns.Add(coluna5);

                    BoundField coluna6 = new BoundField();
                    coluna6.DataField = "descricao";
                    coluna6.HeaderText = "Descrição";
                    coluna6.HtmlEncode = false;

                    gridcheck.Columns.Add(coluna6);
                }


                int tipo = 0;
                string nome = DropDownList1.SelectedValue.ToString();
                //define a string de conexao com provedor caminho e nome do banco de dados

                //define a instrução SQL
                string strSql = "SELECT id_tipoativo From Tipos_ativo WHERE nome LIKE'" + nome + "%'";



                //cria a conexão com o banco de dados
                MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //cria o objeto command para executar a instruçao sql
                MySqlCommand cmd = new MySqlCommand(strSql, con);

                //abre a conexao
                con.Open();


                //define o tipo do comando

                MySqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    string tipao = sdr["id_tipoativo"].ToString();
                    tipo = Convert.ToInt32(tipao);
                }

                sdr.Close();

                string strSql2 = "SELECT * From Ativos WHERE id_tipoativo ='" + tipo + "'";
                MySqlCommand cmd1 = new MySqlCommand(strSql2, con);

                //cria um dataadapter
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataSet dataset = new DataSet();
                da.Fill(dataset);
                //cria um objeto datatable
                DataTable clientes = new DataTable();

                //preenche o datatable via dataadapter
                da.Fill(clientes);
                da.Fill(dataset, "Ativos");

                this.gridcheck.DataSource = dataset;

                gridcheck.DataBind();
                foi = "1";
            }
            else if (Button2.Text == "OK")
            {
                gridcheck.DataSource = null;
                gridcheck.DataBind(); ;
                int contagem = gridcheck.Columns.Count;

                if (contagem <= 0)
                {
                    BoundField coluna1 = new BoundField();
                    coluna1.DataField = "Id_ativo";
                    coluna1.HeaderText = "Código";
                    gridcheck.Columns.Add(coluna1);
                    BoundField coluna2 = new BoundField();
                    coluna2.DataField = "fname";
                    coluna2.HeaderText = "ATIVO";
                    coluna2.HtmlEncode = false;
                    gridcheck.Columns.Add(coluna2);
                    BoundField coluna3 = new BoundField();
                    coluna3.DataField = "Id_tipoativo";
                    coluna3.HeaderText = "Tipo";
                    coluna3.HtmlEncode = false;
                    gridcheck.Columns.Add(coluna3);
                    CheckBoxField coluna4 = new CheckBoxField();
                    coluna4.DataField = "Compra";
                    coluna4.HeaderText = "Compra";
                    coluna4.InsertVisible = true;
                    coluna4.Visible = false;
                    gridcheck.Columns.Add(coluna4);

                    CheckBoxField coluna5 = new CheckBoxField();
                    coluna5.DataField = "Venda";
                    coluna5.HeaderText = "Venda";
                    coluna5.Visible = false;

                    BoundField coluna6 = new BoundField();
                    coluna6.DataField = "descricao";
                    coluna6.HeaderText = "Descrição";
                    coluna6.HtmlEncode = false;

                    gridcheck.Columns.Add(coluna6);
                }
                string strSql2 = "SELECT * From Ativos WHERE fname LIKE'" + txtnome.Text + "%'";
                //cria a conexão com o banco de dados
                MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //cria o objeto command para executar a instruçao sql
                MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
                //abre a conexao
                con.Open();


                //define o tipo do comando



                //cria um dataadapter
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataSet dataset = new DataSet();
                da.Fill(dataset);
                //cria um objeto datatable
                DataTable clientes = new DataTable();

                //preenche o datatable via dataadapter
                da.Fill(clientes);
                da.Fill(dataset, "Ativos");

                this.gridcheck.DataSource = dataset;

                gridcheck.DataBind();

                foi = "2";
            }

            else
            {
                prencherGrid();
            }

           
        }

        protected void gridcheck_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
      
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
             
                 List<ListItem> itemsToRemove = new List<ListItem>();

            foreach (ListItem listItem in lstcompra.Items)
            {
                if (listItem.Selected)
                    itemsToRemove.Add(listItem);
             //   DeleteLinesFromFile(lstcompra.SelectedValue.ToString(), "c");
            }

            foreach (ListItem listItem in itemsToRemove)
            {
                lstcompra.Items.Remove(listItem);
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            List<ListItem> itemsToRemove = new List<ListItem>();

            foreach (ListItem listItem in lstvenda.Items)
            {
                if (listItem.Selected)
                    itemsToRemove.Add(listItem);
              //  DeleteLinesFromFile(lstvenda.SelectedValue.ToString(), "v");
               
            }

            foreach (ListItem listItem in itemsToRemove)
            {
                lstvenda.Items.Remove(listItem);
            }
          
        }
        public void DeleteLinesFromFile(string strLineToDelete , string tipo)
        {
            if (tipo == "c")
            {
                string compra = (Server.MapPath("~/compra.txt"));
                string strFilePath = compra;
                string strSearchText = strLineToDelete;
                string strOldText;
                string n = "";
                StreamReader sr = File.OpenText(strFilePath);
                while ((strOldText = sr.ReadLine()) != null)
                {
                    if (!strOldText.Contains(strSearchText))
                    {
                        n += strOldText + Environment.NewLine;
                    }
                }
                sr.Close();
                File.WriteAllText(strFilePath, n);
            }
            else
            {
                string venda = (Server.MapPath("~/venda.txt"));
                string strFilePath = venda;
                string strSearchText = strLineToDelete;
                string strOldText;
                string n = "";
                StreamReader sr = File.OpenText(strFilePath);
                while ((strOldText = sr.ReadLine()) != null)
                {
                    if (!strOldText.Contains(strSearchText))
                    {
                        n += strOldText + Environment.NewLine;
                    }
                }
                sr.Close();
                File.WriteAllText(strFilePath, n);
            }
        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            lstcompra.Items.Clear();
            lstvenda.Items.Clear();
            try
            {
                File.Delete(Server.MapPath("~/venda.txt"));
                File.Delete(Server.MapPath("~/compra.txt"));
            }
            catch
            {

            }
        }

        protected void gridcheck_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridcheck.EditIndex = -1;
            try
            {
                File.Delete(Server.MapPath("~/venda.txt"));
                File.Delete(Server.MapPath("~/compra.txt"));
            }
            catch
            {

            }
            if (Button1.Text == "OK")
            {
                gridcheck.DataSource = null;
                gridcheck.DataBind();


                int contagem = gridcheck.Columns.Count;

                if (contagem <= 0)
                {

                    BoundField coluna1 = new BoundField();
                    coluna1.DataField = "Id_ativo";
                    coluna1.HeaderText = "Código";
                    coluna1.Visible = false;
                    gridcheck.Columns.Add(coluna1);
                    BoundField coluna2 = new BoundField();
                    coluna2.DataField = "fname";
                    coluna2.HeaderText = "ATIVO";
                    coluna2.ReadOnly = true;
                    coluna2.HtmlEncode = false;

                    gridcheck.Columns.Add(coluna2);
                    BoundField coluna3 = new BoundField();
                    coluna3.DataField = "Id_tipoativo";
                    coluna3.HeaderText = "Tipo";
                    coluna3.HtmlEncode = false;
                    coluna3.Visible = false;
                    gridcheck.Columns.Add(coluna3);
                    CheckBoxField coluna4 = new CheckBoxField();
                    coluna4.DataField = "Compra";
                    coluna4.HeaderText = "Compra";
                    coluna4.InsertVisible = true;
                    gridcheck.Columns.Add(coluna4);
                    coluna4.Visible = false;
                    CheckBoxField coluna5 = new CheckBoxField();
                    coluna5.DataField = "Venda";
                    coluna5.HeaderText = "Venda";
                    coluna5.Visible = false;

                    gridcheck.Columns.Add(coluna5);

                    BoundField coluna6 = new BoundField();
                    coluna6.DataField = "descricao";
                    coluna6.HeaderText = "Descrição";
                    coluna6.HtmlEncode = false;

                    gridcheck.Columns.Add(coluna6);
                }


                int tipo = 0;
                string nome = DropDownList1.SelectedValue.ToString();
                //define a string de conexao com provedor caminho e nome do banco de dados

                //define a instrução SQL
                string strSql = "SELECT id_tipoativo From Tipos_ativo WHERE nome LIKE'" + nome + "%'";



                //cria a conexão com o banco de dados
                MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //cria o objeto command para executar a instruçao sql
                MySqlCommand cmd = new MySqlCommand(strSql, con);

                //abre a conexao
                con.Open();


                //define o tipo do comando

                MySqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    string tipao = sdr["id_tipoativo"].ToString();
                    tipo = Convert.ToInt32(tipao);
                }

                sdr.Close();

                string strSql2 = "SELECT * From Ativos WHERE id_tipoativo ='" + tipo + "'";
                MySqlCommand cmd1 = new MySqlCommand(strSql2, con);

                //cria um dataadapter
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataSet dataset = new DataSet();
                da.Fill(dataset);
                //cria um objeto datatable
                DataTable clientes = new DataTable();

                //preenche o datatable via dataadapter
                da.Fill(clientes);
                da.Fill(dataset, "Ativos");

                this.gridcheck.DataSource = dataset;

                gridcheck.DataBind();
                foi = "1";
            }
            else if (Button2.Text == "OK")
            {
                gridcheck.DataSource = null;
                gridcheck.DataBind(); ;
                int contagem = gridcheck.Columns.Count;

                if (contagem <= 0)
                {
                    BoundField coluna1 = new BoundField();
                    coluna1.DataField = "Id_ativo";
                    coluna1.HeaderText = "Código";
                    gridcheck.Columns.Add(coluna1);
                    BoundField coluna2 = new BoundField();
                    coluna2.DataField = "fname";
                    coluna2.HeaderText = "ATIVO";
                    coluna2.HtmlEncode = false;
                    gridcheck.Columns.Add(coluna2);
                    BoundField coluna3 = new BoundField();
                    coluna3.DataField = "Id_tipoativo";
                    coluna3.HeaderText = "Tipo";
                    coluna3.HtmlEncode = false;
                    gridcheck.Columns.Add(coluna3);
                    CheckBoxField coluna4 = new CheckBoxField();
                    coluna4.DataField = "Compra";
                    coluna4.HeaderText = "Compra";
                    coluna4.InsertVisible = true;
                    gridcheck.Columns.Add(coluna4);
                    coluna4.Visible = false;
                    CheckBoxField coluna5 = new CheckBoxField();
                    coluna5.DataField = "Venda";
                    coluna5.HeaderText = "Venda";
                    coluna5.Visible = false;

                    BoundField coluna6 = new BoundField();
                    coluna6.DataField = "descricao";
                    coluna6.HeaderText = "Descrição";
                    coluna6.HtmlEncode = false;

                    gridcheck.Columns.Add(coluna6);
                }
                string strSql2 = "SELECT * From Ativos WHERE fname LIKE'" + txtnome.Text + "%'";
                //cria a conexão com o banco de dados
                MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
                //cria o objeto command para executar a instruçao sql
                MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
                //abre a conexao
                con.Open();


                //define o tipo do comando



                //cria um dataadapter
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataSet dataset = new DataSet();
                da.Fill(dataset);
                //cria um objeto datatable
                DataTable clientes = new DataTable();

                //preenche o datatable via dataadapter
                da.Fill(clientes);
                da.Fill(dataset, "Ativos");

                this.gridcheck.DataSource = dataset;

                gridcheck.DataBind();

                foi = "2";
            }

            else
            {
                prencherGrid();
            }

           
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
           // const string sPath = "save.txt";
           //   using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/data1.txt"), true))
            try
            {
         //     File.Delete(Server.MapPath("~/data1.txt"));
         //     File.Delete(Server.MapPath("~/compra.txt"));
            }
            catch
            {

            }

            string utao = "";
            try
            {
                utao = DropDownList4.SelectedItem.ToString();
            }
            catch
            {
                DropDownList4.BackColor = Color.LightGray;
                
                return;
            }
            if (DropDownList4.SelectedValue == "M")
            {

                foreach (var item in lstcompra.Items)
                {
                    string acao = item.ToString();
                    string[] dadosDoCadastro = acao.Split('-');
                    //  Console.WriteLine(dadosDoCadastro[1]);
                    string split = dadosDoCadastro[1];
                    string id = dadosDoCadastro[0];


                    MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true;");
                    string sql = "INSERT INTO CLL(ATIVO, TIPO ) VALUES (@ativo, @tipo)";

                    MySqlCommand comando = new MySqlCommand(sql, conn);
                    //Adicionando o valor das textBox nos parametros do comando

                    comando.Parameters.Add(new MySqlParameter("@ativo", acao));
                    comando.Parameters.Add(new MySqlParameter("@tipo", "CM"));


                    //abre a conexao
                    conn.Open();
                    //executa o comando com os parametros que foram adicionados acima
                    comando.ExecuteNonQuery();
                    //fecha a conexao
                    conn.Close();
                    //Minha função para limpar o
                }
                Response.Redirect("Callcompra.aspx", false);
            }
            else if (DropDownList4.SelectedValue == "T")
            {

                foreach (var item in lstcompra.Items)
                {
                    string acao = item.ToString();
                    string[] dadosDoCadastro = acao.Split('-');
                    //  Console.WriteLine(dadosDoCadastro[1]);
                    string split = dadosDoCadastro[1];
                    string id = dadosDoCadastro[0];


                    MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true;");
                    string sql = "INSERT INTO CLL(ATIVO, TIPO ) VALUES (@ativo, @tipo)";

                    MySqlCommand comando = new MySqlCommand(sql, conn);
                    //Adicionando o valor das textBox nos parametros do comando

                    comando.Parameters.Add(new MySqlParameter("@ativo", acao));
                    comando.Parameters.Add(new MySqlParameter("@tipo", "CT"));


                    //abre a conexao
                    conn.Open();
                    //executa o comando com os parametros que foram adicionados acima
                    comando.ExecuteNonQuery();
                    //fecha a conexao
                    conn.Close();
                }
                //Minha função para limpar o
                Response.Redirect("Callcompra.aspx", false);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            // const string sPath = "save.txt";
            //   using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/data1.txt"), true))
        

               string utao = "";
            try
            {
                utao = DropDownList4.SelectedItem.ToString();
            }
            catch
            {
                DropDownList4.BackColor = Color.LightGray;
                
                return;
            }
            if (DropDownList4.SelectedValue == "M")
            {
              

                foreach (var item in lstvenda.Items)
                {
                  
                    string acao = item.ToString();
                    string[] dadosDoCadastro = acao.Split('-');
                    //  Console.WriteLine(dadosDoCadastro[1]);
                    string split = dadosDoCadastro[1];
                    string id = dadosDoCadastro[0];


                    MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true;");
                    string sql = "INSERT INTO CLL(ATIVO, TIPO ) VALUES (@ativo, @tipo)";

                    MySqlCommand comando = new MySqlCommand(sql, conn);
                    //Adicionando o valor das textBox nos parametros do comando

                    comando.Parameters.Add(new MySqlParameter("@ativo", acao));
                    comando.Parameters.Add(new MySqlParameter("@tipo", "VM"));


                    //abre a conexao
                    conn.Open();
                    //executa o comando com os parametros que foram adicionados acima
                    comando.ExecuteNonQuery();
                    //fecha a conexao
                    conn.Close();
                    //Minha função para limpar o
                   
                }

               
                Response.Redirect("Callvenda.aspx", false);
            }
            else if (DropDownList4.SelectedValue == "T")
            {


                foreach (var item in lstvenda.Items)
                {

                    string acao = item.ToString();
                    string[] dadosDoCadastro = acao.Split('-');
                    //  Console.WriteLine(dadosDoCadastro[1]);
                    string split = dadosDoCadastro[1];
                    string id = dadosDoCadastro[0];


                    MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                    string sql = "INSERT INTO CLL(ATIVO, TIPO ) VALUES (@ativo, @tipo)";

                    MySqlCommand comando = new MySqlCommand(sql, conn);
                    //Adicionando o valor das textBox nos parametros do comando

                    comando.Parameters.Add(new MySqlParameter("@ativo", acao));
                    comando.Parameters.Add(new MySqlParameter("@tipo", "VT"));


                    //abre a conexao
                    conn.Open();
                    //executa o comando com os parametros que foram adicionados acima
                    comando.ExecuteNonQuery();
                    //fecha a conexao
                    conn.Close();
                    //Minha função para limpar o

                }


                Response.Redirect("Callvenda.aspx", false);
            }
        }

        protected void txtnome_TextChanged(object sender, EventArgs e)
        {
             gridcheck.DataSource = null;
            gridcheck.DataBind(); ;
               int contagem = gridcheck.Columns.Count;

               if (contagem <= 0)
               {
                   BoundField coluna1 = new BoundField();
                   coluna1.DataField = "Id_ativo";
                   coluna1.HeaderText = "Código";
                   gridcheck.Columns.Add(coluna1);
                   BoundField coluna2 = new BoundField();
                   coluna2.DataField = "fname";
                   coluna2.HeaderText = "ATIVO";
                   coluna2.HtmlEncode = false;
                   gridcheck.Columns.Add(coluna2);
                   BoundField coluna3 = new BoundField();
                   coluna3.DataField = "Id_tipoativo";
                   coluna3.HeaderText = "Tipo";
                   coluna3.HtmlEncode = false;
                   gridcheck.Columns.Add(coluna3);
                   BoundField coluna4 = new BoundField();
                   coluna4.DataField = "Compra";
                   coluna4.HeaderText = "Compra";
                   coluna4.HtmlEncode = false;
                   coluna4.HtmlEncode = false;
                   coluna4.Visible = false;
                   gridcheck.Columns.Add(coluna4);
                   BoundField coluna5 = new BoundField();
                   coluna5.DataField = "Venda";
                   coluna5.HeaderText = "Venda";
                   coluna5.HtmlEncode = false;
                   coluna5.HtmlEncode = false;
                   coluna5.Visible = false;
                   gridcheck.Columns.Add(coluna5);
                   BoundField coluna6 = new BoundField();
                   coluna6.DataField = "descricao";
                   coluna6.HeaderText = "Descrição";
                   coluna6.HtmlEncode = false;

                   gridcheck.Columns.Add(coluna6);
               }
               string strSql2 = "SELECT * From Ativos WHERE fname LIKE'" + txtnome.Text + "%'";
            //cria a conexão com o banco de dados
               MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
            //abre a conexao
            con.Open();


            //define o tipo do comando



            //cria um dataadapter
            MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
            DataSet dataset = new DataSet();
            da.Fill(dataset);
            //cria um objeto datatable
            DataTable clientes = new DataTable();

            //preenche o datatable via dataadapter
            da.Fill(clientes);
            da.Fill(dataset, "Ativos");

            this.gridcheck.DataSource = dataset;

            gridcheck.DataBind();

            foi = "2";
            Button2.Text = "OK";
            Button1.Text = "Filtrar";
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql = "INSERT INTO Ativos(fname, id_tipoativo ) VALUES (@nome, @idtipo)";
            string nome = DropDownList2.SelectedValue.ToString();
            int tipo = 0;
           
            //define a string de conexao com provedor caminho e nome do banco de dados

            //define a instrução SQL
            string strSql = "SELECT id_tipoativo From Tipos_ativo WHERE nome LIKE'" + nome + "'";



            //cria a conexão com o banco de dados
            MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd = new MySqlCommand(strSql, con);

            //abre a conexao
            con.Open();


            //define o tipo do comando

            MySqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                string tipao = sdr["id_tipoativo"].ToString();
                tipo = Convert.ToInt32(tipao);
            }

            sdr.Close();


            try
            {


                MySqlCommand comando = new MySqlCommand(sql, conn);
                //Adicionando o valor das textBox nos parametros do comando

                comando.Parameters.Add(new MySqlParameter("@nome", this.txtnovo.Text));
                comando.Parameters.Add(new MySqlParameter("@idtipo", tipo));


                //abre a conexao
                conn.Open();
                //executa o comando com os parametros que foram adicionados acima
                comando.ExecuteNonQuery();
                //fecha a conexao
                conn.Close();
                //Minha função para limpar os textBox

                //Abaixo temos a ultlização de javascript para apresentar ao usuário um alert
                // referente ao msgbox

            }
            catch
            {

            }
            finally
            {
                gridcheck.DataSource = null;
                gridcheck.DataBind(); ;
                int contagem = gridcheck.Columns.Count;

                if (contagem <= 0)
                {
                    BoundField coluna1 = new BoundField();
                    coluna1.DataField = "Id_ativo";
                    coluna1.HeaderText = "Código";
                    gridcheck.Columns.Add(coluna1);
                    BoundField coluna2 = new BoundField();
                    coluna2.DataField = "fname";
                    coluna2.HeaderText = "ATIVO";
                    coluna2.HtmlEncode = false;
                    gridcheck.Columns.Add(coluna2);
                    BoundField coluna3 = new BoundField();
                    coluna3.DataField = "Id_tipoativo";
                    coluna3.HeaderText = "Tipo";
                    coluna3.HtmlEncode = false;
                    gridcheck.Columns.Add(coluna3);
                    BoundField coluna4 = new BoundField();
                    coluna4.DataField = "Compra";
                    coluna4.HeaderText = "Compra";
                    coluna4.HtmlEncode = false;
                    coluna4.HtmlEncode = false;
                    coluna3.Visible = false;
                    gridcheck.Columns.Add(coluna4);
                    BoundField coluna5 = new BoundField();
                    coluna5.DataField = "Venda";
                    coluna5.HeaderText = "Venda";
                    coluna5.HtmlEncode = false;
                    coluna5.HtmlEncode = false;
                    coluna5.Visible = false;
                    gridcheck.Columns.Add(coluna5);

                    BoundField coluna6 = new BoundField();
                    coluna6.DataField = "descricao";
                    coluna6.HeaderText = "Descrição";
                    coluna6.HtmlEncode = false;

                    gridcheck.Columns.Add(coluna6);
                }
                string strSql2 = "SELECT * From Ativos WHERE fname LIKE'" + txtnome.Text + "%'";
                //cria a conexão com o banco de dados
                MySqlConnection con4 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //cria o objeto command para executar a instruçao sql
                MySqlCommand cmd1 = new MySqlCommand(strSql2, con4);
                //abre a conexao
                con4.Open();


                //define o tipo do comando



                //cria um dataadapter
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataSet dataset = new DataSet();
                da.Fill(dataset);
                //cria um objeto datatable
                DataTable clientes = new DataTable();

                //preenche o datatable via dataadapter
                da.Fill(clientes);
                da.Fill(dataset, "Ativos");

                this.gridcheck.DataSource = dataset;

                gridcheck.DataBind();

                foi = "2";
                Button2.Text = "OK";
                Button1.Text = "Filtrar";
                txtnovo.Text = "";
            }
        }

        protected void gridcheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            int column = gridcheck.Columns.Count;
            GridViewRow row = gridcheck.SelectedRow;

            // Display the first name from the selected row.
            // In this example, the third column (index 2) contains
            // the first name.
            string tipo = DropDownList3.SelectedItem.ToString();
            //     if (e.Row.RowType == DataControlRowType.DataRow)
            //      {
            //          // Display the company name in italics.
            //          foi = "<i>" + e.Row.Cells[2].Text + "</i>";
            string valor = row.Cells[2].Text.ToString();
            string id_Ativo = row.Cells[1].Text.ToString();
            string descricao = row.Cells[5].Text.ToString();
            string tipo_ativo = "";
            string nome_tipo = "";
            string strSql = "SELECT * From Ativos WHERE fname LIKE'" + valor + "'";



            //cria a conexão com o banco de dados
            MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd = new MySqlCommand(strSql, con);

            //abre a conexao
            con.Open();


            //define o tipo do comando

            MySqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
              
               tipo_ativo = sdr["id_tipoativo"].ToString();
            }

            sdr.Close();


            string strSql1 = "SELECT nome From Tipos_ativo WHERE id_tipoativo LIKE'" + tipo_ativo + "'";



            //cria a conexão com o banco de dados
            MySqlConnection con1 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd1 = new MySqlCommand(strSql1, con1);

            //abre a conexao
            con1.Open();


            //define o tipo do comando

            MySqlDataReader sdr1 = cmd1.ExecuteReader();

            while (sdr1.Read())
            {
                nome_tipo = sdr1["nome"].ToString();
               
            }

            sdr1.Close();



            if (tipo == "Compra")
            {
                string tipoativo = DropDownList1.SelectedItem.ToString();

                try
                {
                    lstcompra.Items.Add(nome_tipo +"-"+  valor + " - " + descricao);

                }
                catch
                {
                    lstcompra.Items.Add(nome_tipo + "-" + valor);
                }
                
               
                
              //  System.IO.StreamWriter SaveFile = new StreamWriter(Server.MapPath("~/compra.txt"), true);


             //   SaveFile.WriteLine(foi);


             //   SaveFile.Close();
            }
            else if (tipo == "Venda")
            {
                string tipoativo = DropDownList1.SelectedItem.ToString();

                try
                {
                    lstvenda.Items.Add(nome_tipo + "-" + valor + " - " + descricao);

                }
                catch
                {
                    lstvenda.Items.Add(nome_tipo + "-" + valor);
                }
               
                
                
          //      System.IO.StreamWriter SaveFile = new StreamWriter(Server.MapPath("~/venda.txt"), true);


           //     SaveFile.WriteLine(foi);


            //    SaveFile.Close();
            }
            int count = lstcompra.Items.Count;
            int count2 = lstvenda.Items.Count;
            if ((count <= 1)&(count2 <=1))
            {
                Panel1.Visible = true;

                txtativo.Text = valor;
                txtid.Text = id_Ativo;
                txtdesc.Text = descricao;
            }
            //      }
        }

        protected void lstcompra_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnatual_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql = "UPDATE Ativos SET fname=@fname, DESCRICAO=@desc  WHERE fname LIKE'" + txtativo.Text + "'";

            try
            {


                MySqlCommand comando = new MySqlCommand(sql, conn);
                //Adicionando o valor das textBox nos parametros do comando

                comando.Parameters.Add(new MySqlParameter("@fname", txtativo.Text));
                comando.Parameters.Add(new MySqlParameter("@desc", txtdesc.Text));

                //abre a conexao
                conn.Open();
                //executa o comando com os parametros que foram adicionados acima
                comando.ExecuteNonQuery();
                //fecha a conexao
                conn.Close();
                //Minha função para limpar os textBox

                //Abaixo temos a ultlização de javascript para apresentar ao usuário um alert
                // referente ao msgbox

            }
            catch
            {

            }
            finally
            {
                conn.Close();
                Panel1.Visible = false;
                Response.Redirect(Request.Url.AbsoluteUri);

            }
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql = "DELETE FROM Ativos WHERE fname LIKE'" + txtativo.Text + "'";



            MySqlCommand comando = new MySqlCommand(sql, conn);
            //Adicionando o valor das textBox nos parametros do comando



            //abre a conexao
            conn.Open();
            //executa o comando com os parametros que foram adicionados acima
            comando.ExecuteNonQuery();
            //fecha a conexao
            conn.Close();
            //Minha função para limpar os textBox

            //Abaixo temos a ultlização de javascript para apresentar ao usuário um alert
            // referente ao msgbox


            conn.Close();
            Panel1.Visible = false;
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridcheck.DataSource = null;
            gridcheck.DataBind();


            int contagem = gridcheck.Columns.Count;

            if (contagem <= 0)
            {

                BoundField coluna1 = new BoundField();
                coluna1.DataField = "Id_ativo";
                coluna1.HeaderText = "Código";
                coluna1.Visible = false;
                gridcheck.Columns.Add(coluna1);
                BoundField coluna2 = new BoundField();
                coluna2.DataField = "fname";
                coluna2.HeaderText = "Nome-Resul";
                coluna2.ReadOnly = true;
                coluna2.HtmlEncode = false;
                gridcheck.Columns.Add(coluna2);
                BoundField coluna3 = new BoundField();
                coluna3.DataField = "Id_tipoativo";
                coluna3.HeaderText = "Tipo";
                coluna3.HtmlEncode = false;
                coluna3.Visible = false;
                gridcheck.Columns.Add(coluna3);
                CheckBoxField coluna4 = new CheckBoxField();
                coluna4.DataField = "Compra";
                coluna4.HeaderText = "Compra";
                coluna4.InsertVisible = true;
                coluna4.Visible = false;
                gridcheck.Columns.Add(coluna4);

                CheckBoxField coluna5 = new CheckBoxField();
                coluna5.DataField = "Venda";
                coluna5.HeaderText = "Venda";
                coluna5.Visible = false;

                gridcheck.Columns.Add(coluna5);


                BoundField coluna6 = new BoundField();
                coluna6.DataField = "descricao";
                coluna6.HeaderText = "Descrição";
                coluna6.HtmlEncode = false;

                gridcheck.Columns.Add(coluna6);
            }


            int tipo = 0;
            string nome = DropDownList1.SelectedValue.ToString();
            //define a string de conexao com provedor caminho e nome do banco de dados

            //define a instrução SQL
            string strSql = "SELECT id_tipoativo From Tipos_ativo WHERE nome LIKE'" + nome + "'";



            //cria a conexão com o banco de dados
            MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd = new MySqlCommand(strSql, con);

            //abre a conexao
            con.Open();


            //define o tipo do comando

            MySqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                string tipao = sdr["id_tipoativo"].ToString();
                tipo = Convert.ToInt32(tipao);
            }

            sdr.Close();

            string strSql2 = "SELECT * From Ativos WHERE id_tipoativo ='" + tipo + "'";
            MySqlCommand cmd1 = new MySqlCommand(strSql2, con);

            //cria um dataadapter
            MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
            DataSet dataset = new DataSet();
            da.Fill(dataset);
            //cria um objeto datatable
            DataTable clientes = new DataTable();

            //preenche o datatable via dataadapter
            da.Fill(clientes);
            da.Fill(dataset, "Ativos");

            this.gridcheck.DataSource = dataset;

            gridcheck.DataBind();
            Button1.Text = "OK";
            Button2.Text = "Filtrar";
        }

         
    }
}