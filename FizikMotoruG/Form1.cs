namespace FizikMotoruG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        

        public void Show(System.Drawing.Graphics grp,Pen mp,params Object[] obj)
        {
            foreach(Object o in obj)
            {
                grp.DrawEllipse(mp, (int)(o.transform.position.x + 500 - o.transform.scale / 2), (int)(o.transform.position.y + 500 - o.transform.scale / 2), (int)o.transform.scale, (int)o.transform.scale);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            System.Drawing.Graphics grp;
            Pen mp = new Pen(System.Drawing.Color.Green, 1);
            grp = this.CreateGraphics();

            Object objA = new Object(new Physics(0, 20000000000000), new Transform(new Vector2(500, -500), 100));
            Object objC = new Object(new Physics(0, 20000000000000), new Transform(new Vector2(50, -50), 100));
            Object objB = new Object(new Physics(0, 20000000000000), new Transform(new Vector2(0, 0), 100));
            objA.physics.v.x = 0;
            objA.physics.v.y = 0;
            objB.physics.v.x = 0;
            objB.physics.v.y = 0;
            objC.physics.v.x = 0;
            objC.physics.v.y = 0;
            Object.universe = new Object[]{objA,objB,objC};

            do
            {
                Show(grp,mp,Object.universe);

                objA.Go();
                objB.Go();
                objC.Go();
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
