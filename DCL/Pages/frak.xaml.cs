using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DCL.Pages;



public partial class frak : UserControl
{
    PointCollection pc;

    Int32[] pattern = new Int32[] { 1, 1, 0, 2, 1, 0, 0, 3 };
    Int32[] position = new Int32[] { 0, 0, 0, 0, 0, 0, 0, 0 };
    Boolean toggle;
    Char r = default(Char);

    Int32 distance = 2; // line length
    Int32 step = 20; // paints per step
    Int32 skip = 500; // folds per paint

    Double x = 0;
    Double y = 0;
    Int32 a = 90;


    public frak(MainWindow mainWindow)
    {
        InitializeComponent();
        Frak();
    }


    private void Frak()
    {

        x = canvas.ActualWidth / 3;
        y = canvas.ActualHeight / 1.5;

        pc = new PointCollection();

        var n = step;
        while (--n > 0)
        {
            List<Char> s = getS(skip);
            draw(s);
        }

        Polyline p = new Polyline();
        p.Stroke = new SolidColorBrush(Colors.Red);
        p.StrokeThickness = 0.5;

        p.Points = pc;
        canvas.Children.Add(p);
    }


    List<Char> getS(Int32 n)
    {
        List<Char> s1 = new List<Char>();
        while (n-- > 0) s1.Add(getNext(0));
        return s1;
    }


    void draw(List<Char> s)
    {        
        pc.Add(new Point(x, y));
        for (Int32 i = 0, n = s.Count; i < n; i++)
        {
            pc.Add(new Point(x, y));
            Int32 j;
            if (int.TryParse(s[i].ToString(), out j) && j != 0)
            {
                if ((a + 90) % 360 != 0)
                {
                    a = (a + 90) % 360;
                }
                else
                {
                    a = 360; // Right
                }
            }
            else
            {
                if (a - 90 != 0)
                {
                    a = a - 90;
                }
                else
                {
                    a = 360; // Right
                }
            }
            // new target
            if (a == 0 || a == 360)
            {
                y -= distance;
            }
            else if (a == 90)
            {
                x += distance;
            }
            else if (a == 180)
            {
                y += distance;
            }
            else if (a == 270)
            {
                x -= distance;
            }

            // move
            pc.Add(new Point(x, y));
        }

    }

    Char getNext(Int32 n)
    {
        if (position[n] == 7)
        {
            r = getNext(n + 1);
            position[n] = 0;
        }
        else
        {
            var x = position[n] > 0 ? pattern[position[n]] : pattern[0];

            switch (x)
            {
                case 0:
                    r = '0';
                    break;
                case 1:
                    r = '1';
                    break;
                case 2:
                    if (!toggle)
                    {
                        r = '1';
                    }
                    else
                    {
                        r = '0';
                    }
                    toggle = !toggle;
                    break;
            }
            position[n] = position[n] + 1;                
        }

        return r;
    }

}