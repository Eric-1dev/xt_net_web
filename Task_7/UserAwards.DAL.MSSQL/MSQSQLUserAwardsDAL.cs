using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserAwards.DAL.Interfaces;
using UserAwards.Entities;

namespace UserAwards.DAL.MSSQL
{
    public class MSQSQLUserAwardsDAL : IUserAwardsDAL
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        //private static SqlConnection _connection;

        public bool DeleteAwardById(Guid id)
        {
            string stProc = "UserAwards_DeleteAwardById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", id),
            };
            return ExecuteNonQuery(stProc, param);
        }

        public bool DeleteLinkById(Guid id)
        {
            string stProc = "UserAwards_DeleteLinkById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", id),
            };
            return ExecuteNonQuery(stProc, param);
        }

        public bool DeleteLinkById(IEnumerable<Guid> ids)
        {
            bool result = true;
            foreach (var id in ids)
            {
                string stProc = "UserAwards_DeleteLinkById";
                var param = new KeyValuePair<string, object>[]
                {
                new KeyValuePair<string, object>("@Id", id),
                };
                result = result && ExecuteNonQuery(stProc, param);
            }
            return result;
        }

        public bool DeleteUserById(Guid id)
        {
            string stProc = "UserAwards_DeleteUserById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", id),
            };
            return ExecuteNonQuery(stProc, param);
        }

        public IEnumerable<Award> GetAllAwards()
        {
            string stProc = "UserAwards_GetAllAwards";
            var sqlData = ExecuteReader(stProc);
            var awards = new LinkedList<Award>();

            foreach (var item in sqlData)
            {
                Guid _id = (Guid)item["Id"];
                string _title = item["Title"].ToString();
                string _image = item["Image"].ToString() == "" ? null : item["Image"].ToString();

                var award = new Award(_title, _image)
                {
                    Id = _id
                };

                awards.AddLast(award);
            }

            return awards;
        }

        public IEnumerable<Link> GetAllLinks()
        {
            string stProc = "UserAwards_GetAllLinks";
            var sqlData = ExecuteReader(stProc);
            var links = new LinkedList<Link>();

            foreach (var item in sqlData)
            {
                Guid _id = (Guid)item["Id"];
                Guid _userId = (Guid)item["UserId"];
                Guid _awardId = (Guid)item["AwardId"];

                var link = new Link(_userId, _awardId)
                {
                    Id = _id
                };

                links.AddLast(link);
            }

            return links;
        }

        public IEnumerable<User> GetAllUsers()
        {
            string stProc = "UserAwards_GetAllUsers";
            var sqlData = ExecuteReader(stProc);
            var users = new LinkedList<User>();

            foreach (var item in sqlData)
            {
                Guid _id = (Guid)item["Id"];
                string _name = item["Name"].ToString();
                string _password = item["Password"].ToString();
                DateTime _birthDay = (DateTime)item["DateOfBirth"];
                int _age = (int)item["Age"];
                bool _isAdmin = (bool)item["IsAdmin"];
                string _image = item["Image"].ToString() == "" ? null : item["Image"].ToString();


                var user = new User(_name, _birthDay, _age, _isAdmin, _image)
                {
                    Password = _password,
                    Id = _id
                };

                users.AddLast(user);
            }

            return users;
        }

        public Award GetAwardById(Guid id)
        {
            Award award = null;

            string stProc = "UserAwards_GetAwardById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", id),
            };
            var sqlData = ExecuteReader(stProc, param);

            foreach (var item in sqlData)
            {
                Guid _id = (Guid)item["Id"];
                string _title = item["Title"].ToString();
                string _image = item["Image"].ToString() == "" ? null : item["Image"].ToString();

                award = new Award(_title, _image)
                {
                    Id = _id
                };
            }

            return award;
        }

        public Award GetAwardByTitle(string title)
        {
            Award award = null;

            string stProc = "UserAwards_GetAwardByTitle";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Title", title),
            };
            var sqlData = ExecuteReader(stProc, param);

            foreach (var item in sqlData)
            {
                Guid _id = (Guid)item["Id"];
                string _title = item["Title"].ToString();
                string _image = item["Image"].ToString() == "" ? null : item["Image"].ToString();

                award = new Award(_title, _image)
                {
                    Id = _id
                };
            }

            return award;
        }

        public string[] GetRolesForUser(string name)
        {
            var roles = new LinkedList<string>();
            var user = GetUserByName(name);
            if (user == null)
                return roles.ToArray();

            roles.AddLast("user");
            if (user.IsAdmin)
                return roles.Append("admin").ToArray();
            return roles.ToArray();
        }

        public User GetUserById(Guid id)
        {
            User user = null;

            string stProc = "UserAwards_GetUserById";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", id),
            };
            var sqlData = ExecuteReader(stProc, param);

            foreach (var item in sqlData)
            {
                Guid _id = (Guid)item["Id"];
                string _name = item["Name"].ToString();
                string _password = item["Password"].ToString();
                DateTime _birthDay = (DateTime)item["DateOfBirth"];
                int _age = (int)item["Age"];
                bool _isAdmin = (bool)item["IsAdmin"];
                string _image = item["Image"].ToString() == "" ? null : item["Image"].ToString();

                user = new User(_name, _birthDay, _age, _isAdmin, _image)
                {
                    Password = _password,
                    Id = _id
                };
            }

            return user;
        }

        public User GetUserByName(string name)
        {
            User user = null;

            string stProc = "UserAwards_GetUserByName";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Name", name),
            };
            var sqlData = ExecuteReader(stProc, param);

            foreach (var item in sqlData)
            {
                Guid _id = (Guid)item["Id"];
                string _name = item["Name"].ToString();
                string _password = item["Password"].ToString();
                DateTime _birthDay = (DateTime)item["DateOfBirth"];
                int _age = (int)item["Age"];
                bool _isAdmin = (bool)item["IsAdmin"];
                string _image = item["Image"].ToString() == "" ? null : item["Image"].ToString();

                user = new User(_name, _birthDay, _age, _isAdmin, _image)
                {
                    Password = _password,
                    Id = _id
                };
            }

            return user;
        }

        public IEnumerable<User> GetUsersByAwardId(Guid Id)
        {
            string stProc = "UserAwards_GetUsersByAwardId";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@AwardId", Id),
            };
            var sqlData = ExecuteReader(stProc, param);
            var users = new LinkedList<User>();

            foreach (var item in sqlData)
            {
                Guid _id = (Guid)item["Id"];
                string _name = item["Name"].ToString();
                string _password = item["Password"].ToString();
                DateTime _birthDay = (DateTime)item["DateOfBirth"];
                int _age = (int)item["Age"];
                bool _isAdmin = (bool)item["IsAdmin"];
                string _image = item["Image"].ToString() == "" ? null : item["Image"].ToString();

                var user = new User(_name, _birthDay, _age, _isAdmin, _image)
                {
                    Password = _password,
                    Id = _id
                };

                users.AddLast(user);
            }

            return users;
        }

        public IEnumerable<Award> GetAwardsByUserId(Guid Id)
        {
            string stProc = "UserAwards_GetAwardsByUserId";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@UserId", Id),
            };
            var sqlData = ExecuteReader(stProc, param);
            var awards = new LinkedList<Award>();

            foreach (var item in sqlData)
            {
                Guid _id = (Guid)item["Id"];
                string _title = item["Title"].ToString();
                string _image = item["Image"].ToString() == "" ? null : item["Image"].ToString();

                var award = new Award(_title, _image)
                {
                    Id = _id
                };

                awards.AddLast(award);
            }

            return awards;
        }

        public void InsertAward(Award award)
        {
            string stProc = "UserAwards_InsertAward";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", award.Id),
                new KeyValuePair<string, object>("@Title", award.Title),
                new KeyValuePair<string, object>("@Image", award.Image)
            };
            ExecuteNonQuery(stProc, param);
        }

        public void InsertLink(Link link)
        {
            string stProc = "UserAwards_InsertLink";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", link.Id),
                new KeyValuePair<string, object>("@UserId", link.UserId),
                new KeyValuePair<string, object>("@AwardId", link.AwardId)
            };
            ExecuteNonQuery(stProc, param);
        }

        public void InsertUser(User user)
        {
            string stProc = "UserAwards_InsertUser";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", user.Id),
                new KeyValuePair<string, object>("@Name", user.Name),
                new KeyValuePair<string, object>("@Password", user.Password),
                new KeyValuePair<string, object>("@DateOfBirth", user.DateOfBirth),
                new KeyValuePair<string, object>("@Age", user.Age),
                new KeyValuePair<string, object>("@IsAdmin", user.IsAdmin),
                new KeyValuePair<string, object>("@Image", user.Image)
            };
            ExecuteNonQuery(stProc, param);
        }

        public bool IsAccountExist(string name, string password)
        {
            string stProc = "UserAwards_IsAccountExist";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Name", name),
                new KeyValuePair<string, object>("@Password", password)
            };
            var sqlData = ExecuteScalar(stProc, param);
            return (int)sqlData > 0;
        }

        public bool IsUserInRole(string name, string role)
        {
            string stProc = "UserAwards_IsUserInRole";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Name", name),
                new KeyValuePair<string, object>("@Role", role)
            };
            var sqlData = ExecuteScalar(stProc, param);
            return (int)sqlData > 0;
        }

        public bool SetUserPassword(Guid id, string password)
        {
            string stProc = "UserAwards_SetUserPassword";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", id),
                new KeyValuePair<string, object>("@Password", password)
            };
            return ExecuteNonQuery(stProc, param);
        }

        public bool UpdateAward(Award award)
        {
            string stProc = "UserAwards_UpdateAward";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", award.Id),
                new KeyValuePair<string, object>("@Image", award.Image)
            };
            return ExecuteNonQuery(stProc, param);
        }

        public bool UpdateUser(User user)
        {
            string stProc = "UserAwards_UpdateUser";
            var param = new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("@Id", user.Id),
                new KeyValuePair<string, object>("@Name", user.Name),
                new KeyValuePair<string, object>("@Password", user.Password),
                new KeyValuePair<string, object>("@DateOfBirth", user.DateOfBirth),
                new KeyValuePair<string, object>("@Age", user.Age),
                new KeyValuePair<string, object>("@IsAdmin", user.IsAdmin),
                new KeyValuePair<string, object>("@Image", user.Image)
            };
            return ExecuteNonQuery(stProc, param);
        }

        private IEnumerable<SqlDataReader> ExecuteReader(string procedure, params KeyValuePair<string, object>[] parameters)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                _connection.Open();

                var reader = GetCommand(_connection, procedure, parameters).ExecuteReader();

                while (reader.Read())
                {
                    yield return reader;
                }
            }
        }

        private object ExecuteScalar(string procedure, params KeyValuePair<string, object>[] parameters)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                _connection.Open();

                var scalar = GetCommand(_connection, procedure, parameters).ExecuteScalar();

                return scalar;
            }
        }

        private bool ExecuteNonQuery(string procedure, params KeyValuePair<string, object>[] parameters)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                _connection.Open();

                var result = GetCommand(_connection, procedure, parameters).ExecuteNonQuery();

                return result > 0;
            }
        }

        private SqlCommand GetCommand(SqlConnection _connection, string procedure, params KeyValuePair<string, object>[] parameters)
        {
            var command = new SqlCommand(procedure, _connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }
            return command;
        }
    }
}
