namespace MarioPort
{
   partial class FormMarioPort
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
         this.lblMenuOp0 = new MarioPort.TransparentLabel();
         this.lblMenuOp2 = new MarioPort.TransparentLabel();
         this.lblMenuOp3 = new MarioPort.TransparentLabel();
         this.lblMenuOp4 = new MarioPort.TransparentLabel();
         this.lblMenuOp1 = new MarioPort.TransparentLabel();
         this.SuspendLayout();
         // 
         // lblMenuOp0
         // 
         this.lblMenuOp0.AutoSize = true;
         this.lblMenuOp0.Location = new System.Drawing.Point(137, 57);
         this.lblMenuOp0.Name = "lblMenuOp0";
         this.lblMenuOp0.Size = new System.Drawing.Size(43, 13);
         this.lblMenuOp0.TabIndex = 0;
         this.lblMenuOp0.Text = "START";
         this.lblMenuOp0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.lblMenuOp0.Visible = false;
         // 
         // lblMenuOp2
         // 
         this.lblMenuOp2.AutoSize = true;
         this.lblMenuOp2.Location = new System.Drawing.Point(137, 83);
         this.lblMenuOp2.Name = "lblMenuOp2";
         this.lblMenuOp2.Size = new System.Drawing.Size(30, 13);
         this.lblMenuOp2.TabIndex = 1;
         this.lblMenuOp2.Text = "END";
         this.lblMenuOp2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.lblMenuOp2.Visible = false;
         // 
         // lblMenuOp3
         // 
         this.lblMenuOp3.AutoSize = true;
         this.lblMenuOp3.Location = new System.Drawing.Point(137, 96);
         this.lblMenuOp3.Name = "lblMenuOp3";
         this.lblMenuOp3.Size = new System.Drawing.Size(60, 13);
         this.lblMenuOp3.TabIndex = 2;
         this.lblMenuOp3.Text = "MENUOP3";
         this.lblMenuOp3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.lblMenuOp3.Visible = false;
         // 
         // lblMenuOp4
         // 
         this.lblMenuOp4.AutoSize = true;
         this.lblMenuOp4.Location = new System.Drawing.Point(137, 109);
         this.lblMenuOp4.Name = "lblMenuOp4";
         this.lblMenuOp4.Size = new System.Drawing.Size(60, 13);
         this.lblMenuOp4.TabIndex = 3;
         this.lblMenuOp4.Text = "MENUOP4";
         this.lblMenuOp4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.lblMenuOp4.Visible = false;
         // 
         // lblMenuOp1
         // 
         this.lblMenuOp1.AutoSize = true;
         this.lblMenuOp1.Location = new System.Drawing.Point(137, 70);
         this.lblMenuOp1.Name = "lblMenuOp1";
         this.lblMenuOp1.Size = new System.Drawing.Size(55, 13);
         this.lblMenuOp1.TabIndex = 4;
         this.lblMenuOp1.Text = "OPTIONS";
         this.lblMenuOp1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.lblMenuOp1.Visible = false;
         // 
         // FormMarioPort
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.DarkCyan;
         this.ClientSize = new System.Drawing.Size(320, 196);
         this.Controls.Add(this.lblMenuOp1);
         this.Controls.Add(this.lblMenuOp4);
         this.Controls.Add(this.lblMenuOp3);
         this.Controls.Add(this.lblMenuOp2);
         this.Controls.Add(this.lblMenuOp0);
         this.DoubleBuffered = true;
         this.Name = "FormMarioPort";
         this.Text = "MarioPort";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMarioPort_FormClosing);
         this.Load += new System.EventHandler(this.FormMarioPort_Load);
         this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMarioPort_Paint);
         this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMarioPort_KeyDown);
         this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMarioPort_KeyUp);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      public TransparentLabel lblMenuOp0;
      public TransparentLabel lblMenuOp2;
      public TransparentLabel lblMenuOp3;
      public TransparentLabel lblMenuOp4;
      public TransparentLabel lblMenuOp1;




   }
}

