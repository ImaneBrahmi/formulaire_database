using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;

namespace formulaire_database
{
    public partial class Form1 : Form
    {
        static readonly string chaine = "@Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:Users_Hp_Documents_formule.accdb";

        static OleDbConnection cnx = new OleDbConnection(chaine);
        static OleDbCommand cmd = new OleDbCommand();
        static OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        Ado d = new Ado();
        public Form1()
        {
            InitializeComponent();
            RemplirGrid();
        }
        // REMPLISSAGE DE GRIDVIEW
        public void RemplirGrid()
        {
            d.dap = new OleDbDataAdapter("select *from livre", d.conn);
            d.dap.Fill(d.ds, "livre");
            dataGridView1.DataSource = d.ds.Tables["livre"];
        }


        private void btnselect_Click(object sender, EventArgs e)
        {
            cnx.Open();
            cmd.CommandText = "select * from livre";
            cmd.Connection = cnx;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            cnx.Close();
            (this.Owner as Form1).btndelete.Enabled = true;
            (this.Owner as Form1).btnUpdate.Enabled = true;
            (this.Owner as Form1).btnselect.Enabled = true;
            (this.Owner as Form1).btnconfirmer.Enabled = false;
            (this.Owner as Form1).btnannuler.Enabled = false;
        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            if (txtReference.Text == "" || txttitre.Text == "" || txtprix.Text == "")
            {
                MessageBox.Show("remplir les champs");
                    return;
            }
            cnx.Open();
            cmd.Connection = cnx;
            cmd.CommandText = "insert into livre(Reference,titre,prix) values('" + txtReference.Text + "','" + txttitre.Text + "','" + txtprix.Text + "') ";
            cmd.ExecuteNonQuery();
            cnx.Close();
            for (int i = 0; i < d.ds.Tables["livre"].Rows.Count; i++)
            {
                if (txtRefernce.Text == d.ds.Tables["livre"].Rows[i][0].ToString())
                {
                    MessageBox.Show("le livre deja existe");
                    return;
                }
            }
            d.ds.Tables["livre"].Rows.Add(d.ligne);
            MessageBox.Show("le livre ajouter avec succes");
            dataGridView1.DataSource = d.ds.Tables["livre"];
            (this.Owner as Form1).btndelete.Enabled = false;
            (this.Owner as Form1).btnUpdate.Enabled = false;
            (this.Owner as Form1).btnselect.Enabled = false;
            (this.Owner as Form1).btnconfirmer.Enabled = true;
            (this.Owner as Form1).btnannuler.Enabled = true;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtReference.Text == "" || txttitre.Text == "" || txtprix.Text == "")
            {
                MessageBox.Show("remplir les champs");
                    return;
            }

            if (txtRefernce.Text == "")
            {
                MessageBox.Show("errour");
                return;
            }
            Boolean tr = false;
            for (int i = 0; i < d.ds.Tables["livre"].Rows.Count; i++)
            {
                if (txtRefernce.Text == d.ds.Tables["livre"].Rows[i][0].TotString())
                {
                    tr = true;
                    d.ds.Tables["livre"].Rows[i][1] = txtRefernce.Text;
                    d.ds.Tables["livre"].Rows[i][2] = txttitre.Text;
                    d.ds.Tables["livre"].Rows[i][3] = txtprix.Text;
                    MessageBox.Show("le livre modifier avec succes");
                    dataGridView1.DataSource = d.ds.Tables["livre"];
                    break;

                }
            }
            if (tr == false)
            {
                MessageBox.Show("le livre n'existe pas");
            }
            (this.Owner as Form1).btndelete.Enabled = false;
            (this.Owner as Form1).btninsert.Enabled = false;
            (this.Owner as Form1).btnselect.Enabled = false;
            (this.Owner as Form1).btnconfirmer.Enabled = true;
            (this.Owner as Form1).btnannuler.Enabled = true;

            cnx.Open();
            cmd.Connection = cnx;
            cmd.CommandText = "update livre set titre='" + txttitre.Text + "update livre set prix='" + txtprix.Text + "' where Reference='" + txtRefernce.Text + "' ";
            cnx.Close();

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            {
                if (txtRefernce.Text == "")
                {
                    MessageBox.Show("errour");
                    return;
                }
                Boolean tr = false;
                for (int i = 0; i < d.ds.Tables["livre"].Rows.Count; i++)
                {
                    if (txRefernce.Text == d.ds.Tables["livre"].Rows[i][0].ToString())
                    {
                        tr = true;
                        d.ds.Tables["livre"].Rows[i].Delete();
                        MessageBox.Show("le livre supprimer avec succes");
                        dataGridView1.DataSource = d.ds.Tables["livre"];
                        break;

                    }
                }
                if (tr == false)
                {
                    MessageBox.Show("le livre n'exicte pas");
                }
                (this.Owner as Form1).btninsert.Enabled = false;
                (this.Owner as Form1).btnUpdate.Enabled = false;
                (this.Owner as Form1).btnselect.Enabled = false;
                (this.Owner as Form1).btnconfirmer.Enabled = true;
                (this.Owner as Form1).btnannuler.Enabled = true;

                cnx.Open();
                cmd.Connection = cnx;
                cmd.CommandText = "delete from livre where Refernce='" + txtRefernce.Text + "' ";
                cmd.ExecuteNonQuery();
                cnx.Close();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnconfirmer_Click(object sender, EventArgs e)
        {
            d.bc = new OleDbCommandBuilder(d.dap);
            d.dap.Update(d.ds, "livre");
            MessageBox.Show("les donnes enregister avec succes");
        }
    }
 }

