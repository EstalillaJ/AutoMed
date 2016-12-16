using System;
using AutoMed.Models;
using AutoMed.Models.DataModels;

namespace AutoMed.DAL
{
    public class QuoteRepository : IQuoteRepository
    {
        public QuoteRepository()
        {
            this.documentRepo = new DocumentRepository();
        }

        private DocumentRepository documentRepo;
        public void Create(Quote quote)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                Context.QuoteDataModels.Add(new QuoteDataModel(quote));
                documentRepo.SaveDocumentBlobs(quote.Documents);
                Context.SaveChanges();
            }
        }

        public void Approve(Quote quote)
        {   
            using (ApplicationContext Context = new ApplicationContext())
            {
                if (quote.ApprovedBy == null)
                    throw new Exception("Approved by cannot be null");

                quote.DateApproved = DateTime.Now;
                Context.QuoteDataModels.Attach(new QuoteDataModel(quote));
                Context.Entry(quote).Property(x => x.ApprovedBy).IsModified = true;
                Context.Entry(quote).Property(x => x.DateApproved).IsModified = true;
                Context.SaveChanges();
            }
        }

        public void Delete(Quote quote)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                Context.QuoteDataModels.Remove(Context.QuoteDataModels.Find(quote.Id));
                Context.SaveChanges();
            }
        }

        public void Update(Quote quote)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                Context.QuoteDataModels.Attach(new QuoteDataModel(quote));
                Context.SaveChanges();
            }
        }

        public Quote SelectById(int id)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                Quote quote = Context.QuoteDataModels.Find(id).ToQuote();
                documentRepo.LoadDocumentBlobs(quote.Documents);
                return quote;
            }
        }
    }
}