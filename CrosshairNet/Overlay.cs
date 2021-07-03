using System;
using System.Drawing;
using System.Windows.Forms;

namespace CrosshairNet
{
    public partial class Overlay : Form
    {
        public Pen pen;
        public bool crosshair = true;
        public int thickness = 2;
        public int height = 10;
        public int width = 10;
        public int gap = 0;
        public Color color = Color.FromArgb(255, 255, 0, 0); // or Color.Red, Color.Green

        public Overlay()
        {
            InitializeComponent();
        }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        public void update()
        {
            this.TopMost = true;
            this.pen = new Pen(color, thickness);
            Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int def_gap = 2;
            if (crosshair)
            {
                int x_start = Screen.PrimaryScreen.Bounds.Width / 2;
                int y_start = Screen.PrimaryScreen.Bounds.Height / 2;

                e.Graphics.DrawLine(pen, x_start - width, y_start, x_start - def_gap - gap, y_start);
                e.Graphics.DrawLine(pen, x_start + def_gap + gap, y_start, x_start + width, y_start);

                e.Graphics.DrawLine(pen, x_start, y_start - height, x_start, y_start - def_gap - gap);
                e.Graphics.DrawLine(pen, x_start, y_start + height, x_start, y_start + def_gap + gap);
            }
        }

        private void Overlay_Load(object sender, EventArgs e)
        {
            this.Left = 0;
            this.Top = 0;
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            btn_Close.TabStop = false;
            update();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
