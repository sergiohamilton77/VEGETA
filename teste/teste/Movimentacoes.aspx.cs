using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing;
using System.Web.Mail;
using System.Net.Mail; 
using System.Net.Configuration;
using iTextSharp;//E A BIBLIOTECA ITEXTSHARP E SUAS EXTENSÕES
using iTextSharp.text;//EXTENSAO 1 (TEXT)
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.UI.HtmlControls;
using System.Net;
using MySql.Data.MySqlClient;//EXTENSAO 2 (PDF)
using Root.Reports;


namespace teste
{
    public partial class Movimentacoes : System.Web.UI.Page
    {

        private FontDef fontDef_Helvetica;
        private Double rPosLeft = 20;  // millimeters
        private Double rPosRight = 195;  // millimeters
        private Double rPosTop = 24;  // millimeters
        private Double rPosBottom = 278;  // millimeters

        
        protected void Page_Load(object sender, EventArgs e)
        {

          

            Label lbl = this.Master.FindControl("lblMasterPage") as Label;
            
             Label4.Text =  Request.QueryString["User"];
            string idclientefinal = Label4.Text;


            this.TextBox1.Attributes.Add("onKeyPress",
                 "doClick('" + Button1.ClientID + "',event)");
          //  string user = lbl.Text;
         //   if (user == "")
        //    {
         //       Response.Redirect("Loginuser.aspx", false);
         ///   }
            
            if (!IsPostBack)
            {
                 if (Label4.Text == "")
             {
             }
             else
             {
                 
                 string nomao = "";
                 string strSql2 = "SELECT * FROM Clientes WHERE id_cliente Like'" + Label4.Text + "'";
                 //cria a conexão com o banco de dados
                 MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                 //cria o objeto command para executar a instruçao sql
                 MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
                 //abre a conexao
                 con.Open();
                 MySqlDataReader dr = cmd1.ExecuteReader();

                 while (dr.Read())
                 {

                   nomao = ((string)dr["Nome_cliente"]);

                 }

                 dr.Close();

                 
               
            
                GridView2.DataSource = null;
                GridView2.DataBind();
                //define a instrução SQL

                GridView2.AutoGenerateColumns = false;

                //define e realiza a formatação de cada coluna
                BoundField coluna1 = new BoundField();
                coluna1.DataField = "data";
                coluna1.HeaderText = "Data";

                GridView2.Columns.Add(coluna1);
                BoundField coluna2 = new BoundField();
                coluna2.DataField = "nome";
                coluna2.HeaderText = "Nome";
                coluna2.ReadOnly = true;
                coluna2.HtmlEncode = false;

                GridView2.Columns.Add(coluna2);
                BoundField coluna3 = new BoundField();
                coluna3.DataField = "ativo";
                coluna3.HeaderText = "Ativo";
                coluna3.HtmlEncode = false;


                GridView2.Columns.Add(coluna3);
                BoundField coluna4 = new BoundField();
                coluna4.DataField = "valor";
                coluna4.HeaderText = "valor";
                coluna4.HtmlEncode = false;
                GridView2.Columns.Add(coluna4);

                BoundField coluna5 = new BoundField();
                coluna5.DataField = "tipo";
                coluna5.HeaderText = "tipo";
                coluna5.HtmlEncode = false;
                GridView2.Columns.Add(coluna5);

                BoundField coluna6 = new BoundField();
                coluna6.DataField = "qtde";
                coluna6.HeaderText = "qtde";
                coluna6.HtmlEncode = false;
                GridView2.Columns.Add(coluna6);

                BoundField coluna7 = new BoundField();
                coluna7.DataField = "id";
                coluna7.HeaderText = "id";
                coluna7.HtmlEncode = false;
                GridView2.Columns.Add(coluna7);

                string strSql3 = "SELECT * From Movim WHERE id_cliente LIKE'" + Label4.Text + "'";
                //cria a conexão com o banco de dados
                MySqlConnection con3 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true; ");
                //cria o objeto command para executar a instruçao sql
                MySqlCommand cmd3 = new MySqlCommand(strSql3, con3);
                //abre a conexao
                con3.Open();


                //define o tipo do comando



                //cria um dataadapter
                MySqlDataAdapter da = new MySqlDataAdapter(cmd3);
                DataSet dataset = new DataSet();
                da.Fill(dataset);
                //cria um objeto datatable
                DataTable clientes = new DataTable();

                //preenche o datatable via dataadapter
                da.Fill(clientes);
                da.Fill(dataset, "Movim");

                this.GridView2.DataSource = dataset;

                GridView2.DataBind();

                GridView2.Enabled = true;

                 DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataRow dr1;

                ds.Tables.Add(dt);
                dt.Columns.Add("Id");
                dt.Columns.Add("Data");
                dt.Columns.Add("Ativo");
                dt.Columns.Add("Qtde");
                dt.Columns.Add("Compra");
                dt.Columns.Add("Atual");
                dt.Columns.Add("%", typeof(double));
                //  dt.Columns.Add(new DataColumn("Sel.", typeof(bool)));
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {


                    string data = Convert.ToString(GridView2.Rows[i].Cells[0].Text);
                    string cliente = Convert.ToString(GridView2.Rows[i].Cells[1].Text);
                    string ativo = Convert.ToString(GridView2.Rows[i].Cells[2].Text);
                    string precocompra = Convert.ToString(GridView2.Rows[i].Cells[3].Text);
                    string qtde = Convert.ToString(GridView2.Rows[i].Cells[5].Text);
                    string id = Convert.ToString(GridView2.Rows[i].Cells[6].Text);
                    string precoclose = "0";
                    precoclose = sumary(ativo);

                    if ((precoclose == "") || (precoclose.Length > 6))
                    {
                        precoclose = "0";
                    }




                 precocompra = precocompra.Replace(".", ",");
                         precoclose = precoclose.Replace(".", ",");
                double compra = double.Parse(precocompra);
                 double close = double.Parse(precoclose);


                    //  decimal compra = Convert.ToDecimal(precocompra);
                    //    decimal close = Convert.ToDecimal(precoclose);
                    double dif = close - compra;
                    double perc = (100 * dif) / compra;
                    //   int percentual = Convert.ToInt32(perc);
                    //    close = Math.Round(close, 2);
                    //      compra = Math.Round(compra, 2);
                    perc = Math.Round(perc, 2);
                    string percem = Convert.ToString(perc);
                 //   percem = percem.Replace(",", ".");
                    Label3.Text = precoclose;

                    dr1 = dt.NewRow();
                    string comprado = Convert.ToString(compra);
                    string closado = Convert.ToString(close);
                    dr1["id"] = id;
                    dr1["Data"] = data;
                    dr1["Ativo"] = ativo;
                    dr1["Qtde"] = qtde;
                    dr1["Compra"] = comprado;
                    dr1["Atual"] = closado;
                    dr1["%"] = perc;

                    //  dr["Sel."] = Sel;
                    dt.Rows.Add(dr1);

                    dt.DefaultView.Sort = "% desc";
                    dt = dt.DefaultView.ToTable();
                    gridmovim.DataSource = dt;

                    gridmovim.DataBind();

                    precoclose = "";

                    ListBox1.SelectedValue = idclientefinal;
                     
                }
             }
                rep_bind();
                try
                {
                    Button2.Visible = true;
                    File.Delete(Server.MapPath("~/venda.txt"));
                  
                    if (File.Exists(Server.MapPath("~/temp.pdf")));
                    {
                        HyperLink1.Visible = true;
                    } 
                    
               
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    Button2.Visible = true;
                    ListBox3.BackColor = Color.White;
                    
                    Button10.Enabled = false;
                    File.Delete(Server.MapPath("~/venda.txt"));

                }
                catch
                {

                }
            }

        }
        string idacao = "";
        string pdf = "";

        public void search()
        {
            respostaEnvioLabel.Visible = false;

            //cria a conexão com o banco de dados
            MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql = "select * from Ativos where fname like '" + TextBox1.Text + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            //abre a conexao
            conn.Open();


            //define o tipo do comando

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                string tipao = dr["fname"].ToString();
                idacao = dr["id_ativo"].ToString();
                TextBox1.Text = tipao;
                //       rep_bind();



                //    GridView1.Visible = true; 

                Label2.Text = "";
                txtvalor.Focus();
            }
            else
            {
                GridView1.Visible = false;
                respostaEnvioLabel.Visible = true;
                respostaEnvioLabel.Text = "ATIVO " + TextBox1.Text + " Não Encontrado!";
                respostaEnvioLabel.ForeColor = Color.Red;
            }  
             
        }
        public void preenchemovim()
        {

            int contagem = GridView2.Columns.Count;

            if (contagem <= 0)
            {


                string nome = ListBox1.SelectedItem.ToString();
                GridView2.DataSource = null;
                GridView2.DataBind();
                //define a instrução SQL

                GridView2.AutoGenerateColumns = false;

                //define e realiza a formatação de cada coluna
                BoundField coluna1 = new BoundField();
                coluna1.DataField = "data";
                coluna1.HeaderText = "Data";

                GridView2.Columns.Add(coluna1);
                BoundField coluna2 = new BoundField();
                coluna2.DataField = "nome";
                coluna2.HeaderText = "Nome";
                coluna2.ReadOnly = true;
                coluna2.HtmlEncode = false;

                GridView2.Columns.Add(coluna2);
                BoundField coluna3 = new BoundField();
                coluna3.DataField = "ativo";
                coluna3.HeaderText = "Ativo";
                coluna3.HtmlEncode = false;


                GridView2.Columns.Add(coluna3);
                BoundField coluna4 = new BoundField();
                coluna4.DataField = "valor";
                coluna4.HeaderText = "valor";
                coluna4.HtmlEncode = false;
                GridView2.Columns.Add(coluna4);

                BoundField coluna5 = new BoundField();
                coluna5.DataField = "tipo";
                coluna5.HeaderText = "tipo";
                coluna5.HtmlEncode = false;
                GridView2.Columns.Add(coluna5);

                BoundField coluna6 = new BoundField();
                coluna6.DataField = "qtde";
                coluna6.HeaderText = "qtde";
                coluna6.HtmlEncode = false;
                GridView2.Columns.Add(coluna6);

                BoundField coluna7 = new BoundField();
                coluna7.DataField = "id";
                coluna7.HeaderText = "id";
                coluna7.HtmlEncode = false;
                GridView2.Columns.Add(coluna7);

                string strSql2 = "SELECT * From Movim WHERE nome LIKE'" + nome + "'";
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
                da.Fill(dataset, "Movim");

                this.GridView2.DataSource = dataset;

                GridView2.DataBind();

                GridView2.Enabled = true;
            }
            else
            {
                string nome = ListBox1.SelectedItem.ToString();

                string strSql2 = "SELECT * From Movim WHERE nome LIKE'" + nome + "'";
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
                da.Fill(dataset, "Movim");

                this.GridView2.DataSource = dataset;

                GridView2.DataBind();

                GridView2.Enabled = true;
         
           

           }
        }

        public void prencherGrid(string nome)
        {
           
                //define a string de conexao com provedor caminho e nome do banco de dados
               
                //atribui o datatable ao datagridview para exibir o resultado
          
        }
        public void Clientemail(string nome)
        {
            string email = "";
            string strSql2 = "SELECT * FROM Clientes WHERE Nome_cliente Like'"+nome+"'";
            //cria a conexão com o banco de dados
            MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true; ");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
            //abre a conexao
            con.Open();
            MySqlDataReader dr = cmd1.ExecuteReader();

                while(dr.Read())

                {

                email =((string)dr["email"]);

                }

                dr.Close();

                 

        

        }
        private void rep_bind()
        {




            MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql = "select fname from Ativos where fname like '" + TextBox1.Text + "%'"; 

            
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            respostaEnvioLabel.Visible = false;
            
            //cria a conexão com o banco de dados
            MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql = "select * from Ativos where fname like '" + TextBox1.Text + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            //abre a conexao
            conn.Open();


            //define o tipo do comando

            MySqlDataReader dr = cmd.ExecuteReader();
            
            if (dr.HasRows)
            {
                dr.Read();
                string tipao = dr["fname"].ToString();
                idacao = dr["id_ativo"].ToString();
                TextBox1.Text = tipao;
          //       rep_bind();


                
            //    GridView1.Visible = true; 
            
                Label2.Text = "";
            }
            else
            {
                GridView1.Visible = false;
                respostaEnvioLabel.Visible = true;
                respostaEnvioLabel.Text = "ATIVO "+ TextBox1.Text + " Não Encontrado!";
                respostaEnvioLabel.ForeColor =Color.Red;
            }  
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (txtvalor.Text == "")
            {
                txtvalor.BackColor =  Color.LightGray;
              
                return;
            }
            else
            {
            }
            if (TextBox3.Text == "")
            {
                TextBox3.BackColor =  Color.LightGray;
                
                return;
            }
            else
            {
            }
            if (TextBox4.Text == "")
            {
                TextBox4.BackColor =  Color.LightGray;
                
                return;
            }
            string nome ="";
            try
            {
                  nome = ListBox1.SelectedItem.ToString();
            }
            catch
            {
            }
            string idcliente = Label4.Text;
            string data = "";
            if (TextBox4.Text != "")
            {
                data = TextBox4.Text;
            }
            else
            {
                data = Calendar1.SelectedDate.ToShortDateString();
            }
            if (nome == "")
            {
                string valor = txtvalor.Text;
                string tipo = "Compra";
                int qtde = Convert.ToInt32(TextBox3.Text);
                MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //definição do comando sql
                string sql = "select * from Clientes where Id_cliente like '" +idcliente  + "%'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();


                //define o tipo do comando

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    idcliente = dr["Id_cliente"].ToString();
                    nome = dr["Nome_cliente"].ToString();


                    //    GridView1.Visible = true; 


                }
                else
                {

                }
                MySqlConnection conn2 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //definição do comando sql
                string sql2 = "select Id_ativo from Ativos where fname like '" + TextBox1.Text + "%'";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn2);

                //abre a conexao
                conn2.Open();


                //define o tipo do comando

                MySqlDataReader dr2 = cmd2.ExecuteReader();

                if (dr2.HasRows)
                {
                    dr2.Read();
                    idacao = dr2["Id_ativo"].ToString();



                    //    GridView1.Visible = true; 


                }
                else
                {

                }


                MySqlConnection conn1 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //definição do comando sql
                string sql1 = "INSERT INTO Movim( nome, ativo, id_cliente, id_ativo, tipo, data, valor,qtde ) VALUES (@nome, @ativo, @id_cliente, @id_ativo, @tipo, @data, @valor, @qtde)";

                try
                {


                    MySqlCommand comando = new MySqlCommand(sql1, conn1);
                    //Adicionando o valor das textBox nos parametros do comando

                    comando.Parameters.Add(new MySqlParameter("@nome", nome));
                    comando.Parameters.Add(new MySqlParameter("@ativo", this.TextBox1.Text));
                    comando.Parameters.Add(new MySqlParameter("@id_cliente", idcliente));
                    comando.Parameters.Add(new MySqlParameter("@id_ativo", idacao));
                    comando.Parameters.Add(new MySqlParameter("@tipo", tipo));
                    comando.Parameters.Add(new MySqlParameter("@data", data));
                    comando.Parameters.Add(new MySqlParameter("@valor", valor));
                    comando.Parameters.Add(new MySqlParameter("@qtde", qtde));

                    //abre a conexao
                    conn1.Open();
                    //executa o comando com os parametros que foram adicionados acima
                    comando.ExecuteNonQuery();
                    //fecha a conexao
                    conn1.Close();
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
                    Response.Redirect("~/Movimentacoes.aspx?User=" + idcliente, false);
                    //   Response.Redirect(Request.Url.AbsoluteUri);
                }

            }
            else
            {
                string valor = txtvalor.Text;
                string tipo = "Compra";
                int qtde = Convert.ToInt32(TextBox3.Text);
                MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //definição do comando sql
                string sql = "select Id_cliente from Clientes where Nome_cliente like '" + nome + "%'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                //abre a conexao
                conn.Open();


                //define o tipo do comando

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    idcliente = dr["Id_cliente"].ToString();
                  


                    //    GridView1.Visible = true; 


                }
                else
                {

                }
                MySqlConnection conn2 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //definição do comando sql
                string sql2 = "select Id_ativo from Ativos where fname like '" + TextBox1.Text + "%'";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn2);

                //abre a conexao
                conn2.Open();


                //define o tipo do comando

                MySqlDataReader dr2 = cmd2.ExecuteReader();

                if (dr2.HasRows)
                {
                    dr2.Read();
                    idacao = dr2["Id_ativo"].ToString();



                    //    GridView1.Visible = true; 


                }
                else
                {

                }


                MySqlConnection conn1 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //definição do comando sql
                string sql1 = "INSERT INTO Movim( nome, ativo, id_cliente, id_ativo, tipo, data, valor,qtde ) VALUES (@nome, @ativo, @id_cliente, @id_ativo, @tipo, @data, @valor, @qtde)";

                try
                {


                    MySqlCommand comando = new MySqlCommand(sql1, conn1);
                    //Adicionando o valor das textBox nos parametros do comando

                    comando.Parameters.Add(new MySqlParameter("@nome", nome));
                    comando.Parameters.Add(new MySqlParameter("@ativo", this.TextBox1.Text));
                    comando.Parameters.Add(new MySqlParameter("@id_cliente", idcliente));
                    comando.Parameters.Add(new MySqlParameter("@id_ativo", idacao));
                    comando.Parameters.Add(new MySqlParameter("@tipo", tipo));
                    comando.Parameters.Add(new MySqlParameter("@data", data));
                    comando.Parameters.Add(new MySqlParameter("@valor", valor));
                    comando.Parameters.Add(new MySqlParameter("@qtde", qtde));

                    //abre a conexao
                    conn1.Open();
                    //executa o comando com os parametros que foram adicionados acima
                    comando.ExecuteNonQuery();
                    //fecha a conexao
                    conn1.Close();
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
                    Response.Redirect("~/Movimentacoes.aspx?User=" + idcliente, false);
                    //   Response.Redirect(Request.Url.AbsoluteUri);
                }

            }
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int contagem = GridView2.Columns.Count;

            if (contagem <= 0)
            {


                string nome = ListBox1.SelectedItem.ToString();
                GridView2.DataSource = null;
                GridView2.DataBind();
                //define a instrução SQL

                GridView2.AutoGenerateColumns = false;

                //define e realiza a formatação de cada coluna
                BoundField coluna1 = new BoundField();
                coluna1.DataField = "data";
                coluna1.HeaderText = "Data";

                GridView2.Columns.Add(coluna1);
                BoundField coluna2 = new BoundField();
                coluna2.DataField = "nome";
                coluna2.HeaderText = "Nome";
                coluna2.ReadOnly = true;
                coluna2.HtmlEncode = false;

                GridView2.Columns.Add(coluna2);
                BoundField coluna3 = new BoundField();
                coluna3.DataField = "ativo";
                coluna3.HeaderText = "Ativo";
                coluna3.HtmlEncode = false;


                GridView2.Columns.Add(coluna3);
                BoundField coluna4 = new BoundField();
                coluna4.DataField = "valor";
                coluna4.HeaderText = "valor";
                coluna4.HtmlEncode = false;
                GridView2.Columns.Add(coluna4);

                BoundField coluna5 = new BoundField();
                coluna5.DataField = "tipo";
                coluna5.HeaderText = "tipo";
                coluna5.HtmlEncode = false;
                GridView2.Columns.Add(coluna5);

                BoundField coluna6 = new BoundField();
                coluna6.DataField = "qtde";
                coluna6.HeaderText = "qtde";
                coluna6.HtmlEncode = false;
                GridView2.Columns.Add(coluna6);

                BoundField coluna7 = new BoundField();
                coluna7.DataField = "id";
                coluna7.HeaderText = "id";
                coluna7.HtmlEncode = false;
                GridView2.Columns.Add(coluna7);

                string strSql2 = "SELECT * From Movim WHERE nome LIKE'" + nome + "'";
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
                da.Fill(dataset, "Movim");

                this.GridView2.DataSource = dataset;

                GridView2.DataBind();

                GridView2.Enabled = true;
            }
            else
            {
                string nome = ListBox1.SelectedItem.ToString();

                string strSql2 = "SELECT * From Movim WHERE nome LIKE'" + nome + "'AND STATUS <>'F'";
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
                da.Fill(dataset, "Movim");

                this.GridView2.DataSource = dataset;

                GridView2.DataBind();

                GridView2.Enabled = true;

                respostaEnvioLabel.Visible = false;


            }
            run();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {

            int contagem = GridView2.Columns.Count;

            if (contagem <= 0)
            {


                string nome =  ListBox1.SelectedItem.ToString();
                GridView2.DataSource = null;
                GridView2.DataBind();
                //define a instrução SQL

                GridView2.AutoGenerateColumns = false;

                //define e realiza a formatação de cada coluna
                BoundField coluna1 = new BoundField();
                coluna1.DataField = "data";
                coluna1.HeaderText = "Data";

                GridView2.Columns.Add(coluna1);
                BoundField coluna2 = new BoundField();
                coluna2.DataField = "nome";
                coluna2.HeaderText = "Nome";
                coluna2.ReadOnly = true;
                coluna2.HtmlEncode = false;

                GridView2.Columns.Add(coluna2);
                BoundField coluna3 = new BoundField();
                coluna3.DataField = "ativo";
                coluna3.HeaderText = "Ativo";
                coluna3.HtmlEncode = false;


                GridView2.Columns.Add(coluna3);
                BoundField coluna4 = new BoundField();
                coluna4.DataField = "valor";
                coluna4.HeaderText = "valor";
                coluna4.HtmlEncode = false;
                GridView2.Columns.Add(coluna4);

                BoundField coluna5 = new BoundField();
                coluna5.DataField = "tipo";
                coluna5.HeaderText = "tipo";
                coluna5.HtmlEncode = false;
                GridView2.Columns.Add(coluna5);

                BoundField coluna6 = new BoundField();
                coluna6.DataField = "qtde";
                coluna6.HeaderText = "qtde";
                coluna6.HtmlEncode = false;
                GridView2.Columns.Add(coluna6);

                BoundField coluna7 = new BoundField();
                coluna7.DataField = "id";
                coluna7.HeaderText = "id";
                coluna7.HtmlEncode = false;
                GridView2.Columns.Add(coluna7);

                string strSql2 = "SELECT * From Movim WHERE nome LIKE'" + nome + "'";
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
                da.Fill(dataset, "Movim");

                this.GridView2.DataSource = dataset;

                GridView2.DataBind();

                GridView2.Enabled = true;
            }
            else
            {
                string nome = ListBox1.SelectedItem.ToString();

                string strSql2 = "SELECT * From Movim WHERE nome LIKE'" + nome + "'AND STATUS <>'F'";
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
                da.Fill(dataset, "Movim");

                this.GridView2.DataSource = dataset;

                GridView2.DataBind();

                GridView2.Enabled = true;

                respostaEnvioLabel.Visible = false;

                 
            }
            run();
        }

        protected void FileUpload1_Load(object sender, EventArgs e)
        {
           
        //    Label1.Visible = true;
        //    Label1.ForeColor = System.Drawing.Color.Black;
        //    Label1.Text = FileUpload1.FileContent.ToString();
        }
        private string sumary(string symbol)
        {

      
        

            // Use Path class to manipulate file and directory paths.
        

            // To copy a folder's contents to a new location:
            // Create a new target folder.
            // If the directory already exists, this method does not create a new directory.
       //     System.IO.Directory.CreateDirectory(targetPath);

            // To copy a file to another location and
            // overwrite the destination file if it already exists.
            


            
            string linha = "";
            string valor="" ;
            string simbolo = symbol;
            string[] linhaseparada = null;
    //      FileUpload1.FileContent.Equals(Server.MapPath("~/summary.csv")) ;
   
        //    StreamReader reader = new StreamReader(FileUpload1.FileContent, Encoding.Default, true);
           StreamReader reader = new StreamReader((Server.MapPath("~/summary.csv")), true);
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            while (true)
            {
                linha = reader.ReadLine();
                Label1.Text = linha;
                if (linha == null) break;
                linhaseparada = linha.Split(',');
                string resultado = string.Format(
                @"Linha - 
                    Campo 1: {0}
                    Campo 2: {1}
                    Campo 3: {2}
                    Campo 4: {3}
                    Campo 5: {4}", linhaseparada[0], linhaseparada[1], linhaseparada[2], linhaseparada[3], linhaseparada[4]);

                if (linhaseparada[2] == simbolo)
                {
                    valor = linhaseparada[4];

                    break;
                   
                }
                
               
            }
            
            return (valor);
           
            
        }
        public void run()
        {
            if (pdf == "1")
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataRow dr;

                ds.Tables.Add(dt);
                dt.Columns.Add("Id");
                dt.Columns.Add("Data da Compra");
                dt.Columns.Add("Tipo Ativo");
                dt.Columns.Add("Ativo");
                dt.Columns.Add("Descricao");
                dt.Columns.Add("Qtde");
                dt.Columns.Add("Valor na Compra");
                dt.Columns.Add("Valor Atual");
                dt.Columns.Add("%", typeof(double));
                //  dt.Columns.Add(new DataColumn("Sel.", typeof(bool)));
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {


                    string data = Convert.ToString(GridView2.Rows[i].Cells[0].Text);
                    string cliente = Convert.ToString(GridView2.Rows[i].Cells[1].Text);
                    string ativo = Convert.ToString(GridView2.Rows[i].Cells[2].Text);
                    string precocompra = Convert.ToString(GridView2.Rows[i].Cells[3].Text);
                    string qtde = Convert.ToString(GridView2.Rows[i].Cells[5].Text);
                    string id = Convert.ToString(GridView2.Rows[i].Cells[6].Text);
                    string precoclose = "0";
                    string descricao = "";
                    string tipo_ativo = "";
                    string nome_tipo = "";
                    string strSql = "SELECT * From Ativos WHERE fname LIKE'" + ativo + "'";



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
                        descricao = sdr["descricao"].ToString();
                        tipo_ativo = sdr["id_tipoativo"].ToString();
                    }

                    sdr.Close();

                    con.Close();

                    string strSql1 = "SELECT nome From Tipos_ativo WHERE id_tipoativo LIKE'" + tipo_ativo + "'";



                    //cria a conexão com o banco de dados
                    MySqlConnection con1 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
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


                    con1.Close();


                    precoclose = sumary(ativo);

                    if ((precoclose == "") || (precoclose.Length > 6))
                    {
                        precoclose = "0";
                    }




                   precocompra = precocompra.Replace(".", ",");
                    precoclose = precoclose.Replace(".", ",");
                   double compra = double.Parse(precocompra );
                    double close = double.Parse(precoclose);


                    //  decimal compra = Convert.ToDecimal(precocompra);
                    //    decimal close = Convert.ToDecimal(precoclose);
                    double dif = close - compra;
                    double perc = (100 * dif) / compra;
                    //   int percentual = Convert.ToInt32(perc);
                    //    close = Math.Round(close, 2);
                    //      compra = Math.Round(compra, 2);
                    perc = Math.Round(perc, 2);
                    string percem = Convert.ToString(perc);
                  //  percem = percem.Replace(",", ".");
                    Label3.Text = precoclose;

                    dr = dt.NewRow();
                    string comprado = Convert.ToString(compra);
                    string closado = Convert.ToString(close);
                    dr["id"] = id;
                    dr["Data da Compra"] = data;
                    dr["Tipo Ativo"] = nome_tipo;
                    dr["Ativo"] = ativo;
                    dr["Descricao"] = descricao;
                    dr["Qtde"] = qtde;
                    dr["Valor na Compra"] = comprado;
                    dr["Valor Atual"] = closado;
                    dr["%"] = perc;

                    //  dr["Sel."] = Sel;
                    dt.Rows.Add(dr);

                    dt.DefaultView.Sort = "% desc";
                    dt = dt.DefaultView.ToTable();
                    gridmovim.DataSource = dt;

                    gridmovim.DataBind();

                    precoclose = "";
                }
            }
            else
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataRow dr;

                ds.Tables.Add(dt);
                dt.Columns.Add("Id");
                dt.Columns.Add("Data");
                dt.Columns.Add("Ativo");
                dt.Columns.Add("Qtde");
                dt.Columns.Add("Compra");
                dt.Columns.Add("Atual");
                dt.Columns.Add("%", typeof(double));
                //  dt.Columns.Add(new DataColumn("Sel.", typeof(bool)));
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {


                    string data = Convert.ToString(GridView2.Rows[i].Cells[0].Text);
                    string cliente = Convert.ToString(GridView2.Rows[i].Cells[1].Text);
                    string ativo = Convert.ToString(GridView2.Rows[i].Cells[2].Text);
                    string precocompra = Convert.ToString(GridView2.Rows[i].Cells[3].Text);
                    string qtde = Convert.ToString(GridView2.Rows[i].Cells[5].Text);
                    string id = Convert.ToString(GridView2.Rows[i].Cells[6].Text);
                    string precoclose = "0";
                    precoclose = sumary(ativo);

                    if ((precoclose == "") || (precoclose.Length > 6))
                    {
                        precoclose = "0";
                    }




                      precocompra = precocompra.Replace(".", ",");
                       precoclose = precoclose.Replace(".", ",");
                    double compra = double.Parse(precocompra);
                   double close = double.Parse(precoclose);


                    //  decimal compra = Convert.ToDecimal(precocompra);
                    //    decimal close = Convert.ToDecimal(precoclose);
                    double dif = close - compra;
                    double perc = (100 * dif) / compra;
                    //   int percentual = Convert.ToInt32(perc);
                    //    close = Math.Round(close, 2);
                    //      compra = Math.Round(compra, 2);
                    perc = Math.Round(perc, 2);
                    string percem = Convert.ToString(perc);
                   // percem = percem.Replace(",", ".");
                    Label3.Text = precoclose;

                    dr = dt.NewRow();
                    string comprado = Convert.ToString(compra);
                    string closado = Convert.ToString(close);
                    dr["id"] = id;
                    dr["Data"] = data;
                    dr["Ativo"] = ativo;
                    dr["Qtde"] = qtde;
                    dr["Compra"] = comprado;
                    dr["Atual"] = closado;
                    dr["%"] = perc;

                    //  dr["Sel."] = Sel;
                    dt.Rows.Add(dr);

                    dt.DefaultView.Sort = "% desc";
                    dt = dt.DefaultView.ToTable();
                    gridmovim.DataSource = dt;

                    gridmovim.DataBind();

                    precoclose = "";
                }
            }
            Button10.Enabled = true;
            Response.Redirect("~/Movimentacoes.aspx?User=" + ListBox1.SelectedValue, false);
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            run();
            
            //  DataSet ds = new DataSet();
             //  DataTable dt = new DataTable();
             //  DataRow dr;

             //  ds.Tables.Add(dt);
             //  dt.Columns.Add("Id");
             //  dt.Columns.Add("Data da Compra");
             //  dt.Columns.Add("Ativo");
             //  dt.Columns.Add("Qtde");
             //  dt.Columns.Add("Valor na Compra");
             //  dt.Columns.Add("Valor Atual");            
             //  dt.Columns.Add("%", typeof(double));
             ////  dt.Columns.Add(new DataColumn("Sel.", typeof(bool)));
             //  for (int i = 0; i < GridView2.Rows.Count; i++)
             //  {


             //      string data = Convert.ToString(GridView2.Rows[i].Cells[0].Text);
             //      string cliente = Convert.ToString(GridView2.Rows[i].Cells[1].Text);
             //      string ativo = Convert.ToString(GridView2.Rows[i].Cells[2].Text);
             //      string precocompra = Convert.ToString(GridView2.Rows[i].Cells[3].Text);
             //      string qtde = Convert.ToString(GridView2.Rows[i].Cells[5].Text);
             //      string id = Convert.ToString(GridView2.Rows[i].Cells[6].Text);
             //      string precoclose = "0";
             //      precoclose = sumary(ativo);

             //      if ((precoclose == "") || (precoclose.Length > 6))
             //      {
             //          precoclose = "0";
             //      }




             //      //  precocompra = precocompra.Replace(".", ",");
             //      //     precoclose = precoclose.Replace(".", ",");
             //      double compra = double.Parse(precocompra.Replace(',', '.'));
             //      double close = double.Parse(precoclose.Replace(',', '.'));


             //      //  decimal compra = Convert.ToDecimal(precocompra);
             //      //    decimal close = Convert.ToDecimal(precoclose);
             //      double dif = close - compra;
             //      double perc = (100 * dif) / compra;
             //      //   int percentual = Convert.ToInt32(perc);
             //      //    close = Math.Round(close, 2);
             //      //      compra = Math.Round(compra, 2);
             //      perc = Math.Round(perc, 2);
             //      string percem = Convert.ToString(perc);
             //      percem = percem.Replace(",", ".");
             //      Label3.Text = precoclose;
                  
             //      dr = dt.NewRow();
             //      string comprado = Convert.ToString(compra);
             //      string closado = Convert.ToString(close);
             //      dr["id"] = id;
             //      dr["Data da Compra"] = data;
             //      dr["Ativo"] = ativo;
             //      dr["Qtde"] = qtde;
             //      dr["Valor Compra"] = comprado;
             //      dr["Valor Atual"] = closado;
             //      dr["%"] = perc;

             //      //  dr["Sel."] = Sel;
             //      dt.Rows.Add(dr);
                    
             //      dt.DefaultView.Sort = "% desc";
             //      dt = dt.DefaultView.ToTable();
             //      gridmovim.DataSource = dt;

             //      gridmovim.DataBind();

             //      precoclose = "";
              // }
          
        }

        protected void gridmovim_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridmovim.EditIndex = e.NewEditIndex;

         
        }

        public void Copiagrid()
        {

          

        }

        protected void gridmovim_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void gridmovim_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string idcliente = ListBox1.SelectedValue;
            
            GridViewRow row = (GridViewRow)gridmovim.Rows[e.RowIndex];

            string id1 = Convert.ToString(gridmovim.Rows[e.RowIndex].Cells[1].Text);
            //definição da string de conexão
            MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql = "DELETE FROM Movim WHERE id LIKE'" + id1 + "'";



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

            preenchemovim();


            gridmovim.DataBind();

            Response.Redirect("~/Movimentacoes.aspx?User=" + idcliente, false);
        }

        protected void gridmovim_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gridmovim_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                if (pdf == "")
                {

                    string valor = e.Row.Cells[7].Text.ToString();
                    double valores = double.Parse(valor);
                    string valor2 = e.Row.Cells[6].Text.ToString();
                    if (valores > 5.99)
                    {
                        e.Row.Cells[7].BackColor = System.Drawing.Color.Blue;
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
                    }
                    else if (valores > 0 & valores < 5.99)
                    {
                        e.Row.Cells[7].BackColor = System.Drawing.Color.Red;
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
                    }
                    else if (valores < 0 & valores > -100)
                    {
                        e.Row.Cells[7].BackColor = System.Drawing.Color.Black;
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
                    }
                }
                else
                {
                    gridmovim.AutoGenerateDeleteButton = false;
                    gridmovim.AutoGenerateSelectButton = false;
                }
               

             //   Label lblPassportExpDate = (Label)e.Row.Cells[5];
          //       string PassportExpDateDate = Convert.ToString(lblPassportExpDate.Text);
         //        if (PassportExpDateDate == "0")
       //         {
         //           //e.Row.BackColor = System.Drawing.Color.Red;
       //             gridmovim.Columns[3].ItemStyle.ForeColor = System.Drawing.Color.Red;
       //         }

           }
        }

        protected void gridmovim_SelectedIndexChanged(object sender, EventArgs e)
        {
            var grid = gridmovim as GridView;
            var i = gridmovim.SelectedIndex;
            //Não lembro, mas acho que a coluna do "Selecionar" conta então comecei com 1    
            string Caminho = grid.Rows[i].Cells[1].Text;
            string ponte ="-";
            string acao = grid.Rows[i].Cells[3].Text;
            string descricao = "";
            string strSql2 = "SELECT * FROM Ativos WHERE fname Like'" + acao + "'";
            //cria a conexão com o banco de dados
            MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
            //abre a conexao
            con.Open();
            MySqlDataReader dr = cmd1.ExecuteReader();
            try
            {
                while (dr.Read())
                {

                    descricao = ((string)dr["descricao"]);

                }
            }
            catch
            {
            }

            dr.Close();

            string result = Caminho + ponte + acao+ ponte + descricao;
            ListBox3.Items.Add(result);
            Button2.Visible = false;

           
        }

        void atualizavenda(string id)
        {
            MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql = "UPDATE Movim SET Status = @status  WHERE id LIKE'" + id + "'";

            try
            {


                MySqlCommand comando = new MySqlCommand(sql, conn);
                //Adicionando o valor das textBox nos parametros do comando

                comando.Parameters.Add(new MySqlParameter("@status", "A"));
             

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
                
            }
        }
        void Spausdinimas( )
        {
           
            using (StreamWriter writetext = new StreamWriter(Server.MapPath("~/venda2.txt"), true))
            {
                writetext.WriteLine("ATIVOS PARA VENDA IMEDIATA");
                writetext.WriteLine(" ");

                foreach (var item in ListBox3.Items)
                {
                    string acao = item.ToString();
                    string[] dadosDoCadastro = acao.Split('-');
                    //  Console.WriteLine(dadosDoCadastro[1]);
                    string split = dadosDoCadastro[1];
                    writetext.WriteLine(split);
                    string id = dadosDoCadastro[0];
                    atualizavenda(id);


                }
                             
                          
                writetext.Close();
                writetext.Dispose();
       
            }
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            string item = ListBox3.SelectedItem.ToString();
            ListBox3.Items.Remove(item);
            
        }
           

        protected void ListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = ListBox3.SelectedItem.ToString();

            item = new string(item.Where(char.IsLetter).ToArray());
          //  TextBox1.Text = item;

             
            ListBox3.BackColor = Color.White;
        }

        protected void Button8_Click(object sender, EventArgs e)
        {

            //System.IO.StreamWriter SaveFile = new StreamWriter(Server.MapPath("~/venda.txt"), false);
            //SaveFile.WriteLine("ATIVOS PARA VENDA IMEDIATA");
            //SaveFile.WriteLine(" ");
            //foreach (var item in ListBox3.Items)
            //{
            //  string acao = item.ToString();
            //string[] dadosDoCadastro = acao.Split('-');
            //  Console.WriteLine(dadosDoCadastro[1]);
            //string split = dadosDoCadastro[1];
            //SaveFile.WriteLine(split);
       
            //SaveFile.Close();
            Spausdinimas();
            string nome = ListBox1.SelectedItem.ToString();
            string email = "";
            string strSql2 = "SELECT * FROM Clientes WHERE Nome_cliente Like'" + nome + "'";
            //cria a conexão com o banco de dados
            MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
            //abre a conexao
            con.Open();
            MySqlDataReader dr = cmd1.ExecuteReader();

            while (dr.Read())
            {

                email = ((string)dr["email"]);

            }

            dr.Close();






            string remetenteEmail = "investscop@gmail.com"; //O e-mail do remetente

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            //   mail.To.Add(item.ToString());
            mail.To.Add(email);
            mail.CC.Add("renato.gandolfi@gmail.com");
            mail.From = new MailAddress(remetenteEmail, "INVESTSCORP", System.Text.Encoding.UTF8);

            mail.Subject = "ATIVOS PARA VENDA IMEDIATA";

            Attachment data = new Attachment(Server.MapPath("~/venda2.txt"));

            mail.Attachments.Add(data);

            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            //    mail.Body = mensagemTextBox.Text;

            mail.BodyEncoding = System.Text.Encoding.UTF8;

            mail.IsBodyHtml = true;

            mail.Priority = System.Net.Mail.MailPriority.High; //Prioridade do E-Mail



            SmtpClient client = new SmtpClient();  //Adicionando as credenciais do seu e-mail e senha:

            client.UseDefaultCredentials = false;

            client.Credentials = new System.Net.NetworkCredential(remetenteEmail, "lrgnyqqlzmffhyuw");


            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.Port = 587; // Esta porta é a utilizada pelo Gmail para envio

            client.Host = "smtp.gmail.com"; //Definindo o provedor que irá disparar o e-mail

            client.EnableSsl = true; //Gmail trabalha com Server Secured Layer

            try
            {

                client.Send(mail);
                respostaEnvioLabel.ForeColor = Color.Black;
                respostaEnvioLabel.Text = "Envio do E-mail com sucesso";

                respostaEnvioLabel.Visible = true;
                respostaEnvioLabel.Enabled = false;

                ListBox3.Items.Clear();
            }

            catch (Exception ex)
            {

                respostaEnvioLabel.Text = "Ocorreu um erro ao enviar:" + ex.Message;

                respostaEnvioLabel.Visible = true;

            }

            mail.Dispose();
            client.Dispose();

            // Response.Redirect("~/Default.aspx");
            FileInfo file = new FileInfo(Server.MapPath("~/venda2.txt"));


            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }

            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                Label3.Text = "falha";
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            File.Delete(Server.MapPath("~/venda2.txt"));
            Button2.Visible = true;//file is not locked
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            TextBox4.Text =  Calendar1.SelectedDate.ToShortDateString();
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            
            
            string utao = "";
            try
            {
                 utao = ListBox3.SelectedItem.ToString();
            }
            catch
            {
                ListBox3.BackColor = Color.LightGray;
                Button2.Visible = false;
                return;
            }
            if (txtvalor.Text == "")
            {
                txtvalor.BackColor =  Color.LightGray;
                Button2.Visible = false;
                return;
            }
            else
            {
            }
            if (TextBox3.Text == "")
            {
                TextBox3.BackColor =  Color.LightGray;
                Button2.Visible = false;
                return;
            }
            else
            {
            }
            if (TextBox4.Text == "")
            {
                TextBox4.BackColor =  Color.LightGray;
                Button2.Visible = false;
                return;
            }
            else
            {
            }
          

                string acao = utao.ToString();
                string[] dadosDoCadastro = acao.Split('-');
                //  Console.WriteLine(dadosDoCadastro[1]);
                string split = dadosDoCadastro[1];               
                string id = dadosDoCadastro[0];
                string data_compra = "";
                string qtde_compra = "";
                string valor_compra = "";
                string data_venda = TextBox4.Text;
                string qtde_venda = TextBox3.Text;
                string valor_venda = txtvalor.Text;
                string id_cliente = "";
                string nome = ListBox1.SelectedItem.ToString();
               
                string strSql2 = "SELECT * From Movim WHERE id LIKE'" + id + "'";
                //cria a conexão com o banco de dados
                MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //cria o objeto command para executar a instruçao sql
               MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
                //abre a conexao
                con.Open();


                //define o tipo do comando


                MySqlDataReader dr = cmd1.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    data_compra = dr["data"].ToString();
                    qtde_compra = dr["QTDE"].ToString();
                    valor_compra = dr["valor"].ToString();
                    id_cliente = dr["id_cliente"].ToString();
                    //    GridView1.Visible = true; 


                }
               
                int comprao = Convert.ToInt32(qtde_compra);
                int vendao = Convert.ToInt32(qtde_venda);
               
                double investfinal = Convert.ToDouble(qtde_venda);
                double compra1 = Convert.ToDouble(valor_compra);
                double venda1 = Convert.ToDouble(valor_venda);
                
              
               

                double percentcli = (venda1 - compra1) / compra1 * 100;
                double invest = ((venda1 - compra1) * vendao * 0.2);


                string invest_input = string.Format("{0:f2}", percentcli);
                string rounded_input = string.Format("{0:f2}", invest);

                if (vendao > comprao)
                {
                    respostaEnvioLabel.ForeColor = Color.Red;
                    respostaEnvioLabel.Visible = true;
                    respostaEnvioLabel.Text = "Quantidade de venda não permitida!";
                    Button2.Visible = false;
                    return;
                }
                MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //definição do comando sql
                string sql = "INSERT INTO Contrato(id_cliente, nome_cliente, acao, data_compra, qtde_compra, valor_compra, data_venda, qtde_venda, valor_venda, status, perccli, percinvest ) VALUES (@id_cliente, @nome_cliente, @acao, @data_compra, @qtde_compra, @valor_compra, @data_venda, @qtde_venda, @valor_venda, @status, @cli, @inv)";

                try
                {

                     
                    MySqlCommand comando = new MySqlCommand(sql, conn);
                    //Adicionando o valor das textBox nos parametros do comando

                    comando.Parameters.Add(new MySqlParameter("@id_cliente", id_cliente));
                    comando.Parameters.Add(new MySqlParameter("@nome_cliente", nome));
                    comando.Parameters.Add(new MySqlParameter("@acao", acao));
                    comando.Parameters.Add(new MySqlParameter("@data_compra", data_compra));
                    comando.Parameters.Add(new MySqlParameter("@qtde_compra", qtde_compra));
                    comando.Parameters.Add(new MySqlParameter("@valor_compra", valor_compra));
                    comando.Parameters.Add(new MySqlParameter("@data_venda", data_venda));
                    comando.Parameters.Add(new MySqlParameter("@qtde_venda", qtde_venda));
                    comando.Parameters.Add(new MySqlParameter("@valor_venda", valor_venda));
                    comando.Parameters.Add(new MySqlParameter("@status", "F"));
                    comando.Parameters.Add(new MySqlParameter("@cli", invest_input));
                    comando.Parameters.Add(new MySqlParameter("@inv", rounded_input));
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
                   
                }

                string status_final = "";
                int compra = Convert.ToInt32(qtde_compra);
                int venda = Convert.ToInt32(qtde_venda);
                if (venda > compra)
                {
                    respostaEnvioLabel.Text = "Qtde venda não permitida";
                    return;
                }
                int qtde_Resta = (Convert.ToInt32(qtde_compra) - Convert.ToInt32(qtde_venda));
               
                if (qtde_Resta <= 0)
                {
                    status_final = "F";
                    MySqlConnection conn2 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                    //definição do comando sql
                    string sql2 = "DELETE From Movim  WHERE Id LIKE'" + id + "'";

                    try
                    {


                        MySqlCommand comando = new MySqlCommand(sql2, conn2);
                        //Adicionando o valor das textBox nos parametros do comando

                        //abre a conexao
                        conn2.Open();
                        //executa o comando com os parametros que foram adicionados acima
                        comando.ExecuteNonQuery();
                        //fecha a conexao
                        conn2.Close();
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

                    }

                    Button2.Visible = true;
                    Response.Redirect("~/Movimentacoes.aspx?User=" + id_cliente, false);
                //    Response.Redirect(Request.Url.AbsoluteUri);
                }
                else
                {
                    status_final = "";
                    string sql2 = "UPDATE Movim SET Status = @status, qtde= @qtde  WHERE Id LIKE'" + id + "'";

                    try
                    {

                        MySqlConnection conn2 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                        MySqlCommand comando = new MySqlCommand(sql2, conn2);
                        //Adicionando o valor das textBox nos parametros do comando

                        comando.Parameters.Add(new MySqlParameter("@status", status_final));
                        comando.Parameters.Add(new MySqlParameter("@qtde", qtde_Resta.ToString()));


                        //abre a conexao
                        conn2.Open();
                        //executa o comando com os parametros que foram adicionados acima
                        comando.ExecuteNonQuery();
                        //fecha a conexao
                        conn2.Close();
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
                        Button2.Visible = true;
                    }

                    Response.Redirect("~/Movimentacoes.aspx?User=" + id_cliente, false);
                   // Response.Redirect(Request.Url.AbsoluteUri);

                             
               
            }
                             
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {
            TextBox4.BackColor = Color.White;
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox3.BackColor = Color.White;
                respostaEnvioLabel.Text = "";
                respostaEnvioLabel.Visible = false;
                respostaEnvioLabel.ForeColor = Color.Black;
            }
            catch
            {

            }
        }

        protected void txtvalor_TextChanged(object sender, EventArgs e)
        {
           txtvalor.BackColor = Color.White;
        }

        protected void ListBox3_DataBound(object sender, EventArgs e)
        {
            ListBox3.BackColor = Color.White;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            TextBox3.Text = "";
            txtvalor.Text = "";
            ListBox3.SelectedIndex = -1;
        }

        protected void Button9_Click(object sender, EventArgs e)
        {

            //Variavel do Nome e caminho do arquivo

         



         
           
         

            //  copiargrid();

            //     string nome = ListBox1.SelectedItem.ToString();
            //     Document doc = new Document(PageSize.A4);//criando e

            //     doc.SetMargins(40, 40, 40, 80);//estibulando o

            //     doc.AddCreationDate();//adicionando as configuracoes

            //     //caminho onde sera criado o pdf + nome desejado
            //     //OBS: o nome sempre deve ser terminado com .pdf
            //     string caminho = (Server.MapPath("~/CONTRATO.PDF"));

            //     //criando o arquivo pdf embranco, passando como parametro

            //     //doc criada acima e a variavel caminho
            //     //tambem criada acima.
            //     PdfWriter writer = PdfWriter.GetInstance(doc, new
            //     FileStream(caminho, FileMode.Create));

            //     doc.Open();

            //     //criando uma string vazia
            //     string dados = "";
            //     iTextSharp.text.Font novo = new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 14);
            //     //criando a variavel para paragrafo
            //     Paragraph paragrafo = new Paragraph(dados,novo);
            //     //etipulando o alinhamneto
            //     paragrafo.Alignment = Element.ALIGN_JUSTIFIED;
            //     //Alinhamento Justificado
            //     //adicioando texto
            //     paragrafo.Add("CLIENTE : " + nome);            
            //     //acidionado paragrafo ao documento
            //     doc.Add(paragrafo);
            //     //fechando documento para que seja salva as

            //     doc.Close();
            //    Response.AddHeader("content-disposition", "inline; filename=" + (Server.MapPath("~/CONTRATO.PDF")));
            // //   Response.AddHeader("Contrato", (Server.MapPath("~/CONTRATO.PDF")));
            ////      Response.Redirect((Server.MapPath("~/CONTRATO.PDF")));
            //    HyperLink1.Visible = true;
            //     Button9.Visible = false;

            //     System.Diagnostics.Process.Start((Server.MapPath("~/CONTRATO.PDF")));

            Create();


        }
        protected void Button10_Click(object sender, EventArgs e)
        {
            Server.MapPath("Arquivos/sumary.csv");
            try
            {
                System.IO.File.Copy((Server.MapPath("FTP/sumary.csv")), (Server.MapPath("Arquivos/sumary1.csv")), true);
               
             //   System.IO.File.Copy((Server.MapPath("~/sumary.csv")), (Server.MapPath("~/sumary1.csv")), true);
            }
            catch (IOException ex)
            {
                Label3.Text = ex.ToString();
                return;
            }
        }
       

        public void copiargrid()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr;

            ds.Tables.Add(dt);
          
            dt.Columns.Add("Data");
            dt.Columns.Add("Ativo");
            dt.Columns.Add("Qtde");
            dt.Columns.Add("Compra");
            dt.Columns.Add("Atual");
            dt.Columns.Add("%", typeof(double));
            
            for (int i = 0; i < gridmovim.Rows.Count; i++)
            {


                string data = Convert.ToString(gridmovim.Rows[i].Cells[2].Text);             
                string ativo = Convert.ToString(gridmovim.Rows[i].Cells[3].Text);
                string precocompra = Convert.ToString(gridmovim.Rows[i].Cells[5].Text);
                string qtde = Convert.ToString(gridmovim.Rows[i].Cells[4].Text);
               string precoclose = Convert.ToString(gridmovim.Rows[i].Cells[6].Text);
              string percent = Convert.ToString(GridView2.Rows[i].Cells[7].Text); 

                  precocompra = precocompra.Replace(".", ",");
                  precoclose = precoclose.Replace(".", ",");
              //  double compra = double.Parse(precocompra.Replace(',', '.'));
            //    double close = double.Parse(precoclose.Replace(',', '.'));


                //  decimal compra = Convert.ToDecimal(precocompra);
                //    decimal close = Convert.ToDecimal(precoclose);

                dr = dt.NewRow();               
                dr["Data"] = data;
                dr["Ativo"] = ativo;
                dr["Qtde"] = qtde;
                dr["Compra"] = precocompra;
                dr["Atual"] = precoclose;
                dr["%"] = percent;

                //  dr["Sel."] = Sel;
                dt.Rows.Add(dr);

                dt.DefaultView.Sort = "% desc";
                dt = dt.DefaultView.ToTable();
                GridView3.DataSource = dt;

                GridView3.DataBind();

                precoclose = "";
            }
          
        }
        protected void gridExportToPdf_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           GridView3.PageIndex = e.NewPageIndex;
            copiargrid();
        }
        // create the following Method to Export the Grid view to PDF as
        //Below is the code to Export GridView to PDF file with Paging enabled.
        private void ExportGridToPDF()
        {
            FtpWebRequest request =
            (FtpWebRequest)WebRequest.Create("ftp://ftp.investscorp.com.br/web/FTP/temp.pdf");
            request.Credentials = new NetworkCredential("investscorp1", "tank76retJRS!!");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UsePassive = true;
            //dados binarios
            request.UseBinary = true;
            //setar o KeepAlive para false
            request.KeepAlive = false;
         
            
            string hoje = DateTime.Now.ToShortDateString() ;
            string hour = DateTime.Now.ToShortTimeString();
            string linha =  ListBox1.SelectedItem.ToString()+ "- "+ hoje + "-" + hour;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename='"+ ListBox1.SelectedItem.ToString() + "-" + hoje +"-"+hour+ "'.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
      //      HtmlTextWriter hw = new HtmlTextWriter(sw);
           gridmovim.AllowPaging = false;
          
           // gvdetails.DataBind();
           gridmovim.RowStyle.VerticalAlign = VerticalAlign.Middle;
           gridmovim.HorizontalAlign = HorizontalAlign.Center;
     //      gridmovim.RenderControl(hw);
           gridmovim.HeaderRow.Style.Add("width", "15%");
           gridmovim.HeaderRow.Style.Add("font-size", "10px");
           gridmovim.Style.Add("text-decoration", "none");
           gridmovim.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
           gridmovim.Style.Add("font-size", "8px");
           gridmovim.Style.Add("text-align", "center");
           
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A2, 7f, 5f, 9f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

           Stream ftpStream = request.GetRequestStream();


           PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ftpStream);
           pdfDoc.Open();
        
        //    PdfWriter.GetInstance(pdfDoc, new FileStream((Server.MapPath("~/FTP/temp1.pdf")), FileMode.Create));
         iTextSharp.text.pdf.draw.VerticalPositionMark seperator = new iTextSharp.text.pdf.draw.LineSeparator();

         
        //    pdfDoc.Open();
            pdfDoc.Add(new Paragraph(linha));
            pdfDoc.Add(new Paragraph(" "));
            pdfDoc.Add(new Paragraph(" "));

            pdfDoc.Add(seperator);
            htmlparser.Parse(sr);
            pdfDoc.Close();
          
            Response.Write(pdfDoc);
            
          
            pdf = "";
            
            Response.Redirect(Request.RawUrl);

      
         //   Response.Redirect(Request.RawUrl); 
           
        }
        protected void Button10_Click1(object sender, EventArgs e)
        {
         
            
            pdf = "1";
            gridmovim.DataSource = null;
            gridmovim.DataBind();

            run();
         
            ExportGridToPDF();   
           
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string pdfPath = Server.MapPath("~/FTP/temp1.pdf");
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(pdfPath);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", buffer.Length.ToString());
            Response.BinaryWrite(buffer);
        }

        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
           // Response.Redirect("~/Movimentacoes.aspx?User=" + ListBox1.SelectedValue, false);
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        public void Create()
        {
            string strSql3 = "SELECT * From Contrato WHERE id_cliente LIKE'" + Label4.Text + "'";
            //cria a conexão com o banco de dados
            MySqlConnection con3 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true; ");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd3 = new MySqlCommand(strSql3, con3);
            //abre a conexao
            con3.Open();
            string vArq = "";

            // Abre janela para usuário escolher a pasta onde o arquivo será gerado

            FtpWebRequest request =
           (FtpWebRequest)WebRequest.Create("ftp://ftp.investscorp.com.br/web/FTP/temp.pdf");
            request.Credentials = new NetworkCredential("investscorp1", "tank76retJRS!!");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UsePassive = true;
            //dados binarios
            request.UseBinary = true;
            //setar o KeepAlive para false
            request.KeepAlive = false;


            // Insere na variavel o caminho selecionado pelo usuário e concatena com o nome do arquivo

            vArq = Server.MapPath("teeee" + ".pdf");



            // Cria um objeto PDF

            Report vPdf = new Report(new PdfFormatter());


            // Define a fonte que sera usada no relatório PDF

            FontDef vDef = new FontDef(vPdf, FontDef.StandardFont.TimesRoman);

            FontProp vDrop = new FontProp(vDef, 8);


            // Cria uma Nova Pagina

            Page vPage = new Page(vPdf);
           

           
            fontDef_Helvetica = new FontDef(vPdf, FontDef.StandardFont.Helvetica);
            FontProp fontProp_Text = new FontPropMM(fontDef_Helvetica, 1.9);  // standard font
            FontProp fontProp_Header = new FontPropMM(fontDef_Helvetica, 1.9);  // font of the table header
            fontProp_Header.bBold = true;

      //      Stream stream_Phone = GetType().Assembly.GetManifestResourceStream("ReportSamples.Phone.jpg");
            Random random = new Random(6);

            // create table
            TableLayoutManager tlm;
            using (tlm = new TableLayoutManager(fontProp_Header))
            {
              tlm.rContainerHeightMM = rPosBottom - rPosTop;  // set height of table
              tlm.tlmCellDef_Header.rAlignV = RepObj.rAlignCenter;  // set vertical alignment of all header cells
              tlm.tlmCellDef_Default.penProp_LineBottom = new PenProp(vPdf, 0.05, Color.LightGray);  // set bottom line for all cells
              tlm.tlmHeightMode = TlmHeightMode.AdjustLast;
                

                // define columns
                TlmColumn col;
                col = new TlmColumnMM(tlm, "data_venda", 30);

                col = new TlmColumnMM(tlm, "acao", 40);
                col.tlmCellDef_Default.tlmTextMode = TlmTextMode.MultiLine;

                col = new TlmColumnMM(tlm, "valor_compra", 36);

                col = new TlmColumnMM(tlm, "qtde_venda", 22);

                col = new TlmColumnMM(tlm, "preco_venda", 36);

              
 

                // open data set
                MySqlDataAdapter da = new MySqlDataAdapter(cmd3);
                DataSet dataset = new DataSet();
                da.Fill(dataset);
                //cria um objeto datatable
                DataTable clientes = new DataTable();

                //preenche o datatable via dataadapter
                da.Fill(clientes);
             //   da.Fill(dataset, "Contrato");

               
             
               DataTable dataTable_Customers = dataset.Tables[0];
               
                foreach (DataRow dr in dataTable_Customers.Rows)
                {

                    tlm.NewRow();
                    tlm.Add(0, new RepString(fontProp_Text, (String)dr["data_venda"]));
                    tlm.Add(1, new RepString(fontProp_Text, (String)dr["acao"]));
                    tlm.Add(2, new RepString(fontProp_Text, (String)dr["valor_compra"]));
                    tlm.Add(3, new RepString(fontProp_Text, (String)dr["qtde_venda"]));
                    tlm.Add(4, new RepString(fontProp_Text, (String)dr["valor_venda"]));
                   
     
                   
                }
            }
          
          vPage.AddCT_MM(rPosLeft + tlm.rWidthMM / 2, rPosTop + tlm.rCurY_MM + 2, new RepString(fontProp_Text, "- end of table -"));

            // print page number and current date/time
            Double rY = rPosBottom + 1.5;
            vPdf.Save(vArq);
        }
       
       
    }
}