using System.Collections.Specialized;

namespace PacMan.Model
{
    public static class ImagesPathDataBase
    {
        private static NameValueCollection _images;

        static ImagesPathDataBase()
        {
            _images = new NameValueCollection
              {
                    {"Blinky", @"Resources\blinky.png"},
                    {"Pinky", @"Resources\pinky.png"},
                    {"Inky", @"Resources\inky.png"},
                    {"Clyde", @"Resources\clyde.png"},
                    {"PacmanFull", @"Resources\pacmanfull.png"},
                    {"Pacman", @"Resources\pacman.png"},
                    {"BlueEnemy", @"Resources\blueenemy.png"},
                    {"Eyes", @"Resources\eyes.png"},
                    {"Apple", @"Resources\apple.png"},
                    {"Strawberry", @"Resources\strawberry.png"},
                    {"Orange", @"Resources\orange.png"},
                    {"Melon", @"Resources\melon.png"},
                    {"Cherry", @"Resources\cherry.png"}                    
              };
        }

        public static NameValueCollection GetAllPathImages()
        {
            return _images;
        }

        public static string GetPathImage(string name)
        {
            return _images[name] ?? ""; 
        }

        public static string GetPathImage(int index)
        {
            return _images[index] ?? "";
        }
    }
}
