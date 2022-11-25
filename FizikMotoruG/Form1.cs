namespace FizikMotoruG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }


        public void Show(System.Drawing.Graphics grp, Pen mp, params Object[] obj)
        {
            foreach (Object o in obj)
            {
                grp.DrawEllipse(mp, (int)(o.transform.position.x + 800 - o.transform.scale / 2), (int)(o.transform.position.y + 350 - o.transform.scale / 2), (int)o.transform.scale, (int)o.transform.scale);
            }
        }
        public void ShowObj(Object obj)
        {
            foreach (Object o in Object.universe)
            {
                o.physics.v.x -= obj.physics.v.x;
                o.physics.v.y -= obj.physics.v.y;
                o.transform.position.x -= obj.transform.position.x;
                o.transform.position.y -= obj.transform.position.y;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            System.Drawing.Graphics grp;
            Pen mp = new Pen(System.Drawing.Color.Green, 1);
            Pen mp1 = new Pen(System.Drawing.Color.Red, 1);
            grp = this.CreateGraphics();

            Object objA = new Object(new Physics(0, 200000000000), new Transform(new Vector2(100, -100), 100));
            //Object objC = new Object(new Physics(0, 20000000000000), new Transform(new Vector2(10, -100), 10));
            Object objB = new Object(new Physics(0, 200000000000), new Transform(new Vector2(600, 100), 100));
            objA.physics.v.x = 0;
            objA.physics.v.y = 0;
            objB.physics.v.x = 0;
            objB.physics.v.y = 0;
            //objC.physics.v.x = 0;
            //objC.physics.v.y = 0;
            Object.universe = new Object[] { objA, objB };

            do
            {
                Application.DoEvents();
                Show(grp, mp, objA);
                Show(grp, mp1, objB);

                objA.Calc();
                objB.Calc();

                objA.Go();
                objB.Go();

                Thread.Sleep(Math.Abs(Time.deltaTime));
                grp.Clear(Color.White);
            } while (true);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}