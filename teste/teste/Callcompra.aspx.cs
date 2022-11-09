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
using System.Collections;
using MySql.Data.MySqlClient;


namespace teste
{
    public partial class Callcompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Label lbl = this.Master.FindControl("lblMasterPage") as Label;
         //   string user = lbl.Text;
         //   if (user == "")
        //    {
        //        Response.Redirect("Loginuser.aspx", false);
        //    }
            
            if (!IsPostBack)
            {
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                // Validate();          

                prencherGrid();
                preenchelist();
                preenchetxt();
            }
        }
        string tipo = "";
        public void preenchetxt()
        {
            if (tipo == "CM")
            {

                string texto = "ATIVOS PARA COMPRA" + Environment.NewLine + Environment.NewLine + "Bom Dia!" +  Environment.NewLine +  Environment.NewLine + "Segue prévia da manhã, onde a INVESTSCORP recomenda os seguintes CALLS como válidos para hoje:"

                    + Environment.NewLine + Environment.NewLine;
                string textoacao = ""; ;

                foreach (var item1 in list.Items)
                {

                    string atual = textoacao + item1.ToString()  + Environment.NewLine;
                    textoacao = atual;
                    ;

                }
                ;
                string pe =  Environment.NewLine + "NOTA1: os preços considerados para nossos controles quando emitido CALL de VENDA sempre serão os de fechamento do dia - PORÉM melhores rentabilidades podem ser obtidas quando em resposta ao contato direto comprando ou vendendo, por favor enviem as boletas PARA CONTROLE" + Environment.NewLine;

                txtarq.Text = texto + textoacao + pe;
            }
            else if (tipo == "CT")
            {

                string texto = "ATIVOS PARA COMPRA" + "\n" + "\n" + "Boa Tarde!" + "\n" + "\n" + "Segue prévia da tarde, onde a INVESTSCORP recomenda os seguintes CALLS como válidos para hoje:"

                    + Environment.NewLine + Environment.NewLine;
                string textoacao = ""; ;

                foreach (var item1 in list.Items)
                {

                    string atual = textoacao + item1.ToString() + Environment.NewLine;
                    textoacao = atual;
                    ;

                }
                ;
                string pe = Environment.NewLine + "NOTA1: os preços considerados para nossos controles quando emitido CALL de VENDA sempre serão os de fechamento do dia - PORÉM melhores rentabilidades podem ser obtidas quando em resposta ao contato direto comprando ou vendendo, por favor enviem as boletas PARA CONTROLE" + "\n";

                txtarq.Text = texto + textoacao + pe;
            }
        }
       public void preenchelist()
        {
            string strSql2 = "SELECT * FROM CLL WHERE Tipo LIKE 'C%'";
            //cria a conexão com o banco de dados
            MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
            //abre a conexao
            con.Open();
            MySqlDataReader dr = cmd1.ExecuteReader();

            while (dr.Read())
            {


                list.Items.Add((string)dr["ATIVO"]);
                tipo = ((string)dr["Tipo"]);
            }
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


                string remetenteEmail = "investscop@gmail.com"; //O e-mail do remetente

                MailMessage mail = new MailMessage();

                mail.To.Add(item.ToString());

                mail.From = new MailAddress(remetenteEmail, "INVESTSCORP", System.Text.Encoding.UTF8);

                mail.Subject = "Assunto:CALLS de compra";

                string conteudoTexto = txtarq.Text.Replace(System.Environment.NewLine, "<br />");

                  mail.Body = conteudoTexto;

                mail.SubjectEncoding = System.Text.Encoding.UTF8;

                //    mail.Body = mensagemTextBox.Text;

                mail.BodyEncoding = System.Text.Encoding.UTF8;

                mail.IsBodyHtml = true;

                mail.Priority = MailPriority.High; //Prioridade do E-Mail



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

                    respostaEnvioLabel.Text = "Envio do E-mail com sucesso";

                    respostaEnvioLabel.Visible = true;
                    respostaEnvioLabel.Enabled = false;
                   
                }

                catch (Exception ex)
                {

                    respostaEnvioLabel.Text = "Ocorreu um erro ao enviar:" + ex.Message;

                    respostaEnvioLabel.Visible = true;

                }
                mail.Dispose();
                Response.Redirect("Ativos.aspx", true);
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
            MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true; ");
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

        protected void gridenvio_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string valor = e.Row.Cells[2].Text.ToString();
            }
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
           
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(Server.MapPath("~/data.txt"));
                File.Delete(Server.MapPath("~/compra.txt"));
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
        
    }
}