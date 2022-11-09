using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Net.Mail;
using MySql.Data.MySqlClient;


namespace teste
{
    public partial class Manutencao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Visible = false;
            Label lbl = this.Master.FindControl("lblMasterPage") as Label;
            string user = lbl.Text;
      //      if (user == "")
      //      {
       //         Response.Redirect("Loginuser.aspx", false);
      //      }
            if (!IsPostBack)
            {
                preenchetipo();
                preencheempresa();
            }
        }
        string tipo = "";
        string mercado = "";
        public void preenchetipo()
        {
            string nome = droptipo.SelectedValue.ToString();

            MySqlConnection conn1 =  new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql1 = "SELECT * From Tipos_ativo ";

            MySqlCommand command = new MySqlCommand(sql1, conn1);

            conn1.Open();

            MySqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                tipo = sdr["nome"].ToString();
                lsttipos.Items.Add(tipo);
                
            }

            conn1.Close();
        }
        public void preencheempresa()
        {
            string nome = droptipo.SelectedValue.ToString();

            MySqlConnection conn1 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql1 = "SELECT * From Empresa ";

            MySqlCommand command = new MySqlCommand(sql1, conn1);

            conn1.Open();

            MySqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                txtnomeempr.Text= sdr["NOME"].ToString();
                txtcnpj.Text = sdr["CPF"].ToString();
                txtcep.Text = sdr["CEP"].ToString();
                txtrua.Text = sdr["RUA"].ToString();
                txtcidade.Text = sdr["CIDADE"].ToString();
                txtuf.Text = sdr["ESTADO"].ToString();
                txtnr.Text = sdr["NR"].ToString();
                txttel.Text = sdr["TEL"].ToString();

            }

            conn1.Close();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            string nome = droptipo.SelectedValue.ToString();
            MySqlConnection conn1 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
           string sql1 = "SELECT id_tipoativo From Tipos_ativo WHERE nome LIKE'" + nome + "'";

            MySqlCommand command = new MySqlCommand(sql1, conn1);

           conn1.Open();

            MySqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                tipo = sdr["id_tipoativo"].ToString();
            }

             conn1.Close();

            StreamReader rd = new StreamReader(FileUpload1.FileContent, Encoding.Default, true);

                string linha = null;
                string[] linhaseparada = null;
                List<Object> linhas = new List<Object>();

                // ler o conteudo da linha e add na list 
                while ((linha = rd.ReadLine()) != null)
                { 
                    linhaseparada = linha.Split(',');                
                    foreach(var item in linhaseparada)
                    {

                        MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                        //definição do comando sql
                        string sql = "INSERT INTO Ativos( fname, id_tipoativo ) VALUES (   @fname, @id)";

                        try
                        {


                            MySqlCommand comando = new MySqlCommand(sql, conn);
                            //Adicionando o valor das textBox nos parametros do comando

                            comando.Parameters.Add(new MySqlParameter("@fname", item));
                            comando.Parameters.Add(new MySqlParameter("@id", Convert.ToInt32(tipo)));
                             

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
                           
                           Label1.Text = "CSV importado com sucesso";
                           Label1.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
        }

        protected void FileUpload1_Load(object sender, EventArgs e)
        {
            Label1.Visible = true;
            Label1.ForeColor = System.Drawing.Color.Black;
            Label1.Text = FileUpload1.FileContent.ToString();
        }

        protected void droptipo_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        protected void droptipo_TextChanged(object sender, EventArgs e)
        {
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(Server.MapPath("~/data.txt"));
                File.Delete(Server.MapPath("~/venda.txt"));
                File.Delete(Server.MapPath("~/compra.txt"));
                File.Delete(Server.MapPath("~/vendas2.txt"));
            }
            catch
            {

            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report3.rdlc");
             
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string valor_compra = "";
            string valor_venda = "";
            string perccli = "";
            string percinvest = "";
            string id_contrato = "";
            string qtde_compra = "";
            string qtde_venda = "";
            MySqlConnection conn2 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql2 = "SELECT * FROM Contrato";



            MySqlCommand comando2 = new MySqlCommand(sql2, conn2);
            //Adicionando o valor das textBox nos parametros do comando



            //abre a conexao
            conn2.Open();
            //executa o comando com os parametros que foram adicionados acima
            comando2.ExecuteNonQuery();
            //fecha a conexao

            //Minha função para limpar os textBox

            //Abaixo temos a ultlização de javascript para apresentar ao usuário um alert
            // referente ao msgbox

            MySqlDataReader dr2 = comando2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    
                    valor_compra = dr2["valor_compra"].ToString();
                    valor_venda = dr2["valor_venda"].ToString();
                    id_contrato = dr2["id_contrato"].ToString();
                    qtde_compra = dr2["qtde_compra"].ToString();
                    qtde_venda = dr2["qtde_venda"].ToString();
                    int qtde_v = Convert.ToInt32(qtde_venda);
                    double investfinal = Convert.ToDouble(qtde_venda);
                    double compra =Convert.ToDouble(valor_compra);
                    double venda = Convert.ToDouble(valor_venda);
                    double percentcli = (venda - compra) / compra * 100;
                    double invest = ((venda - compra) * qtde_v * 0.2)/100;

                    string invest_input = string.Format("{0:f2}", percentcli);
                    string rounded_input = string.Format("{0:f2}", invest);
                 //   double dif = venda - compra;
                //    double percc = (dif * 100) / compra;
               //     double percin= investfinal*((20 * dif) / 100);

                    

                    percentcli = Math.Round(percentcli, 2);
         //           invest = Math.Round(invest, 2);
                    string sql3 = "UPDATE Contrato SET  perccli = @cli, percinvest= @inv  WHERE id_contrato LIKE'" + id_contrato + "'";

                    try
                    {

                        MySqlConnection conn3 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                        MySqlCommand comando1 = new MySqlCommand(sql3, conn3);
                        //Adicionando o valor das textBox nos parametros do comando

                        comando1.Parameters.Add(new MySqlParameter("@cli", invest_input.ToString()));
                        comando1.Parameters.Add(new MySqlParameter("@inv", rounded_input.ToString()));
                      
                        //abre a conexao
                        conn3.Open();
                        //executa o comando com os parametros que foram adicionados acima
                        comando1.ExecuteNonQuery();
                        //fecha a conexao
                        conn3.Close();
                        //Minha função para limpar os textBox

                        //Abaixo temos a ultlização de javascript para apresentar ao usuário um alert
                        // referente ao msgbox

                    }
                    catch
                    {

                    }
                    finally
                    {


                    }







                }
            }

            conn2.Close();

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void usuarios_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string mail = ListBox1.SelectedItem.ToString();
                string login = "";
                string strSql2 = "SELECT * From Usuario WHERE email LIKE'" + mail + "'";
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
                    while (dr.Read())
                    {


                        tipo = dr["tipo"].ToString();

                        login = dr["fun_login"].ToString();
                    }
                }
                dr.Close();
                txtuser.Text = login;

                DropDownList1.SelectedValue = tipo;
            }
            catch
            {

            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
             string user = txtuser.Text;
             MySqlConnection conn1 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //definição do comando sql
                string sql1 = "UPDATE Usuario SET fun_login=@login, tipo=@tipo  WHERE fun_login LIKE'" +  user + "'";

                try
                {


                    MySqlCommand comando1 = new MySqlCommand(sql1, conn1);
                    //Adicionando o valor das textBox nos parametros do comando

                    comando1.Parameters.Add(new MySqlParameter("@login", txtuser.Text));
                    comando1.Parameters.Add(new MySqlParameter("@tipo", DropDownList1.SelectedValue.ToString()));

                    //abre a conexao
                    conn1.Open();
                    //executa o comando com os parametros que foram adicionados acima
                    comando1.ExecuteNonQuery();
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
                    conn1.Close();
                }

        }

        protected void btnredefinir_Click(object sender, EventArgs e)
        {
            string chars = "abcdefghjkmnpqrstuvwxyz023456789";
            string pass = "";
            Random random = new Random();
            for (int f = 0; f < 6; f++)
            {
                pass = pass + chars.Substring(random.Next(0, chars.Length - 1), 1);
            }


            string sql2 = "UPDATE Usuario SET  fun_senha=@pass  WHERE fun_login LIKE'" + txtuser.Text + "'";

            try
            {

                MySqlConnection conn2 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                MySqlCommand comando = new MySqlCommand(sql2, conn2);
                //Adicionando o valor das textBox nos parametros do comando

                comando.Parameters.Add(new MySqlParameter("@pass", pass));

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


            }




            string remetenteEmail = "investscop@gmail.com"; //O e-mail do remetente

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            mail.To.Add(ListBox1.SelectedItem.ToString());

            mail.From = new MailAddress(remetenteEmail, "INVESTCORP", System.Text.Encoding.UTF8);

            mail.Subject = "RECUPERACAO DE SENHA - INVESTCOP";





            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            mail.Body = "Sua nova senha é :´" + pass + "'";

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

                Label1.Text = "Envio do E-mail com sucesso";

                Label1.Visible = true;
                Label1.Enabled = false;

            }

            catch (Exception ex)
            {

                Label1.Text = "Ocorreu um erro ao enviar:" + ex.Message;

                Label1.Visible = true;

            }
            mail.Dispose();
                 
        }

        protected void TextBox1_TextChanged1(object sender, EventArgs e)
        {
            

            //cria a conexão com o banco de dados
            MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql'
            string sql = "select * from Clientes where email like '" + TextBox1.Text +"%' ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            //abre a conexao
            conn.Open();


            //define o tipo do comando

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                string tipao = dr["email"].ToString(); 
                TextBox1.Text = tipao;
                //       rep_bind();



                //    GridView1.Visible = true; 

                
            }
            else
            {
                TextBox1.Text = "";
                lblaviso.Visible = true;
                lblaviso.Text = "CLIENTE NAO ENCONTRADO OU JA HABILITADO";
            }  
        }

        protected void Button6_Click(object sender, EventArgs e)
        {

            string sql2 = "UPDATE clientes SET  usuario=@usuario  WHERE email LIKE'" + TextBox1.Text + "'";

            try
            {

                MySqlConnection conn2 =   new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                MySqlCommand comando = new MySqlCommand(sql2, conn2);
                //Adicionando o valor das textBox nos parametros do comando

                comando.Parameters.Add(new MySqlParameter("@usuario", "H"));

                conn2.Open();
                //executa o comando com os parametros que foram adicionados acima
                comando.ExecuteNonQuery();
                //fecha a conexao
                conn2.Close();
                //Minha função para limpar os textBox

                //Abaixo temos a ultlização de javascript para apresentar ao usuário um alert
                // referente ao msgbox
                lblaviso.Visible = true;


            }
            catch
            {

            }
            finally
            {
                MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                string sql = "INSERT INTO Usuario (email, tipo ) VALUES (@email, @tipo)";

                MySqlCommand comando = new MySqlCommand(sql, conn);
                //Adicionando o valor das textBox nos parametros do comando

                comando.Parameters.Add(new MySqlParameter("@email", TextBox1.Text));
                comando.Parameters.Add(new MySqlParameter("@tipo", "C"));


                //abre a conexao
                conn.Open();
                //executa o comando com os parametros que foram adicionados acima
                comando.ExecuteNonQuery();
                //fecha a conexao
                conn.Close();

            }

        }

        protected void lsttipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nome = lsttipos.SelectedItem.ToString();
            string qua = "";
            string co = "";
            MySqlConnection conn1 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql1 = "SELECT * From Tipos_ativo WHERE Nome LIKE '"+nome + "'";

            MySqlCommand command = new MySqlCommand(sql1, conn1);

            conn1.Open();

            MySqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                tipo = sdr["mercado"].ToString();
                qua = sdr["QTDEFRA"].ToString();
                co = sdr["QTDECO"].ToString();
            }
            if (tipo == "F")
            {
                drpmercado.SelectedIndex= 1 ;
            }
            else if (tipo == "C")
            {
                drpmercado.SelectedIndex = 2 ;
            }
            else
            {
                drpmercado.SelectedIndex = 0;
            }
            txtfra.Text = qua;
            txtcotas.Text = co;
            conn1.Close();
        }

        protected void btnatual_Click(object sender, EventArgs e)
        {

             string nome = lsttipos.SelectedItem.ToString();
             MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql = "UPDATE Tipos_ativo SET mercado=@mercado,QTDEFRA = @fra, QTDECO =@Co WHERE nome LIKE'" + nome + "'";

            try
            {


                MySqlCommand comando = new MySqlCommand(sql, conn);
                //Adicionando o valor das textBox nos parametros do comando

                comando.Parameters.Add(new MySqlParameter("@mercado", drpmercado.SelectedValue.ToString()));
                comando.Parameters.Add(new MySqlParameter("@fra", txtfra.Text.ToString()));
                comando.Parameters.Add(new MySqlParameter("@Co", txtcotas.Text.ToString()));
                

                //abre a conexao
                conn.Open();
                //executa o comando com os parametros que foram adicionados acima
                comando.ExecuteNonQuery();
                //fecha a conexao
                conn.Close();
                //Minha função para limpar os textBox

                //Abaixo temos a ultlização de javascript para apresentar ao usuário um alert
                // referente ao msgbox
                drpmercado.SelectedIndex = 0;
            }
            catch
            {

            }
        }

        protected void btnsalvaemp_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            string sql = "UPDATE Empresa SET NOME=@NOME, CPF=@CPF, RUA=@RUA, NR=@NR, CEP=@CEP, ESTADO=@ESTADO, CIDADE=@CIDADE, TEL=@TEL  WHERE idempresa = 1";

            try
            {
              

                MySqlCommand comando = new MySqlCommand(sql, conn);
                //Adicionando o valor das textBox nos parametros do comando

                comando.Parameters.Add(new MySqlParameter("@NOME", txtnomeempr.Text.ToString()));
                comando.Parameters.Add(new MySqlParameter("@CPF", txtcnpj.Text.ToString()));
                comando.Parameters.Add(new MySqlParameter("@TEL", txttel.Text.ToString()));
                comando.Parameters.Add(new MySqlParameter("@CEP", txtcep.Text.ToString()));
                comando.Parameters.Add(new MySqlParameter("@NR", txtnr.Text.ToString()));
                comando.Parameters.Add(new MySqlParameter("@RUA", txtrua.Text.ToString()));
                comando.Parameters.Add(new MySqlParameter("@CIDADE", txtcidade.Text.ToString()));
                comando.Parameters.Add(new MySqlParameter("@ESTADO", txtuf.Text.ToString()));



                //abre a conexao
                conn.Open();
                //executa o comando com os parametros que foram adicionados acima
                comando.ExecuteNonQuery();
                //fecha a conexao
                conn.Close();
                //Minha função para limpar os text
            }
            catch
            {
            }
            preencheempresa();
        }
    }
}