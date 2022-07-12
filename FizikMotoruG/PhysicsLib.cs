public class Vector2
{   
    public float x;
    public float y;
    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public static float Distance(Vector2 a,Vector2 b)
    {
        return MathF.Sqrt(MathF.Pow((a.x - b.x),2) + MathF.Pow((a.y - b.y),2));
    }
}

public class Vector3
{
    public float x;
    public float y;
    public float z;
    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

public class Transform
{
    public Vector2 position;
    public float scale;
    public Transform(Vector2 position, float scale)
    {
        this.position = position;
        this.scale = scale;
    }
}

public class Physics
{
    public float velocity;
    public float mass;

    public Physics(float velocity, float mass)
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
                    _obj.physics.velocity = _obj.physics.mass*obj.physics.mass/MathF.Pow(Vector2.Distance(_obj.transform.position,obj.transform.position),2);
                    Object.GoTo(_obj, obj);
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
    public static void GoTo(Object self,Object target)
    {
        Vector2 trgRot = new Vector2(target.transform.position.x - self.transform.position.x, target.transform.position.y - self.transform.position.y);

        for (int i = 0; i < MathF.Abs(trgRot.x); i++)
        {
            if(!(Vector2.Distance(self.transform.position, target.transform.position) > (self.transform.scale  + target.transform.scale )/ 2)){
                break;
            }
            self.transform.position.x += trgRot.x < 0 ? -1 * self.physics.velocity / Time.deltaTime : 1 * self.physics.velocity/Time.deltaTime;
        }

        for (int i = 0; i < MathF.Abs(trgRot.y); i++)
        {
            if (!(Vector2.Distance(self.transform.position, target.transform.position) > (self.transform.scale  + target.transform.scale) / 2))
            {
                break;
            }
            self.transform.position.y += trgRot.y < 0 ? -1 * self.physics.velocity / Time.deltaTime : 1 *self.physics.velocity / Time.deltaTime;
        }
    }
}