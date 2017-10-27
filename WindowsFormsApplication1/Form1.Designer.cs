namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.jisuan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.TextBox();
            this.beishu = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // jisuan
            // 
            this.jisuan.Location = new System.Drawing.Point(584, 83);
            this.jisuan.Name = "jisuan";
            this.jisuan.Size = new System.Drawing.Size(75, 23);
            this.jisuan.TabIndex = 0;
            this.jisuan.Text = "相乘计算";
            this.jisuan.UseVisualStyleBackColor = true;
            this.jisuan.Click += new System.EventHandler(this.jisuan_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "购买价格：";
            // 
            // price
            // 
            this.price.Location = new System.Drawing.Point(107, 46);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(165, 21);
            this.price.TabIndex = 2;
            this.price.Text = "100";
            // 
            // beishu
            // 
            this.beishu.Location = new System.Drawing.Point(385, 46);
            this.beishu.Name = "beishu";
            this.beishu.Size = new System.Drawing.Size(165, 21);
            this.beishu.TabIndex = 3;
            this.beishu.Text = "0.02";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(302, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "倍数：";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(107, 85);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(165, 21);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = "0.75";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "提现比例：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(302, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "不要的价格：";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(385, 85);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(165, 21);
            this.textBox3.TabIndex = 8;
            this.textBox3.Text = "1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(38, 148);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(621, 265);
            this.textBox1.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 449);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.beishu);
            this.Controls.Add(this.price);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.jisuan);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button jisuan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox price;
        private System.Windows.Forms.TextBox beishu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox1;
    }
}

