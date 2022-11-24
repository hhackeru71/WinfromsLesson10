using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinfromsLesson10
{
    public partial class Form1 : Form
    {
        DBEntities1 db = new DBEntities1();
        Person person = new Person();
        public Form1()
        {
            InitializeComponent();
        }



        private void LoadData()
        {
            dataGridView1.DataSource = db.Person.ToList();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int.TryParse(txtAge.Text, out int result);
                person = new Person()
                {
                    Age = result,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text
                };
                db.Person.Add(person);
                if (db.SaveChanges() > 0)
                {
                    MessageBox.Show("success");
                }
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (person != null)
                {
                    db.Person.Remove(person);
                    db.SaveChanges();
                    LoadData();
                    MessageBox.Show("delete success");
                }
                else
                {
                    MessageBox.Show("error! empty data!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error delete!");


            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentCell.RowIndex != -1)
                {

                    int empId = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Id"].Value);
                    person = db.Person.Where(t => t.Id == empId).FirstOrDefault();
                    if (person != null)
                    {
                        txtFirstName.Text = person.FirstName;
                        txtLastName.Text = person.LastName;
                        txtAge.Text = person.Age.ToString();
                        txtId.Text = person.Id.ToString();
                    }

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("error 1!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (person != null)
                {
                    person.FirstName = txtFirstName.Text;
                    person.LastName = txtLastName.Text;
                    int.TryParse(txtAge.Text, out int result);
                    person.Age = result;
                    db.Entry(person).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("success to update");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("error! empty data!");

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("eror to update!");
            }


        }
    }
}
