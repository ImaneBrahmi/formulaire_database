using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDB;
using static System.Net.Mime.MediaTypeNames;
using System.Data.OleDb;

namespace formulaire_database
{
    public partial class Form1 : Form
    {
        static string chaine = @"Data Source=localhost;Initial Catalog=BDGED_copy;Integrated Security=True";
        ";Data Source=C:\Users\Hp\Documents\formule.accdb"
        //"Server=.\SQLEXPRESS; DataBase=VotreBD;USER ID=sa; PASSWORD="
        static OleDbConnection cnx = new OleDblConnection(chaine);
        static OleDbCommand cmd = new OleDbCommand();
        static OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
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
        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            if (txtReference.Text == "" || txttitre.Text == "" || txtprix.Text == "")
            {
                MessageBox.Show("remplir les champs")
                    return;
            }
            cnx.Open();
            cmd.Connection = cnx;
            cmd.CommandText = "insert into livre(Reference,titre,prix) values('" + txtReference.Text + "','" + txttitre.Text + "','" + txtprix.Text + "') ";
            cmd.ExecuteNonQuery();
            cnx.Close();
            for (int i = 0; i < d.ds.Tables["livre"].Rows.Count; i++)
            {
                if (txt.Refernce.Text == d.ds.Tables["livre"].Rows[i][0].TotString())
                {
                    MessageBox.Show("le livre deja existe");
                    return;
                }
            }
            d.ds.Tables["livre"].Rows.Add(d.ligne);
            MessageBox.Show("le livre ajouter avec succes");
            dataGridView1.DataSource = d.ds.Tables["livre"];
            (this.Owner as Form1).btnDelete.Enabled = false;
            (this.Owner as Form1).btnUpdate.Enabled = false;
            (this.Owner as Form1).btnselect.Enabled = false;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtReference.Text == "" || txttitre.Text == "" || txtprix.Text == "")
            {
                MessageBox.Show("remplir les champs")
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
                if (txtId_dossier.Refernce.Text == d.ds.Tables["livre"].Rows[i][0].TotString())
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
            (this.Owner as Form1).btnDelete.Enabled = false;
            (this.Owner as Form1).btnInsert.Enabled = false;
            (this.Owner as Form1).btnselect.Enabled = false;

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
                    if (txtId_dossier.Refernce.Text == d.ds.Tables["livre"].Rows[i][0].TotString())
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
                    MessageBox.Show("le livre n'exicte" pas);
                }
                (this.Owner as Form1).btnInsert.Enabled = false;
                (this.Owner as Form1).btnUpdate.Enabled = false;
                (this.Owner as Form1).btnselect.Enabled = false;
                cnx.Open();
                cmd.Connection = cnx;
                cmd.CommandText = "delete from livre where Refernce='" + txtRefernce.Text + "' ";
                cmd.ExecuteNonQuery();
                cnx.Close();
            }
        }
       
    }
 }

