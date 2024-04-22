namespace PageantVotingSystem.Sources.FormControls
{
    partial class InformationLayout
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label = new System.Windows.Forms.Label();
            this.loadingTimer = new System.Windows.Forms.Timer(this.components);
            this.displayDelayTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoEllipsis = true;
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            this.label.Dock = System.Windows.Forms.DockStyle.Right;
            this.label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(2)))), ((int)(((byte)(22)))));
            this.label.Location = new System.Drawing.Point(123, 0);
            this.label.MaximumSize = new System.Drawing.Size(1000, 33);
            this.label.Name = "label";
            this.label.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.label.Size = new System.Drawing.Size(93, 33);
            this.label.TabIndex = 59;
            this.label.Text = "Message";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label.Visible = false;
            // 
            // loadingTimer
            // 
            this.loadingTimer.Interval = 1000;
            this.loadingTimer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // displayDelayTimer
            // 
            this.displayDelayTimer.Interval = 3000;
            this.displayDelayTimer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // InformationBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label);
            this.Name = "InformationBox";
            this.Size = new System.Drawing.Size(216, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Timer loadingTimer;
        private System.Windows.Forms.Timer displayDelayTimer;
    }
}
