namespace FizikMotoruG
{
    public partial class Form1 : Form
    {
        public bool flag;
        public Form1()
        {
            InitializeComponent();

        }
        

        public void Show(System.Drawing.Graphics grp,Pen mp,params Object[] obj)
        {
            foreach(Object o in obj)
            {
                grp.DrawEllipse(mp, (o.transform.position.x + 300) - o.transform.scale / 2, (o.transform.position.y + 300) - o.transform.scale / 2, o.transform.scale, o.transform.scale);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag = true;
            Transform transform1 = new Transform(new Vector2(100, 50),10);
            Transform transform2 = new Transform(new Vector2(200, 0),10);
            Transform transform3 = new Transform(new Vector2(0, 0), 20);

            Physics physics = new Physics(0,20);
            Physics physics2 = new Physics(0,40);
            Physics physics3 = new Physics(0, 20);

            Object obj1 = new Object(physics, transform1);
            Object obj2 = new Object(physics2, transform2);
            Object obj3 = new Object(physics3 , transform3);

            Object[] universe = {obj1,obj2,obj3};

            System.Drawing.Graphics grp;
            Pen mp = new Pen(System.Drawing.Color.Green, 1);
            grp = this.CreateGraphics();

            do
            {
                Physics.Gravity(universe);
                Show(grp,mp,universe);
                Thread.Sleep(Time.deltaTime);
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