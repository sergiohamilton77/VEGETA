using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net.Configuration;
using System.IO;
using MySql.Data.MySqlClient;


namespace teste
{
    public partial class Callvenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            
            Label lbl = this.Master.FindControl("lblMasterPage") as Label;
            string user = lbl.Text;
          //  if (user == "")
          //  {
          //      Response.Redirect("Loginuser.aspx", false);
        //    }
            
            if (!IsPostBack)
            {
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                // Validate();          

                prencherGrid();
                checarmovim();

            }
        }
        public void criartxt()
        {
            
                DateTime date1 = DateTime.UtcNow;

                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

                DateTime date2 = TimeZoneInfo.ConvertTime(date1, tz);
           
            if(date2.Hour<12)
            {
                string texto = "ATIVOS PARA VENDA" + Environment.NewLine + Environment.NewLine + "Bom Dia!" + Environment.NewLine + Environment.NewLine + "Segue prévia da manhã, onde a INVESTSCORP recomenda os seguintes CALLS como válidos para hoje:"

                         + Environment.NewLine + Environment.NewLine;
                string textoacao = ""; ;

                foreach (var item1 in lstvenda.Items)
                {

                    string atual = textoacao + item1.ToString() + Environment.NewLine;
                    textoacao = atual;
                    ;

                }
                string pe = Environment.NewLine + "NOTA : Caso não haja o envio da boleta da(s) venda(s) indicada(s), será gerado automaticamente um contrato de venda com o valor de fechamento de cada ativo do CALL de Venda. Conforme previsto em Contrato de Consultoria de dados com a INVESTSCORP." + "\n";
                txtarq.Text = texto + textoacao + pe;  
                ;
            }
            else if(date2.Hour<17)
            {
                string texto = "ATIVOS PARA VENDA" + Environment.NewLine + Environment.NewLine + "Boa Tarde!" + Environment.NewLine + Environment.NewLine + "Segue prévia da tarde, onde a INVESTSCORP recomenda os seguintes CALLS como válidos para hoje:"

                           + Environment.NewLine + Environment.NewLine;
                string textoacao = ""; ;

                foreach (var item1 in lstvenda.Items)
                {

                    string atual = textoacao + item1.ToString() + Environment.NewLine;
                    textoacao = atual;
                    ;

                }
                string pe = Environment.NewLine + "NOTA : Caso não haja o envio da boleta da(s) venda(s) indicada(s), será gerado automaticamente um contrato de venda com o valor de fechamento de cada ativo do CALL de Venda. Conforme previsto em Contrato de Consultoria de dados com a INVESTSCORP." + "\n";
                txtarq.Text = texto + textoacao + pe;  
                ;
            }
            else
            {
                string texto = "ATIVOS PARA VENDA" + Environment.NewLine + Environment.NewLine + "Boa Noite!" + Environment.NewLine + Environment.NewLine + "Segue prévia da noite, onde a INVESTSCORP recomenda os seguintes CALLS como válidos para hoje:"

                            + Environment.NewLine + Environment.NewLine;
                string textoacao = ""; ;

                foreach (var item1 in lstvenda.Items)
                {

                    string atual = textoacao + item1.ToString() + Environment.NewLine;
                    textoacao = atual;
                    ;

                }
                string pe = Environment.NewLine + "NOTA : Caso não haja o envio da boleta da(s) venda(s) indicada(s), será gerado automaticamente um contrato de venda com o valor de fechamento de cada ativo do CALL de Venda. Conforme previsto em Contrato de Consultoria de dados com a INVESTSCORP." + "\n";
                txtarq.Text = texto + textoacao + pe;  
                ;
            }

           
               
        }
       public void checarmovim()
       {
           string ATIVO = "";
           string strSql2 = "SELECT * FROM CLL";
           //cria a conexão com o banco de dados
           MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
           //cria o objeto command para executar a instruçao sql
           MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
           //abre a conexao
           con.Open();
           MySqlDataReader dr = cmd1.ExecuteReader();
         
         
           while (dr.Read())
           {

               ATIVO = ((string)dr["ATIVO"]);


               string[] dadosDoCadastro1 = ATIVO.Split('-');
               //  Console.WriteLine(dadosDoCadastro[1]);
               string split1 = dadosDoCadastro1[1];
               string id1 = dadosDoCadastro1[0];
               string final= split1.Trim();
               string nome = "";
               string id_cliente = "";
               string strSql3 = "SELECT * FROM Movim WHERE ativo Like '"+ final + "'";
               //cria a conexão com o banco de dados
               MySqlConnection con3 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
               //cria o objeto command para executar a instruçao sql
               MySqlCommand cmd3 = new MySqlCommand(strSql3, con3);
               //abre a conexao
               con3.Open();
               MySqlDataReader dr1 = cmd3.ExecuteReader();

               while (dr1.Read())
               {

                   nome = ((string)dr1["nome"]);
                 
                   if (listenvio.Items.FindByText(nome) != null)
                   {
                        
                   }
                   else
                   {
                       listenvio.Items.Add(nome);
                       
                   }
               }

               dr1.Close();

               con3.Close(); 

           }

           dr.Close();
           con.Close();

                 
       }
        public void prencherGrid()
        {
            //define a string de conexao com provedor caminho e nome do banco de dados
            gridenvio.DataSource = null;
            gridenvio.DataBind(); ;
            //define a instrução SQL

           gridenvio.AutoGenerateColumns = false;
           int contagem = gridenvio.Columns.Count;

            if (contagem <= 0)
            {
                //define e realiza a formatação de cada coluna
                BoundField coluna1 = new BoundField();
                coluna1.DataField = "Id_Cliente";
                coluna1.HeaderText = "Código";
                coluna1.Visible = false;
                gridenvio.Columns.Add(coluna1);
                BoundField coluna2 = new BoundField();
                coluna2.DataField = "Nome_cliente";
                coluna2.HeaderText = "Cliente";
                coluna2.ReadOnly = true;
                coluna2.HtmlEncode = false;
                gridenvio.Columns.Add(coluna2);
                BoundField coluna3 = new BoundField();
                coluna3.DataField = "email";
                coluna3.HeaderText = "E-mail";
                coluna3.ReadOnly = true;
                coluna3.HtmlEncode = false;              
                gridenvio.Columns.Add(coluna3);
              

                

            }
            string strSql2 = "SELECT * FROM Clientes";
            //cria a conexão com o banco de dados
            MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true;");
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
            da.Fill(dataset, "Clientes");

            this.gridenvio.DataSource = dataset;

            gridenvio.DataBind();

            gridenvio.Enabled = true;
            //atribui o datatable ao datagridview para exibir o resultado
        }

        protected void gridenvio_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridenvio.EditIndex = e.NewEditIndex;
            prencherGrid();
        }

        protected void gridenvio_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string foi = "";
            
            GridViewRow row = gridenvio.Rows[e.RowIndex];
           
        
            CheckBox Enviar = (CheckBox)(row.Cells[4].Controls[0]);

            if (row.RowType == DataControlRowType.DataRow)
            {
                // Display the company name in italics.
                foi = row.Cells[3].Text;

            }
            
            if (Enviar.Checked == true)
            {
                listenvio.Items.Add(foi);
            }
        }

        protected void gridenvio_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridenvio.EditIndex = -1;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            foreach (var item in listenvio.Items)
            {
                // same the item to the database
                string email = "";
                string strSql2 = "SELECT * FROM Clientes WHERE Nome_cliente Like'" + item + "'";
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

                MailMessage mail = new MailMessage();

                mail.To.Add(email.ToString());

                mail.From = new MailAddress(remetenteEmail, "INVESTSCORP", System.Text.Encoding.UTF8);

                mail.Subject = "Assunto:CALLS de Venda";

                string conteudoTexto = txtarq.Text.Replace(System.Environment.NewLine, "<br />");

                mail.Body = conteudoTexto;

                mail.SubjectEncoding = System.Text.Encoding.UTF8;

                //    mail.Body = mensagemTextBox.Text;


                mail.IsBodyHtml = true;

                mail.Priority = MailPriority.High; //Prioridade do E-Mail



                SmtpClient client = new SmtpClient();  //Adicionando as credenciais do seu e-mail e senha:

                client.UseDefaultCredentials = false;

                client.Credentials = new System.Net.NetworkCredential(remetenteEmail, "lrgnyqqlzmffhyuw");

               // AIzaSyCl1n179m9InUA5pcdyCaPf-3IqL4GIqXs
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Port = 587; // Esta porta é a utilizada pelo Gmail para envio

                client.Host = "smtp.gmail.com"; //Definindo o provedor que irá disparar o e-mail
               
                client.EnableSsl = true; //Gmail trabalha com Server Secured Layer

                try
                {

                    client.Send(mail);

                    respostaEnvioLabel.Text = "Envio do E-mail com sucesso";

                    respostaEnvioLabel.Visible = true;
                    respostaEnvioLabel.Enabled = false;
                   
                }

                catch (Exception ex)
                {

                    respostaEnvioLabel.Text = "Ocorreu um erro ao enviar:" + ex.Message;

                    respostaEnvioLabel.Visible = true;
                    mail.Dispose();
                    Response.Redirect("Ativos.aspx", true);
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ativos.aspx", true);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            listenvio.Items.Clear();

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string strSql2 = "SELECT * FROM Clientes";
            //cria a conexão com o banco de dados
            MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
            //abre a conexao
            con.Open();
            MySqlDataAdapter adpt = new MySqlDataAdapter(strSql2, con);

            DataSet myDataSet = new DataSet();

            adpt.Fill(myDataSet, "Clientes");

            DataTable myDataTable = myDataSet.Tables[0];

            DataRow tempRow = null;



            foreach (DataRow tempRow_Variable in myDataTable.Rows)
            {

                tempRow = tempRow_Variable;

             //   listenvio.Items.Add((tempRow["id"] + " (" + tempRow["name"] + ")" + " (" + tempRow["Country"] + ")" + " (" + tempRow["City"] + ")"));
                listenvio.Items.Add(tempRow["email"].ToString());
            }   
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (this.listenvio.SelectedItem != null)
                this.listenvio.Items.Remove(this.listenvio.SelectedItem);
        }

        protected void gridenvio_SelectedIndexChanged(object sender, EventArgs e)
        {

            int column = gridenvio.Columns.Count;
            GridViewRow row = gridenvio.SelectedRow;

            // Display the first name from the selected row.
            // In this example, the third column (index 2) contains
            // the first name.
            string valor = row.Cells[3].Text.ToString();
            listenvio.Items.Add(valor);
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            txtarq.Text = System.IO.File.ReadAllText(Server.MapPath("~/data.txt"));
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(Server.MapPath("~/data.txt"));
                File.Delete(Server.MapPath("~/venda.txt"));
            }
            catch
            {

            }


            System.IO.StreamWriter SaveFile = new StreamWriter(Server.MapPath("~/data.txt"), true);

            SaveFile.Write(txtarq.Text);

            //Posiciono o ponteiro na próxima linha do arquivo.
            SaveFile.Write("\r\n");

            SaveFile.Close();
            txtarq.Text = "";

            txtarq.Text = System.IO.File.ReadAllText(Server.MapPath("~/data.txt"));
            respostaEnvioLabel.Text = "TXT ATUALIZADO";
            respostaEnvioLabel.Visible = true;
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
            string valor = "";
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
                  //  percem = percem.Replace(",", ".");
                   string  nomao = listenvio.SelectedItem.ToString();
                   if (cliente == nomao)
                   {
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
                   else
                   {
                       string nooo ="!";
                   }
                }
        }

        protected void listenvio_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
            gridmovim.DataSource = null;
            gridmovim.DataBind();

            lstvenda.Items.Clear();
            


            List<string> list = new List<string>();
            try
            {  
                string nome = listenvio.SelectedItem.ToString();
                string ATIVO = "";
                string strSql2 = "SELECT * FROM CLL WHERE Tipo LIKE 'VM' OR 'VT'";
                //cria a conexão com o banco de dados
                MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //cria o objeto command para executar a instruçao sql
                MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
                //abre a conexao
                con.Open();
                MySqlDataReader dr = cmd1.ExecuteReader();

                while (dr.Read())
                {
                    ATIVO = ((string)dr["ATIVO"]);


                    string[] dadosDoCadastro1 = ATIVO.Split('-');
                    //  Console.WriteLine(dadosDoCadastro[1]);
                    string split1 = dadosDoCadastro1[1];
                    string id1 = dadosDoCadastro1[0];
                    string final = split1.Trim();
                 
                  list.Add(final );
                    
                }



                string combindedString = string.Join("' OR ativo LIKE '", list.ToArray());
                
                
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

                    string strSql3 = "SELECT * FROM Movim WHERE ativo LIKE'" + combindedString+"'";
                    //cria a conexão com o banco de dados
                    MySqlConnection con3 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                    //cria o objeto command para executar a instruçao sql
                    MySqlCommand cmd3 = new MySqlCommand(strSql3, con3);
                    //abre a conexao
                    con3.Open();

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
                    //define a instrução SQL

                    GridView2.AutoGenerateColumns = false;

                    con3.Close();
               
            }
            catch
            {
            }
            run();
            criartxt();
            if (lstvenda.Items.Count > 0)
            {
                
            }
            else
            {
                txtarq.Text = "SEM ATIVOS NO MOMENTO";
            }
        }

        protected void gridmovim_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string ativo = e.Row.Cells[2].Text.ToString();
                string valor = e.Row.Cells[6].Text.ToString();
                double valores = double.Parse(valor);
                string valor2 = e.Row.Cells[5].Text.ToString();
                if (valores > 5.99)
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Blue;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.White;

                     

                    if (lstvenda.Items.FindByText(ativo) != null)
                    {

                    }
                    else
                    {
                        lstvenda.Items.Add(ativo);

                    }

                  
                }
                else if (valores > 0 & valores < 5.99)
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
                    if (lstvenda.Items.FindByText(ativo) != null)
                    {

                    }
                    else
                    {
                        lstvenda.Items.Add(ativo);

                    }
                }
                else if (valores < 0 & valores > -100)
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Black;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
                }
            }
        }

        protected void respostaEnvioLabel_TextChanged(object sender, EventArgs e)
        {

        }
        
    }
}