namespace PageantVotingSystem.Sources.FormControls
{
    partial class SingleValuedItem
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.itemControl = new System.Windows.Forms.Panel();
            this.margin = new System.Windows.Forms.Panel();
            this.value = new System.Windows.Forms.Button();
            this.itemControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(464, 0);
            this.panel4.TabIndex = 2;
            // 
            // itemControl
            // 
            this.itemControl.Controls.Add(this.margin);
            this.itemControl.Controls.Add(this.value);
            this.itemControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.itemControl.Location = new System.Drawing.Point(0, 0);
            this.itemControl.MaximumSize = new System.Drawing.Size(0, 45);
            this.itemControl.Name = "itemControl";
            this.itemControl.Size = new System.Drawing.Size(464, 45);
            this.itemControl.TabIndex = 39;
            // 
            // margin
            // 
            this.margin.Dock = System.Windows.Forms.DockStyle.Top;
            this.margin.Location = new System.Drawing.Point(0, 40);
            this.margin.Name = "margin";
            this.margin.Size = new System.Drawing.Size(464, 5);
            this.margin.TabIndex = 39;
            // 
            // value
            // 
            this.value.AutoEllipsis = true;
            this.value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            this.value.Dock = System.Windows.Forms.DockStyle.Top;
            this.value.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            this.value.FlatAppearance.BorderSize = 0;
            this.value.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            this.value.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(0)))), ((int)(((byte)(241)))));
            this.value.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.value.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value.Location = new System.Drawing.Point(0, 0);
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(464, 40);
            this.value.TabIndex = 38;
            this.value.TabStop = false;
            this.value.Text = "Value";
            this.value.UseVisualStyleBackColor = false;
            // 
            // SingleValuedItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.itemControl);
            this.Controls.Add(this.panel4);
            this.Name = "SingleValuedItem";
            this.Size = new System.Drawing.Size(464, 45);
            this.itemControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel itemControl;
        private System.Windows.Forms.Panel margin;
        private System.Windows.Forms.Button value;
    }
}
