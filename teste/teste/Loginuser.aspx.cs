using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Mail;
using System.Net.Mail;
using MySql.Data.MySqlClient;


namespace teste
{
    public partial class Loginuser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MySqlConnection con2 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true;");

        

            con2.Open();
                    
        }
        string email = "";
        protected void ENTRAR_Click(object sender, EventArgs e)
        {
            try
            {

                string user = txtuser.Text;
                string pass = txtpass.Text;
                string id = "";
                string real = "";
                string tipo = "";
                string login = "";
                if (user.Contains("@"))
                {
                     string strSql2 = "SELECT * From Usuario WHERE email LIKE'" + user + "'";
                    //cria a conexão com o banco de dados


                     MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
                     //    SqlConnection con = new SqlConnection("workstation id=INVESTSCOP.mssql.somee.com;packet size=4096;user id=sergiobarrosgoku_SQLLogin_1;pwd=xacv6lswwa;data source=investscorp.mysql.dbaas.com.br;persist security info=False;initial catalog=INVESTSCOP");
                    //cria o objeto command para executar a instruçao sql
               
             //       SqlCommand cmd1 = new SqlCommand(strSql2, con);
                    //abre a conexao
                    MySqlCommand cmd1 = new MySqlCommand("strSql2, con");
                  
                    
                    con.Open();


                    //define o tipo do comando

                   
                     MySqlDataReader dr = cmd1.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            real = dr["fun_senha"].ToString();
                            tipo = dr["tipo"].ToString();
                            id = dr["fun_codigo"].ToString();
                            login = dr["fun_login"].ToString();
                        }
                    }
                    dr.Close();
                    email = txtuser.Text;
                    lblusuario.Text = txtuser.Text;
                    con.Close();
                    if (tipo == "")
                    {
                        Label1.Visible = true;
                        Label1.Text = " USUARIO NÂO HABILITADO! Contate o Administrador";
                        txtpass.Text = "";
                        txtuser.Text = "";
                        return;
                    }
                    else if (login == "")
                    {
                        firstacess();
                        return;
                    }

                    if (pass == real)
                    {
                        if (tipo == "A")
                        {
                            Response.Redirect("~/Default.aspx?User=" + user, false);
                        }
                        else if (tipo == "C")
                        {
                            Response.Redirect("AreaCliente.aspx?User=" + user, false);
                        }
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "SENHA INCORRETA! Tente novamente";
                        txtpass.Text = "";
                        txtuser.Text = "";
                    }
                }
                else
                {
                   
                    
                    
                    
                    
                    string strSql2 = "SELECT * From Usuario WHERE fun_login LIKE'" + user + "'";
                    //cria a conexão com o banco de dados
            //        using (var connection = new MySqlConnection(“Server=SEU_BANCO;User Id=SEU_USUARIO;Database=SUA_BASE_DADOS;Pwd=SUA_SENHA;includesecurityasserts=true;”))



                    MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
                    MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
                    //abre a conexao
                    con.Open();


                    //define o tipo do comando


                    MySqlDataReader dr = cmd1.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            real = dr["fun_senha"].ToString();
                            tipo = dr["tipo"].ToString();
                            id = dr["fun_codigo"].ToString();
                        }
                    }
                    dr.Close();

                    con.Close();

                    if (pass == real)
                    {
                    
                        if (tipo == "A")
                        {
                            Response.Redirect("~/Default.aspx?User=" + user, false);
                        }
                        else if (tipo == "C")
                        {
                            Response.Redirect("AreaCliente.aspx?User=" + user, false);
                        
                            }
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "SENHA INCORRETA! Tente novamente";
                        txtpass.Text = "";
                        txtuser.Text = "";
                    }
                }
            }
            catch
            {
               
                
                Label1.Visible = true;
                Label1.Text = "FERROU";
                txtpass.Text = "";
                txtuser.Text = "";
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            txtemail.Visible = true;
            lblenvio.Visible = true;
            btnenvio.Visible = true;

        }

        public void firstacess()
        {
            Lblnovouser.Visible = true;
            txtnovouser.Visible = true;
            lblpass.Visible = false;
            lbluser.Visible = false;
            txtuser.Visible = false;
            txtpass.Visible = false;
            ENTRAR.Visible = false;
            LinkButton1.Visible = false;
        }
        protected void btnenvio_Click(object sender, EventArgs e)
        {
            try
            {

                string id = "";
                string strSql2 = "SELECT * From Usuario WHERE email LIKE'" + txtemail.Text + "'";
                //cria a conexão com o banco de dados
                MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
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

                       
                        id = dr["fun_codigo"].ToString();
                    }
                }
                dr.Close();

                con.Close();
                if (id == "")
                {
                    Label1.Visible = true;
                    Label1.Text = "EMAIL NÂO ENCONTRADO! Por favor verifique";
                    return;
                }
                string chars = "abcdefghjkmnpqrstuvwxyz023456789";
                string pass = "";
                Random random = new Random();
                for (int f = 0; f < 6; f++)
                {
                    pass = pass + chars.Substring(random.Next(0, chars.Length - 1), 1);
                }


                 string sql2 = "UPDATE Usuario SET  fun_senha=@pass  WHERE fun_codigo LIKE'" + id + "'";

                try
                {

                    MySqlConnection conn2 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
                    MySqlCommand comando = new MySqlCommand(sql2, conn2);
                    //Adicionando o valor das textBox nos parametros do comando

                    comando.Parameters.Add(new MySqlParameter("@pass",  pass));
                   
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




                string remetenteEmail = "investscop@gmail.comm"; //O e-mail do remetente

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                mail.To.Add(txtemail.Text);

                mail.From = new MailAddress(remetenteEmail, "INVESTCORP", System.Text.Encoding.UTF8);

                mail.Subject = "RECUPERACAO DE SENHA - INVESTCOP";
               
                  
                    
               

                mail.SubjectEncoding = System.Text.Encoding.UTF8;

                mail.Body = "Sua nova senha é :´"+ pass +"'";

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



         
            catch
            {
                Label1.Visible = true;
                Label1.Text = "EMAIL NÂO ENCONTRADO! Por favor verifique";
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

            string user = txtnovouser.Text;
            string strSql2 = "SELECT * From Usuario WHERE fun_login LIKE'" + user + "'";
            //cria a conexão com o banco de dados
            MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
            //cria o objeto command para executar a instruçao sql
            MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
            //abre a conexao
            con.Open();


            //define o tipo do comando


            MySqlDataReader dr = cmd1.ExecuteReader();

            if (dr.HasRows)
            {
                Label1.Visible = true;
                Label1.Text = "USUARIO NÂO DISPONÍVEL! POR FAVOR ESCOLHA OUTRO USUÁRIO";

                firstacess();
            }
           

            else
            {
                lblnovasenha.Visible = true;
                txtnovasenha.Visible = true;
                txtnovasenhac.Visible = true;
                lblnovasenhac.Visible = true;
                btnregistro.Visible = true;
                lblpass.Visible = false;
                txtpass.Visible = false;
                txtuser.Visible = false;
                lbluser.Visible = false;
                ENTRAR.Visible = false;
                LinkButton1.Visible = false;
                Label1.Text = user;
                string email =lblusuario.Text;

                MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
                //definição do comando sql
                string sql = "UPDATE Usuario SET fun_login=@login  WHERE email LIKE'" + email + "'";

                try
                {


                    MySqlCommand comando = new MySqlCommand(sql, conn);
                    //Adicionando o valor das textBox nos parametros do comando

                    comando.Parameters.Add(new MySqlParameter("@login",txtnovouser.Text ));


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
            dr.Close();

            con.Close();
        }

        protected void txtnovasenhac_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtnovasenha_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnregistro_Click(object sender, EventArgs e)
        {
            if (txtnovasenha.Text == txtnovasenhac.Text)
            {
                string email = lblusuario.Text;
                MySqlConnection conn1 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
                //definição do comando sql
                string sql1 = "UPDATE Usuario SET fun_senha=@senha  WHERE email LIKE'" + email + "'";

                try
                {


                    MySqlCommand comando1 = new MySqlCommand(sql1, conn1);
                    //Adicionando o valor das textBox nos parametros do comando

                    comando1.Parameters.Add(new MySqlParameter("@senha",txtnovasenha.Text ));


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

                    string user = "";
                    string tipo = "";
                    string strSql2 = "SELECT * From Usuario WHERE email LIKE'" + lblusuario.Text + "'";
                    //cria a conexão com o banco de dados
                    MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
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
                            
                            user = dr["fun_login"].ToString();
                        }
                    }
                    dr.Close();


                    if (tipo == "A")
                    {
                        Response.Redirect("~/Default.aspx?User=" + user, false);
                    }
                    else if (tipo == "C")
                    {
                        Response.Redirect("AreaCliente.aspx?User=" + user, false);
                    }

                }
           
          

            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "A SENHA NÃO COINCIDE! POR FAVOR VERIFIQUE!";
                txtnovasenha.Text = "";
                txtnovasenhac.Text = "";
                lblnovasenha.Visible = true;
                txtnovasenha.Visible = true;
                txtnovasenhac.Visible = true;
                lblnovasenhac.Visible = true;
                btnregistro.Visible = true;
                lblpass.Visible = false;
                txtpass.Visible = false;
                txtuser.Visible = false;
                lbluser.Visible = false;
                ENTRAR.Visible = false;
                LinkButton1.Visible = false;
                string email = txtuser.Text;
                string nova = email;
            }
        }
    }
}