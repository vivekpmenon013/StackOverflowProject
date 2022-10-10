using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;


namespace StackOverflowProject.Repositories
{
    public interface ICategoriesRepository
    {
        void InsertCategory(Category c);
        void UpdateCategory(Category c);
        void DeleteCategory(int cid);
        List<Category> GetCategories();
        List<Category> GetCategoryByCategoryID(int CategoryID);

    }
    public class CategoriesRepository : ICategoriesRepository
    {
        StackOverflowDatabaseDbContext db;

        public CategoriesRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }

        public void InsertCategory(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
        }
        public void UpdateCategory(Category c)
        {
            Category category = db.Categories.Where(temp => temp.CategoryID == c.CategoryID).FirstOrDefault();
            if (category != null)
            {
                category.CategoryName = c.CategoryName;
                db.SaveChanges();
            }
        }
        public void DeleteCategory(int cid)
        {
            Category category = db.Categories.Where(temp => temp.CategoryID == cid).FirstOrDefault();
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = db.Categories.ToList();
            return categories;
        }

        public List<Category> GetCategoryByCategoryID(int CategoryID)
        {
            List<Category> category = db.Categories.Where(temp => temp.CategoryID == CategoryID).ToList();
            return category;
        }
    }
}
