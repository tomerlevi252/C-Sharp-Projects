
namespace RecoursiveConstructor_Image_Text
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
            Min_Max_label = new Label();
            Image_Text_label = new Label();
            bySize_byBrightness_label = new Label();
            MinMax_Result_label = new Label();
            radioButton_Green = new RadioButton();
            radioButton_Red = new RadioButton();
            SuspendLayout();
            // 
            // Min_Max_label
            // 
            Min_Max_label.Font = new Font("Arial", 16.125F, FontStyle.Bold);
            Min_Max_label.Location = new Point(174, 1);
            Min_Max_label.Name = "Min_Max_label";
            Min_Max_label.Size = new Size(66, 30);
            Min_Max_label.TabIndex = 12;
            Min_Max_label.Text = "Min";
            // 
            // Image_Text_label
            // 
            Image_Text_label.Font = new Font("Arial", 16.125F, FontStyle.Bold);
            Image_Text_label.Location = new Point(285, 1);
            Image_Text_label.Name = "Image_Text_label";
            Image_Text_label.Size = new Size(73, 30);
            Image_Text_label.TabIndex = 14;
            Image_Text_label.Text = "Image";
            // 
            // bySize_byBrightness_label
            // 
            bySize_byBrightness_label.Font = new Font("Arial", 16.125F, FontStyle.Bold);
            bySize_byBrightness_label.Location = new Point(377, -2);
            bySize_byBrightness_label.Name = "bySize_byBrightness_label";
            bySize_byBrightness_label.Size = new Size(151, 33);
            bySize_byBrightness_label.TabIndex = 14;
            bySize_byBrightness_label.Text = "bySize";
            // 
            // MinMax_Result_label
            // 
            MinMax_Result_label.BackColor = Color.White;
            MinMax_Result_label.Font = new Font("Arial", 12.125F, FontStyle.Bold);
            MinMax_Result_label.Location = new Point(1, 1);
            MinMax_Result_label.Name = "MinMax_Result_label";
            MinMax_Result_label.Size = new Size(9, 8);
            MinMax_Result_label.TabIndex = 19;
            MinMax_Result_label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // radioButton_Green
            // 
            radioButton_Green.AutoSize = true;
            radioButton_Green.Checked = true;
            radioButton_Green.Font = new Font("Arial", 16.125F, FontStyle.Bold);
            radioButton_Green.Location = new Point(574, -2);
            radioButton_Green.Margin = new Padding(2);
            radioButton_Green.Name = "radioButton_Green";
            radioButton_Green.Size = new Size(114, 36);
            radioButton_Green.TabIndex = 17;
            radioButton_Green.TabStop = true;
            radioButton_Green.Text = "Green";
            radioButton_Green.UseVisualStyleBackColor = true;
            // 
            // radioButton_Red
            // 
            radioButton_Red.AutoSize = true;
            radioButton_Red.Font = new Font("Arial", 16.125F, FontStyle.Bold);
            radioButton_Red.Location = new Point(709, 1);
            radioButton_Red.Margin = new Padding(2);
            radioButton_Red.Name = "radioButton_Red";
            radioButton_Red.Size = new Size(87, 36);
            radioButton_Red.TabIndex = 18;
            radioButton_Red.Text = "Red";
            radioButton_Red.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            BackColor = Color.FromArgb(255, 192, 255);
            ClientSize = new Size(1924, 408);
            Controls.Add(bySize_byBrightness_label);
            Controls.Add(Min_Max_label);
            Controls.Add(Image_Text_label);
            Controls.Add(radioButton_Green);
            Controls.Add(radioButton_Red);
            Controls.Add(MinMax_Result_label);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Cat";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Min_Max_label;
        private System.Windows.Forms.Label bySize_byBrightness_label;
        private System.Windows.Forms.Label Image_Text_label;

        private System.Windows.Forms.RadioButton radioButton_Green;
        private System.Windows.Forms.RadioButton radioButton_Red;

        private System.Windows.Forms.Label MinMax_Result_label;
    }
}

