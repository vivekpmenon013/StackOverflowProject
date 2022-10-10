using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IUsersRepository
    {
        void InsertUser(User u);
        void UpdateUserDetails(User u);
        void UpdateUserPassword(User u);
        void DeleteUser(int uid);
        List<User> GetUsers();
        List<User> GetUserByEmailAndPassword(string Email, string Password);
        List<User> GetUsersByEmail(string Email);
        List<User> GetUsersByUserID(int UserID);
        int GetLatestUserID();
    }
    public class UsersRepository: IUsersRepository
    {
        StackOverflowDatabaseDbContext db;

        public UsersRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }

        public void InsertUser(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();
        }

        public void UpdateUserDetails(User u)
        {
            User user = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (user != null)
            {
                user.Name = u.Name;
                user.Mobile = u.Mobile;
                db.SaveChanges();
            }
        }

        public void UpdateUserPassword(User u)
        {
            User user = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (user != null)
            {
                user.PasswordHash = u.PasswordHash;
                db.SaveChanges();
            }
        }

        public void DeleteUser(int uid)
        {
            User user = db.Users.Where(temp => temp.UserID == uid).FirstOrDefault();
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public List<User> GetUsers()
        {
            List<User> users = db.Users.Where(temp => temp.IsAdmin == false).OrderBy(temp=> temp.Name).ToList();
            return users;
        }

        public List<User> GetUserByEmailAndPassword(string Email, string PasswordHash)
        {
            List<User> users = db.Users.Where(temp => temp.Email == Email && temp.PasswordHash == PasswordHash)
                .ToList();
            return users;
        }

        public List<User> GetUsersByEmail(string Email)
        {
            List<User> users = db.Users.Where(temp => temp.Email == Email)
                .ToList();
            return users;
        }

        public List<User> GetUsersByUserID(int UserID)
        {
            List<User> users = db.Users.Where(temp => temp.UserID == UserID)
                .ToList();
            return users;
        }

        public int GetLatestUserID()
        {
            int userID = db.Users.Select(temp => temp.UserID)
                .Max();
            return userID;
        }
    }
}
