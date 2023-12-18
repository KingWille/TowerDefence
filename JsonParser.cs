using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


namespace TowerDefence
{
    internal class JsonParser
    {
        static string currentFileName;
        static JObject wholeJFileObj;

        //Läser json filen och sätter all information som ett Json object
        public static void GetJObjectFromFile(string fileName)
        {
            currentFileName = fileName;

            //Använder en streamreader för att läsa json filen
            StreamReader file = File.OpenText(currentFileName);

            //Använder en Json reader för att läsa filen genom streamreader
            JsonTextReader reader = new JsonTextReader(file);

            //Sätter hela filen som ett JsonObject
            wholeJFileObj = JObject.Load(reader);

            file.Close();
            reader.Close();
        }

        public static Rectangle GetRectangle(string fileName, string propertyName)
        {
            //Hämtar värdena under en specifik "titel" i Json filen
            if(wholeJFileObj == null)
            {
                GetJObjectFromFile(fileName);
            }

            JObject obj = (JObject)wholeJFileObj.GetValue(propertyName);

            return GetRectangle(obj);
        }

        public static Rectangle GetRectangle(JObject obj) 
        {
            int x, y, height, width;

            //Hämtar värdena för ett object i json filen
            x = Convert.ToInt32(obj.GetValue("PosX"));
            y = Convert.ToInt32(obj.GetValue("PosY"));
            width = Convert.ToInt32(obj.GetValue("SizeX"));
            height = Convert.ToInt32(obj.GetValue("SizeY"));

            //Skapar en rectangel med värdena från Json filen
            Rectangle rect = new Rectangle(x, y, width, height);

            return rect;
        }

        public static List<Rectangle> GetRectanglesList(string fileName, string propertyName)
        {

            if (wholeJFileObj == null)
            {
                GetJObjectFromFile(fileName);
            }

            List<Rectangle> rectList = new List<Rectangle>();

            //Hämtar en json array av alla objecten under samma titel i Json filen
            JArray arrayObj = (JArray)wholeJFileObj.GetValue(propertyName);

            //Räknar igenom alla array objekten, hämtar deras värden, och skapar en rektangel som läggs till i en list
            for(int i = 0; i < arrayObj.Count; i++)
            {
                JObject obj = (JObject)arrayObj[i];

                Rectangle rect = GetRectangle(obj);
                rectList.Add(rect);
            }

            wholeJFileObj = null;

            return rectList;
        }

        //Hämtar object och lägger till dem i json fil
        public static void WriteJsonToFile(string fileName, List<Terrain> Terrain)
        {
            JArray WaterArray = new JArray();
            JArray MountainArray = new JArray();
            JArray PathArray = new JArray();

            JObject finalObj = new JObject();

            for(int i = 0; i < Terrain.Count; i++)
            {
                if (Terrain[i] is Water)
                {
                    JObject obj = CreateObject(Terrain[i].Rect);
                    WaterArray.Add(obj);
                }
                else if (Terrain[i] is Mountain)
                {
                    JObject obj = CreateObject(Terrain[i].Rect);
                    MountainArray.Add(obj);
                }
                else if (Terrain[i] is Path)
                {
                    JObject obj = CreateObject(Terrain[i].Rect);
                    PathArray.Add(obj);
                }
            }

            finalObj.Add("WaterTerrain", WaterArray);
            finalObj.Add("MountainTerrain", MountainArray);
            finalObj.Add("Path", PathArray);

            File.WriteAllText(fileName, finalObj.ToString());
        }

        //Skapar ett nytt json object med dimensionerna av en rectangel
        private static JObject CreateObject(Rectangle rect)
        {
            
            JObject obj = new JObject();

            obj.Add("PosX", rect.X);
            obj.Add("PosY", rect.Y);
            obj.Add("SizeX", rect.Width);
            obj.Add("SizeY", rect.Height);

            return obj;
        }
    }
}
