namespace node
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
            this.dataGridView_list = new System.Windows.Forms.DataGridView();
            this.button_createEntity = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_list)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_list
            // 
            this.dataGridView_list.AllowUserToAddRows = false;
            this.dataGridView_list.AllowUserToDeleteRows = false;
            this.dataGridView_list.AllowUserToOrderColumns = true;
            this.dataGridView_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_list.Location = new System.Drawing.Point(12, 81);
            this.dataGridView_list.Name = "dataGridView_list";
            this.dataGridView_list.Size = new System.Drawing.Size(637, 500);
            this.dataGridView_list.TabIndex = 0;
            // 
            // button_createEntity
            // 
            this.button_createEntity.Location = new System.Drawing.Point(12, 12);
            this.button_createEntity.Name = "button_createEntity";
            this.button_createEntity.Size = new System.Drawing.Size(119, 63);
            this.button_createEntity.TabIndex = 1;
            this.button_createEntity.Text = "Create Entity";
            this.button_createEntity.UseVisualStyleBackColor = true;
            this.button_createEntity.Click += new System.EventHandler(this.button_createEntity_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 593);
            this.Controls.Add(this.button_createEntity);
            this.Controls.Add(this.dataGridView_list);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_list)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_list;
        private System.Windows.Forms.Button button_createEntity;
    }
}

