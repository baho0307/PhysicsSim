public class Vector2
{   
    public double x;
    public double y;
    public Vector2(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
    public static double Distance(Vector2 a,Vector2 b)
    {
        return Math.Sqrt(Math.Pow((a.x - b.x),2) + Math.Pow((a.y - b.y),2));
    }
}

public class Vector3
{
    public double x;
    public double y;
    public double z;
    public Vector3(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

public class Transform
{
    public Vector2 position;
    public double scale;
    public Transform(Vector2 position, double scale)
    {
        this.position = position;
        this.scale = scale;
    }
}

public class Physics
{
    public double mass;
    public double newton;
    public double velocity;
    public Vector2 v;
    public Vector2 a;
    public Vector2 n;
    public Vector2 rot;
    public const double G = .0000000000667 ;
    public Physics(double velocity, double mass)
    {
        this.velocity = velocity;
        this.mass = mass;
        this.v = new Vector2(0, 0);
        this.a = new Vector2(0, 0);
    }
    
    public static Object IsColliding(Object self,params Object[] objects)
    {
        foreach(Object obj in objects)
        {
            if(Vector2.Distance(self.transform.position, obj.transform.position) <= (self.transform.scale + obj.transform.scale) / 2 && self != obj)
            {
                return obj;
            }
        }
        return null;
    }

    public double Velocity()
    {
        return Math.Sqrt(Math.Pow(this.v.x, 2) + Math.Pow(this.v.y,2));
    }

    public static void Gravity(params Object[] objects)
    {
        foreach(Object _obj in objects)
        {
            foreach(Object obj in objects)
            {
                if(_obj != obj && Physics.IsColliding(_obj, objects) == null)
                {
                    obj.physics.newton = G*(obj.physics.mass * _obj.physics.mass / Math.Pow(Vector2.Distance(obj.transform.position, _obj.transform.position), 2));
                    double acceleration = obj.physics.newton / _obj.physics.mass;
                    Object.GoToObj(_obj,obj,acceleration );
                    _obj.physics.velocity = acceleration;
                }
            }
        }
    }
}

public class Object
{
    public Physics physics;
    public Transform transform;
    public static Object[] universe = null;

    public Object(Physics physics, Transform transform)
    {
        this.physics = physics;
        this.transform = transform;
    }

    public void Acceleration()
    {
        this.physics.a.x = this.physics.n.x / this.physics.mass;
        this.physics.a.y = this.physics.n.y / this.physics.mass;
        this.physics.v.x += this.physics.a.x;
        this.physics.a.x = 0;
        this.physics.v.y += this.physics.a.y;
        this.physics.a.y = 0;
    }

    public void GravityForce()
    {
        Vector2 gSum = new Vector2(0, 0);

        foreach (Object obj in Object.universe)
        {
            if (obj != this)
            {
                Vector2 trgRot = new Vector2(obj.transform.position.x - this.transform.position.x, obj.transform.position.y - this.transform.position.y);
                gSum.x += (trgRot.x/Math.Abs(trgRot.y)) * (Physics.G * this.physics.mass * obj.physics.mass / Math.Pow(Vector2.Distance(this.transform.position,obj.transform.position), 2));
                gSum.y += (trgRot.y / Math.Abs(trgRot.y)) * (Physics.G * this.physics.mass * obj.physics.mass / Math.Pow(Vector2.Distance(this.transform.position, obj.transform.position), 2));
            }
        }
        this.physics.n = gSum;
    }
    public void Go()
    {
        if (Physics.IsColliding(this, Object.universe) == null)
        {
            this.GravityForce();
            this.Acceleration();
            this.transform.position.x += this.physics.v.x / Time.deltaTime;
            this.transform.position.y += this.physics.v.y / Time.deltaTime;
        }
        else
        {
            this.physics.n.x += -this.physics.v.x * this.physics.mass;
            this.physics.n.y += -this.physics.v.y * this.physics.mass;
            this.Acceleration();
            this.transform.position.x += this.physics.v.x / Time.deltaTime;
            this.transform.position.y += this.physics.v.y / Time.deltaTime;
        }
    }
    public static void GoTo(Object self ,Vector2 target,double a,params Object[] obj)
    {
        Vector2 trgRot = new Vector2(target.x - self.transform.position.x, target.y - self.transform.position.y);
        
        for (int i = 0; i < Math.Abs(trgRot.x); i++)
        {
            if (Physics.IsColliding(self, obj) == null)
            {
                self.transform.position.x += trgRot.x < 0 ? -1 * a / Time.deltaTime : 1 * a / Time.deltaTime;
            }
        }

        for (int i = 0; i < Math.Abs(trgRot.y); i++)
        {
            if (Physics.IsColliding(self, obj) == null)
            {
                self.transform.position.y += trgRot.y < 0 ? -1 * a / Time.deltaTime : 1 * a / Time.deltaTime;
            }
        }
    }
    public static void GoToObj(Object self,Object target,double a)
    {
        Vector2 trgRot = new Vector2(target.transform.position.x - self.transform.position.x, target.transform.position.y - self.transform.position.y);
        self.physics.rot = trgRot;
        for (int i = 0; i < Math.Abs(trgRot.x); i++)
        {
            if(Physics.IsColliding(self, target) == null)
            {
                self.transform.position.x += trgRot.x < 0 ? -1 * a / Time.deltaTime : 1 * a/Time.deltaTime;
            }
        }

        for (int i = 0; i < Math.Abs(trgRot.y); i++)
        {
            if (Physics.IsColliding(self, target) == null)
            {
                self.transform.position.y += trgRot.y < 0 ? -1 * a / Time.deltaTime : 1 * a / Time.deltaTime;
            }
        }
    }
}
