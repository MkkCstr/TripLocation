﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDotNet;
using TripLocation.Model;

namespace TripLocation
{
    class RnetInstance
    {
        public void runScript(HashSet<TableInfo> mytable)
        {
            REngine.SetEnvironmentVariables(); // <-- May be omitted; the next line would call it.
            REngine engine = REngine.GetInstance();
            NumericMatrix myVal = engine.CreateNumericMatrix(5, 5);

            List<TableInfo> newtable = mytable.ToList();
            double[,] myData = new double[mytable.Count, 3];
            Parallel.For(0, newtable.Count, index =>
            {
                myData[index, 0] = newtable[index].Note;
                myData[index, 1] = newtable[index].Longitude;
                myData[index, 2] = newtable[index].Latitude;
            });

            NumericMatrix myMatrix = engine.CreateNumericMatrix(myData);
           // engine.Evaluate("install.packages(\"ape\")");
            engine.Evaluate("library(\"ape\")");
            engine.SetSymbol("matrix", myMatrix);

            NumericMatrix toDist = engine.Evaluate("dist <- as.matrix(dist(matrix[,2:3]))").AsNumericMatrix();
            NumericMatrix invDist = engine.Evaluate("invdist <- 1/dist").AsNumericMatrix();
            engine.Evaluate("diag(invdist) <- 0").AsNumericMatrix();
            invDist = engine.Evaluate("invdist").AsNumericMatrix();
            NumericVector obs = engine.Evaluate("note <- as.vector(matrix[,1])").AsNumeric();
            NumericMatrix moranI = engine.Evaluate("Moran.I(note, invdist)").AsNumericMatrix();
            
            engine.Dispose();
        }
    }
}
