using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Specialized;
using System;

public class mapmp : MonoBehaviour {

    Texture2D pr12vs;
    public GameObject proooooovs;


    const int MSIZE = 513;

    public int get_manh(int x1, int y1, int x2, int y2)
    {
        return Mathf.Abs(x1 - x2) + Mathf.Abs(y1 - y2);
    }

    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }


        public T First { get; set; }
        public U Second { get; set; }


    };

    public OrderedDictionary[] heap;  
    public Texture2D mapm;


    public Color rivcol = Color.blue, rivcol2 = Color.red;



    //public heap[] hips = new heap[2049 * 2049];




    Vector2 mb2;
        


    public void dsq2d(Vector2 a, Vector2 b, float coef, Vector2 or1, Vector2 or2)
    {
        mapm.SetPixel((int)(a.x), (int)(a.y), rivcol);
        mapm.SetPixel((int)(a.x) - 1, (int)(a.y), rivcol);
        mapm.SetPixel((int)(a.x) + 1, (int)(a.y), rivcol);
        mapm.SetPixel((int)(a.x), (int)(a.y) - 1, rivcol);
        mapm.SetPixel((int)(a.x) - 1, (int)(a.y) - 1, rivcol);
        mapm.SetPixel((int)(a.x) + 1, (int)(a.y) - 1, rivcol);
        mapm.SetPixel((int)(a.x), (int)(a.y) + 1, rivcol);
        mapm.SetPixel((int)(a.x) - 1, (int)(a.y) + 1, rivcol);
        mapm.SetPixel((int)(a.x) + 1, (int)(a.y) + 1, rivcol);
        mapm.SetPixel((int)(b.x), (int)(b.y), rivcol);
        mapm.SetPixel((int)(b.x) - 1, (int)(b.y), rivcol);
        mapm.SetPixel((int)(b.x) + 1, (int)(b.y), rivcol);
        mapm.SetPixel((int)(b.x), (int)(b.y) - 1, rivcol);
        mapm.SetPixel((int)(b.x) - 1, (int)(b.y) - 1, rivcol);
        mapm.SetPixel((int)(b.x) + 1, (int)(b.y) - 1, rivcol);
        mapm.SetPixel((int)(b.x), (int)(b.y) + 1, rivcol);
        mapm.SetPixel((int)(b.x) - 1, (int)(b.y) + 1, rivcol);
        mapm.SetPixel((int)(b.x) + 1, (int)(b.y) + 1, rivcol);
        if ((or1 - or2).magnitude < 1)
        {
            return;
        }
        else
        {
            Vector2 c = (or1 + or2) / 2;
            Vector2 d = (or1 - or2) / 2;
            Vector2 norm = new Vector2(-d.y, d.x);
            float hlp = UnityEngine.Random.Range(-1f, 1f);
            dsq2d(a, (a + b) / 2 + hlp * norm * coef, coef, or1, (or1 + or2) / 2);
            dsq2d(b, (a + b) / 2 + hlp * norm * coef, coef, or2, (or1 + or2) / 2);
        }
    }


    public Vector2[] cities;

    public float[][][] closed, opened, not_touched, closed2;

    public Double get_astar_cost(Vector2Int start, Vector2Int goal)
    {

        return getcost(start.x, start.y) + Mathf.Abs(start.x - goal.x) + Mathf.Abs(start.y - goal.y);
    }

    public float[][] closedas, openedas, ntouchedas;

    public void astar(Vector2Int start, Vector2Int goal)
    {
        closedas = new float[MSIZE / 3 + 1][];
        openedas = new float[MSIZE / 3 + 1][];
        ntouchedas = new float[MSIZE / 3 + 1][];
        for (int i = 0; i < MSIZE / 3; i++)
        {
            closedas[i] = new float[MSIZE / 3 + 1];
            ntouchedas[i] = new float[MSIZE / 3 + 1];
            openedas[i] = new float[MSIZE / 3 + 1];
            for (int j = 0; j < MSIZE / 3; j++)
            {
                closedas[i][j] = -1;
                ntouchedas[i][j] = 1;
                openedas[i][j] = -1;
            }
        }
        OrderedDictionary hieap = new OrderedDictionary();
        Dictionary<Vector2Int, Vector2Int> pores = new Dictionary<Vector2Int, Vector2Int>();


        int i1 = 0;
        Vector2Int current = start;
        current.x /= 3;
        current.y /= 3;
        Vector2Int startn = start;
        startn.x /= 3;
        startn.y /= 3;
        Vector2Int ngoal = goal;
        ngoal.x /= 3;
        ngoal.y /= 3;
        hieap.Add(0.0, new Vector4(startn.x, startn.y, startn.x, startn.y));
        ntouchedas[startn.x][startn.y] = -1;
        openedas[startn.x][startn.y] = 0;


        while (current != ngoal)
        {
            i1++;
            if(hieap.Count == 0)
            {
                i1 = MSIZE * MSIZE / 9 + 1;
            }
            if(i1 > MSIZE * MSIZE / 9)
            {
                break;
            }
            pores.Add(new Vector2Int((int)((Vector4)hieap[0]).x, (int)((Vector4)hieap[0]).y),
                new Vector2Int((int)((Vector4)hieap[0]).z, (int)((Vector4)hieap[0]).w));
            current = new Vector2Int((int)((Vector4)hieap[0]).x, (int)((Vector4)hieap[0]).y);
            closedas[current.x][current.y] = openedas[current.x][current.y];
            hieap.RemoveAt(0);
            if (current == ngoal)
            {
                break;
            }
            if (current.x > 0)
            {
                if(highs.GetPixel(current.x * 3 - 3, current.y * 3).b >= 0.1 && 
                    highs.GetPixel(current.x * 3 - 3, current.y * 3).b < 0.95f)
                {

                    if (ntouchedas[current.x - 1][current.y] == 1)
                    {
                        ntouchedas[current.x - 1][current.y] = -1;
                        openedas[current.x - 1][current.y] = (float)closedas[current.x][current.y] + (float)getcost2(current.x - 1, current.y);
                        hieap.Add(openedas[current.x - 1][current.y] + Mathf.Abs(current.x - 1 - ngoal.x) + Mathf.Abs(current.y - ngoal.y) +
                            ((double)(current.x - 1)) / 204900 + ((double)current.y) / 20490000000,
                            new Vector4(current.x - 1, current.y, current.x, current.y));
                    }
                    else if (closedas[current.x][current.y] + getcost(current.x - 1, current.y) < openedas[current.x - 1][current.y] && closedas[current.x - 1][current.y] == -1)
                    {

                        hieap.Remove(openedas[current.x - 1][current.y] + Mathf.Abs(current.x - 1 - ngoal.x) + Mathf.Abs(current.y - ngoal.y) +
                            ((double)(current.x - 1)) / 204900 + ((double)current.y) / 20490000000);
                        ntouchedas[current.x - 1][current.y] = -1;
                        openedas[current.x - 1][current.y] = (float)closedas[current.x][current.y] + (float)getcost2(current.x - 1, current.y);
                        hieap.Add(openedas[current.x - 1][current.y] + Mathf.Abs(current.x - 1 - ngoal.x) + Mathf.Abs(current.y - ngoal.y) +
                            ((double)(current.x - 1)) / 204900 + ((double)current.y) / 20490000000,
                            new Vector4(current.x - 1, current.y, current.x, current.y));
                    }
                }
            }
            if (current.x < MSIZE / 3 - 2)
            {
                if (highs.GetPixel(current.x * 3 + 3, current.y * 3).b >= 0.1 &&
                    highs.GetPixel(current.x * 3 + 3, current.y * 3).b < 0.95f)
                {
                    if (ntouchedas[current.x + 1][current.y] == 1)
                    {
                        ntouchedas[current.x + 1][current.y] = -1;
                        openedas[current.x + 1][current.y] = (float)closedas[current.x][current.y] + (float)getcost2(current.x + 1, current.y);
                        hieap.Add(openedas[current.x + 1][current.y] + Mathf.Abs(current.x + 1 - ngoal.x) + Mathf.Abs(current.y - ngoal.y) +
                            ((double)(current.x + 1)) / 204900 + ((double)current.y) / 20490000000,
                            new Vector4(current.x + 1, current.y, current.x, current.y));
                    }
                    else if (closedas[current.x][current.y] + getcost(current.x + 1, current.y) < openedas[current.x + 1][current.y] && closedas[current.x + 1][current.y] == -1)
                    {

                        hieap.Remove(openedas[current.x + 1][current.y] + Mathf.Abs(current.x + 1 - ngoal.x) + Mathf.Abs(current.y - ngoal.y) +
                            ((double)(current.x + 1)) / 204900 + ((double)current.y) / 20490000000);
                        ntouchedas[current.x + 1][current.y] = -1;
                        openedas[current.x + 1][current.y] = (float)closedas[current.x][current.y] + (float)getcost2(current.x + 1, current.y);
                        hieap.Add(openedas[current.x + 1][current.y] + Mathf.Abs(current.x + 1 - ngoal.x) + Mathf.Abs(current.y - ngoal.y) +
                            ((double)(current.x + 1)) / 204900 + ((double)current.y) / 20490000000,
                            new Vector4(current.x + 1, current.y, current.x, current.y));
                    }
                }
            }

            if (current.y > 0)
            {
                if (highs.GetPixel(current.x * 3, current.y * 3 - 3).b >= 0.1 &&
                    highs.GetPixel(current.x * 3, current.y * 3 - 3).b < 0.95f)
                {
                    if (ntouchedas[current.x][current.y - 1] == 1)
                    {
                        ntouchedas[current.x][current.y - 1] = -1;
                        openedas[current.x][current.y - 1] = (float)closedas[current.x][current.y] + (float)getcost2(current.x, current.y - 1);
                        hieap.Add(openedas[current.x][current.y - 1] + Mathf.Abs(current.x - ngoal.x) + Mathf.Abs(current.y - 1 - ngoal.y) +
                            ((double)(current.x)) / 204900 + ((double)current.y - 1) / 20490000000,
                            new Vector4(current.x, current.y - 1, current.x, current.y));
                    }
                    else if (closedas[current.x][current.y] + getcost(current.x, current.y - 1) < openedas[current.x][current.y - 1] && closedas[current.x][current.y - 1] == -1)
                    {

                        hieap.Remove(openedas[current.x][current.y - 1] + Mathf.Abs(current.x - ngoal.x) + Mathf.Abs(current.y - 1 - ngoal.y) +
                            ((double)(current.x)) / 204900 + ((double)current.y - 1) / 20490000000);
                        ntouchedas[current.x][current.y - 1] = -1;
                        openedas[current.x][current.y - 1] = (float)closedas[current.x][current.y] + (float)getcost2(current.x, current.y - 1);
                        hieap.Add(openedas[current.x][current.y - 1] + Mathf.Abs(current.x - ngoal.x) + Mathf.Abs(current.y - 1 - ngoal.y) +
                            ((double)(current.x)) / 204900 + ((double)current.y - 1) / 20490000000,
                            new Vector4(current.x, current.y - 1, current.x, current.y));
                    }
                }
            }
            if (current.y < MSIZE / 3 - 2)
            {
                if (highs.GetPixel(current.x * 3, current.y * 3 + 3).b >= 0.1 &&
                    highs.GetPixel(current.x * 3, current.y * 3 + 3).b < 0.95f)
                {
                    if (ntouchedas[current.x][current.y + 1] == 1)
                    {
                        ntouchedas[current.x][current.y + 1] = -1;
                        openedas[current.x][current.y + 1] = (float)closedas[current.x][current.y] + (float)getcost2(current.x, current.y + 1);
                        hieap.Add(openedas[current.x][current.y + 1] + Mathf.Abs(current.x - ngoal.x) + Mathf.Abs(current.y + 1 - ngoal.y) +
                            ((double)(current.x)) / 204900 + ((double)current.y + 1) / 20490000000,
                            new Vector4(current.x, current.y + 1, current.x, current.y));
                    }
                    else if (closedas[current.x][current.y] + getcost(current.x, current.y + 1) < openedas[current.x][current.y + 1] && closedas[current.x][current.y + 1] == -1)
                    {

                        hieap.Remove(openedas[current.x][current.y + 1] + Mathf.Abs(current.x - ngoal.x) + Mathf.Abs(current.y + 1 - ngoal.y) +
                            ((double)(current.x)) / 204900 + ((double)current.y + 1) / 20490000000);
                        ntouchedas[current.x][current.y + 1] = -1;
                        openedas[current.x][current.y + 1] = (float)closedas[current.x][current.y] + (float)getcost2(current.x, current.y + 1);
                        hieap.Add(openedas[current.x][current.y + 1] + Mathf.Abs(current.x - ngoal.x) + Mathf.Abs(current.y + 1 - ngoal.y) +
                            ((double)(current.x)) / 204900 + ((double)current.y + 1) / 20490000000,
                            new Vector4(current.x, current.y + 1, current.x, current.y));
                    }
                }
            }


        }
        current = ngoal;
        Vector2Int precur;
        while(current != pores[current])
        {
            if (i1 > MSIZE * MSIZE / 9)
            {
                break;
            }
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
            //        mapm.SetPixel(current.x * 3 + i, current.y * 3 + j, Color.red);
                }
            precur = current;
            current = pores[current];
            current = pores[current];
            current = pores[current];
            current = pores[current];
            current = pores[current];
            current = pores[current];
            current = pores[current];
            current = pores[current];
            dsq2d2(current * 3, precur * 3, 0.2f, current * 3, precur * 3);
        }

        List<Vector2Int> res = new List<Vector2Int>();

        
    }

    public void kmeans()
    {
        pr12vs = new Texture2D(MSIZE, MSIZE);
        //return;
        for(int i = 0; i < MSIZE; i++)
        {
            for(int j = 0; j < MSIZE; j++)
            {
                double mind = 10000000000;
                int n = 0;
                n = (int)closed2[0][i][j];/*
                for(int l = 0; l < provs.Length; l++)
                {

                    
                    if(mind > closed[l][i][j] && closed[l][i][j] != -1)
                    {
                        n = l;
                        mind = closed[l][i][j];
                    }
                }*/
               // Debug.Log(mind);
                pr12vs.SetPixel(i, j,
                        new Color((float)(n) / (float)(provs.Length), (float)(n) / (float)(provs.Length), (float)(n) / (float)(provs.Length), 1f));
                

            }
        }
        pr12vs.Apply();
        highs.Apply();
        proooooovs.GetComponent<Image>().sprite = Sprite.Create(highs, new Rect(0, 0, MSIZE, MSIZE), new Vector2(0.5f, 0.5f));
    }

    public bool fl;

    public double getcost(int x, int y)
    {
        if(x >= MSIZE || y >= MSIZE || x < 0 || y < 0)
        {
            return 0;
        }
        if(highs.GetPixel(x, y).b < 0.1)
                        {
            return 1000;
        } else if((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.blue)
        {
            return RIVER;
        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.red || (Vector4)mapm.GetPixel(x, y) == (Vector4)Color.black)
        {

            return ROAD;
        }
        else if (highs.GetPixel(x, y).b < 0.2)
        {

            return SAND  + (GRASS - SAND) / 0.1 * (highs.GetPixel(x, y).b - 0.1);
        }
        else if (highs.GetPixel(x, y).b < 0.7)
        {

            return GRASS  + (STONE - GRASS) / 0.5 * (highs.GetPixel(x, y).b - 0.2);
        }
        else if (highs.GetPixel(x, y).b < 0.95)
        {

            return STONE * (1 + (highs.GetPixel(x, y).b - 0.7) / 2.5);
        }
        else
        {

            return 1000;
        }
    }


    public void setcost(int x, int y, float cost)
    {
        if (x >= MSIZE || y >= MSIZE || x < 0 || y < 0)
        {
            return;
        }
        closed2[0][x][y] = (float)cost;
    }


    public double getcost2(int x, int y)
    {
        double res = 0;
        for (int l = 0; l < 3; l++)
        {
            for (int ll = 0; ll < 3; ll++)
            {
                res += getcost(x*3 + l, y*3 + ll);
            }
        }
        return res;
    }

    public void setcost2(int x, int y, float cost)
    {
        //double res = 0;
        for (int l = 0; l < 3; l++)
        {
            for (int ll = 0; ll < 3; ll++)
            {
                setcost(x*3 + l, y*3 + ll, cost);
            }
        }
        //return res;
    }


    public Queue<int> income;

    public void dijkstra(int i)
    {
        for(int l = 0; l < provs.Length; l++)
        {
            provs[l].x = (int)provs[l].x / 3;
            provs[l].y = (int)provs[l].y / 3;
            opened[i][(int)provs[l].x][(int)provs[l].y] = 0;
            not_touched[i][(int)provs[l].x][(int)provs[l].y] = -1;
            heap[i].Add(0 + ((double)((int)provs[l].x)) / 204900 + ((double)((int)provs[l].y)) / 20490000000, provs[l]);
        }
        //opened[i][(int)provs[i].x][(int)provs[i].y] = 0;
        bool fl = true;
        int x, y, z;
        for (int count = 0; count < MSIZE * MSIZE && heap[i].Count > 0; count++)
        {
           // object xy;
            x = (int)(((Vector3)heap[i][0]).x);
            y = (int)(((Vector3)heap[i][0]).y);
            z = (int)(((Vector3)heap[i][0]).z);
            //Debug.Log(x);
            // Debug.Log(y);
            setcost2(x, y, z);
            closed[i][x][y] = opened[i][x][y];
            //closed2[i][x][y] = z;
            heap[i].RemoveAt(0);

            if (x > 0)
            {
               /* if((mapm.GetPixel(x - 1, y) != Color.blue + new Color(0.5f, 0.5f, 0.5f, 1f)) && (mapm.GetPixel(x - 1, y) != Color.white))
                {*/
                    
                    if(not_touched[i][x - 1][y] == 1)
                    {
                        opened[i][x - 1][y] = closed[i][x][y] + (float)getcost2(x, y);/*
                        if((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.blue)
                        {
                            opened[i][x - 1][y] = closed[i][x][y] + 5;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.red)
                        {

                            opened[i][x - 1][y] = closed[i][x][y] + 0.7f;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.yellow)
                        {

                            opened[i][x - 1][y] = closed[i][x][y] + 1.25f;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.green)
                        {

                            opened[i][x - 1][y] = closed[i][x][y] + 1f;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.gray)
                        {

                            opened[i][x - 1][y] = closed[i][x][y] + 1.3f;
                        } else
                    {

                        opened[i][x - 1][y] = closed[i][x][y] + 1.3f;
                    }*/
                    not_touched[i][x - 1][y] = -1;
                        heap[i].Add(opened[i][x - 1][y] + ((double)(x - 1)) / 204900 + ((double)y) / 20490000000, new Vector3(x-1, y, z));
                       
                    } else if(closed[i][x - 1][y] != -1)
                    {

                        double old = opened[i][x - 1][y];
                    opened[i][x - 1][y] = Math.Min(opened[i][x - 1][y], closed[i][x][y] + (float)getcost2(x, y));/*
                        if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.blue)
                        {
                            opened[i][x - 1][y] = Math.Min(opened[i][x - 1][y], closed[i][x][y] + 5);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.red)
                        {

                            opened[i][x - 1][y] = Math.Min(opened[i][x - 1][y], closed[i][x][y] + 0.7f);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.yellow)
                        {

                            opened[i][x - 1][y] = Math.Min(opened[i][x - 1][y], closed[i][x][y] + 1.25f);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.green)
                        {

                            opened[i][x - 1][y] = Math.Min(opened[i][x - 1][y], closed[i][x][y] + 1f);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.gray)
                        {

                            opened[i][x - 1][y] = Math.Min(opened[i][x - 1][y], closed[i][x][y] + 1.3f);
                        }
                        else
                    {

                        opened[i][x - 1][y] = Math.Min(opened[i][x - 1][y], closed[i][x][y] + 1.3f);
                    }*/
                    if (old != opened[i][x - 1][y])
                        {
                            heap[i].Remove(old + ((double)(x - 1)) / 204900 + ((double)y) / 20490000000);
                            heap[i].Add(opened[i][x - 1][y] + ((double)(x - 1)) / 204900 + ((double)y) / 20490000000, new Vector3(x - 1, y, z));
                        }
                    }
               // }
            }
            if (x <MSIZE/3 - 1)
            {

               /* if ((mapm.GetPixel(x + 1, y) != Color.blue + new Color(0.5f, 0.5f, 0.5f, 1f)) && (mapm.GetPixel(x + 1, y) != Color.white))
                {*/

                    if (not_touched[i][x + 1][y] == 1)
                {
                    opened[i][x + 1][y] = closed[i][x][y] + (float)getcost2(x, y);/*
                    if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.blue)
                        {
                            opened[i][x + 1][y] = closed[i][x][y] + 5;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.red)
                        {

                            opened[i][x + 1][y] = closed[i][x][y] + 0.7f;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.yellow)
                        {

                            opened[i][x + 1][y] = closed[i][x][y] + 1.25f;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.green)
                        {

                            opened[i][x + 1][y] = closed[i][x][y] + 1f;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.gray)
                        {

                            opened[i][x + 1][y] = closed[i][x][y] + 1.3f;
                        }
                        else
                    {

                        opened[i][x + 1][y] = closed[i][x][y] + 1.3f;
                    }*/

                    not_touched[i][x + 1][y] = -1;
                        heap[i].Add(opened[i][x + 1][y] + ((double)(x + 1)) / 204900 + ((double)y) / 20490000000, new Vector3(x + 1, y, z));
                    }
                    else if (closed[i][x + 1][y] != -1)
                    {

                        double old = opened[i][x + 1][y];

                    opened[i][x + 1][y] = Math.Min(opened[i][x + 1][y], closed[i][x][y] + (float)getcost2(x, y));/*
                    if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.blue)
                        {
                            opened[i][x + 1][y] = Math.Min(opened[i][x + 1][y], closed[i][x][y] + 5);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.red)
                        {

                            opened[i][x + 1][y] = Math.Min(opened[i][x + 1][y], closed[i][x][y] + 0.7f);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.yellow)
                        {

                            opened[i][x + 1][y] = Math.Min(opened[i][x + 1][y], closed[i][x][y] + 1.25f);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.green)
                        {

                            opened[i][x + 1][y] = Math.Min(opened[i][x + 1][y], closed[i][x][y] + 1f);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.gray)
                        {

                            opened[i][x + 1][y] = Math.Min(opened[i][x + 1][y], closed[i][x][y] + 1.3f);
                        }
                        else
                    {

                        opened[i][x + 1][y] = Math.Min(opened[i][x + 1][y], closed[i][x][y] + 1.3f);
                    }*/
                    if (old != opened[i][x + 1][y])
                        {
                            heap[i].Remove(old + ((double)(x + 1)) / 204900 + ((double)y) / 20490000000);
                            heap[i].Add(opened[i][x + 1][y] + ((double)(x + 1)) / 204900 + ((double)y) / 20490000000, new Vector3(x + 1, y, z));
                        }
                    }
              //  }
            }
            if (y > 0)
            {

               /* if ((mapm.GetPixel(x, y - 1) != Color.blue + new Color(0.5f, 0.5f, 0.5f, 1f)) && (mapm.GetPixel(x, y - 1) != Color.white))
                {*/

                    if (not_touched[i][x][y - 1] == 1)
                {
                    opened[i][x][y - 1] = closed[i][x][y] + (float)getcost2(x, y);/*
                    if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.blue)
                        {
                            opened[i][x][y - 1] = closed[i][x][y] + 5;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.red)
                        {

                            opened[i][x][y - 1] = closed[i][x][y] + 0.7f;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.yellow)
                        {

                            opened[i][x][y - 1] = closed[i][x][y] + 1.25f;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.green)
                        {

                            opened[i][x][y - 1] = closed[i][x][y] + 1f;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.gray)
                        {

                            opened[i][x][y - 1] = closed[i][x][y] + 1.3f;
                        }
                        else
                    {

                        opened[i][x][y - 1] = closed[i][x][y] + 1.3f;
                    }*/
                    not_touched[i][x][y - 1] = -1;
                        heap[i].Add(opened[i][x][y - 1] + ((double)(x)) / 204900 + ((double)(y - 1)) / 20490000000, new Vector3(x, y - 1, z));
                    }
                    else if (closed[i][x][y - 1] != -1)
                    {

                        double old = opened[i][x][y - 1];

                    opened[i][x][y - 1] = Math.Min(opened[i][x][y - 1], closed[i][x][y] + (float)getcost2(x, y));/*
                    if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.blue)
                        {
                            opened[i][x][y - 1] = Math.Min(opened[i][x][y - 1], closed[i][x][y] + 5);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.red)
                        {

                            opened[i][x][y - 1] = Math.Min(opened[i][x][y - 1], closed[i][x][y] + 0.7f);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.yellow)
                        {

                            opened[i][x][y - 1] = Math.Min(opened[i][x][y - 1], closed[i][x][y] + 1.25f);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.green)
                        {

                            opened[i][x][y - 1] = Math.Min(opened[i][x][y - 1], closed[i][x][y] + 1f);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.gray)
                        {

                            opened[i][x][y - 1] = Math.Min(opened[i][x][y - 1], closed[i][x][y] + 1.3f);
                    }
                    else
                    {

                        opened[i][x][y - 1] = Math.Min(opened[i][x][y - 1], closed[i][x][y] + 1.3f);
                    }*/
                    if (old != opened[i][x][y - 1])
                        {
                            heap[i].Remove(old + ((double)(x)) / 204900 + ((double)(y - 1)) / 20490000000);
                            heap[i].Add(opened[i][x][y - 1] + ((double)(x)) / 204900 + ((double)(y - 1)) / 20490000000, new Vector3(x, y - 1, z));
                        }
                    }
               // }
            }
            if (y < MSIZE/3 - 1)
            {


               /* if ((mapm.GetPixel(x, y + 1) != Color.blue + new Color(0.5f, 0.5f, 0.5f, 1f)) && (mapm.GetPixel(x, y + 1) != Color.white))
                {*/

                    if (not_touched[i][x][y + 1] == 1)
                    {

                    opened[i][x][y + 1] = closed[i][x][y] + (float)getcost2(x, y);/*
                    if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.blue)
                        {
                            opened[i][x][y + 1] = closed[i][x][y] + 5;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.red)
                        {

                            opened[i][x][y + 1] = closed[i][x][y] + 0.7f;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.yellow)
                        {

                            opened[i][x][y + 1] = closed[i][x][y] + 1.25f;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.green)
                        {

                            opened[i][x][y + 1] = closed[i][x][y] + 1f;
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.gray)
                        {

                            opened[i][x][y + 1] = closed[i][x][y] + 0.3f;
                        }
                        else
                    {

                        opened[i][x][y + 1] = closed[i][x][y] + 0.3f;
                    }*/
                    not_touched[i][x][y + 1] = -1;
                        heap[i].Add(opened[i][x][y + 1] + ((double)(x)) / 204900 + ((double)(y + 1)) / 20490000000, new Vector3(x, y + 1, z));
                    }
                    else if (closed[i][x][y + 1] != -1)
                    {

                        double old = opened[i][x][y + 1];

                    opened[i][x][y + 1] = Math.Min(opened[i][x][y + 1], closed[i][x][y] + (float)getcost2(x, y));/*
                    if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.blue)
                        {
                            opened[i][x][y + 1] = Math.Min(opened[i][x][y + 1], closed[i][x][y] + 5);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.red)
                        {

                            opened[i][x][y + 1] = Math.Min(opened[i][x][y + 1], closed[i][x][y] + 0.7f);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.yellow)
                        {

                            opened[i][x][y + 1] = Math.Min(opened[i][x][y + 1], closed[i][x][y] + 1.25f);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.green)
                        {

                            opened[i][x][y + 1] = Math.Min(opened[i][x][y + 1], closed[i][x][y] + 1f);
                        }
                        else if ((Vector4)mapm.GetPixel(x, y) == (Vector4)Color.gray)
                        {

                            opened[i][x][y + 1] = Math.Min(opened[i][x][y + 1], closed[i][x][y] + 1.3f);
                        }
                        else
                        {

                            opened[i][x][y + 1] = Math.Min(opened[i][x][y + 1], closed[i][x][y] + 1.3f);
                        }*/
                    if (old != opened[i][x][y + 1])
                        {
                            heap[i].Remove(old + ((double)(x)) / 204900 + ((double)(y + 1)) / 20490000000);
                            heap[i].Add(opened[i][x][y + 1] + ((double)(x)) / 204900 + ((double)(y + 1)) / 20490000000, new Vector3(x, y + 1, z));
                        }
                    }
            //    }
            }
            

        }
        Debug.Log(heap[i].Count);
        heap[i].Clear();
    }

    public void gettor()
    {
        int x = UnityEngine.Random.Range(0, MSIZE);
        int y = UnityEngine.Random.Range(0, MSIZE);
        Debug.Log(closed[0][x][y]);
        Debug.Log(closed[1][x][y]);
        Debug.Log(closed[2][x][y]);
        Debug.Log(closed[3][x][y]);
        Debug.Log(1f==1);
    }


    public void dsq2d2(Vector2 a, Vector2 b, float coef, Vector2 or1, Vector2 or2)
    {
        mapm.SetPixel((int)(a.x), (int)(a.y), rivcol2);
        mapm.SetPixel((int)(a.x) - 1, (int)(a.y), rivcol2);
        mapm.SetPixel((int)(a.x) + 1, (int)(a.y), rivcol2);
        mapm.SetPixel((int)(a.x), (int)(a.y) - 1, rivcol2);
        mapm.SetPixel((int)(a.x) - 1, (int)(a.y) - 1, rivcol2);
        mapm.SetPixel((int)(a.x) + 1, (int)(a.y) - 1, rivcol2);
        mapm.SetPixel((int)(a.x), (int)(a.y) + 1, rivcol2);
        mapm.SetPixel((int)(a.x) - 1, (int)(a.y) + 1, rivcol2);
        mapm.SetPixel((int)(a.x) + 1, (int)(a.y) + 1, rivcol2);
        mapm.SetPixel((int)(b.x), (int)(b.y), rivcol2);
        mapm.SetPixel((int)(b.x) - 1, (int)(b.y), rivcol2);
        mapm.SetPixel((int)(b.x) + 1, (int)(b.y), rivcol2);
        mapm.SetPixel((int)(b.x), (int)(b.y) - 1, rivcol2);
        mapm.SetPixel((int)(b.x) - 1, (int)(b.y) - 1, rivcol2);
        mapm.SetPixel((int)(b.x) + 1, (int)(b.y) - 1, rivcol2);
        mapm.SetPixel((int)(b.x), (int)(b.y) + 1, rivcol2);
        mapm.SetPixel((int)(b.x) - 1, (int)(b.y) + 1, rivcol2);
        mapm.SetPixel((int)(b.x) + 1, (int)(b.y) + 1, rivcol2);
        if ((or1 - or2).magnitude < 1)
        {
            return;
        }
        else
        {
            Vector2 c = (or1 + or2) / 2;
            Vector2 d = (or1 - or2) / 2;
            Vector2 norm = new Vector2(-d.y, d.x);
            float hlp = UnityEngine.Random.Range(-1f, 1f);
            dsq2d2(a, (a + b) / 2 + hlp * norm * coef, coef, or1, (or1 + or2) / 2);
            dsq2d2(b, (a + b) / 2 + hlp * norm * coef, coef, or2, (or1 + or2) / 2);
        }
    }


    public void generate()
    {
        mapm = new Texture2D(1000, 1000);
      //  mapm = Texture2D.blackTexture;
        float val;
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                val = UnityEngine.Random.Range(0f, 1f);
                 mapm.SetPixel(i, j, new Color(val, val, val, 1f));
                //mapm.SetPixel(i, j, new Color((float)0.0, (float)0.0, (float)0.0, (float)1.0));
            }
        }
        mapm.Apply();
        gameObject.GetComponent<Image>().sprite = Sprite.Create(mapm, new Rect(0, 0, mapm.width, mapm.height), new Vector2(0.5f, 0.5f));

    }

    public Texture2D highs;
    public Vector3[] provs;
    public int seed = 0;
    public List<int> seeeeds;

    public void genseed()
    {
        if (seeeeds.Count == 0)
        {
            seeeeds = get_list_sids();
        }
        seed = seeeeds[0];
        seeeeds.RemoveAt(0);
        if(seeeeds.Count == 0)
        {
            seeeeds = get_list_sids();
        }
    }



    public List<int> get_list_sids()
    {
        List<int> res = new List<int>();
        for(int i = 0; i < 100000; i++)
        {
            res.Add(UnityEngine.Random.Range(0, 1000000000));
        }
        return res;
    }

    public int nn1, nn2, i1, i2;
    public int[][] incomes, outcomes;
    List<float> holp = new List<float>();

    public void generate2()
    {
        mapm = new Texture2D(MSIZE, MSIZE);
        highs = new Texture2D(MSIZE, MSIZE);
        UnityEngine.Random.InitState(seed);
        mapm = new Texture2D(MSIZE, MSIZE);
        //  mapm = Texture2D.blackTexture;
        float val;

        int step;
        step = MSIZE - 1;
        int[] coordinatesX, coordinatesY, newX, newY;
        coordinatesX = new int[2];
        coordinatesY = new int[2];
        coordinatesX[0] = 0;
        coordinatesX[1] = MSIZE - 1;
        coordinatesY[0] = 0;
        coordinatesY[1] = MSIZE - 1;

        val = UnityEngine.Random.Range(0f, 1f);

        Color v2;

        mapm.SetPixel(0, 0, new Color(val, val, val, 1f));

        val = UnityEngine.Random.Range(0f, 1f);
        mapm.SetPixel(0, MSIZE - 1, new Color(val, val, val, 1f));

        val = UnityEngine.Random.Range(0f, 1f);
        mapm.SetPixel(MSIZE - 1, 0, new Color(val, val, val, 1f));

        val = UnityEngine.Random.Range(0f, 1f);
        mapm.SetPixel(MSIZE - 1, MSIZE - 1, new Color(val, val, val, 1f));


        nn1 = 0; nn2 = 0;
        int prevstep;
        while (step > 1)
        {
            prevstep = step;
            step = 0;
            newX = new int[coordinatesX.Length * 2];
            newY = new int[coordinatesX.Length * 2];
            for(int i = 0; i < coordinatesX.Length; i += 2)
            {
                for(int j = 0; j < coordinatesY.Length; j += 2)
                {
                    newX[i * 2] = coordinatesX[i];
                    newX[i * 2 + 1] = (coordinatesX[i] + coordinatesX[i + 1]) / 2;
                    newX[i * 2 + 2] = (coordinatesX[i] + coordinatesX[i + 1]) / 2;
                    newX[i * 2 + 3] = coordinatesX[i + 1];
                    newY[j * 2] = coordinatesY[j];
                    newY[j * 2 + 1] = (coordinatesY[j] + coordinatesY[j + 1]) / 2;
                    newY[j * 2 + 2] = (coordinatesY[j] + coordinatesY[j + 1]) / 2;
                    newY[j * 2 + 3] = coordinatesY[j + 1];
                    step = Mathf.Max(step, newX[i * 2 + 3] - newX[i * 2 + 2]);
                    step = Mathf.Max(step, newY[j * 2 + 3] - newY[j * 2 + 2]);




                    v2 = (mapm.GetPixel(coordinatesX[i], coordinatesY[j]) + mapm.GetPixel(coordinatesX[i + 1], coordinatesY[j]) + mapm.GetPixel(coordinatesX[i],
                        coordinatesY[j + 1]) + mapm.GetPixel(coordinatesX[i + 1], coordinatesY[j + 1])) / 4;
                    val = UnityEngine.Random.Range(-((float)prevstep) / (float)(MSIZE - 1), ((float)prevstep) / (float)(MSIZE - 1));



                    v2 = v2 + new Color(val, val, val, 1f);
                    

                    mapm.SetPixel(newX[i * 2 + 1], newY[j * 2 + 1], v2);



                    v2 = (mapm.GetPixel(coordinatesX[i], coordinatesY[j]) + mapm.GetPixel(coordinatesX[i + 1], coordinatesY[j]) + mapm.GetPixel(newX[i * 2 + 1],
                        newY[j * 2 + 1])) / 3;
                    val = UnityEngine.Random.Range(-((float)prevstep) / (float)(MSIZE - 1), ((float)prevstep) / (float)(MSIZE - 1));
                    v2 = v2 + new Color(val, val, val, 1f);

                    

                    mapm.SetPixel(newX[i * 2 + 1], coordinatesY[j], v2);



                    v2 = (mapm.GetPixel(coordinatesX[i], coordinatesY[j + 1]) + mapm.GetPixel(coordinatesX[i + 1], coordinatesY[j + 1]) + mapm.GetPixel(newX[i * 2 + 1],
                        newY[j * 2 + 1])) / 3;
                    val = UnityEngine.Random.Range(-((float)prevstep) / (float)(MSIZE - 1), ((float)prevstep) / (float)(MSIZE - 1));
                    v2 = v2 + new Color(val, val, val, 1f);

                    

                    mapm.SetPixel(newX[i * 2 + 1], coordinatesY[j + 1], v2);



                    v2 = (mapm.GetPixel(coordinatesX[i], coordinatesY[j]) + mapm.GetPixel(coordinatesX[i], coordinatesY[j + 1]) + mapm.GetPixel(newX[i * 2 + 1],
                        newY[j * 2 + 1])) / 3;
                    val = UnityEngine.Random.Range(-((float)prevstep) / (float)(MSIZE - 1), ((float)prevstep) / (float)(MSIZE - 1));
                    v2 = v2 + new Color(val, val, val, 1f);

                    

                    mapm.SetPixel(newX[i * 2], newY[j * 2 + 1], v2);



                    v2 = (mapm.GetPixel(coordinatesX[i + 1], coordinatesY[j + 1]) + mapm.GetPixel(coordinatesX[i + 1], coordinatesY[j]) + mapm.GetPixel(newX[i * 2 + 1],
                        newY[j * 2 + 1])) / 3;
                    val = UnityEngine.Random.Range(-((float)prevstep) / (float)(MSIZE - 1), ((float)prevstep) / (float)(MSIZE - 1));
                    v2 = v2 + new Color(val, val, val, 1f);

                    

                    mapm.SetPixel(newX[i * 2 + 3], newY[j * 2 + 1], v2);
                }
            }
            coordinatesX = new int[newX.Length];
            for(int i = 0; i < newX.Length; i++)
            {
                coordinatesX[i] = newX[i];
            }
            coordinatesY = new int[newY.Length];
            for (int i = 0; i < newY.Length; i++)
            {
                coordinatesY[i] = newY[i];
            }
        }



        for(int i = 0; i < MSIZE; i++)
        {
            for(int j = 0; j < MSIZE; j++)
            {
                highs.SetPixel(i, j, mapm.GetPixel(i, j));
                holp.Add(mapm.GetPixel(i, j).b);
            }
        }

        holp.Sort();


        for (int i = 0; i < MSIZE; i++)
        {
            for (int j = 0; j < MSIZE; j++)
            {
                val = UnityEngine.Random.Range(0f, 1f);
                if (mapm.GetPixel(i, j).b < 0.1)
                {
                    nn1++;
                }
                else if (mapm.GetPixel(i, j).b < 0.2)
                {
                }
                else if (mapm.GetPixel(i, j).b < 0.7)
                {
                }
                else if (mapm.GetPixel(i, j).b < 0.95f)
                {
                }
                else
                {
                    nn2++;
                }
                //mapm.SetPixel(i, j, new Color((float)0.0, (float)0.0, (float)0.0, (float)1.0));
            }
        }



        incomes = new int[nn1][]; outcomes = new int[nn2][];
        i1 = 0; i2 = 0;


        for (int i = 0; i < MSIZE; i++)
        {
            for (int j = 0; j < MSIZE; j++)
            {
                val = UnityEngine.Random.Range(0f, 1f);
                if (mapm.GetPixel(i, j).b < 0.1)
                {
                    incomes[i1] = new int[2];
                    incomes[i1][0] = i;
                    incomes[i1][1] = j;
                    i1++;
                    //Debug.Log(i.ToString() + " " + j.ToString() + " blue");
                    mapm.SetPixel(i, j, Color.blue);// + new Color(0.5f, 0.5f, 0.5f, 1f));
                    //highs.SetPixel(i, j, new Color(0f, 0f, 0f));
                } else if(mapm.GetPixel(i, j).b < 0.2)
                {
                    mapm.SetPixel(i, j, Color.yellow);
                    //highs.SetPixel(i, j, new Color(0.33f, 0.33f, 0.33f));
                }
                else if (mapm.GetPixel(i, j).b < 0.7)
                {
                    mapm.SetPixel(i, j, Color.green);
                    //highs.SetPixel(i, j, new Color(0.66f, 0.66f, 0.66f));
                }
                else if (mapm.GetPixel(i, j).b < 0.95f)
                {
                    mapm.SetPixel(i, j, Color.grey);
                    //highs.SetPixel(i, j, new Color(0.83f, 0.83f, 0.83f));
                }
                else
                {
                    
                    outcomes[i2] = new int[2];
                    outcomes[i2][0] = i;
                    outcomes[i2][1] = j;
                    i2++;
                    //  Debug.Log(i.ToString() + " " + j.ToString() + " white");
                    mapm.SetPixel(i, j, Color.white);
                    //highs.SetPixel(i, j, new Color(1f, 1f, 1f));
                    // mapm.SetPixel(i, j, Color.grey);
                }
                //mapm.SetPixel(i, j, new Color((float)0.0, (float)0.0, (float)0.0, (float)1.0));
            }
        }



        int numriv = UnityEngine.Random.Range(3, 5);
        //int numriv = 0;

       // Queue<int> vis = new Queue<int>();
        float a, b;
        float k;
        for(int i = 0; i < numriv; i++)

        {
            /*
            a = 0;
            b = 0;
            int x1 = Random.Range(0, 2049);
//            x1 = 0;
            int x11 = x1;
            int x2 = Random.Range(0, 2049);
           // x2 = 2048;
           // b = 1 / (x2 - x1);*/
            int x1, x2;
            x1 = UnityEngine.Random.Range(0, nn1 + 1);
            x2 = UnityEngine.Random.Range(0, nn2 + 1);

            int x11, x12, x21, x22;
            if (x1 == nn1)
            {
                x11 = UnityEngine.Random.Range(0, 2) * UnityEngine.Random.Range(0, MSIZE);
                if (x11 == 0)
                {
                    x12 = UnityEngine.Random.Range(0, MSIZE);
                }
                else
                {
                    x12 = 0;
                }
            }
            else
            {
                x11 = incomes[x1][0];
                x12 = incomes[x1][1];
            }

            if (x2 == nn2)
            {
                x21 = UnityEngine.Random.Range(0, 2) * UnityEngine.Random.Range(0, MSIZE);
                if (x21 == 0)
                {
                    x22 = UnityEngine.Random.Range(0, MSIZE);
                }
                else
                {
                    x22 = 0;
                }
            }
            else
            {
                x21 = outcomes[x2][0];
                x22 = outcomes[x2][1];
            }

            Vector2 aaa, bbb;
            aaa = new Vector2(x11, x12);
            bbb = new Vector2(x21, x22);
            dsq2d(aaa, bbb, 0.5f, aaa, bbb);

            //vis.Enqueue((x11 + x21) / 2);

            /*
            if (Random.Range(0, 2) == 1) {
                if(x1 > x2)
                {
                        int l1 = 0;
                        int l111 = 0;
                    for(int j = x1; j >= x2; j--)
                    {
                        for(int l = l1; l < l1 + 2048 / (x1 - x2 + 1) + 1 && l < 2049; l++, l111=l)
                        {
                            mapm.SetPixel(j + 1, l, Color.red);
                            mapm.SetPixel(j + 2, l, Color.red);
                            mapm.SetPixel(j, l, Color.red);
                            mapm.SetPixel(j - 1, l, Color.red);
                            mapm.SetPixel(j - 2, l, Color.red);
                            //l111 = l;
                        }
                        l1 = l111;
                    }
                } else
                {

                    int l1 = 0;
                    int l111 = 0;
                    for (int j = x1; j <= x2; j++)
                    {
                        for (int l = l1; l < l1 + 2048 / (x2 - x1 + 1) + 1 && l < 2049; l++, l111 = l)
                        {
                            mapm.SetPixel(j + 1, l, Color.red);
                            mapm.SetPixel(j + 2, l, Color.red);
                            mapm.SetPixel(j, l, Color.red);
                            mapm.SetPixel(j - 1, l, Color.red);
                            mapm.SetPixel(j - 2, l, Color.red);
                        }
                        l1 = l111;
                    }
                }
            } else
            {

                if (x1 > x2)
                {
                        int l1 = 0;
                        int l111 = 0;
                    for (int j = x1; j >= x2; j--)
                    {
                        for (int l = l1; l < l1 + 2048 / (x1 - x2 + 1) + 1 && l < 2049; l++, l111 = l)
                        {
                            mapm.SetPixel(l, j + 1, Color.red);
                            mapm.SetPixel(l, j + 2, Color.red);
                            mapm.SetPixel(l, j, Color.red);
                            mapm.SetPixel(l, j - 1, Color.red);
                            mapm.SetPixel(l, j - 2, Color.red);
                        }
                        l1 = l111;
                    }
                }
                else
                {

                        int l1 = 0;
                        int l111 = 0;
                    for (int j = x1; j <= x2; j++)
                    {
                        for (int l = l1; l < l1 + 2048 / (x2 - x1 + 1) + 1 && l < 2049; l++, l111 = l)
                        {
                            mapm.SetPixel(l, j + 1, Color.red);
                            mapm.SetPixel(l, j + 2, Color.red);
                            mapm.SetPixel(l, j, Color.red);
                            mapm.SetPixel(l, j - 1, Color.red);
                            mapm.SetPixel(l, j - 2, Color.red);
                        }
                        l1 = l111;
                    }
                }
            }*/
        }
        

        int numcits = UnityEngine.Random.Range(7, 10);
        cities = new Vector2[numcits];
        int[] cities1 = new int[numcits], cities2 = new int[numcits];

        for(int i = 0; i < numcits; i++)
        {
            bool fl = false;
            while(!fl)
            {
                fl = true;
                int x1 = UnityEngine.Random.Range(0, MSIZE - 100);
                int x2 = UnityEngine.Random.Range(0, MSIZE - 100);
                cities1[i] = x1;
                cities2[i] = x2;
                for (int j = 0; j < 10 && fl; j++)
                {
                    for (int l = 0; l < 10 && fl; l++)
                    {
                        if(mapm.GetPixel(x1 + j, x2 + l) == Color.blue || mapm.GetPixel(x1 + j, x2 + l) == Color.black ||
                            mapm.GetPixel(x1 + j, x2 + l) == Color.white)
                        {
                            fl = false;
                            break;
                        }
                    }
                }
                if(fl)
                {
                    cities[i] = new Vector2(x1, x2);

                    for (int j = 0; j < 10 && fl; j++)
                    {
                        for (int l = 0; l < 10 && fl; l++)
                        {
                            mapm.SetPixel(x1 + j, x2 + l, Color.black);

                        }
                    }
                }
            }
            if(i > 0)
            {
                astar(new Vector2Int(cities1[i], cities2[i]), new Vector2Int(cities1[i - 1], cities2[i - 1]));
               // dsq2d2(new Vector2(cities1[i], cities2[i]), new Vector2(cities1[i - 1], cities2[i - 1]), 0.2f, new Vector2(cities1[i], cities2[i]),
                 //   new Vector2(cities1[i - 1], cities2[i - 1]));
            }

        }

        int provinces;
        provinces = 4;
        
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < MSIZE; j++)
            {
                for (int l = 0; l < MSIZE; l++)
                {
                    closed[i][j][l] = -1;
                    closed2[i][j][l] = -1;
                    opened[i][j][l] = -1;
                    not_touched[i][j][l] = 1;
                }
            }
        }

        int provincesLeft = provinces - 1;
        provs = new Vector3[provinces];
        provs[0] = new Vector3(UnityEngine.Random.Range(0, MSIZE), UnityEngine.Random.Range(0, MSIZE), 0);
        

        while(provincesLeft > 0)
        {
            Vector3 minp = new Vector3(0, 0, 0);
            float maxd = 0;
            for(int i1 = 0; i1 < 20; i1++)
            {
                int i = UnityEngine.Random.Range(0, MSIZE);
                int j = UnityEngine.Random.Range(0, MSIZE);
                float mmaxd = MSIZE * MSIZE * 2;
                for (int l = 0; l < provinces - provincesLeft; l++)
                {
                    if ((new Vector3(i, j) - provs[l]).magnitude < mmaxd)
                    {
                        mmaxd = (new Vector3(i, j, 0) - provs[l]).magnitude;
                    }
                }
                if (mmaxd > maxd)
                {
                    maxd = mmaxd;
                    minp = new Vector2(i, j);
                }
            }
            provs[provinces - provincesLeft] = minp;
            provincesLeft--;
        }

        for (int l = 0; l < provs.Length; l++)
        {
            provs[l].z = l;
        }

        for(int i = 0; i < provinces; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                for(int l = 0; l < 10; l++)
                {
                 //   mapm.SetPixel((int)provs[i].x - j, (int)provs[i].y - l, Color.red);
                   // mapm.SetPixel((int)provs[i].x + j, (int)provs[i].y + l, Color.red);
                    //mapm.SetPixel((int)provs[i].x - j, (int)provs[i].y + l, Color.red);
                    //mapm.SetPixel((int)provs[i].x + j, (int)provs[i].y - l, Color.red);
                }
            }
            //dsq2d2(provs[i], provs[i], 0.0f, provs[i], provs[i]);
        }

        for(int i = 0; i < 1; i++)
        {
            dijkstra(i);
        }
        kmeans();

        mapm.Apply();
        gameObject.GetComponent<Image>().sprite = Sprite.Create(mapm, new Rect(0, 0, mapm.width, mapm.height), new Vector2(0.5f, 0.5f), 1f);



        coordinatesX = new int[2];
        int rivers;
        rivers = UnityEngine.Random.Range(0, 1);
        for(int i = 0; i < rivers; i++)
        {
            if(UnityEngine.Random.Range(0, 2) == 1)
            {
                int wid1, wid2;
                wid1 = 2;
                wid2 = 2;
                int st, en;

                st = UnityEngine.Random.Range(0, MSIZE + 1 - wid1);
                en = UnityEngine.Random.Range(0, MSIZE + 1 - wid2);


            }
        }

    }

    public float GRASS, ROAD, SAND, STONE, RIVER;
    // Use this for initialization
    void Start()
    {
        seeeeds = get_list_sids();
        fl = true;
        int i;
        List<Chunk> chuks = gameObject.GetComponent<StaticData>().chunks.list;
        for (i = 0; i < chuks.Count; i++)
        {
            if (chuks[i].keyName == "GRASS")
            {
                GRASS = chuks[i].speed;
            }
            if (chuks[i].keyName == "ROAD")
            {
                ROAD = chuks[i].speed;
            }
            if (chuks[i].keyName == "STONE")
            {
                STONE = chuks[i].speed;
            }
            if (chuks[i].keyName == "RIVER")
            {
                RIVER = chuks[i].speed;
            }
            if (chuks[i].keyName == "SAND")
            {
                SAND = chuks[i].speed;
            }
            Debug.Log(chuks[i].keyName + " " + chuks[i].speed.ToString());
        }


            closed = new float[4][][];
            closed2 = new float[4][][];
            opened = new float[4][][];
            heap = new OrderedDictionary[4];
            not_touched = new float[4][][];
            for (i = 0; i < 4; i++)
            {
                heap[i] = new OrderedDictionary();
                closed[i] = new float[MSIZE][];
                closed2[i] = new float[MSIZE][];
                opened[i] = new float[MSIZE][];
                not_touched[i] = new float[MSIZE][];
                for (int j = 0; j < MSIZE; j++)
                {
                    closed[i][j] = new float[MSIZE];
                    closed2[i][j] = new float[MSIZE];
                    opened[i][j] = new float[MSIZE];
                    not_touched[i][j] = new float[MSIZE];
                    for (int l = 0; l < MSIZE; l++)
                    {
                        closed[i][j][l] = -1;
                        closed2[i][j][l] = -1;
                        opened[i][j][l] = -1;
                        not_touched[i][j][l] = 1;
                    }
                }
            }
            generate2();
        }
    
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            fl = !fl;
        }
    }
}
