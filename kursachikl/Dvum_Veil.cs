using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kursachikl
{
    public partial class Dvum_Veil : Form
    {
        public Dvum_Veil()
        {
            InitializeComponent();
        }
        DataBases dataBases = new DataBases();

        private void Odnom_Veil_Load(object sender, EventArgs e)
        {
            CreateComponent();
            RefreshDataGrid(dataGridView1);
        }
        private void CreateComponent()
        {
            dataGridView1.Columns.Add("AthleteID ", "id");
            dataGridView1.Columns.Add("AthleteLastName", "Фамилия");
            dataGridView1.Columns.Add("AthleteDOB", "Дата рождения");
            dataGridView1.Columns.Add("AthletePassport", "Паспорт спортсмена");
            dataGridView1.Columns.Add("AthleteRating", "Рейтинг спортсмена");
            dataGridView1.Columns.Add("id_Coach", "id статус");

        }
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetString(2), record.GetInt32(3), record.GetInt32(4), DataRowState.Modified);
        }
        private void Dvum_Veil_Load(object sender, EventArgs e)
        {
            CreateComponent();
            RefreshDataGrid(dataGridView1);
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string querystring = $"select * from sportiki";
            SqlCommand cmd = new SqlCommand(querystring, dataBases.getConnection());

            dataBases.openConnection();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }
        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string searchString = $"select * from sportiki where concat (AthleteID, AthleteLastName, AthleteDOB, AthletePassport, AthleteRating,id_Coach) like '%" + textBox_search.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, dataBases.getConnection());
            dataBases.openConnection();

            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Veil veil = new Veil();
            veil.Show();
            this.Close();
        }

        
    }
}
