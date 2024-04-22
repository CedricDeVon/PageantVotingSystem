namespace PageantVotingSystem.Sources.FormControls
{
    partial class EventStructureItem
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
            this.control = new System.Windows.Forms.Panel();
            this.itemControl = new System.Windows.Forms.Panel();
            this.valueLabel = new System.Windows.Forms.Button();
            this.leftMargin = new System.Windows.Forms.Panel();
            this.bottomMargin = new System.Windows.Forms.Panel();
            this.control.SuspendLayout();
            this.itemControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // control
            // 
            this.control.Controls.Add(this.itemControl);
            this.control.Controls.Add(this.bottomMargin);
            this.control.Dock = System.Windows.Forms.DockStyle.Top;
            this.control.Location = new System.Drawing.Point(0, 0);
            this.control.MaximumSize = new System.Drawing.Size(0, 45);
            this.control.Name = "control";
            this.control.Size = new System.Drawing.Size(591, 45);
            this.control.TabIndex = 40;
            // 
            // itemControl
            // 
            this.itemControl.Controls.Add(this.valueLabel);
            this.itemControl.Controls.Add(this.leftMargin);
            this.itemControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemControl.Location = new System.Drawing.Point(0, 0);
            this.itemControl.Name = "itemControl";
            this.itemControl.Size = new System.Drawing.Size(591, 40);
            this.itemControl.TabIndex = 43;
            // 
            // valueLabel
            // 
            this.valueLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            this.valueLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.valueLabel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            this.valueLabel.FlatAppearance.BorderSize = 0;
            this.valueLabel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            this.valueLabel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(0)))), ((int)(((byte)(241)))));
            this.valueLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.valueLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueLabel.Location = new System.Drawing.Point(0, 0);
            this.valueLabel.MaximumSize = new System.Drawing.Size(300, 40);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(300, 40);
            this.valueLabel.TabIndex = 49;
            this.valueLabel.Text = "Value";
            this.valueLabel.UseVisualStyleBackColor = false;
            // 
            // leftMargin
            // 
            this.leftMargin.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftMargin.Location = new System.Drawing.Point(0, 0);
            this.leftMargin.Name = "leftMargin";
            this.leftMargin.Size = new System.Drawing.Size(0, 40);
            this.leftMargin.TabIndex = 48;
            // 
            // bottomMargin
            // 
            this.bottomMargin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomMargin.Location = new System.Drawing.Point(0, 40);
            this.bottomMargin.Name = "bottomMargin";
            this.bottomMargin.Size = new System.Drawing.Size(591, 5);
            this.bottomMargin.TabIndex = 42;
            // 
            // EventStructureItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.control);
            this.Name = "EventStructureItem";
            this.Size = new System.Drawing.Size(591, 45);
            this.control.ResumeLayout(false);
            this.itemControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel control;
        private System.Windows.Forms.Panel itemControl;
        private System.Windows.Forms.Panel bottomMargin;
        private System.Windows.Forms.Panel leftMargin;
        private System.Windows.Forms.Button valueLabel;
    }
}
