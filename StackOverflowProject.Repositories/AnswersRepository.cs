using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IAnswersRepository
    {
        void InsertAnswer(Answer a);
        void UpdateAnswer(Answer a);
        void UpdateAnswerVotesCount(int aid, int uid, int value);
        void DeleteAnswer(int aid);
        List<Answer> GetAnswersByQuestionID(int qid);
        List<Answer> GetAnswersByAnswerID(int aid);
    }
    public class AnswersRepository: IAnswersRepository
    {
        StackOverflowDatabaseDbContext db;
        IQuestionsRepository qr;
        IVotesRepository vr;
        public AnswersRepository()
        {
            db = new StackOverflowDatabaseDbContext();
            qr = new QuestionsRepository();
            vr = new VotesRepository();
        }

        public void DeleteAnswer(int aid)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerID == aid).FirstOrDefault();
            if (ans != null)
            {
                db.Answers.Remove(ans);
                db.SaveChanges();
                qr.UpdateQuestionAnswersCount(ans.QuestionID, -1);
            }
        }

        public List<Answer> GetAnswersByAnswerID(int aid)
        {
            List<Answer> ans = db.Answers.Where(temp => temp.AnswerID == aid).OrderByDescending(temp => temp.AnswerDateAndTime)
                .ToList();
            return ans;
        }

        public List<Answer> GetAnswersByQuestionID(int qid)
        {
            List<Answer> ans = db.Answers.Where(temp => temp.QuestionID == qid).OrderByDescending(temp => temp.AnswerDateAndTime)
                .ToList();
            return ans;
        }

        public void InsertAnswer(Answer a)
        {
            db.Answers.Add(a);
            db.SaveChanges();
            qr.UpdateQuestionAnswersCount(a.QuestionID, 1);
        }

        public void UpdateAnswer(Answer a)
        {
            Answer answer = db.Answers.Where(temp => temp.AnswerID == a.AnswerID).FirstOrDefault();
            if (answer != null)
            {
                answer.AnswerText = a.AnswerText;
                db.SaveChanges();
            }
        }

        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            Answer answer = db.Answers.Where(temp => temp.AnswerID == aid).FirstOrDefault();
            if (answer != null)
            {
                answer.VotesCount += value;
                db.SaveChanges();
                qr.UpdateQuestionVotesCount(answer.QuestionID, value);
                vr.UpdateVote(aid, uid, value);
            }
        }
    }
}
