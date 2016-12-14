using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.DAL
{
    public class DocumentRepository
    {
        private ApplicationContext Context;
        public DocumentRepository(ApplicationContext context)
        {
            this.Context = context;
        }


    }
}