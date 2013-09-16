using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Correctionary
{
    public partial class Result : Form
    {
        public Result(String Special_Word, string Others)
        {
            InitializeComponent();
            
            String[] arr = Others.Split(@" ".ToCharArray());
            Point Location = new Point(this.Size.Width - 5, 45);
            Point EnglishLocation = new Point(5, 30);
           // lbl_special.Text = Special_Word;
            AddSpeacialLabel(Special_Word);
            
            foreach (String str in arr)
            {
                String word = str.Replace('_', ' ');

                AddLabel(ref Location, ref EnglishLocation, word);
                

                
                
            }
          //  lbl_others.Text = Others;
            


        }

        private void AddSpeacialLabel(string Special_Word)
        {
            {
                Point Location = new Point(this.Size.Width, 21);
                Point EnglishLocation = new Point(5,21);
                
                // MessageBox.Show(Location.ToString());
                Label lbl = new Label();
                lbl.Name = "lbl_special";
                this.Controls.Add(lbl);
                lbl.Location = Location;
                lbl.Text = Special_Word;
                lbl.AutoSize = true;
                lbl.ForeColor = Color.Red;
                //lbl.Font.Name = "David";

                lbl.MouseLeave += new System.EventHandler(lbl_MouseLeave);
                lbl.MouseHover += new System.EventHandler(lbl_MouseHover);

                if (lbl.Text.ToCharArray().Length > 0 && (int)lbl.Text[0] > 1488 && lbl.Text[0] < 1514)
                {
                    lbl.RightToLeft = RightToLeft.Yes;
                    lbl.Location = Location;
                    lbl.TextAlign = ContentAlignment.MiddleLeft;
                  

                    //lbl_special.RightToLeft = RightToLeft.Yes;
                    //lbl_special.Location = Location;// new System.Drawing.Point(this.Size.Width - 5, 1);
                    //lbl_special.TextAlign = ContentAlignment.MiddleRight;
                    //Location.Y += 15;
                }
                else
                {
                    lbl.RightToLeft = RightToLeft.No;
                    lbl.Location = EnglishLocation;
                    lbl.TextAlign = ContentAlignment.MiddleLeft;

                    //lbl_special.RightToLeft = RightToLeft.No;
                    //lbl_special.Location = new System.Drawing.Point(5, 1);
                    //lbl_special.TextAlign = ContentAlignment.MiddleLeft;
                    EnglishLocation.Y += 20;
                }
            }
        }

        private void AddLabel(ref Point Location, ref Point EnglishLocation, String str)
        {
            // MessageBox.Show(Location.ToString());
            Label lbl = new Label();
            this.Controls.Add(lbl);
            lbl.Location = Location;
            lbl.Text = str;
            lbl.AutoSize = true;
            //lbl.Font.Name = "David";

            lbl.MouseLeave += new System.EventHandler(lbl_MouseLeave);
            lbl.MouseHover += new System.EventHandler(lbl_MouseHover);

            if (lbl.Text.ToCharArray().Length > 0 && (int)lbl.Text[0] > 1488 && lbl.Text[0] < 1514)
            {
                lbl.RightToLeft = RightToLeft.Yes;
                lbl.Location = Location;
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                Location.Y += 15;

                //lbl_special.RightToLeft = RightToLeft.Yes;
                //lbl_special.Location = Location;// new System.Drawing.Point(this.Size.Width - 5, 1);
                //lbl_special.TextAlign = ContentAlignment.MiddleRight;
                Location.Y += 15;
            }
            else
            {
                lbl.RightToLeft = RightToLeft.No;
                lbl.Location = EnglishLocation;
                lbl.TextAlign = ContentAlignment.MiddleLeft;

                //lbl_special.RightToLeft = RightToLeft.No;
                //lbl_special.Location = new System.Drawing.Point(5, 1);
                //lbl_special.TextAlign = ContentAlignment.MiddleLeft;
                EnglishLocation.Y += 20;
            }
        }

        private void lbl_others_TextChanged(object sender, EventArgs e)
        {
            //if ((int)lbl_others.Text[0] > 1488 && lbl_others.Text[0] < 1514)
            //{
            //    lbl_others.RightToLeft = RightToLeft.Yes;
            //    lbl_others.Location = new System.Drawing.Point(this.Size.Width-5, 21);
            //    lbl_others.TextAlign = ContentAlignment.MiddleLeft;

            //    lbl_special.RightToLeft = RightToLeft.Yes;
            //    lbl_special.Location = new System.Drawing.Point(this.Size.Width - 5, 1);
            //    lbl_special.TextAlign = ContentAlignment.MiddleLeft;


            //}
            //else
            //{
            //    lbl_others.RightToLeft = RightToLeft.No;
            //    lbl_others.Location = new System.Drawing.Point(5, 21);
            //    lbl_others.TextAlign = ContentAlignment.MiddleLeft;

            //    lbl_special.RightToLeft = RightToLeft.No;
            //    lbl_special.Location = new System.Drawing.Point(5, 1);
            //    lbl_special.TextAlign = ContentAlignment.MiddleLeft;

                
            //}
            
        }

        
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Result_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbl_special_MouseHover(object sender, EventArgs e)
        {
            //lbl_special.BackColor = Color.Yellow;
        }

        private void lbl_special_MouseLeave(object sender, EventArgs e)
        {
           // lbl_special.BackColor = Color.Transparent;
        }


        private void lbl_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.Transparent;
        }

        private void lbl_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.Yellow;
            this.Opacity = 1;
        }

        private void Result_MouseHover(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void Result_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.65;
        }

 

       

        
    }
}
