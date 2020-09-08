using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAwards.Common;
using UserAwards.Entities;

namespace UserAwards.DAL.File
{
    class FileUserAwardsDAL : IUserAwardsDAL
    {
        public static string WorkDirectory = Environment.CurrentDirectory + "\\" + "Data\\";
        public const string UsersFile = "Users.txt";
        public const string AwardsFile = "Awards.txt";
        public const string LinksFile = "Links.txt";

        public bool DeleteAwardById(Guid id) => DeleteObjectById<Award>(id, AwardsFile);

        public bool DeleteLinkById(Guid id) => DeleteObjectById<Link>(id, LinksFile);

        public bool DeleteUserById(Guid id) => DeleteObjectById<User>(id, UsersFile);

        public IEnumerable<Award> GetAllAwards() => GetAllObjects<Award>(AwardsFile);

        public IEnumerable<Link> GetAllLinks() => GetAllObjects<Link>(LinksFile);

        public IEnumerable<User> GetAllUsers() => GetAllObjects<User>(UsersFile);

        public bool AddAward(Award award) => AddObject(award, AwardsFile);

        public bool AddLink(Link link) => AddObject(link, LinksFile);

        public bool AddUser(User user) => AddObject(user, UsersFile);

        public bool UpdateUser(User user) => UpdateObject(user, UsersFile);

        public bool UpdateAward(Award award) => UpdateObject(award, AwardsFile);

        public bool UpdateLink(Link link) => UpdateObject(link, LinksFile);

        public User GetUserById(Guid id) => GetObjectById<User>(id, UsersFile);

        public Award GetAwardById(Guid id) => GetObjectById<Award>(id, AwardsFile);

        public Link GetLinkById(Guid id) => GetObjectById<Link>(id, LinksFile);

        private IEnumerable<T> GetAllObjects<T>(string file)
        {
            var objects = new LinkedList<T>();
            string line;

            using (var reader = new StreamReader(WorkDirectory + file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    objects.AddLast(JsonConvert.DeserializeObject<T>(line));
                }
            }

            return objects;
        }

        private void SaveAllObjects<T>(IEnumerable<T> objects, string file)
        {
            using (var writer = new StreamWriter(WorkDirectory + file))
                foreach (var elem in objects)
                    writer.WriteLine(JsonConvert.SerializeObject(elem));
        }

        private bool AddObject<T>(T obj, string file) where T : IHasId
        {
            var objects = new LinkedList<T>(GetAllObjects<T>(file));

            foreach (var elem in objects)
                if (elem.Id == obj.Id)
                    return false;   // Если id'шники пересекаются - значит нарушается уникальность и добавлять нельзя

            objects.AddLast(obj);
            SaveAllObjects(objects, file);
            return true;
        }

        private bool DeleteObjectById<T>(Guid id, string file) where T : IHasId
        {
            var objects = new LinkedList<T>(GetAllObjects<T>(file));
            var objToDelete = objects.Where(obj => obj.Id == id);
            
            if (objToDelete.Count() == 0)
                return false;

            objects.Remove(objToDelete.FirstOrDefault());

            SaveAllObjects(objects, file);

            return true;
        }

        private bool UpdateObject<T>(T obj, string file) where T : IHasId
        {
            var objects = new List<T>(GetAllObjects<T>(file));

            for (int i = 0; i < objects.Count(); i++)
                if (objects[i].Id == obj.Id)
                {
                    objects[i] = obj;
                    SaveAllObjects<T>(objects, file);
                    return true;
                }

            return false;
        }

        private T GetObjectById<T>(Guid id, string file) where T : IHasId
        {
            var objects = GetAllObjects<T>(file);

            var findedObj = objects.Where(obj => obj.Id == id);

            return findedObj.FirstOrDefault();
        }
    }
}
