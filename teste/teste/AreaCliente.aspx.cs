using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;

namespace teste
{
    public partial class AreaCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = Request.QueryString["User"];
            if (Label1.Text == "")
            {
                Response.Redirect("Loginuser.aspx", false);
            }
            preenchegrid();
            usuario();
            drptipo.SelectedIndex = 0;
            preenche_contrato();
        }
        string pdf = "";
        public void usuario()

        {
            string id = Label1.Text;
            string login = "";
            string strSql2 = "SELECT * From Usuario WHERE fun_login LIKE'" + id + "'";
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
                    
                    login = dr["fun_login"].ToString();
                }
            }
            dr.Close();

            txtusuario.Text = login;
        }
        
        public void preenchegrid()
        {
            string nomao = "";
                 string strSql2 = "SELECT * FROM Clientes WHERE Nome_cliente Like'" + Label1.Text + "'";
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

                string strSql3 = "SELECT * From movim WHERE nome LIKE'" + Label1.Text + "'";
                //cria a conexão com o banco de dados
                MySqlConnection con3 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
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




                    //  precocompra = precocompra.Replace(".", ",");
                    //     precoclose = precoclose.Replace(".", ",");
                    double compra = double.Parse(precocompra.Replace(',', '.'));
                    double close = double.Parse(precoclose.Replace(',', '.'));


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

                  
                }
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

        protected void gridmovim_RowDataBound(object sender, GridViewRowEventArgs e)
        {
             if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                if (pdf == "")
                {

                    string valor = e.Row.Cells[6].Text.ToString();
                    double valores = double.Parse(valor);
                    string valor2 = e.Row.Cells[5].Text.ToString();
                    if (valores > 5.99)
                    {
                        e.Row.Cells[6].BackColor = System.Drawing.Color.Blue;
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
                    }
                    else if (valores > 0 & valores < 5.99)
                    {
                        e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
                    }
                    else if (valores < 0 & valores > -100)
                    {
                        e.Row.Cells[6].BackColor = System.Drawing.Color.Black;
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string pdfPath = Server.MapPath("~/temp.pdf");
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(pdfPath);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", buffer.Length.ToString());
            Response.BinaryWrite(buffer);
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




                    //  precocompra = precocompra.Replace(".", ",");
                    //     precoclose = precoclose.Replace(".", ",");
                    double compra = double.Parse(precocompra.Replace(',', '.'));
                    double close = double.Parse(precoclose.Replace(',', '.'));


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




                    //  precocompra = precocompra.Replace(".", ",");
                    //     precoclose = precoclose.Replace(".", ",");
                    double compra = double.Parse(precocompra.Replace(',', '.'));
                    double close = double.Parse(precoclose.Replace(',', '.'));


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
         
            Response.Redirect("~/AreaCliente.aspx?User=" + Label1.Text, false);
        }
        protected void gridExportToPdf_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridmovim.PageIndex = e.NewPageIndex;
             
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        private void ExportGridToPDF()
        {
            string hoje = DateTime.Now.ToShortDateString();
            string hour = DateTime.Now.ToShortTimeString();
            string linha =Label1.Text + "- " + hoje + "-" + hour;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename='" + Label1.Text + "-" + hoje + "-" + hour + "'.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gridmovim.AllowPaging = false;

            // gvdetails.DataBind();
            gridmovim.RowStyle.VerticalAlign = VerticalAlign.Middle;
            gridmovim.HorizontalAlign = HorizontalAlign.Center;
            gridmovim.RenderControl(hw);
            gridmovim.HeaderRow.Style.Add("width", "15%");
            gridmovim.HeaderRow.Style.Add("font-size", "10px");
            gridmovim.Style.Add("text-decoration", "none");
            gridmovim.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
            gridmovim.Style.Add("font-size", "8px");
            gridmovim.Style.Add("text-align", "center");

            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A2, 7f, 5f, 9f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

            PdfWriter.GetInstance(pdfDoc, new FileStream((Server.MapPath("~/temp.pdf")), FileMode.Create));
            iTextSharp.text.pdf.draw.VerticalPositionMark seperator = new iTextSharp.text.pdf.draw.LineSeparator();


            pdfDoc.Open();
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
        protected void btnrelatorio_Click(object sender, EventArgs e)
        {
            File.Delete(Server.MapPath("~/temp.pdf"));


            pdf = "1";
            gridmovim.DataSource = null;
            gridmovim.DataBind();

            run();

            ExportGridToPDF();   
           
        }
        public void preenche_contrato()
               {

                   string nome = Label1.Text;
                    gridcontrato.DataSource = null;
                    gridcontrato.DataBind();
                    //define a instrução SQL

                    gridcontrato.AutoGenerateColumns = false;

                    //define e realiza a formatação de cada coluna
                    BoundField coluna1 = new BoundField();
                    coluna1.DataField = "acao";
                    coluna1.HeaderText = "Ativo";

                    gridcontrato.Columns.Add(coluna1);
                    BoundField coluna2 = new BoundField();
                    coluna2.DataField = "data_compra";
                    coluna2.HeaderText = "Data Compra";
                    coluna2.ReadOnly = true;
                    coluna2.HtmlEncode = false;

                    gridcontrato.Columns.Add(coluna2);


                    BoundField coluna3 = new BoundField();
                    coluna3.DataField = "valor_compra";
                    coluna3.HeaderText = "Valor Compra";
                    coluna3.HtmlEncode = false;

                    gridcontrato.Columns.Add(coluna3);

                    BoundField coluna4 = new BoundField();
                    coluna4.DataField = "qtde_compra";
                    coluna4.HeaderText = "Qtde Compra";
                    coluna4.HtmlEncode = false;
                    gridcontrato.Columns.Add(coluna4);

                    BoundField coluna5 = new BoundField();
                    coluna5.DataField = "data_venda";
                    coluna5.HeaderText = "Data Venda";
                    coluna5.HtmlEncode = false;
                    gridcontrato.Columns.Add(coluna5);

                    BoundField coluna6 = new BoundField();
                    coluna6.DataField = "valor_venda";
                    coluna6.HeaderText = "Valor Venda";
                    coluna6.HtmlEncode = false;
                    gridcontrato.Columns.Add(coluna6);

                    BoundField coluna7 = new BoundField();
                    coluna7.DataField = "qtde_venda";
                    coluna7.HeaderText = "Qtde Venda";
                    coluna7.HtmlEncode = false;
                    gridcontrato.Columns.Add(coluna7);

                    BoundField coluna8 = new BoundField();
                    coluna8.DataField = "data_pagamento";
                    coluna8.HeaderText = "Data Pagamento";
                    coluna8.HtmlEncode = false;
                    gridcontrato.Columns.Add(coluna8);

                    BoundField coluna9 = new BoundField();
                    coluna9.DataField = "id_contrato";
                    coluna9.HeaderText = "Id";
                    coluna9.HtmlEncode = false;
                    coluna9.Visible = false;
                    gridcontrato.Columns.Add(coluna9);

                    BoundField coluna10 = new BoundField();
                    coluna10.DataField = "status";
                    coluna10.HeaderText = "Status";
                    coluna10.HtmlEncode = false;
                    //     coluna10.Visible = false;
                    gridcontrato.Columns.Add(coluna10);

                    BoundField coluna11 = new BoundField();
                    coluna11.DataField = "perccli";
                    coluna11.HeaderText = "% Cliente";
                    coluna11.HtmlEncode = false;
                    //     coluna10.Visible = false;
                    gridcontrato.Columns.Add(coluna11);


                    BoundField coluna12 = new BoundField();
                    coluna12.DataField = "percinvest";
                    coluna12.HeaderText = "Invest(R$)";
                    coluna12.HtmlEncode = false;
                    //     coluna10.Visible = false;
                    gridcontrato.Columns.Add(coluna12);


                    string strSql2 = " ";
                    //cria a conexão com o banco de dados
                    MySqlConnection con = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
                    //cria o objeto command para executar a instruçao sql
                        

                    if (drptipo.SelectedValue == "P")
                    {
                          strSql2 = "SELECT * From Contrato WHERE nome_cliente LIKE'" + nome + "' AND status LIKE 'P'";
                        //cria a conexão com o banco de dados
                       


                    }
                    else if (drptipo.SelectedValue == "F")
                    {

                         strSql2 = "SELECT * From Contrato WHERE nome_cliente LIKE'" + nome + "' AND status LIKE 'F'";
                        //cria a conexão com o banco de dados
                       
                        
                    }

                    
                    MySqlCommand cmd1 = new MySqlCommand(strSql2, con);
                    //abre a conexao
                    con.Open();
                    //define o tipo do comando

             

                    //cria um dataadapter
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataSet dataset = new DataSet();
                    da.Fill(dataset);
                    //cria um objeto datatable
                    DataTable contrato = new DataTable();

                    //preenche o datatable via dataadapter
                    da.Fill(contrato);
                    da.Fill(dataset, "Contrato");

                    this.gridcontrato.DataSource = dataset;

                    gridcontrato.DataBind();

                    gridcontrato.Enabled = true;
                }

        protected void btnaltera_Click(object sender, EventArgs e)
        {
            string user = txtusuario.Text;
            MySqlConnection conn1 = new MySqlConnection("Server=investscorp.mysql.dbaas.com.br;User Id=investscorp;Database=investscorp;Pwd=Tank76ret; includesecurityasserts=true;");
            //definição do comando sql
            string sql1 = "UPDATE Usuario SET fun_senha=@fun_senha  WHERE fun_login LIKE'" + user + "'";

            try
            {


                MySqlCommand comando1 = new MySqlCommand(sql1, conn1);
                //Adicionando o valor das textBox nos parametros do comando

                comando1.Parameters.Add(new MySqlParameter("@fun_senha", TextBox1.Text));
                ;

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
                Label2.Text = "SENHA ALTERADA COM SUCESSO";
                Label2.Visible = true;
                TextBox1.Text = "";
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void gridcontrato_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}