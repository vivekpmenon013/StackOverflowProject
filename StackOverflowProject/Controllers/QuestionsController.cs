using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.ViewModels;

namespace StackOverflowProject.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionsService qs;
        IAnswersService ans;
        ICategoriesService cs;

        public QuestionsController(IQuestionsService qs, IAnswersService ans, ICategoriesService cs)
        {
            this.qs = qs;
            this.ans = ans;
            this.cs = cs;
        }
        // GET: Questions
        public ActionResult View(int id)
        {
            this.qs.UpdateQuestionViewsCount(id, 1);
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel qvm =  this.qs.GetQuestionByQuestionID(id, uid);
            return View(qvm);
        }
    }
}