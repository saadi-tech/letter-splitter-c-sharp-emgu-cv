namespace WindowsFormsApp3
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.input_path_box = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.output_path_box = new System.Windows.Forms.TextBox();
            this.browse_input = new System.Windows.Forms.Button();
            this.output_browse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.progress_bar = new System.Windows.Forms.ProgressBar();
            this.start_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.status_label = new System.Windows.Forms.Label();
            this.threshold_selector = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.split_words = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.threshold_selector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(64, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input files:";
            // 
            // input_path_box
            // 
            this.input_path_box.Location = new System.Drawing.Point(189, 65);
            this.input_path_box.Name = "input_path_box";
            this.input_path_box.Size = new System.Drawing.Size(350, 22);
            this.input_path_box.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(64, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output files:";
            // 
            // output_path_box
            // 
            this.output_path_box.Location = new System.Drawing.Point(189, 130);
            this.output_path_box.Name = "output_path_box";
            this.output_path_box.Size = new System.Drawing.Size(350, 22);
            this.output_path_box.TabIndex = 4;
            // 
            // browse_input
            // 
            this.browse_input.Location = new System.Drawing.Point(585, 55);
            this.browse_input.Name = "browse_input";
            this.browse_input.Size = new System.Drawing.Size(107, 43);
            this.browse_input.TabIndex = 5;
            this.browse_input.Text = "Browse";
            this.browse_input.UseVisualStyleBackColor = true;
            this.browse_input.Click += new System.EventHandler(this.browse_input_Click);
            // 
            // output_browse
            // 
            this.output_browse.Location = new System.Drawing.Point(585, 120);
            this.output_browse.Name = "output_browse";
            this.output_browse.Size = new System.Drawing.Size(107, 43);
            this.output_browse.TabIndex = 6;
            this.output_browse.Text = "Browse";
            this.output_browse.UseVisualStyleBackColor = true;
            this.output_browse.Click += new System.EventHandler(this.output_browse_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(64, 265);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Progress:";
            // 
            // progress_bar
            // 
            this.progress_bar.Location = new System.Drawing.Point(189, 265);
            this.progress_bar.Name = "progress_bar";
            this.progress_bar.Size = new System.Drawing.Size(350, 23);
            this.progress_bar.TabIndex = 8;
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(189, 186);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(107, 43);
            this.start_button.TabIndex = 9;
            this.start_button.Text = "Split Letters";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(64, 323);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Status:";
            // 
            // status_label
            // 
            this.status_label.AutoSize = true;
            this.status_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status_label.Location = new System.Drawing.Point(184, 319);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(78, 29);
            this.status_label.TabIndex = 11;
            this.status_label.Text = "Idle...";
            // 
            // threshold_selector
            // 
            this.threshold_selector.Location = new System.Drawing.Point(370, 20);
            this.threshold_selector.Maximum = new decimal(new int[] {
            230,
            0,
            0,
            0});
            this.threshold_selector.Name = "threshold_selector";
            this.threshold_selector.Size = new System.Drawing.Size(72, 22);
            this.threshold_selector.TabIndex = 12;
            this.threshold_selector.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(186, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Threshold value: (0-230)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(565, 200);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(167, 170);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(621, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Preview";
            // 
            // split_words
            // 
            this.split_words.Location = new System.Drawing.Point(432, 186);
            this.split_words.Name = "split_words";
            this.split_words.Size = new System.Drawing.Size(107, 43);
            this.split_words.TabIndex = 16;
            this.split_words.Text = "Split Words";
            this.split_words.UseVisualStyleBackColor = true;
            this.split_words.Click += new System.EventHandler(this.split_words_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 398);
            this.Controls.Add(this.split_words);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.threshold_selector);
            this.Controls.Add(this.status_label);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.progress_bar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.output_browse);
            this.Controls.Add(this.browse_input);
            this.Controls.Add(this.output_path_box);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.input_path_box);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.threshold_selector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox input_path_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox output_path_box;
        private System.Windows.Forms.Button browse_input;
        private System.Windows.Forms.Button output_browse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progress_bar;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label status_label;
        private System.Windows.Forms.NumericUpDown threshold_selector;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button split_words;
    }
}

