
namespace Chess960
{
    partial class PawnPromotionBlack
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
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Horse = new System.Windows.Forms.PictureBox();
            this.Bishop = new System.Windows.Forms.PictureBox();
            this.Rook = new System.Windows.Forms.PictureBox();
            this.Queen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Horse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bishop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Queen)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(132, 414);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(132, 32);
            this.btnConfirm.TabIndex = 10;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label2.Location = new System.Drawing.Point(12, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(371, 31);
            this.label2.TabIndex = 9;
            this.label2.Text = "Pick a figure to promote pawn";
            // 
            // Horse
            // 
            this.Horse.Image = global::Chess960.Properties.Resources._41;
            this.Horse.Location = new System.Drawing.Point(211, 251);
            this.Horse.Name = "Horse";
            this.Horse.Size = new System.Drawing.Size(156, 147);
            this.Horse.TabIndex = 8;
            this.Horse.TabStop = false;
            this.Horse.Click += new System.EventHandler(this.Horse_Click);
            // 
            // Bishop
            // 
            this.Bishop.Image = global::Chess960.Properties.Resources._31;
            this.Bishop.Location = new System.Drawing.Point(35, 251);
            this.Bishop.Name = "Bishop";
            this.Bishop.Size = new System.Drawing.Size(152, 147);
            this.Bishop.TabIndex = 7;
            this.Bishop.TabStop = false;
            this.Bishop.Click += new System.EventHandler(this.Bishop_Click);
            // 
            // Rook
            // 
            this.Rook.Image = global::Chess960.Properties.Resources._51;
            this.Rook.Location = new System.Drawing.Point(211, 71);
            this.Rook.Name = "Rook";
            this.Rook.Size = new System.Drawing.Size(156, 149);
            this.Rook.TabIndex = 6;
            this.Rook.TabStop = false;
            this.Rook.Click += new System.EventHandler(this.Rook_Click);
            // 
            // Queen
            // 
            this.Queen.Image = global::Chess960.Properties.Resources._21;
            this.Queen.Location = new System.Drawing.Point(35, 81);
            this.Queen.Name = "Queen";
            this.Queen.Size = new System.Drawing.Size(152, 139);
            this.Queen.TabIndex = 5;
            this.Queen.TabStop = false;
            this.Queen.Click += new System.EventHandler(this.Queen_Click);
            // 
            // PawnPromotionBlack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 458);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Horse);
            this.Controls.Add(this.Bishop);
            this.Controls.Add(this.Rook);
            this.Controls.Add(this.Queen);
            this.Name = "PawnPromotionBlack";
            this.Text = "Pawn promotion";
            ((System.ComponentModel.ISupportInitialize)(this.Horse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bishop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Queen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Queen;
        private System.Windows.Forms.PictureBox Rook;
        private System.Windows.Forms.PictureBox Bishop;
        private System.Windows.Forms.PictureBox Horse;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label2;
    }
}