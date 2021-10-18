
namespace RevitAPIExtension.Wizards
{
    partial class VersionWizardForm
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
            this.RevitAPIVerCB = new System.Windows.Forms.ComboBox();
            this.accept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RevitAPIVerCB
            // 
            this.RevitAPIVerCB.FormattingEnabled = true;
            this.RevitAPIVerCB.Location = new System.Drawing.Point(166, 60);
            this.RevitAPIVerCB.Name = "RevitAPIVerCB";
            this.RevitAPIVerCB.Size = new System.Drawing.Size(170, 21);
            this.RevitAPIVerCB.TabIndex = 0;
            this.RevitAPIVerCB.SelectedIndexChanged += new System.EventHandler(this.RevitAPIVerCB_SelectedIndexChanged);
            // 
            // accept
            // 
            this.accept.Location = new System.Drawing.Point(388, 103);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 1;
            this.accept.Text = "Accept";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // VersionWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 151);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.RevitAPIVerCB);
            this.Name = "VersionWizardForm";
            this.Text = "Revit API Version Selector";
            this.Load += new System.EventHandler(this.VersionWizardForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox RevitAPIVerCB;
        private System.Windows.Forms.Button accept;
    }
}