namespace AdminPanelInfrastructure.Classes
{
    using System;
    using System.IO;

    public static class Reader
    {
        public static string[] GetData(BinaryReader reader)
        {
            try
            {
                string[] storageFirstSecondPointsOfDiagram = new string[reader.BaseStream.Length];

                string[] pointsY = new string[500000];

                for (int i = 0; i < 1500000; i++)
                {
                    storageFirstSecondPointsOfDiagram[i] = Convert.ToString(reader.ReadSByte());
                }

                for (int step = 0; step < 500000; step++)
                {
                    pointsY[step] = storageFirstSecondPointsOfDiagram[step*3];
                }

                return pointsY;
            }
            catch (Exception ex)
            {
                return new string[] { "Exception", ex.Message };
            }
        }
    }
}
