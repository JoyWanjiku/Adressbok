using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Adressbook
{
    public partial class Adressbook : Form
    {
        public Adressbook()
        {
            InitializeComponent();
        }
        string file = @"C:\Users\cikup\source\repos\Adressbook";
        private List<Person> people = new List<Person>(); //list that will hold all our contacts given class person
        class Person
        {
            public string Name { get; set; }
            public string Adress { get; set; }
            public string City { get; set; }
            public string ZipCode { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Search { get; set; }
        }
        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = people[listView1.SelectedItems[0].Index].Name; //we want the information put in to be held in the list view and when clicked on the information should show up in the textbox
            textBox2.Text = people[listView1.SelectedItems[0].Index].City; //thats why number 0
            textBox3.Text = people[listView1.SelectedItems[0].Index].ZipCode;
            textBox4.Text = people[listView1.SelectedItems[0].Index].Adress;
            textBox5.Text = people[listView1.SelectedItems[0].Index].PhoneNumber;
            textBox6.Text = people[listView1.SelectedItems[0].Index].Email;
        }
        private void Button2_Click_1(object sender, EventArgs e) //add contacts button
        {
            StreamWriter writer = new StreamWriter(file, true);
            Person p = new Person
            {
                Name = textBox1.Text, //here we are assigning what will be written in the textboxes
                Email = textBox6.Text,
                PhoneNumber = textBox5.Text,
                Adress = textBox4.Text,
                ZipCode = textBox3.Text,
                City = textBox2.Text
            };

            people.Add(p); //we are adding the person here ref 15
            listView1.Items.Add(p.Name); //adding the person to the list view box

            textBox1.Clear();
            textBox2.Clear(); //when contacts saved, textboxes are reset 
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
        private void Button3_Click(object sender, EventArgs e) //remove contact button
        {
            string tempFile = Path.GetTempFileName();
            using (var sr = new StreamReader(file))
                Remove();
            File.Delete(@"C:\Users\cikup\source\repos\Adressbook");
            File.Move(tempFile, file);
        }
        void Remove() //we are removing contacts from the people list when user clicks on remove
        {
                try
            {
                listView1.Items.Remove(listView1.SelectedItems[0]); //removes contact from list view
                people.RemoveAt(listView1.SelectedItems[0].Index);
            }//removes contact from our people container at line 
            //using catch method in case nothing is selected from the list view to prevent index -1 which is invalid and will throw an error
            catch (ArgumentOutOfRangeException myExp)
            {
                Console.WriteLine("We picked up a error of type " + myExp.Message);
            }
        }
        private void Button1_Click(object sender, EventArgs e) // save changes button
        {
            string tempFile = Path.GetTempFileName();
            using (var sr = new StreamReader(file))

            people[listView1.SelectedItems[0].Index].Name = textBox1.Text; //selects the contact index clicked on. we are also mentioning where ex. name is located =textbox1
            people[listView1.SelectedItems[0].Index].Email = textBox6.Text;
            people[listView1.SelectedItems[0].Index].PhoneNumber = textBox5.Text;
            people[listView1.SelectedItems[0].Index].Adress = textBox4.Text;
            people[listView1.SelectedItems[0].Index].ZipCode = textBox3.Text;
            people[listView1.SelectedItems[0].Index].City = textBox2.Text;
            listView1.SelectedItems[0].Text = textBox1.Text;

            File.Delete(@"C:\Users\cikup\source\repos\Adressbook");
            File.Move(tempFile, file);
        }
        private void TextBox7_TextChanged(object sender, EventArgs e) //search textbox
        {
           
        }
        private void Button4_Click(object sender, EventArgs e) //search button
        {

            listView1.Items.Clear();
            string sokaValue = textBox7.Text;
            StreamReader reader = new StreamReader(file, true);
            string row = reader.ReadLine();
            int counterInfo = 0;
            while (row != null)
            {
                string[] infoValues = row.Split(',');
                if (sokaValue == infoValues[0] || sokaValue == infoValues[1] || sokaValue == infoValues[3])
                {
                    listView1.Items.Add(row);
                    counterInfo++;
                }
                row = reader.ReadLine();
            }
            reader.Close();

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
