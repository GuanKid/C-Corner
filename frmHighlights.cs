using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp5
{
    public partial class frmHighlights : Form
    {
        public frmHighlights()
        {
            InitializeComponent();
        }
        Data_Handler handler = new Data_Handler();

        private void frmHighlights_Load(object sender, EventArgs e)
        {

            SqlDataAdapter adapter = handler.viewFaultyFlights(85);
            
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            int rows = dt.Rows.Count;

            for (int l = 0; l < dt.Rows.Count; l++)
            {
                chart2.Series["Health"].Points.AddXY(dt.Rows[l]["Plane_ID"].ToString(), int.Parse(dt.Rows[l]["Condition"].ToString()));

            }

            
                for (int j = 0; j < rows; j++)
                {

                
                label9.Text = dt.Rows[j]["Plane_ID"].ToString();
                SqlDataAdapter plu = handler.Check_UpByID(label9.Text.ToString());
                DataTable data = new DataTable();
                plu.Fill(data);
                progressBar1.Value = int.Parse(data.Rows[0]["Condition"].ToString());
                label2.Text = dt.Rows[j]["Plane_Capacity"].ToString();
                    SqlDataAdapter dapt = handler.viewModelPlane(dt.Rows[j]["Plane_ID"].ToString());
                    DataTable datar2 = new DataTable();
                    dapt.Fill(datar2);
                    label4.Text = datar2.Rows[0]["Model_Year"].ToString();
                    using (FileStream fs = new FileStream(dt.Rows[j]["img_Path"].ToString(), FileMode.Open, FileAccess.Read))
                    {
                        Size size = new Size(pictureBox1.Width, pictureBox1.Height);

                        Bitmap resizedImage = new Bitmap(Image.FromStream(fs), size);
                        pictureBox1.Image = resizedImage;
                    }
                    Task.Delay(6000).Wait();


                }


            SqlDataAdapter adapt = handler.viewtoday();
            DataTable dt2 = new DataTable();
            adapt.Fill(dt2);
            TimeSpan timeSpan = new TimeSpan(3, 0, 0);
            int count = 0, done_count = 0, left_count = 0, ongoing_count = 0;
           TimeSpan now = DateTime.Now.TimeOfDay + timeSpan;

            foreach (DataRow dr in dt2.Rows)
            {
                count += 1;
                if (TimeSpan.Parse(dr["Flight_Time"].ToString()) <= DateTime.Now.TimeOfDay)
                {
                    done_count += 1;
                }

                if (TimeSpan.Parse(dr["Flight_Time"].ToString()) >= DateTime.Now.TimeOfDay)
                {
                    left_count += 1;
                }

                

            }
            label12.Text = done_count.ToString();
            lblLeft.Text = left_count.ToString();
            lblOngoing.Text = ongoing_count.ToString();
            lblToday.Text = count.ToString();


            // planes that are healthy, dead, unstable, and in repair

            SqlDataAdapter adapte = handler.planeView();
            DataTable dt3 = new DataTable();
            adapte.Fill(dt3);
            int count_unstable = 0;
            double unstable_avg = 0;
            foreach (DataRow item in dt3.Rows)
            {
                count_unstable += 1;
            }
            if (count_unstable >0)
            {
                unstable_avg = (rows / count_unstable) * 100;
            }
            
            
            
            SqlDataAdapter adapterr = handler.Check_UpView();
            DataTable dt4 = new DataTable();
            adapterr.Fill(dt4);

            int count_onCheck = 0;
            double oncheck_avg = 0;
            foreach (DataRow item in dt4.Rows)
            {
                count_onCheck += 1;
            }
            oncheck_avg = (count_onCheck/ count_unstable) * 100;


            double stable = count_unstable - rows;
            double toBeChecked = rows - count_onCheck;
            chart1.Series["Planes"].Points.AddXY("Stable", stable );
            chart1.Series["Planes"].Points.AddXY("Unstable",rows);
            chart1.Series["Planes"].Points.AddXY("Checked Today", count_onCheck);


        }

        public string dateFormat(string date)
        {
            string output = "";
            int index = date.IndexOf(" ");
            if (index >= 0)
            {


                output = date.Substring(0, index);
            }
            return output;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void flights()
        {

        }
        public void FaultyPlanes()
        {
        
        }
    }
}
