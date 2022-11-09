using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

namespace teste
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label lbl = this.Master.FindControl("lblMasterPage") as Label;
            string user = lbl.Text;
           // if (user == "")
         //   {
        //        Response.Redirect("Loginuser.aspx", false);
        //    }
            //if (!this.IsPostBack)
            //{

            //    MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; ");

            //    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Clientes");

            //    MySqlDataAdapter da = new MySqlDataAdapter();

            //    cmd.Connection = con;
            //    da.SelectCommand = cmd;
            //    DataTable dt = new DataTable();

            //    da.Fill(dt);
            //    GridView1.DataSource = dt;
            //    GridView1.DataBind();

            //}   
  
        }
       
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            string valor = "";  
            try
            {
                 valor = GridView1.SelectedRow.Cells[1].Text;
            }
            catch
            {
            }
            if (Button1.Text == "Cadastrar")
            {

                //definição da string de conexão
                MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //definição do comando sql
                string sql = "INSERT INTO Clientes(  Nome_cliente, Telefone, email, CPF ) VALUES (@nome, @telefone, @email, @CPF)";

                try
                {


                    MySqlCommand comando = new MySqlCommand(sql, conn);
                    //Adicionando o valor das textBox nos parametros do comando

                    comando.Parameters.Add(new MySqlParameter("@nome", this.txtnome.Text));
                    comando.Parameters.Add(new MySqlParameter("@telefone", this.txttel.Text));
                    comando.Parameters.Add(new MySqlParameter("@email", this.txtemail.Text));
                    comando.Parameters.Add(new MySqlParameter("@CPF", this.txtcpf.Text));

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
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            else
            {
                
                //definição da string de conexão
                MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                //definição do comando sql
                string sql = "UPDATE Clientes SET Nome_cliente = @nome, Telefone = @telefone, email=@email, CPF=@CPF WHERE Nome_Cliente LIKE'" +  valor + "'";

                try
                {


                    MySqlCommand comando = new MySqlCommand(sql, conn);
                    //Adicionando o valor das textBox nos parametros do comando

                    comando.Parameters.Add(new MySqlParameter("@nome", txtnome.Text));
                    comando.Parameters.Add(new MySqlParameter("@telefone", this.txttel.Text));
                    comando.Parameters.Add(new MySqlParameter("@email", this.txtemail.Text));
                    comando.Parameters.Add(new MySqlParameter("@CPF", this.txtcpf.Text));
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
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            txtnome.Text = "";
            txtemail.Text = "";
            txttel.Text = "";
            Button1.Text = "Cadastrar";
            Button3.Visible = false;

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
            txtemail.Text = "";
            txttel.Text = "";
            Button1.Text = "Cadastrar";
            Button3.Visible = false;
            //definição da string de conexão
            MySqlConnection conn = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true;");
            //definição do comando sql
            string sql = "DELETE FROM Clientes WHERE Nome_Cliente LIKE'" + txtnome.Text + "'";

           

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
                txtnome.Text = "";
                Response.Redirect(Request.Url.AbsoluteUri);
           
            
        }



        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
          
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int linhaSelec = GridView1.SelectedIndex;

                //lblid.Text = GridView1.SelectedRow.Cells[1].Text;
               
                txtnome.Text = GridView1.SelectedRow.Cells[1].Text;
                txttel.Text = GridView1.SelectedRow.Cells[2].Text;
                txtemail.Text = GridView1.SelectedRow.Cells[3].Text;
                txtcpf.Text = GridView1.SelectedRow.Cells[4].Text;
                Button1.Text = "Alterar";
                Button3.Visible = true;
            }
            catch
            {
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            string nome = Convert.ToString(GridView1.Rows[e.RowIndex].Cells[1].Text);

            MySqlConnection conn4 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret;includesecurityasserts=true; ");
            //definição do comando sql
            string sql4 = "DELETE FROM Clientes WHERE Nome_cliente LIKE'" + nome + "'";



            MySqlCommand comando4 = new MySqlCommand(sql4, conn4);
            //Adicionando o valor das textBox nos parametros do comando



            //abre a conexao
            conn4.Open();
            //executa o comando com os parametros que foram adicionados acima
            comando4.ExecuteNonQuery();
            //fecha a conexao
            conn4.Close();
            //Minha função para limpar os textBox
            Response.Redirect(Request.Url.AbsoluteUri);

        }
    }
}