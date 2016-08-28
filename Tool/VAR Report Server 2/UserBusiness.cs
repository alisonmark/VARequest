using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace VAR_Report_Server
{
    public class UserBusiness
    {
        private static List<User> _currentUserList;
        public static List<User> CurrentUserList 
        {
            get 
            {
                if (_currentUserList == null)
                    return GetAll();
                else
                    return _currentUserList;
            }
        }

        private static object _lockObj = new object();

        public const string DataFile = "data.var";
        public static List<User> GetAll()
        {
            if (!File.Exists(DataFile))
            {
                FileStream s = File.Create(DataFile);
                s.Flush();
                s.Close();
                return new List<User>();
            }

            List<User> lstUser = new List<User>();
            using (StreamReader r = new StreamReader(DataFile))
            {
                while (!r.EndOfStream)
                {
                    try
                    {
                        string line = r.ReadLine();
                        string[] dataLine = line.Split('$');
                        if (dataLine.Length == 3)
                        {
                            User user = new User
                            {
                                Username = dataLine[0],
                                LastModified = DateTime.Parse(dataLine[1]),
                                Input = dataLine[2]
                            };
                            lstUser.Add(user);
                        }
                    }
                    catch { }
                }
            }
            lstUser.Sort((x, y) => { return x.Username.CompareTo(y.Username); });
            _currentUserList = lstUser;
            return lstUser;
        }

        public static User GetUser(string username)
        {
            lock (_lockObj)
            {
                foreach (User user in CurrentUserList)
                {
                    if (user.Username.ToLower() == username.ToLower())
                        return user;
                }
                return null;
            }
        }

        public static void Insert(User user)
        {
            if (string.IsNullOrEmpty(user.Username))
                throw new Exception("Username được bỏ trống");
            string[] dataInput = user.Input.Split(';');
            int purpose = -1;
            if (!string.IsNullOrEmpty(user.Input) && (dataInput.Length != 8 || !int.TryParse(dataInput[0], out purpose)))
                throw new Exception("Input không đúng định dạng");

            lock (_lockObj) { _currentUserList = GetAll(); }
            User existedUser = null;
            foreach (User item in _currentUserList)
            {
                if (item.Username.ToLower() == user.Username.ToLower())
                {
                    existedUser = item;
                    break;
                }
            }

            if (existedUser == null)
            {
                existedUser = new User { Username = user.Username, LastModified = DateTime.Now, Input = user.Input };
                lock (_lockObj) { _currentUserList.Add(existedUser); }
                UpdateDataFile(_currentUserList);
            }
            else
                throw new Exception("User này đã tồn tại !");
            
        }

        public static void Update(User user)
        {
            string[] dataInput = user.Input.Split(';');
            int purpose = -1;
            if (!string.IsNullOrEmpty(user.Input) && (dataInput.Length != 8 || !int.TryParse(dataInput[0], out purpose)))
                throw new Exception("Input không đúng định dạng");

            lock (_lockObj) { _currentUserList = GetAll(); }
            User existedUser = null;
            foreach (User item in _currentUserList)
            {
                if (item.Username.ToLower() == user.Username.ToLower())
                {
                    existedUser = item;
                    break;
                }
            }

            if (existedUser == null)
                throw new Exception("User này không tồn tại !");

            existedUser.Input = user.Input;
            existedUser.LastModified = DateTime.Now;
            UpdateDataFile(_currentUserList);
        }

        public static void Remove(User user)
        {
            List<User> lstUser = GetAll();
            User existedUser = null;
            foreach (User item in lstUser)
            {
                if (item.Username == user.Username)
                {
                    existedUser = item;
                    break;
                }
            }

            if (existedUser == null)
                throw new Exception("User này không tồn tại !");

            lstUser.Remove(existedUser);
            UpdateDataFile(lstUser);
        }

        public static void UpdateInputAll(string input)
        {
            string[] dataInput = input.Split(';');
            int purpose = -1;
            if (!string.IsNullOrEmpty(input) && (dataInput.Length != 8 || !int.TryParse(dataInput[0], out purpose)))
                throw new Exception("Input không đúng định dạng");

            lock (_lockObj) { _currentUserList = GetAll(); }
            foreach (User item in _currentUserList)
            {
                item.Input = input;
            }
            UpdateDataFile(_currentUserList);
        }

        public static void InsertUpdate(User user)
        {
            string[] dataInput = user.Input.Split(';');
            int purpose = -1;
            if (!string.IsNullOrEmpty(user.Input) && (dataInput.Length != 8 || !int.TryParse(dataInput[0], out purpose)))
                throw new Exception("Input không đúng định dạng");

            lock (_lockObj) { _currentUserList = GetAll(); }
            User existedUser = null;
            foreach (User item in _currentUserList)
            {
                if (item.Username == user.Username)
                {
                    existedUser = item;
                    break;
                }
            }

            if (existedUser == null)
            {
                existedUser = new User { Username = user.Username };
                lock (_lockObj) { _currentUserList.Add(existedUser); }
            }
            existedUser.Input = user.Input;
            UpdateDataFile(_currentUserList);
        }

        public static void UpdateInputForList(List<User> lstUser, string input)
        {
            string[] dataInput = input.Split(';');
            int purpose = -1;
            if (!string.IsNullOrEmpty(input) && (dataInput.Length != 8 || !int.TryParse(dataInput[0], out purpose)))
                throw new Exception("Input không đúng định dạng");

            lock (_lockObj) { _currentUserList = GetAll(); }
            foreach (User item in _currentUserList)
            {
                if (lstUser.Find((u) => u.Username == item.Username) != null)
                    item.Input = input;
            }
            UpdateDataFile(_currentUserList);
        }

        private static void UpdateDataFile(List<User> lstUser)
        {
            if (!File.Exists(DataFile))
                File.Create(DataFile);
            lock (_lockObj)
            {
                using (StreamWriter w = new StreamWriter(DataFile))
                {
                    foreach (User user in lstUser)
                    {
                        w.WriteLine("{0}${1}${2}", user.Username, user.LastModified, user.Input);
                    }
                }
            }
        }
    }
}
