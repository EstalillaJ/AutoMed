using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMed.Models;
namespace AutoMed.DAL
{
    public class QuoteRepository
    {
        private ApplicationContext Context;
        public QuoteRepository(ApplicationContext context)
        {
            this.Context = context;
        }

        public void Create(Quote quote)
        {
            Context.Quotes.Add(quote);
            Context.SaveChanges();
        }

        public void Approve(Quote quote)
        {
            if (quote.ApprovedBy == null)
                throw new Exception("Approved by cannot be null");
            Context.Quotes.Attach(quote);
            Context.Entry(quote).Property(x => x.ApprovedBy).IsModified = true;
            Context.SaveChanges();
        }
    }
}