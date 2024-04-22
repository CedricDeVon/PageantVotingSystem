namespace PageantVotingSystem.Sources.FormControls
{
    partial class OrderedValueItem
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
            this.itemControl = new System.Windows.Forms.Panel();
            this.value = new System.Windows.Forms.Button();
            this.horizontalMargin = new System.Windows.Forms.Panel();
            this.orderedNumber = new System.Windows.Forms.Button();
            this.verticalMargin = new System.Windows.Forms.Panel();
            this.itemControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemControl
            // 
            this.itemControl.Controls.Add(this.value);
            this.itemControl.Controls.Add(this.horizontalMargin);
            this.itemControl.Controls.Add(this.orderedNumber);
            this.itemControl.Controls.Add(this.verticalMargin);
            this.itemControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.itemControl.Location = new System.Drawing.Point(0, 0);
            this.itemControl.MaximumSize = new System.Drawing.Size(0, 45);
            this.itemControl.Name = "itemControl";
            this.itemControl.Size = new System.Drawing.Size(344, 45);
            this.itemControl.TabIndex = 40;
            // 
            // value
            // 
            this.value.AutoEllipsis = true;
            this.value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            this.value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.value.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            this.value.FlatAppearance.BorderSize = 0;
            this.value.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            this.value.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(0)))), ((int)(((byte)(241)))));
            this.value.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.value.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value.Location = new System.Drawing.Point(85, 0);
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(259, 40);
            this.value.TabIndex = 50;
            this.value.Text = "Value";
            this.value.UseVisualStyleBackColor = false;
            // 
            // horizontalMargin
            // 
            this.horizontalMargin.Dock = System.Windows.Forms.DockStyle.Left;
            this.horizontalMargin.Location = new System.Drawing.Point(80, 0);
            this.horizontalMargin.Name = "horizontalMargin";
            this.horizontalMargin.Size = new System.Drawing.Size(5, 40);
            this.horizontalMargin.TabIndex = 49;
            // 
            // orderedNumber
            // 
            this.orderedNumber.AutoEllipsis = true;
            this.orderedNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            this.orderedNumber.Dock = System.Windows.Forms.DockStyle.Left;
            this.orderedNumber.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            this.orderedNumber.FlatAppearance.BorderSize = 0;
            this.orderedNumber.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            this.orderedNumber.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(0)))), ((int)(((byte)(241)))));
            this.orderedNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.orderedNumber.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderedNumber.Location = new System.Drawing.Point(0, 0);
            this.orderedNumber.Name = "orderedNumber";
            this.orderedNumber.Size = new System.Drawing.Size(80, 40);
            this.orderedNumber.TabIndex = 48;
            this.orderedNumber.Text = "0000";
            this.orderedNumber.UseVisualStyleBackColor = false;
            // 
            // verticalMargin
            // 
            this.verticalMargin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.verticalMargin.Location = new System.Drawing.Point(0, 40);
            this.verticalMargin.Name = "verticalMargin";
            this.verticalMargin.Size = new System.Drawing.Size(344, 5);
            this.verticalMargin.TabIndex = 46;
            // 
            // OrderedValueItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.itemControl);
            this.Name = "OrderedValueItem";
            this.Size = new System.Drawing.Size(344, 45);
            this.itemControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel itemControl;
        private System.Windows.Forms.Button value;
        private System.Windows.Forms.Panel horizontalMargin;
        private System.Windows.Forms.Button orderedNumber;
        private System.Windows.Forms.Panel verticalMargin;
    }
}
