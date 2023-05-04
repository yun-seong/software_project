﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Software_Project
{
    public partial class Main_ui : Form
    {
        
        public Main_ui()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void choose_deopbab_Click(object sender, EventArgs e)
        {
            this.Visible = false; //현재창 숨김
            Deopbab deopbab = new Deopbab(); //객체 생성
            Point parentPoint = this.Location; //폼 열리는 위치 설정
            deopbab.StartPosition = FormStartPosition.Manual;  
            deopbab.Location = new Point(parentPoint.X, parentPoint.Y);
            deopbab.ShowDialog(); //폼 open
            this.Close();
        }

        private void choose_bockbab_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Bockbab bockbab = new Bockbab();
            Point parentPoint = this.Location;
            bockbab.StartPosition = FormStartPosition.Manual; 
            bockbab.Location = new Point(parentPoint.X, parentPoint.Y);
            bockbab.ShowDialog();
            this.Close();
        }

        private void choose_side_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Side side = new Side();
            Point parentPoint = this.Location;
            side.StartPosition = FormStartPosition.Manual;
            side.Location = new Point(parentPoint.X, parentPoint.Y);
            side.ShowDialog();
            this.Close();
        }

        private void choose_drink_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Drink drink = new Drink();
            Point parentPoint = this.Location;
            drink.StartPosition = FormStartPosition.Manual;  
            drink.Location = new Point(parentPoint.X, parentPoint.Y);
            drink.ShowDialog();
            this.Close();
        }
    }
}