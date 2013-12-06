using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TheoryOfGraphs
{
    class Arc
    {
        Top begin = new Top();
        Top end = new Top();
        double weight = 0;
        int number = 0;
        Color color = Color.White;

        public Arc()
        {
            begin = new Top();
            end = new Top();
            weight = 0;
            number = 0;
        }

        public Arc(Top begin, Top end, double weight, int number, Color color)
        {
            this.begin = begin;
            this.end = end;
            this.weight = weight;
            this.number = number;
            this.color = color;
        }

        public bool isEqual(Arc a)
        {
            if (this.getBegin().getName().Equals(a.getBegin().getName()))
                if (this.getEnd().getName().Equals(a.getEnd().getName()))
                    if (this.getWeight() == a.getWeight() && this.getColor() == a.getColor() && this.getNumber() == a.getNumber())
                        return true;
            return false;
        }

        public string ToString()
        {
            return String.Format("begin:{0} end:{1} weight:{2} number:{3} color:{4}", begin, end, weight, number, color);
        }

        public void setBegin(Top begin)
        {
            this.begin = begin;
        }

        public void setEnd(Top end)
        {
            this.end = end;
        }

        public void setWeight(double weight)
        {
            this.weight = weight;
        }

        public void setNumber(int number)
        {
            this.number = number;
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public Top getBegin()
        {
            return begin;
        }

        public Top getEnd()
        {
            return end;
        }

        public double getWeight()
        {
            return weight;
        }

        public int getNumber()
        {
            return number;
        }

        public Color getColor()
        {
            return color;
        }

    }
}
