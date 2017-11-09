using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ECE304Project2
{
    class PaternRecognition
    {
        private List<SampleData> Data;
        private double minx = Double.MaxValue;
        private double miny = Double.MaxValue;
        private double maxx = 0;
        private double maxy = 0;

        //Default Constructor
        public PaternRecognition()
        {
            Data = new List<SampleData>();
        }

        //Reads in the data from a file into a list and normailizes the data
        public void ReadIn(String fileName)
        {
            StreamReader sr = new StreamReader(fileName);

            String[] temp = new String[3];
            while (!sr.EndOfStream)
            {
                temp = sr.ReadLine().Split();
                SampleData d = new SampleData(double.Parse(temp[0]), double.Parse(temp[1]), temp[2]);
                Data.Add(d);
            }
            foreach(SampleData sd in Data)
            {
                if (sd.GetX() < minx)
                    minx = sd.GetX();
                if (sd.GetX() > maxx)
                    maxx = sd.GetX();
                if (sd.GetY() < miny)
                    miny = sd.GetY();
                if (sd.GetY() > maxy)
                    maxy = sd.GetY();
            }
            foreach(SampleData sd in Data)
            {
                sd.Normalize(minx, maxx, miny, maxy);
            }
        }

        //Calls the Euchlidian function to get the smallest k distances the compares all the groups for the majority
        public void NNEK(SampleData testData, int k)
        {
            String G1;
            String G2;
            int count1 = 0;
            int count2 = 0;
            if (!testData.GetNflag())
                testData.Normalize(minx, maxx, miny, maxy);
            SampleData[] distMin = new SampleData[k];
            distMin = K(testData, k);
            SampleData temp = new SampleData(0,0);
            foreach (SampleData sd in distMin)
            {
                if (sd.GetDist() == 0)
                {
                    testData.SetGroup(sd.GetGroup());
                    testData.SetDist(sd.GetDist());
                    return;
                }
            }
            G1 = distMin[0].GetGroup();
            G2 = "";
            foreach(SampleData sd in distMin)
            {
                if (sd.GetGroup() != G1)
                    G2 = sd.GetGroup();
                break;
            }
            foreach(SampleData sd in distMin)
            {
                if (sd.GetGroup() == G1)
                    count1++;
                else
                    count2++;
            }
            if (count1 > count2)
                testData.SetGroup(G1);
            else
                testData.SetGroup(G2);
        }

        //Does the Euchlidian algorithm to find the k minimum distances
        public SampleData[] K(SampleData testData, int k)
        {
            SampleData empty = new SampleData(Double.MaxValue, Double.MaxValue);
            SampleData[] distMin = new SampleData[k];
            double min;
            SampleData Min = empty;
            for(int i = 0; i < k; i++)
            {
                distMin[i] = empty;
            }
            foreach (SampleData sample in Data)
            {
                double dist = Math.Sqrt(Math.Pow(sample.GetX() - testData.GetX(), 2) + Math.Pow(sample.GetY() - testData.GetY(), 2));
                sample.SetDist(dist);
            }
            for (int i = 0; i < k; i++)
            {
                min = Double.MaxValue;
                foreach (SampleData dataMin in Data)
                {
                    if ((dataMin.GetDist() != distMin[0].GetDist()) && (dataMin.GetDist() != distMin[1].GetDist()) && (dataMin.GetDist() != distMin[2].GetDist()))
                    {
                        if (dataMin.GetDist() < min)
                        {
                            min = dataMin.GetDist();
                            Min = dataMin;
                        }
                    }
                }
                distMin[i] = Min;
            }
            return distMin;
        }

        //Uses the Menhatten function to find the k minimum distances and finds tyhe majority group of those
        public void NNEkman(SampleData testData, int k)
        {
            String G1;
            String G2;
            int count1 = 0;
            int count2 = 0;
            if (!testData.GetNflag())
                testData.Normalize(minx, maxx, miny, maxy);
            SampleData[] distMin = new SampleData[k];
            distMin = KM(testData, k);
            SampleData temp = new SampleData(0, 0);
            foreach (SampleData sd in distMin)
            {
                if (sd.GetDist() == 0)
                {
                    testData.SetGroup(sd.GetGroup());
                    testData.SetDist(sd.GetDist());
                    return;
                }
            }
            G1 = distMin[0].GetGroup();
            G2 = "";
            foreach (SampleData sd in distMin)
            {
                if (sd.GetGroup() != G1)
                    G2 = sd.GetGroup();
                break;
            }
            foreach (SampleData sd in distMin)
            {
                if (sd.GetGroup() == G1)
                    count1++;
                else
                    count2++;
            }
            if (count1 > count2)
                testData.SetGroup(G1);
            else
                testData.SetGroup(G2);
        }

        //Uses the Manhatten algorithm to find the smallest distances
        public SampleData[] KM(SampleData testData, int k)
        {
            SampleData empty = new SampleData(Double.MaxValue, Double.MaxValue);
            SampleData[] distMin = new SampleData[k];
            double min;
            SampleData Min = empty;
            for (int i = 0; i < k; i++)
            {
                distMin[i] = empty;
            }
            foreach (SampleData sample in Data)
            {
                double dist = Math.Abs(testData.GetX() - sample.GetX()) + Math.Abs(testData.GetY() - sample.GetY());
                sample.SetDist(dist);
            }
            for (int i = 0; i < k; i++)
            {
                min = Double.MaxValue;
                foreach (SampleData dataMin in Data)
                {
                    if ((dataMin.GetDist() != distMin[0].GetDist()) && (dataMin.GetDist() != distMin[1].GetDist()) && (dataMin.GetDist() != distMin[2].GetDist()))
                    {
                        if (dataMin.GetDist() < min)
                        {
                            min = dataMin.GetDist();
                            Min = dataMin;
                        }
                    }
                }
                distMin[i] = Min;
            }
            return distMin;
        }

        //Prints the list of sample data
        public void Print()
        {
            foreach(SampleData sample in Data){
                Console.WriteLine(sample.GetX() + " " + sample.GetY() + " " + sample.GetGroup());
            }
        }

        //Finds the success rate of the Euchlidian algorithm
        public double Successeuch(int k)
        {
            double s;
            String g = "";
            double count = 0;
            foreach(SampleData sd in Data)
            {
                g = sd.GetGroup();
                this.NNEK(sd, k);
                if (g == sd.GetGroup())
                    count++;
            }
            s = count / (Data.Count() * 1.0);
            return s;
        }

        //Finds the success rate of the manhatten algorithm
        public double Successman(int k)
        {
            double s;
            String g = "";
            double count = 0;
            foreach (SampleData sd in Data)
            {
                g = sd.GetGroup();
                this.NNEkman(sd, k);
                if (g == sd.GetGroup())
                    count++;
            }
            s = count / (Data.Count() * 1.0);
            return s;
        }
    }
}
