﻿using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Software_Project
{
    public partial class Side : Form
    {
        public Jangbaguni jangbaguni = new Jangbaguni();
        public List<Jangbaguni_button_set> jan_btn_combi = new List<Jangbaguni_button_set>(); //장바구니와 버튼을 쌍으로 저장
        public Side(List<Jangbaguni_button_set> list)
        {
            InitializeComponent();
            jan_btn_combi = list;
            for (int i = 0; i < jan_btn_combi.Count; i++)
            {
                jan_btn_combi[i].btn.Controls[1].Click += clear_menu;
                jan_btn_combi[i].btn.Controls[2].Controls[0].Click += push_plus_button;
                jan_btn_combi[i].btn.Controls[2].Controls[1].Click += push_minus_button;
            }
        }
        public void Side_Load(object sender, EventArgs e)
        {
            ReadCsvFile("menu.csv",flowLayoutPanel1);
            Total_cost_TextChanged();
            for (int i = 0; i < jan_btn_combi.Count; i++)
            {
                flowLayoutPanel2.Controls.Add(jan_btn_combi[i].btn);
            }
        }

        public void choose_deopbab_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            for (int i = 0; i < jan_btn_combi.Count; i++) //이벤트헨들러 지우고 보내기
            {
                jan_btn_combi[i].btn.Controls[1].Click -= clear_menu;
                jan_btn_combi[i].btn.Controls[2].Controls[0].Click -= push_plus_button;
                jan_btn_combi[i].btn.Controls[2].Controls[1].Click -= push_minus_button;
            }
            Deopbab deopbab = new Deopbab(jan_btn_combi);
            Point parentPoint = this.Location;
            deopbab.StartPosition = FormStartPosition.Manual;
            deopbab.Location = new Point(parentPoint.X, parentPoint.Y);
            deopbab.ShowDialog();
            this.Close();
        }

        public void choose_bockbab_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            for (int i = 0; i < jan_btn_combi.Count; i++) //이벤트헨들러 지우고 보내기
            {
                jan_btn_combi[i].btn.Controls[1].Click -= clear_menu;
                jan_btn_combi[i].btn.Controls[2].Controls[0].Click -= push_plus_button;
                jan_btn_combi[i].btn.Controls[2].Controls[1].Click -= push_minus_button;
            }
            Bockbab bockbab = new Bockbab(jan_btn_combi);
            Point parentPoint = this.Location;
            bockbab.StartPosition = FormStartPosition.Manual;
            bockbab.Location = new Point(parentPoint.X, parentPoint.Y);
            bockbab.ShowDialog();
            this.Close();
        }

        public void choose_drink_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            for (int i = 0; i < jan_btn_combi.Count; i++) //이벤트헨들러 지우고 보내기
            {
                jan_btn_combi[i].btn.Controls[1].Click -= clear_menu;
                jan_btn_combi[i].btn.Controls[2].Controls[0].Click -= push_plus_button;
                jan_btn_combi[i].btn.Controls[2].Controls[1].Click -= push_minus_button;
            }
            Drink drink = new Drink(jan_btn_combi);
            Point parentPoint = this.Location;
            drink.StartPosition = FormStartPosition.Manual;
            drink.Location = new Point(parentPoint.X, parentPoint.Y);
            drink.ShowDialog();
            this.Close();
        }
        public void ReadCsvFile(string filePath, FlowLayoutPanel panel) //메뉴 csv 파일 읽기
        {
            List<List<string>> data = new List<List<string>>();
            try
            {
                var reader = new StreamReader(filePath, Encoding.Default);

                string[] headers = reader.ReadLine().Split(','); //앞부분은 스키마이므로 생략
                int numColumns = headers.Length;
                while (!reader.EndOfStream)
                {
                    string[] row = reader.ReadLine().Split(',');
                    List<string> rowData = new List<string>(row);
                    if (row[2] == "사이드")
                    {
                        data.Add(rowData);
                    }
                }
            }
            catch
            {
                Console.WriteLine($"Error reading CSV file: 오류입니다. ");
                return;
            }
            Make_button(data, panel);
        }
        public void Make_button(List<List<string>> list, FlowLayoutPanel panel) //동적으로 메뉴 버튼 만들기
        {
            int num = list.Count;
            List<Guna2Button> buttonList = new List<Guna2Button>();

            // 버튼 생성 및 속성 설정 후 리스트에 추가
            for (int i = 0; i < num; i++)
            {
                //버튼 생성
                Guna2Button button = new Guna2Button();
                button.Name = list[i][0];
                button.Text = list[i][0] + "\n[" + list[i][1] + "원]";
                button.Size = new Size(237, 180);
                button.BorderThickness = 1;

                //폰트 설정
                button.ForeColor = Color.DimGray;
                button.FillColor = Color.AntiqueWhite;
                button.Font = new Font("휴먼둥근헤드라인", 10);
                button.TextAlign = HorizontalAlignment.Center;


                //이미지 설정
                button.Image = Image.FromFile(list[i][0] + ".jpg");
                button.ImageSize = new Size(150, 90);
                button.ImageOffset = new Point(35, -15);
                button.TextOffset = new Point(0, 55);
                button.ImageAlign = HorizontalAlignment.Left;
                panel.Controls.Add(button);
                button.Click += button_Click;
            }
        }
        public void button_Click(object sender, EventArgs e)
        {
            //현재 메뉴 탐색
            this.Visible = false;
            Guna2Button btn = (Guna2Button)sender;
            string name = btn.Name;
            Choose choose = new Choose(name);
            choose.chooseSendEvent += new Choose.FormSendDataHandler(add_Choose);
            Point parentPoint = this.Location;
            choose.StartPosition = FormStartPosition.Manual;
            choose.Location = new Point(parentPoint.X, parentPoint.Y);
            choose.ShowDialog();
            this.Visible = true;
        }

        public void add_Choose(Jangbaguni jan) //장바구니에 추가가 됐을 때
        {
            string option = "";
            int num = jan.options.Count;
            for (int i = 0; i < num; i++) //박스에 띄울 옵션 넣기
            {
                if (jan.options[i][3] != "0")
                {
                    option += jan.options[i][0] + "" + jan.options[i][3] + "개 ";
                }

            }
            if (jan.memo != "")
            {
                option += "\r\n" + jan.memo;
            }
            //버튼들 생성
            Button button = new Button();
            Button X_button = new Button();
            Button plus = new Button();
            Button minus = new Button();
            Label price = new Label();
            TextBox box = new TextBox();
            plus.Text = "+";
            plus.Dock = DockStyle.Right;
            plus.Size = new Size(35, 35);
            plus.FlatStyle = FlatStyle.Flat;
            plus.Click += push_plus_button;
            minus.Text = "-";
            minus.Dock = DockStyle.Left;
            minus.Size = new Size(35, 35);
            minus.FlatStyle = FlatStyle.Flat;
            minus.Click += push_minus_button;
            box.Text = "\r\n" + jan.count.ToString() + " 개";
            box.TextAlign = HorizontalAlignment.Center;
            box.Multiline = true;
            box.Size = new Size(120, 35);
            box.Location = new Point(280, 8);
            box.Controls.Add(plus);
            box.Controls.Add(minus);
            price.Size = new Size(70, 0);
            price.Text = Convert.ToString(jan.total_price) + " 원 ";
            price.TextAlign = ContentAlignment.MiddleRight;
            price.Dock = DockStyle.Right;
            X_button.Text = "X";
            X_button.Font = new Font("휴먼둥근헤드라인", 12);
            X_button.Dock = DockStyle.Right;
            X_button.Size = new Size(50, 50);
            X_button.FlatStyle = FlatStyle.Flat;
            X_button.FlatAppearance.BorderSize = 0;
            X_button.ForeColor = Color.Gray;
            X_button.BackColor = Color.Coral;
            X_button.Click += clear_menu;
            button.Controls.Add(price);
            button.Controls.Add(X_button);
            button.Controls.Add(box);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Name = jan.menu_name + jan.memo;
            button.Text = "[" + jan.menu_name + "]\r\n" + option;
            button.ForeColor = Color.WhiteSmoke;
            button.BackColor = Color.SandyBrown;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("휴먼둥근헤드라인", 8);
            button.Size = new Size(525, 50);
            push_janbaguni_panel(flowLayoutPanel2, button, jan);
        }

        public void clear_menu(object sender, EventArgs e)
        {
            Control button = ((Control)sender).Parent;

            for (int i = 0; i < jan_btn_combi.Count; i++)
            {
                if (jan_btn_combi[i].btn == button)
                {
                    flowLayoutPanel2.Controls.Remove(jan_btn_combi[i].btn);
                    jan_btn_combi[i].btn.Dispose();
                    jan_btn_combi.Remove(jan_btn_combi[i]);
                    break;
                }

            }
            Total_cost_TextChanged();
        }
        public void push_janbaguni_panel(FlowLayoutPanel panel, Button btn, Jangbaguni j)
        {
            Jangbaguni_button_set set;
            set.btn = btn;
            set.jangbaguni = j;
            jan_btn_combi.Add(set);
            panel.Controls.Add(btn); //패널에 버튼 추가
            Total_cost_TextChanged(); //모두 합한 가격 수정
        }
        public void push_plus_button(object sender, EventArgs e)
        {
            Control button = ((Control)((Control)sender).Parent).Parent; //메뉴 버튼
            for (int i = 0; i < jan_btn_combi.Count; i++)
            {
                if (jan_btn_combi[i].btn == button)
                {
                    Jangbaguni jang;
                    Jangbaguni_button_set set;
                    Button bb = jan_btn_combi[i].btn;
                    jang = jan_btn_combi[i].jangbaguni;
                    jang.total_price += jang.total_price / jang.count; //버튼에 속해있는 장바구니 메뉴의 수량과 가격을 수정함
                    jang.count++;
                    set.btn = jan_btn_combi[i].btn;
                    set.jangbaguni = jang;
                    jan_btn_combi[i] = set;
                    TextBox box = (TextBox)button.Controls[2]; //개수 수정
                    box.Text = "\r\n" + jan_btn_combi[i].jangbaguni.count.ToString() + " 개";
                    Label label = (Label)button.Controls[0];
                    label.Text = Convert.ToString(jan_btn_combi[i].jangbaguni.total_price) + " 원 ";
                    break;
                }
            }
            Total_cost_TextChanged();

        }
        public void push_minus_button(object sender, EventArgs e)
        {
            Control button = ((Control)((Control)sender).Parent).Parent; //메뉴 버튼
            for (int i = 0; i < jan_btn_combi.Count; i++)
            {
                if (jan_btn_combi[i].btn == button)
                {
                    Jangbaguni jang;
                    Jangbaguni_button_set set;
                    Button bb = jan_btn_combi[i].btn;
                    jang = jan_btn_combi[i].jangbaguni;
                    jang.total_price -= jang.total_price / jang.count; //버튼에 속해있는 장바구니 메뉴의 수량과 가격을 수정함
                    jang.count--;
                    if (jang.count == 0)
                    {
                        flowLayoutPanel2.Controls.Remove(bb);
                        jan_btn_combi.Remove(jan_btn_combi[i]);
                        Total_cost_TextChanged();
                        break;
                    }

                    set.btn = jan_btn_combi[i].btn;
                    set.jangbaguni = jang;
                    jan_btn_combi[i] = set;
                    TextBox box = (TextBox)button.Controls[2]; //개수 수정
                    box.Text = "\r\n" + jan_btn_combi[i].jangbaguni.count.ToString() + " 개";
                    Label label = (Label)button.Controls[0]; //가격 수정
                    label.Text = Convert.ToString(jan_btn_combi[i].jangbaguni.total_price) + " 원 ";
                    break;
                }
            }
            Total_cost_TextChanged();
        }

        public void Total_cost_TextChanged()
        {
            int num = 0;
            for (int i = 0; i < jan_btn_combi.Count; i++)
            {
                num += jan_btn_combi[i].jangbaguni.total_price;
            }
            Application.DoEvents();
            Total_cost.Text = "\r\n총\r\n" + num.ToString() + " 원";
            Total_cost.Update();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            jan_btn_combi.Clear();
            Main_ui main = new Main_ui();
            Point parentPoint = this.Location; //폼 열리는 위치 설정
            main.StartPosition = FormStartPosition.Manual;
            main.Location = new Point(parentPoint.X, parentPoint.Y);
            main.ShowDialog();
        }
    }
}
