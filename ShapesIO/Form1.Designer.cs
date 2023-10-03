namespace ShapesIO
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
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.score = new System.Windows.Forms.Label();
            this.enemyTimer = new System.Windows.Forms.Timer(this.components);
            this.scoreTime = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 20;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // score
            // 
            this.score.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.score.AutoSize = true;
            this.score.BackColor = System.Drawing.Color.Transparent;
            this.score.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.score.Location = new System.Drawing.Point(682, 57);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(55, 16);
            this.score.TabIndex = 0;
            this.score.Text = "TIME: 0 ";
            this.score.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // enemyTimer
            // 
            this.enemyTimer.Interval = 200;
            this.enemyTimer.Tick += new System.EventHandler(this.enemy_Timer);
            // 
            // scoreTime
            // 
            this.scoreTime.Interval = 1000;
            this.scoreTime.Tick += new System.EventHandler(this.Score_Time);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.score);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Paint_Game);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label score;
        private System.Windows.Forms.Timer enemyTimer;
        private System.Windows.Forms.Timer scoreTime;
    }
}

