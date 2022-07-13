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
                grp.DrawEllipse(mp, (int)(o.transform.position.x + 300 - o.transform.scale / 2), (int)(o.transform.position.y + 300 - o.transform.scale / 2), (int)o.transform.scale, (int)o.transform.scale);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag = true;
            Transform transform1 = new Transform(new Vector2(100, 0),20);
            Transform transform2 = new Transform(new Vector2(600, 0),10);
            Transform transform3 = new Transform(new Vector2(10, 10), 20);

            Physics physics = new Physics(0,2000);
            Physics physics2 = new Physics(0,20);
            Physics physics3 = new Physics(0, 60);

            Object obj1 = new Object(physics, transform1);
            Object obj2 = new Object(physics2, transform2);


            Object[] universe = {obj1,obj2};

            System.Drawing.Graphics grp;
            Pen mp = new Pen(System.Drawing.Color.Green, 1);
            grp = this.CreateGraphics();
            int sayac = 0;
            do
            {
                Physics.Gravity(universe);
                Show(grp,mp,universe);
                Thread.Sleep(Time.deltaTime);
                grp.Clear(Color.White);
                sayac++;
                label1.Text = "" + Vector2.Distance(obj1.transform.position, obj2.transform.position); ;
            } while (true);
            Show(grp, mp, universe);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
    }
}