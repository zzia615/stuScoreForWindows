using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace stuScore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string yhzh = textBox1.Text;
            string yhmm = textBox2.Text;
            string yhlb = "管理员";
            if (radioButton1.Checked)
            {
                yhlb = "学生";
            }
            if (radioButton2.Checked)
            {
                yhlb = "教师";
            }

            string sql = string.Format("select * from yhxx where yhzh='{0}' and yhmm='{1}' and yhlb='{2}'",
                yhzh, yhmm, yhlb);
            var table = new SqlServerHelper().QuerySqlDataTable(sql);
            if (table.Rows.Count <= 0)
            {
                MessageBox.Show("用户名密码有误","登录失败");
                return;
            }


        }
    }
}
