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
    public Physics(double velocity, double mass)
    {
        this.velocity = velocity;
        this.mass = mass;
    }

    public static bool IsColliding(Object self,params Object[] objects)
    {
        foreach(Object obj in objects)
        {
            if(Vector2.Distance(self.transform.position, obj.transform.position) <= (self.transform.scale + obj.transform.scale) / 2 && self != obj)
            {
                return true;
            }
        }
        return false;
    }

    public static void Gravity(params Object[] objects)
    {
        foreach(Object _obj in objects)
        {
            foreach(Object obj in objects)
            {
                if(_obj != obj && !Physics.IsColliding(_obj, objects))
                {
                    obj.physics.newton = 6.67*MathF.Pow(10,-11)*(obj.physics.mass * _obj.physics.mass / Math.Pow(Vector2.Distance(obj.transform.position, _obj.transform.position), 2));
                    double acceleration = obj.physics.newton / _obj.physics.mass;
                    Object.GoTo(_obj,obj,acceleration );
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

    public Object(Physics physics, Transform transform)
    {
        this.physics = physics;
        this.transform = transform;
    }
    public static void GoTo(Object self,Object target,double a)
    {
        Vector2 trgRot = new Vector2(target.transform.position.x - self.transform.position.x, target.transform.position.y - self.transform.position.y);

        for (int i = 0; i < Math.Abs(trgRot.x); i++)
        {
            if(!Physics.IsColliding(self, target))
            {
                self.transform.position.x += trgRot.x < 0 ? -1 * a / Time.deltaTime : 1 * a/Time.deltaTime;
            }
        }

        for (int i = 0; i < Math.Abs(trgRot.y); i++)
        {
            if (!Physics.IsColliding(self, target))
            {
                self.transform.position.y += trgRot.y < 0 ? -1 * a / Time.deltaTime : 1 * a / Time.deltaTime;
            }
        }
    }
}