using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myset : MonoBehaviour {

    public class pair
    {
        public int x, y;


        public pair(int x1, int y1)
        {
            x = x1;
            y = y1;
        }

        public pair()
        {
            x = -1;
            y = -1;
        }

        public static bool operator <(pair a, pair b)
        {
            return a.x < b.x || (a.x == b.x && a.y < b.y);
        }


        public static bool operator >(pair a, pair b)
        {
            return a.x > b.x || (a.x == b.x && a.y > b.y);
        }


        public static bool operator <=(pair a, pair b)
        {
            return !(a > b);
        }

        public static bool operator >=(pair a, pair b)
        {
            return !(a < b);
        }



        public static bool operator ==(pair a, pair b)
        {
            return (a >= b) && (b >= a);
        }


        public static bool operator !=(pair a, pair b)
        {
            return a != b;
        }
    }

    public class seq
    {
        public class node
        {
            double x;
            pair y;

            public node(double a, pair b)
            {
                x = a;
                y = b;
            }



            public node()
            {
                x = -1;
                y = new pair();
            }


            public static bool operator <(node a, node b)
            {
                return a.x < b.x || (a.x == b.x && a.y < b.y);
            }


            public static bool operator >(node a, node b)
            {
                return a.x > b.x || (a.x == b.x && a.y > b.y);
            }


            public static bool operator <=(node a, node b)
            {
                return !(a > b);
            }

            public static bool operator >=(node a, node b)
            {
                return !(a < b);
            }



            public static bool operator ==(node a, node b)
            {
                return a.y == b.y;
            }


            public static bool operator !=(node a, node b)
            {
                return a != b;
            }

        }

        public List<node> nodes;
    }

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
