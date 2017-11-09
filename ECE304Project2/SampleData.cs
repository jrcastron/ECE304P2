using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE304Project2
{
    class SampleData
    {
        private double x;
        private double y;
        private String group;
        private double dist;
        private Boolean Nflag;

        //Constructor one
        public SampleData(double x1, double x2, String g)
        {
            x = x1;
            y = x2;
            group = g;
            dist = -1;
            Nflag = false;
        }

        //Constructor two
        public SampleData(double x1, double x2)
        {
            x = x1;
            y = x2;
            group = null;
            dist = -1;
            Nflag = false;
        }

        //Normalizes Data
        public void Normalize(double minx, double maxx, double miny, double maxy)
        {
            x = (x - minx) / (maxx - minx);
            y = (y - miny) / (maxy - miny);
            Nflag = true;
        }

        //returns the Normalizing flag (determines weather we need to normalize the data or not)
        public Boolean GetNflag()
        {
            return Nflag;
        }

        //Returns the X1 value
        public double GetX()
        {
            return x;
        }

        //returns the X2 value
        public double GetY()
        {
            return y;
        }

        //Returns the group of the Sample data
        public String GetGroup()
        {
            return group;
        }

        //Sets the groupo of the Sample Data
        public void SetGroup(String g)
        {
            group = g;
        }

        //Sets the Distance of the Sample Data
        public void SetDist(double d)
        {
            dist = d;
        }

        //Returns the distance of the Sample Data
        public double GetDist()
        {
            return dist;
        }

    }
}
